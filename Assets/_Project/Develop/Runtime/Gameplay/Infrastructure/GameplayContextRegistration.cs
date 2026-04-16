using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.Ability;
using Assets._Project.Develop.Runtime.Gameplay.Features.Ability.Core;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.EnemysEntity;
using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity;
using Assets._Project.Develop.Runtime.Gameplay.GameStates;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.GamePlayScreen;
using Assets._Project.Develop.Runtime.UI.UIRoot;
using Assets._Project.Develop.Runtime.Utilities.AssetsLoader;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        private static GameplayInputArgs _gameplayInputArgs;

        public static void Process(DIContainer container, GameplayInputArgs inputArgs = null)
        {
            container.RegisterAsSingle(CreateTowerFactory);
            container.RegisterAsSingle(CreateBrainsFactory);
            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateAIBrainsContext);
            container.RegisterAsSingle(CreateEnemyEntityFactory);
            container.RegisterAsSingle(CreateEntitiesLifeContext);
            container.RegisterAsSingle(CreateCollidersRegistryService);
            container.RegisterAsSingle(CreateGamePlayPresentersFactory);
            container.RegisterAsSingle(CreateProjectileEntityFactory);
            container.RegisterAsSingle(CreateStageProviderService);
            container.RegisterAsSingle(CreatePreperationTriggerService);
            container.RegisterAsSingle(CreateStagesFactory);
            container.RegisterAsSingle(CreateGameplayStatesFactory);
            container.RegisterAsSingle(CreateGameplayStatesContext);
            container.RegisterAsSingle(CreateAbilityFactory);
            container.RegisterAsSingle(CreateAbilityService);
            container.RegisterAsSingle(CreateAbilityInputService);
           
            container.RegisterAsSingle(CreateGamePlayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateTowerHolderService).NonLazy();
            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();
            container.RegisterAsSingle(CreateGamePlayScreenPresenter).NonLazy();

            container.RegisterAsSingle<IInputService>(CreateDesktopInput);

            _gameplayInputArgs = inputArgs;
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

        private static GameplayStatesContext CreateGameplayStatesContext(DIContainer container)
        {
            return new GameplayStatesContext(container.Resolve<GameplayStatesFactory>().CreateGameplayStateMachine(_gameplayInputArgs));
        }

        private static DesktopInput CreateDesktopInput(DIContainer container) => new DesktopInput();

        private static GamePlayPresentersFactory CreateGamePlayPresentersFactory(DIContainer container) => new(container);

        private static EntitiesFactory CreateEntitiesFactory(DIContainer container) => new EntitiesFactory(container);

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer container) => new EntitiesLifeContext();

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer container) => new CollidersRegistryService();

        private static BrainsFactory CreateBrainsFactory(DIContainer container) => new BrainsFactory(container);

        private static AIBrainsContext CreateAIBrainsContext(DIContainer container) => new AIBrainsContext();

        private static TowerFactory CreateTowerFactory(DIContainer container) => new TowerFactory(container);

        private static EnemyEntityFactory CreateEnemyEntityFactory(DIContainer container) => new EnemyEntityFactory(container);

        private static TowerHolderService CreateTowerHolderService(DIContainer container) => new TowerHolderService(container.Resolve<EntitiesLifeContext>());

        private static ProjectileEntityFactory CreateProjectileEntityFactory(DIContainer container) => new ProjectileEntityFactory(container);

        private static StageProviderService CreateStageProviderService(DIContainer container)
        {
            return new StageProviderService(
                container.Resolve<ConfigsProviderService>().GetConfig<LevelsListConfig>().GetBy(_gameplayInputArgs.LevelNumber),
                container.Resolve<StagesFactory>());
        }

        private static PreperationTriggerService CreatePreperationTriggerService(DIContainer container) => new PreperationTriggerService();

        private static StagesFactory CreateStagesFactory(DIContainer container) => new StagesFactory(container);

        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer container) => new GameplayStatesFactory(container);

        private static GamePlayScreenPresenter CreateGamePlayScreenPresenter(DIContainer container)
        {
            GamePlayUIRoot uiRoot = container.Resolve<GamePlayUIRoot>();
            GamePlayScreenView view = container.Resolve<ViewsFactory>().Create<GamePlayScreenView>(ViewIDs.GamePlayScreenView, uiRoot.HUDLayer);
            GamePlayScreenPresenter presenter = new GamePlayScreenPresenter(container.Resolve<ProjectPresentersFactory>(), view, container.Resolve<GamePlayPresentersFactory>());
            return presenter;
        }

        private static AbilityFactory CreateAbilityFactory(DIContainer container) => new AbilityFactory(container);

        private static AbilityService CreateAbilityService(DIContainer container) 
            => new AbilityService(container.Resolve<AbilityFactory>(), container.Resolve<AbilityInputService>());

        private static AbilityInputService CreateAbilityInputService(DIContainer container)
            => new AbilityInputService(container.Resolve<IInputService>());
    }
}
