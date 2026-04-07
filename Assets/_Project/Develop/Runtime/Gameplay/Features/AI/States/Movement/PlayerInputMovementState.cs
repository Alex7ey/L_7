using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class PlayerInputMovementState : State, IUpdatableState
    {
        private IInputService _inputService;
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveVariable<Quaternion> _rotationAngle;

        public PlayerInputMovementState(Entity entity, IInputService inputService)
        {
            _inputService = inputService;
            _movementDirection = entity.MoveDirection;
            _rotationAngle = entity.RotationAngle;
        }

        public void Update(float deltaTime)
        {
            if (_inputService.MovementDirection == Vector3.zero)
                return;

            _movementDirection.Value = _inputService.MovementDirection;
            _rotationAngle.Value = Quaternion.LookRotation(_inputService.MovementDirection);
        }

        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }
    }
}
