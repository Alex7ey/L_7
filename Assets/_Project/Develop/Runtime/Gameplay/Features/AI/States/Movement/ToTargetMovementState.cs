using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.Movement
{
    public class ToTargetMovementState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Entity> _currentTarget;
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Quaternion> _rotationAngle;
        private readonly Transform _transform;

        private Vector3 _currentDirection;

        public ToTargetMovementState(Entity entity)
        {
            _movementDirection = entity.MoveDirection;
            _rotationAngle = entity.RotationAngle;
            _currentTarget = entity.CurrentTarget;
            _transform = entity.Transform;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public void Update(float deltaTime)
        {
            _currentDirection = _currentTarget.Value.Transform.position - _transform.position;
            _movementDirection.Value = _currentDirection;
            _rotationAngle.Value = Quaternion.LookRotation(_currentDirection);
        }

        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }
    }
}
