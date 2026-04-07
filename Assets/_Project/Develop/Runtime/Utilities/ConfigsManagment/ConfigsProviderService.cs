using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ConfigsProviderService
    {
        private Dictionary<Type, object> _configs = new();

        private IConfigsLoader[] _loaders;

        public ConfigsProviderService(params IConfigsLoader[] loaders) => _loaders = loaders;  

        public IEnumerator LoadAsync()
        {
            _configs.Clear();

            foreach (var loader in _loaders)
                yield return loader.LoadAsync(loadedConfigs => _configs.AddRange(loadedConfigs));
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException($"Not found config by {typeof(T)}");

            return (T)_configs[typeof(T)];
        }
    }
}
