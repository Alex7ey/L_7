using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.KeyStorage
{
    public class DataKeysStorage : IKeyStorage
    {
        private readonly Dictionary<Type, string> Keys = new()
        {
            {typeof(PlayerData), "PlayerData" },
        };
  
        public string GetKeyFor<TData>() where TData : ISaveData
            => Keys[typeof(TData)];
    }
}
