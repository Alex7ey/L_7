using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotateFeature
{
    public class TransformRotateSystem : IInitializableSystem, IUpdatableSystem
    {
        private Transform _transform;
        private ReactiveVariable<float> _rotationSpeed;
        private ReactiveVariable<Vector3> _direction;

        public void OnInit(Entity entity)
        {
            _direction = entity.MoveDirection;
            _transform = entity.Transform;
            _rotationSpeed = entity.RotationSpeed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_direction.Value != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(_direction.Value);
                float step = _rotationSpeed.Value * deltaTime;
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
            }
        }
    }
}
