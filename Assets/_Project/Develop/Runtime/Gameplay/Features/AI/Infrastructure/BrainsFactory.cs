using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.FindTarget;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.TeleportState;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Timer;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly DIContainer _container;
        private readonly TimerServiceFactory _timerServiceFactory;
        private readonly AIBrainsContext _brainsContext;
        private readonly IInputService _inputService;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public BrainsFactory(DIContainer container)
        {
            _container = container;
            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
            _brainsContext = _container.Resolve<AIBrainsContext>();
            _inputService = _container.Resolve<IInputService>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        public StateMachineBrain CreateRandomTeleportingEntity(Entity entity)
        {
            AIStateMachine stateMachine = CreateRandomTeleportStateMachine(entity);
            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateToTargetTeleportingEntity(Entity entity)
        {
            AIStateMachine stateMachine = CreateTeleportTargetStateMachine(entity);
            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateMainHeroBrain(Entity entity)
        {
            AIStateMachine movementState = CreateMovementStateMachine(entity);
            AttackTriggerState attackState = new AttackTriggerState(entity);

            ICompositeCondition fromMovementToAttackStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _inputService.AttackKeyPress == true))
                .Add(new FuncCondition(() => _inputService.RotationAngle == 0))
                .Add(new FuncCondition(() => _inputService.MovementDirection == Vector3.zero));

            ICompositeCondition fromAttackToMovementStateCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
                .Add(new FuncCondition(() => _inputService.MovementDirection != Vector3.zero));

            AIStateMachine mainStateMachine = new();

            mainStateMachine.AddState(movementState);
            mainStateMachine.AddState(attackState);

            mainStateMachine.AddTransition(movementState, attackState, fromMovementToAttackStateCondition);
            mainStateMachine.AddTransition(attackState, movementState, fromAttackToMovementStateCondition);

            StateMachineBrain brain = new(mainStateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        private AIStateMachine CreateMovementStateMachine(Entity entity)
        {
            AIStateMachine stateMachine = new AIStateMachine();

            EmptyState emptyState = new();
            PlayerInputMovementState movementState = new(entity, _inputService);
            PlayerInputRotateState rotationState = new(entity, _inputService);

            FuncCondition fromRotateToEmptyStateCondition = new(() => _inputService.RotationAngle == 0);
            FuncCondition fromRotateToMovementStateCondition = new(() => _inputService.MovementDirection != Vector3.zero);

            FuncCondition fromEmptyToMovementStateCondition = new(() => _inputService.MovementDirection != Vector3.zero);
            FuncCondition fromEmptyToRotationStateCondition = new(() => _inputService.RotationAngle != 0);

            FuncCondition fromMovementToEmptyStateCondition = new(() => _inputService.MovementDirection == Vector3.zero);

            stateMachine.AddState(emptyState);
            stateMachine.AddState(movementState);
            stateMachine.AddState(rotationState);

            stateMachine.AddTransition(rotationState, emptyState, fromRotateToEmptyStateCondition);
            stateMachine.AddTransition(rotationState, movementState, fromRotateToMovementStateCondition);

            stateMachine.AddTransition(emptyState, rotationState, fromEmptyToRotationStateCondition);
            stateMachine.AddTransition(emptyState, movementState, fromEmptyToMovementStateCondition);

            stateMachine.AddTransition(movementState, emptyState, fromMovementToEmptyStateCondition);

            return stateMachine;
        }

        private AIStateMachine CreateRandomTeleportStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new();

            TeleportToRandomPointState teleportState = new TeleportToRandomPointState(entity);
            EmptyState emptyState = new();

            TimerService idleTimer = _timerServiceFactory.Create(2f);
            disposables.Add(idleTimer);
            disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));

            FuncCondition teleportTimerEndedCondition = new FuncCondition(() => entity.InTeleportProcess.Value == false);
            ICompositeCondition idleTimerEndedCondition = new CompositeCondition()
                .Add(new FuncCondition(() => idleTimer.IsOver))
                .Add(new FuncCondition(() => entity.CanTeleportProcess.Evaluate()));

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(emptyState);
            stateMachine.AddState(teleportState);

            stateMachine.AddTransition(emptyState, teleportState, idleTimerEndedCondition);
            stateMachine.AddTransition(teleportState, emptyState, teleportTimerEndedCondition);

            return stateMachine;
        }

        private AIStateMachine CreateTeleportTargetStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new();

            EmptyState emptyState = new();
            AttackTriggerState attackState = new(entity);
            TeleportToTargetState teleportState = new(entity);
            FindTargetState findTargetState = new(new WeakestTargeSelector(entity), _entitiesLifeContext, entity);

            TimerService idleTimer = _timerServiceFactory.Create(2f);

            disposables.Add(idleTimer);
            disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));

            FuncCondition fromEmptyToFindTargetStateCondition = new(() => idleTimer.IsOver);
            FuncCondition fromTeleportToAttackStateCondition = new(() => entity.InTeleportProcess.Value == false);
            FuncCondition fromAttackStateToEmptyCondition = new(() => entity.InAttackProcess.Value == false);

            ICompositeCondition fromFindTargetToTeleportStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => idleTimer.IsOver))
                .Add(new FuncCondition(() => entity.CanTeleportProcess.Evaluate()))
                .Add(new FuncCondition(() => entity.CurrentTarget.Value != null))
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value >= entity.MaxEnergy.Value * (entity.TeleportEnergyThreshold.Value / 100)));

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(emptyState);
            stateMachine.AddState(findTargetState);
            stateMachine.AddState(teleportState);
            stateMachine.AddState(attackState);

            stateMachine.AddTransition(emptyState, findTargetState, fromEmptyToFindTargetStateCondition);
            stateMachine.AddTransition(findTargetState, teleportState, fromFindTargetToTeleportStateCondition);
            stateMachine.AddTransition(teleportState, attackState, fromTeleportToAttackStateCondition);
            stateMachine.AddTransition(attackState, emptyState, fromAttackStateToEmptyCondition);

            return stateMachine;
        }
    }
}
