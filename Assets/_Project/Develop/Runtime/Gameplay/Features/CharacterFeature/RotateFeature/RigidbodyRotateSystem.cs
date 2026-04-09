using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotateFeature
{
    public class RigidbodyRotateSystem : IInitializableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        private ReactiveVariable<float> _rotationSpeed;
        private ReactiveVariable<Quaternion> _rotationAngle;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _rotationAngle = entity.RotationAngle;
            _rotationSpeed = entity.RotationSpeed;
        }

        public void OnUpdate(float deltaTime)
        {
            float step = _rotationSpeed.Value * deltaTime;

            Quaternion rotation = Quaternion.RotateTowards(_rigidbody.rotation, _rotationAngle.Value, step);

            _rigidbody.MoveRotation(rotation);
        }
    }
}

