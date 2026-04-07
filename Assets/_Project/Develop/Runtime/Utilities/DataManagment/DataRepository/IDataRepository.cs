using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
    public interface IDataRepository
    {
        IEnumerator Remove(string key);
        IEnumerator Read(string key, Action<string> onRead);
        IEnumerator Write(string key, string serializedData);
        IEnumerator Exists(string key, Action<bool> onExistsResult);
    }
}
