using System;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Economy
{
    public class EconomySubsystem : ISubsystem
    {
        public string Name => "Economy";
        public int Version => 1;
        public void Init(IServiceProvider services) { }
        public void Tick() { }
        public void Dispose() { }
    }
}
