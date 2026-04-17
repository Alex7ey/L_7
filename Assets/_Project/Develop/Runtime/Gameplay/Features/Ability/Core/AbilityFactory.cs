using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Ability
{
    public class AbilityFactory
    {
        private readonly DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public IAbility CreateExplosionAbility()
            => new ExplosionAbility(
                _container.Resolve<ProjectileEntityFactory>());
      
        public IAbility CreateMinePlacement()
        {
           return new MinePlacementAbility(
                _container.Resolve<ProjectileEntityFactory>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<InventoryService>(),
                _container.Resolve<PlayerDataProvider>()
                );
        }
    }
}
