using UnityEngine;

namespace Assets.Sources.Services.SaveLoadProgress
{
    internal class SaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";

        public TData LoadProgress<TData>()
            where TData : class =>
            PlayerPrefs.GetString(Key)?.ToDeserialized<TData>();

        public void SaveProgress<TData>(TData data) =>
            PlayerPrefs.SetString(Key, data.ToJson());
    }
}