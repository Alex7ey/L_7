using System;
using System.Collections;
using Object = UnityEngine.Object;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class SceneSwitcherService
    {
        private ILoadingScreen _loadingScreen;
        private DIContainer _projectContainer;
        private SceneLoaderService _sceneLoaderService;

        public SceneSwitcherService(SceneLoaderService sceneLoaderService, ILoadingScreen loadingScreen, DIContainer container)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _projectContainer = container;
        }

        public IEnumerator ProcessSwitchTo(string nameScene, IInputSceneArgs inputSceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(nameScene);

            Bootstrap sceneBootstrap = Object.FindObjectOfType<Bootstrap>();

            if (sceneBootstrap == null)   
                throw new NullReferenceException(nameof(sceneBootstrap) + " not found");

            DIContainer sceneContainer = new(_projectContainer);

            sceneBootstrap.ProcessRegistrations(sceneContainer, inputSceneArgs);

            yield return sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}