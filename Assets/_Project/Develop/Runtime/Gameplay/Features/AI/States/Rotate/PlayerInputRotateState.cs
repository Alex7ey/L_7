using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class PlayerInputRotateState : State, IUpdatableState
    {
        private ReactiveVariable<Quaternion> _rotationAngle;
        private IInputService _inputService;
        private Transform _transform;

        private float _currentAngle;

        public PlayerInputRotateState(Entity entity, IInputService inputService)
        {
            _inputService = inputService;
            _rotationAngle = entity.RotationAngle;
            _transform = entity.Transform;
        }

        public override void Enter()
        {
            base.Enter();

            _currentAngle = _transform.rotation.eulerAngles.y;
        }

        public void Update(float deltaTime)
        {
            if (_inputService.RotationAngle == 0)
                return;

            _currentAngle += _inputService.RotationAngle;
            _rotationAngle.Value = Quaternion.AngleAxis(_currentAngle, Vector3.up);
        }

        public override void Exit()
        {
            base.Exit();

            _currentAngle = 0;
        }
    }
}
