using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
    public abstract class DataProvider<TData> where TData : ISaveData
    {
        private TData _data;

        private List<IDataWriter<TData>> _writers = new();
        private List<IDataReader<TData>> _readers = new();

        private ISaveLoadService _saveLoadService;

        public DataProvider(ISaveLoadService saveLoadService) => _saveLoadService = saveLoadService;
      
        public void RegisterWriter(IDataWriter<TData> writer)
        {
            if (_writers.Contains(writer))
                throw new ArgumentException(nameof(writer));

            _writers.Add(writer);
        }

        public void RegisterReader(IDataReader<TData> reader)
        {
            if (_readers.Contains(reader))
                throw new ArgumentException(nameof(reader));

            _readers.Add(reader);
        }

        public IEnumerator LoadAsync()
        {
            yield return _saveLoadService.Load<TData>(result => _data = result);

            SendDataToReaders();
        }

        public IEnumerator SaveAsync()
        {
            UpdateDataFromWriters();

            yield return _saveLoadService.Save(_data);
        }

        public IEnumerator ExistsAsync(Action<bool> onExistsResult)
        {
            yield return _saveLoadService.Exists<TData>(result => onExistsResult?.Invoke(result));
        }

        public void Reset()
        {
            _data = GetOriginData();

            SendDataToReaders();
        }

        protected abstract TData GetOriginData();

        private void UpdateDataFromWriters()
        {
            foreach (var writer in _writers)
                writer.WriteTo(_data);
        }

        private void SendDataToReaders()
        {
            foreach (IDataReader<TData> reader in _readers)
                reader.ReadFrom(_data);
        }
    }
}
