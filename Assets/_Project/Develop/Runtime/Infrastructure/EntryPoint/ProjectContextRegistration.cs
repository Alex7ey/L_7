using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsLoader;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializer;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Popups;
using Assets._Project.Develop.Runtime.Utilities.Timer;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateViewFactory);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreatePlayerDataProvider);
            container.RegisterAsSingle(CreateResourceAssetsLoader);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            container.RegisterAsSingle(CreateConfigsProviderService);
            container.RegisterAsSingle(CreateProjectPresentersFactory);
            container.RegisterAsSingle(CreateTimerServiceFactory);
         
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);
            container.RegisterAsSingle<IConfigsLoader>(CreateResourcesConfigsLoader);
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinePerformer);

            container.RegisterAsSingle(CreateWalletService).NonLazy();
            container.RegisterAsSingle(CreatePlayerStatisticsService).NonLazy();
        }

        private static SaveLoadService CreateSaveLoadService(DIContainer container)
        {
            IKeyStorage keyStorage = new DataKeysStorage();
            IDataSerializer serializer = new JsonSerializer();

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

            return new SaveLoadService(dataRepository, serializer, keyStorage);
        }

        private static WalletService CreateWalletService(DIContainer container)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, container.Resolve<PlayerDataProvider>());
        }

        private static PlayerStatisticsService CreatePlayerStatisticsService(DIContainer container) => new PlayerStatisticsService(container.Resolve<PlayerDataProvider>());
       
        private static CoroutinesPerformer CreateCoroutinePerformer(DIContainer container)
        {
            ResourcesAssetsLoader _assetsLoader = container.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinePerformer = _assetsLoader.Load<CoroutinesPerformer>("Prefabs/CoroutinePerformer");

            return Object.Instantiate(coroutinePerformer);
        }

        private static LoadingScreen CreateLoadingScreen(DIContainer container)
        {
            ResourcesAssetsLoader _assetsLoader = container.Resolve<ResourcesAssetsLoader>();

            LoadingScreen loadingScreen = _assetsLoader.Load<LoadingScreen>("Prefabs/StandartLoadingScreen");

            return Object.Instantiate(loadingScreen);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer container)
        {
            return new SceneSwitcherService(container.Resolve<SceneLoaderService>(), container.Resolve<ILoadingScreen>(), container);
        }

        private static SceneLoaderService CreateSceneLoaderService(DIContainer container) => new SceneLoaderService();

        private static ResourcesAssetsLoader CreateResourceAssetsLoader(DIContainer container) => new ResourcesAssetsLoader();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer container) => new ConfigsProviderService(container.Resolve<IConfigsLoader>());

        private static ResourcesConfigsLoader CreateResourcesConfigsLoader(DIContainer container) => new(container.Resolve<ResourcesAssetsLoader>());

        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer container) => new PlayerDataProvider(container.Resolve<ISaveLoadService>(), container.Resolve<ConfigsProviderService>());

        private static ViewsFactory CreateViewFactory(DIContainer container) => new ViewsFactory(container.Resolve<ResourcesAssetsLoader>());

        private static ProjectPresentersFactory CreateProjectPresentersFactory(DIContainer container)
            => new ProjectPresentersFactory(container);

        private static TimerServiceFactory CreateTimerServiceFactory(DIContainer container) => new TimerServiceFactory(container);

    }

}