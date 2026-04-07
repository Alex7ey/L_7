using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Develop.Runtime.Meta.Features.Statistics
{
    public class PlayerStatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private Dictionary<StatisticsItemTypes, ReactiveVariable<int>> _statistics = new();

        public PlayerStatisticsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<StatisticsItemTypes> AvailableStatisticsItems => _statistics.Keys.ToList();

        public IReadOnlyVariable<int> GetStats(StatisticsItemTypes type) => _statistics[type];

        public void Add(StatisticsItemTypes type, int amount = 1) => _statistics[type].Value += amount;

        public void ReadFrom(PlayerData data)
        {
            foreach (var statItem in data.StatsData)
            {
                if (_statistics.ContainsKey(statItem.Key))
                    _statistics[statItem.Key].Value = statItem.Value;
                else
                    _statistics.Add(statItem.Key, new ReactiveVariable<int>(statItem.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (var statItem in _statistics)
            {
                if (data.StatsData.ContainsKey(statItem.Key))
                    data.StatsData[statItem.Key] = statItem.Value.Value;
                else
                    data.StatsData.Add(statItem.Key, statItem.Value.Value);
            }
        }

        public void Reset()
        {
            foreach (StatisticsItemTypes statItem in Enum.GetValues(typeof(StatisticsItemTypes)))
                if (_statistics.TryGetValue(statItem, out var value))
                    value.Value = 0;
        }
    }
}
