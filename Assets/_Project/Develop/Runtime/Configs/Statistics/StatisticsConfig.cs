using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Statistics/NewStatisticsConfig", fileName = "StatisticsConfig")]
    public class StatisticsConfig : ScriptableObject
    {
        [SerializeField] private List<StatisticsItem> _configs;

        public int GetValueFor(StatisticsItemTypes currencyType) => _configs.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class StatisticsItem
        {
            [field: SerializeField] public StatisticsItemTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
