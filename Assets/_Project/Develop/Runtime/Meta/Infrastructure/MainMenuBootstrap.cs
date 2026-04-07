using UnityEngine;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : Bootstrap
    {
        private DIContainer _container;
        private MetaProcess _metaProcess;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs inputSceneArgs)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);

            container.Initialize();
        }

        public override IEnumerator Initialize()
        {
            _metaProcess = _container.Resolve<MetaProcess>();

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
        }
    }
}