using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Entities;
using Assets._Project.Develop.Runtime.Utilities.AssetsLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources = new();

        private Dictionary<Type, string> _configsResourcesPaths = new()
        {
            {typeof(StartWalletConfig), "Configs/StartWalletConfig" },
            {typeof(CurrencyIconsConfig),"Configs/CurrencyIconsConfig"},
            {typeof(TowerConfig),"Configs/Entities/TowerConfig"},
            {typeof(EnemyConfig),"Configs/Entities/EnemyConfig"},
            {typeof(ProjectileConfig),"Configs/Entities/ProjectileConfig"},
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (var configsResourcesPaths in _configsResourcesPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configsResourcesPaths.Value);
                loadedConfigs.Add(configsResourcesPaths.Key, config);
                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }

    }
}
