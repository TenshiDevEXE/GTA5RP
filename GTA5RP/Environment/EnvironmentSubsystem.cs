using System;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Environment
{
    public class EnvironmentSubsystem : ISubsystem
    {
        public string Name => "Environment";
        public int Version => 1;
        public void Init(IServiceProvider services) { }
        public void Tick() { }
        public void Dispose() { }
    }
}
