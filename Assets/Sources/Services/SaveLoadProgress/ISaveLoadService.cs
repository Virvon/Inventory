namespace Assets.Sources.Services.SaveLoadProgress
{
    public interface ISaveLoadService
    {
        void SaveProgress<TData>(TData data);

        TData LoadProgress<TData>()
            where TData : class;
    }
}