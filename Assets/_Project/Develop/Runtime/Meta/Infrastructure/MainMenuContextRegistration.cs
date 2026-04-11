using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.MainMenuScreen;
using Assets._Project.Develop.Runtime.UI.UIRoot;
using Assets._Project.Develop.Runtime.Utilities.AssetsLoader;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateMainMenuPopupService);
            container.RegisterAsSingle(CreateMainMenuPresentersFactory);

            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
        }

        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer container) => new MainMenuPresentersFactory(container);
    
        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot mainMenuUIRootPrefab = resourcesAssetsLoader
                .Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(mainMenuUIRootPrefab);
        }

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer container)
        {
            MainMenuUIRoot uiRoot = container.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = container
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter = container
                .Resolve<MainMenuPresentersFactory>()
                .CreateMainMenuScreenPresenter(view);

            return presenter;
        }

        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer container)
        {
            return new MainMenuPopupService(
                container.Resolve<ViewsFactory>(),
                container.Resolve<ProjectPresentersFactory>(),
                container.Resolve<MainMenuUIRoot>());
        }
    }
}
