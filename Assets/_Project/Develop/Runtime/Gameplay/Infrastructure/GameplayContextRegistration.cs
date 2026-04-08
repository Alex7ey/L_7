using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.EnemysEntity;
using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.GamePlayScreen;
using Assets._Project.Develop.Runtime.UI.UIRoot;
using Assets._Project.Develop.Runtime.Utilities.AssetsLoader;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateGameplay);
            container.RegisterAsSingle(CreateTowerFactory);
            container.RegisterAsSingle(CreateBrainsFactory);
            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateAIBrainsContext);
            container.RegisterAsSingle(CreateEnemyEntityFactory);
            container.RegisterAsSingle(CreateEntitiesLifeContext);
            container.RegisterAsSingle(CreateCollidersRegistryService);
            container.RegisterAsSingle(CreateGamePlayPresentersFactory);
            container.RegisterAsSingle(CreateProjectileEntityFactory);
            

            container.RegisterAsSingle(CreateGamePlayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateTowerHolderService).NonLazy();
            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();

            container.RegisterAsSingle<IInputService>(CreateDesktopInput);
        }

        private static GamePlayUIRoot CreateGamePlayUIRoot(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();

            GamePlayUIRoot gamePlayUIRootPrefab = resourcesAssetsLoader
                .Load<GamePlayUIRoot>("UI/GamePlay/GamePlayUIRoot");

            return Object.Instantiate(gamePlayUIRootPrefab);
        }

        private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer container)
        {
            return new MonoEntitiesFactory(
                container.Resolve<ResourcesAssetsLoader>(),
                container.Resolve<EntitiesLifeContext>(),
                container.Resolve<CollidersRegistryService>());
        }
        private static DesktopInput CreateDesktopInput(DIContainer container) => new DesktopInput();
     
        private static GamePlayPresentersFactory CreateGamePlayPresentersFactory(DIContainer container) => new(container);

        private static EntitiesFactory CreateEntitiesFactory(DIContainer container) => new EntitiesFactory(container);

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer container) => new EntitiesLifeContext();

        private static TestGameplay CreateGameplay(DIContainer container) => new(container.Resolve<TowerFactory>(), container.Resolve<EnemyEntityFactory>(), container.Resolve<ProjectileEntityFactory>());

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer container) => new CollidersRegistryService();

        private static BrainsFactory CreateBrainsFactory(DIContainer container) => new BrainsFactory(container);

        private static AIBrainsContext CreateAIBrainsContext(DIContainer container) => new AIBrainsContext();

        private static TowerFactory CreateTowerFactory(DIContainer container) => new TowerFactory(container);

        private static EnemyEntityFactory CreateEnemyEntityFactory(DIContainer container) => new EnemyEntityFactory(container);

        private static TowerHolderService CreateTowerHolderService(DIContainer container) => new TowerHolderService(container.Resolve<EntitiesLifeContext>());

        private static ProjectileEntityFactory CreateProjectileEntityFactory(DIContainer container) => new ProjectileEntityFactory(container);
    }
}
