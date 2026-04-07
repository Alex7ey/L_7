using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.MainMenuScreen;

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
            return new MainMenuScreenPresenter(_container.Resolve<ProjectPresentersFactory>(), view, _container.Resolve<MainMenuPopupService>());
        }
    }
}
