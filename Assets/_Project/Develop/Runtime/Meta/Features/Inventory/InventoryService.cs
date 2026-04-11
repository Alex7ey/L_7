using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.Inventory
{
    public class InventoryService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private Dictionary<string, ReactiveVariable<int>> _inventory = new();

        public InventoryService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public IReadOnlyDictionary<string, ReactiveVariable<int>> Inventory => _inventory;

        public void ReadFrom(PlayerData data)
        {
            _inventory.Clear();

            foreach (var item in data.Inventory)
            {
                if (_inventory.ContainsKey(item.Key))
                    _inventory[item.Key] = item.Value;
                else
                    _inventory.Add(item.Key, item.Value);
            }
        }

        public void WriteTo(PlayerData data)
        {
            data.Inventory.Clear();

            foreach (var item in _inventory)
            {
                if (data.Inventory.ContainsKey(item.Key))
                    data.Inventory[item.Key] = item.Value;
                else
                    data.Inventory.Add(item.Key, item.Value);
            }
        }


        public bool Add(string name, int count = 0)
        {
            if (_inventory.ContainsKey(name))
            {
                _inventory[name].Value += count;
                return true;
            }

            _inventory.Add(name, new ReactiveVariable<int>(count));
            return true;
        }

        public bool Remove(string item, int count = 0)
        {
            if (HasItem(item, count))
            {
                _inventory[item].Value -= count;

                if (_inventory[item].Value <= 0)
                    _inventory.Remove(item);

                return true;
            }

            return false;
        }

        public bool HasItem(string item, int count = 0)
        {
            if (_inventory.ContainsKey(item))
            {
                if (_inventory[item].Value >= count)
                    return true;
            }
            return false;
        }
    }
}
