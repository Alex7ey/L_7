namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
      void WriteTo(TData data);
    }
}
