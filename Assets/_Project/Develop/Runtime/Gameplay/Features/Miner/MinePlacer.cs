using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Miner
{
    public class MinePlacer : IMiner
    {
        private ProjectileEntityFactory _projectileEntityFactory;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly InventoryService _inventoryService;
        private readonly PlayerDataProvider _playerDataProvider;

        private const string Mine = "Mine";
        private const int RequiredMines = 1;

        public MinePlacer(
            ProjectileEntityFactory projectileEntityFactory,
            ICoroutinesPerformer coroutinesPerformer,
            InventoryService inventoryService,
            PlayerDataProvider playerDataProvider)
        {
            _projectileEntityFactory = projectileEntityFactory;
            _coroutinesPerformer = coroutinesPerformer;
            _inventoryService = inventoryService;
            _playerDataProvider = playerDataProvider;
        }

        public void PlaceMine(Vector3 position)
        {
            if (_inventoryService.HasItem(Mine, RequiredMines) == false)
                return;

            _projectileEntityFactory.CreateMineEntity(position);

            _inventoryService.Remove(Mine, RequiredMines);

            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }
    }
}
