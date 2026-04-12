using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Shooting
{
    public class ShooterController
    {
        private readonly IShooter _shooter;
        private readonly IInputService _inputService;

        private Vector3 _shootPosition;

        public ShooterController(IShooter shooter, IInputService inputService)
        {
            _shooter = shooter;
            _inputService = inputService;
        }

        public void Update(float deltaTime)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) == false)
                return;

            _shootPosition  = hit.point;

            if (_inputService.AttackKeyPress)
                _shooter.Shoot(_shootPosition);
        }
    }
}
