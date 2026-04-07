using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Shoot;
using Assets._Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycleFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.RotateFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Sensors;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            entity.AddCurrentTarget();

            _monoEntitiesFactory.Create(entity, position, "Prefabs/Entity");

            AddTeleportFeature(entity);
            AddEnergyFeature(entity);
            AddLifeCycleFeature(entity);
            AddAreaDamageFeature(entity);
            AddSensorFeature(entity);

            AddConditions(entity);

            AddSystems(entity);

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateEntityEnemy(Vector3 position, float hp)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Prefabs/EntityEnemy");

            AddLifeCycleFeature(entity, hp);

            AddConditions(entity);

            entity
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateHero(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Hero");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(40))
                .AddIsMoving()

                .AddRotationAngle(new ReactiveVariable<Quaternion>(Quaternion.identity))
                .AddRotationSpeed(new ReactiveVariable<float>(400))

                .AddRadiusDetecting(new ReactiveVariable<float>(15))

                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))

                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()

                .AddAttackProcessInitialTime(new ReactiveVariable<float>(3))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddStartAttackRequest()
                .AddStartAttackEvent()
                .AddEndAttackEvent()
                .AddAttackDelayTime(new ReactiveVariable<float>(1))
                .AddAttackDelayEndEvent()
                .AddInstantAttackDamage(new ReactiveVariable<float>(50))
                .AddAttackCanceledEvent()
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(2))
                .AddAttackCooldownCurrentTime()
                .AddInAttackCooldown();

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsMoving.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false));

            ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.IsMoving.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)

                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)

                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack)
                .AddMustCancelAttack(mustCancelAttack);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotateSystem())
                .AddSystem(new AttackCancelSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessTimerSystem())
                .AddSystem(new AttackDelayEndTriggerSystem())
                .AddSystem(new InstantShootSystem(this))
                .AddSystem(new EndAttackSystem())
                .AddSystem(new AttackCooldownTimerSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateProjectile(Vector3 position, Vector3 direction, float damage)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Projectile");

            entity        
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationSpeed(new ReactiveVariable<float>(9999))
                .AddIsDead()
                .AddRotationAngle(new ReactiveVariable<Quaternion>(Quaternion.identity))
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(damage))
                .AddDeathMask(1 << LayerMask.NameToLayer("Characters"))
                .AddIsTouchDeathMask();

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsTouchDeathMask.Value));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)

                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotateSystem())
                .AddSystem(new ContactsDetectingSystem())
                .AddSystem(new ContactsEntitiesFilterSystem(_container.Resolve<CollidersRegistryService>()))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new DeathMaskTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private void AddTeleportFeature(Entity entity)
        {
            entity
                .AddTeleportCurrentTime()
                .AddTeleportProcessTime(new ReactiveVariable<float>(0.5f))
                .AddTeleportCost(new ReactiveVariable<float>(20))
                .AddTeleportRadius(new ReactiveVariable<float>(3))
                .AddInTeleportProcess()
                .AddTeleportEnergyThreshold(new ReactiveVariable<float>(40))
                .AddCurrentTargetPosition()
                .AddTeleportEvent()
                .AddTeleportRequest()
                .AddTeleportEnded();
        }

        private void AddEnergyFeature(Entity entity)
        {
            entity
                .AddCurrentEnergy(new ReactiveVariable<float>(35))
                .AddMaxEnergy(new ReactiveVariable<float>(100))
                .AddEnergyRegenPercent(new ReactiveVariable<float>(15))
                .AddEnergyRegenDelay(new ReactiveVariable<float>(3))             
                .AddEnergyRegenCurrentTime()
                .AddInRegenEnergyProcess()
                .AddEnergyRegenEvent()
                .AddEnergyRegenRequest()
                .AddEnergySpendEvent();
        }

        private void AddLifeCycleFeature(Entity entity, float hp = 100)
        {
            entity
              .AddMaxHealth(new ReactiveVariable<float>(100))
              .AddCurrentHealth(new ReactiveVariable<float>(hp))
              .AddIsDead()
              .AddInDeathProcess()
              .AddTakeDamageRequest()
              .AddTakeDamageEvent();
        }

        private void AddAreaDamageFeature(Entity entity)
        {
            entity
                .AddInstantAttackDamage(new ReactiveVariable<float>(60))
                .AddInAttackProcess()
                .AddStartAttackEvent()
                .AddStartAttackRequest();
        }

        private void AddSensorFeature(Entity entity)
        {
            entity
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddRadiusDetecting(new ReactiveVariable<float>(5));
        }

        private void AddConditions(Entity entity)
        {
            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRegenEnergy = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false))
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value < entity.MaxEnergy.Value));

            ICompositeCondition canTeleportingProcess = new CompositeCondition()
               .Add(new FuncCondition(() => entity.IsDead.Value == false))
               .Add(new FuncCondition(() => entity.InDeathProcess.Value == false))
               .Add(new FuncCondition(() => entity.InTeleportProcess.Value == false))
               .Add(new FuncCondition(() => entity.CurrentEnergy.Value >= entity.TeleportCost.Value));

            ICompositeCondition canStartAttack = new CompositeCondition()
               .Add(new FuncCondition(() => entity.IsDead.Value == false))
               .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            entity
               .AddMustDie(mustDie)
               .AddMustSelfRelease(mustSelfRelease)
               .AddCanApplyDamage(canApplyDamage)
               .AddCanTeleportProcess(canTeleportingProcess)
               .AddCanStartAttack(canStartAttack)
               .AddCanRegenerateEnergy(canRegenEnergy);
        }

        private void AddSystems(Entity entity)
        {
            entity
                .AddSystem(new ContactsDetectingSystem())
                .AddSystem(new ContactsEntitiesFilterSystem(_container.Resolve<CollidersRegistryService>()))

                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new ApplyAreaDamageSystem())

                .AddSystem(new EnergyRegenCooldownSystem())
                .AddSystem(new EnergyRegenSystem())
                .AddSystem(new EnergySpendSystem())
                .AddSystem(new EnergyRegenDeciderSystem())

                .AddSystem(new StartTeleportingProcessSystem())
                .AddSystem(new TimerTeleportingProcessSystem())
                .AddSystem(new EndTeleportingProcessSystem())
                .AddSystem(new TeleportPositionSystem());
        }

        private Entity CreateEmpty() => new Entity();
    }
}
