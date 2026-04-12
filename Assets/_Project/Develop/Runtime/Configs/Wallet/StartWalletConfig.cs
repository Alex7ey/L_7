using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _configs;
        
        public int GetValueFor(CurrencyTypes currencyType) => _configs.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
