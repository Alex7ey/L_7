using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.MainMenuScreen;
using Assets._Project.Develop.Runtime.Utilities.Cleanup;

namespace Assets._Project.Develop.Runtime.UI
{
    public class MainMenuPresentersFactory
    {
        private DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreenPresenter(MainMenuScreenView view)
        {
            MainMenuScreenPresenter presenter = new(_container.Resolve<ProjectPresentersFactory>(), view, _container.Resolve<MainMenuPopupService>());
            _container.Resolve<DisposableService>().Add(presenter);
            return presenter;
        }
    }
}
