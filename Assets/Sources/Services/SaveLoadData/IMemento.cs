namespace Assets.Sources.Services.SaveLoadData
{
    public interface IMemento
    {
        TData GetData<TData>()
            where TData : class;
    }
}
