using UnityEngine;

namespace Assets.Sources.Services.SaveLoadProgress
{
    public static class DataExtansions
    {
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);
    }
}