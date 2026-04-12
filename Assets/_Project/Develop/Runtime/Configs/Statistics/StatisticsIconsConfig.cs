using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Statistics/NewStatisticsIconsConfig", fileName = "StatisticsIconsConfig")]
    public class StatisticsIconsConfig : ScriptableObject
    {
        [SerializeField] private List<StatisticsItemConfig> _configs;

        public Sprite GetSpriteFor(StatisticsItemTypes statisticsType)
            => _configs.First(config => config.Type == statisticsType).Sprite;

        [Serializable]
        private class StatisticsItemConfig
        {
            [field: SerializeField] public StatisticsItemTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
