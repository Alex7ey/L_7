using Assets._Project.Develop.Runtime.Gameplay.Features.Ability.Core;
using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Ability
{
    public class MinePlacementAbility : IAbility
    {
        private ProjectileEntityFactory _projectileEntityFactory;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly InventoryService _inventoryService;
        private readonly PlayerDataProvider _playerDataProvider;

        private const string Mine = "Mine";
        private const int RequiredMines = 1;

        public MinePlacementAbility(
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

        public void Use(AbilityInputData input)
        {
            if (_inventoryService.HasItem(Mine, RequiredMines) == false)
                return;

            if (input.MousePosition == null)
                return;

            _projectileEntityFactory.CreateMineEntity((Vector3)input.MousePosition);

            _inventoryService.Remove(Mine, RequiredMines);

            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

    }
}
