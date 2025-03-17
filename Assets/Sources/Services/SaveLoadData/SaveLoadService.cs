using UnityEngine;

namespace Assets.Sources.Services.SaveLoadData
{
    internal class SaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";

        public TData TryLoad<TData>()
            where TData : class =>
            PlayerPrefs.GetString(Key)?.ToDeserialized<TData>();

        public void Save<TData>(TData data) =>
            PlayerPrefs.SetString(Key, data.ToJson());
    }
}