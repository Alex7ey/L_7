using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Data
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public Dictionary<string, ReactiveVariable<int>> Inventory;

        public List<int> CompletedLevels;
    }
}
