using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(string nameScene)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
            yield return new WaitWhile(() => asyncOperation.isDone == false);
        }

        public IEnumerator UnloadAsync(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            yield return new WaitWhile(() => asyncOperation.isDone == false);
        }
    }
}
