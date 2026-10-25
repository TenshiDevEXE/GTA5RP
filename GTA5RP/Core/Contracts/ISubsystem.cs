using System;

namespace GTA5RP.Core.Contracts
{
    public interface ISubsystem
    {
        string Name { get; }
        int Version { get; }
        void Init(IServiceProvider services);
        void Tick();
        void Dispose();
    }

    public interface IAddon
    {
        string Name { get; }
        int ApiVersion { get; }
        void Init(IServiceProvider services);
        void Tick();
        void Dispose();
    }

    public interface IServiceRegistry
    {
        void Register<T>(T instance) where T : class;
        T Get<T>() where T : class;
        bool TryGet<T>(out T instance) where T : class;
    }

    public interface IEventBus
    {
        void Publish<T>(T evt) where T : class;
        void Subscribe<T>(Action<T> handler) where T : class;
        void Unsubscribe<T>(Action<T> handler) where T : class;
    }
}
