using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class TeleportPositionSystem : IInitializableSystem, IDisposableSystem
    {
        private IDisposable _teleportEvents;
        private Transform _characterTransform;

        private ReactiveVariable<float> _availableRadius;
        private ReactiveVariable<Vector3> _targetPosition;

        public void OnInit(Entity entity)
        {
            _availableRadius = entity.TeleportRadius;
            _characterTransform = entity.Transform;
            _targetPosition = entity.CurrentTargetPosition;
            _teleportEvents = entity.TeleportEvent.Subscribe(Teleport);
        }

        private void Teleport()
        {
            if (_targetPosition == null)
                return;

            Vector3 direction = (_targetPosition.Value - _characterTransform.position);

            if (direction.magnitude > _availableRadius.Value)
                direction = direction.normalized * _availableRadius.Value;

            _characterTransform.position += direction;
        }

        public void OnDispose()
        {
            _teleportEvents.Dispose();
        }
    }
}
