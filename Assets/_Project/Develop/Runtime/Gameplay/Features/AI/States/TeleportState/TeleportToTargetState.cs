using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class TeleportToTargetState : State, IUpdatableState
    {
        private ReactiveEvent _teleportRequest;

        private ReactiveVariable<Vector3> _currentTargetPosition;
        private ReactiveVariable<Entity> _currentTargetEntity;

        private ReactiveEvent<float> _energySpendEvent;
        private ReactiveVariable<float> _teleportCost;

        public TeleportToTargetState(Entity entity)
        {
            _teleportRequest = entity.TeleportRequest;
            _currentTargetPosition = entity.CurrentTargetPosition;
            _currentTargetEntity = entity.CurrentTarget;
            _energySpendEvent = entity.EnergySpendEvent;
            _teleportCost = entity.TeleportCost;
        }

        public override void Enter()
        {        
            base.Enter();

            _currentTargetPosition.Value = _currentTargetEntity.Value.Transform.position;

            _teleportRequest.Invoke();

            _energySpendEvent.Invoke(_teleportCost.Value);
        }

        public void Update(float deltaTime) {  }   
    }
}
