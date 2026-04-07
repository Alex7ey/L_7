namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializer
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);
        TData Desirialize<TData>(string serializedData);
    }
}
