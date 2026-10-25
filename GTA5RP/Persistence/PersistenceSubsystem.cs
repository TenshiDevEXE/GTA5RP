using System;
using GTA;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Persistence
{
    public class PersistenceSubsystem : ISubsystem
    {
        public string Name => "Persistence";
        public int Version => 1;
        public void Init(IServiceProvider services) { }
        public void Tick() { }
        public void Dispose() { }
    }
}
