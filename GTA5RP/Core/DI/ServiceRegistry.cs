using System;
using System.Collections.Generic;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Core.DI
{
    public sealed class ServiceRegistry : IServiceRegistry
    {
        private readonly Dictionary<Type, object> _map = new Dictionary<Type, object>();

        public void Register<T>(T instance) where T : class
        {
            _map[typeof(T)] = instance;
        }

        public T Get<T>() where T : class
        {
            if (!_map.TryGetValue(typeof(T), out var o)) throw new InvalidOperationException("Service not registered: " + typeof(T).FullName);
            return (T)o;
        }

        public bool TryGet<T>(out T instance) where T : class
        {
            if (_map.TryGetValue(typeof(T), out var o)) { instance = (T)o; return true; }
            instance = null;
            return false;
        }
    }
}
