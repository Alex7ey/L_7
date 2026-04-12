using UnityEngine;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.UI.MainMenuScreen;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : Bootstrap
    {
        private DIContainer _container;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs inputSceneArgs)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);

            container.Initialize();
        }

        public override IEnumerator Initialize()
        {
            yield return _container.Resolve<ConfigsProviderService>().LoadAsync();
        }

        private IEnumerator SwitchScene()
        {
            ILoadingScreen loadingScreen = _container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();

            loadingScreen.Show();

            yield return new WaitForSeconds(1);

            loadingScreen.Hide();

            yield return sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay);
        }

        public override void Run()
        {
            _container.Resolve<MainMenuPopupService>().OpenLevelsMenuPopup();
        }
    }
}