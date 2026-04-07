using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializer;
using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    public class SaveLoadService : ISaveLoadService
    {
        private IDataRepository _repository;
        private IDataSerializer _serializer;
        private IKeyStorage _keyStorage;

        public SaveLoadService(IDataRepository repository, IDataSerializer serializer, IKeyStorage keyStorage)
        {
            _repository = repository;
            _serializer = serializer;
            _keyStorage = keyStorage;
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            yield return _repository.Exists(key, exists => onExistsResult?.Invoke(exists));
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            string serializedData = "";

            yield return _repository.Read(key, result => serializedData = result);

            TData data = _serializer.Desirialize<TData>(serializedData);

            onLoad?.Invoke(data);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            yield return _repository.Remove(key);
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);

            string key = _keyStorage.GetKeyFor<TData>();

            yield return _repository.Write(key, serializedData);
        }
    }
}
