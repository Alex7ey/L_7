using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Miner
{
    public class MinerController
    {
        private readonly IMiner _miner;
        private readonly IInputService _inputService;

        private Vector3 _targetPosition;

        public MinerController(IMiner shooter, IInputService inputService)
        {
            _miner = shooter;
            _inputService = inputService;
        }

        public void Update(float deltaTime)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) == false)
                return;

            _targetPosition = hit.point;

            if (_inputService.MinePlaceButtonPress)
                _miner.PlaceMine(_targetPosition);         
        }
    }
}
