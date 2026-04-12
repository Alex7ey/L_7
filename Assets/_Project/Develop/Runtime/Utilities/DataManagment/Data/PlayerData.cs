using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Data
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public Dictionary<string, ReactiveVariable<int>> Inventory;

        public Dictionary<StatisticsItemTypes, int> StatsData;

        public List<int> CompletedLevels;
    }
}
