namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.KeyStorage
{
    public interface IKeyStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}
