using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.LoadingTree.SharedDataBundle
{
    public class SharedBundle
    {
        private readonly Dictionary<string, object> _values = new();

        public void Add(in string key, in object value) =>
            _values.Add(key, value);

        public void Remvoe(in string key) =>
            _values.Remove(key);

        public TValue Get<TValue>(in string key)
            where TValue : class =>
            _values[key] as TValue;
    }
}