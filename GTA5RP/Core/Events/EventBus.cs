using System;
using System.Collections.Generic;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Core.Events
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();

        public void Publish<T>(T evt) where T : class
        {
            if (evt == null) return;
            Type t = typeof(T);
            if (!_handlers.TryGetValue(t, out var list)) return;
            var snapshot = list.ToArray();
            foreach (var d in snapshot)
            {
                try { ((Action<T>)d)(evt); }
                catch (Exception) { /* logged by caller */ }
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : class
        {
            Type t = typeof(T);
            if (!_handlers.TryGetValue(t, out var list))
            {
                list = new List<Delegate>();
                _handlers[t] = list;
            }
            list.Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : class
        {
            Type t = typeof(T);
            if (_handlers.TryGetValue(t, out var list)) list.Remove(handler);
        }
    }
}
