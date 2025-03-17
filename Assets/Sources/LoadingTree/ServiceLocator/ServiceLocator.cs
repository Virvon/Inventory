using System;
using System.Collections.Generic;

namespace Assets.Sources.LoadingTree.ServiceLocator
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new();

        public static void Resgister<TInstance>(TInstance instance)
            where TInstance : class
        {
            if (instance == null)
                throw new ArgumentNullException($"instance of type {typeof(TInstance)} is null");

            if (_services.TryAdd(typeof(TInstance), instance) == false)
                throw new ArgumentException($"instance of type {typeof(TInstance)} is already registered");
        }

        public static TInstance Get<TInstance>()
            where TInstance : class
        {
            if (_services.TryGetValue(typeof(TInstance), out object instance) == false)
                throw new ArgumentException($"instance of type {typeof(TInstance)} is not registered");

            return instance as TInstance;
        }
    }
}
