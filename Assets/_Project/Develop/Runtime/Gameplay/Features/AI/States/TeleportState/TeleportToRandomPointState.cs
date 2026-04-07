using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.TeleportState
{
    public class TeleportToRandomPointState : State, IUpdatableState, IDisposable
    {
        private ReactiveEvent _teleportRequest;

        private ReactiveVariable<Vector3> _currentTargetPosition;
        private ReactiveVariable<float> _availableRadius;

        private ReactiveEvent<float> _energySpendEvent;
        private ReactiveVariable<float> _teleportCost;

        private IDisposable _startTeleportEvent;

        public TeleportToRandomPointState(Entity entity)
        {
            _teleportRequest = entity.TeleportRequest;
            _currentTargetPosition = entity.CurrentTargetPosition;
            _availableRadius = entity.RadiusDetecting;
            _energySpendEvent = entity.EnergySpendEvent;
            _teleportCost = entity.TeleportCost;
            _startTeleportEvent = entity.TeleportEvent.Subscribe(() => _energySpendEvent.Invoke(_teleportCost.Value));
        }

        public override void Enter()
        {
            base.Enter();

            _currentTargetPosition.Value = GetRandomPoint();

            _teleportRequest.Invoke();
        }

        private Vector3 GetRandomPoint()
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);

            float x = _availableRadius.Value * Mathf.Cos(angle);
            float y = _availableRadius.Value * Mathf.Sin(angle);

            return new Vector3(x, 0, y);
        }

        public void Update(float deltaTime) { }

        public void Dispose() => _startTeleportEvent.Dispose();
    }
}
