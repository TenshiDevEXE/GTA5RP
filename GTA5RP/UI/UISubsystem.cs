using System;
using GTA5RP.Core.Contracts;

namespace GTA5RP.UI
{
    public class UISubsystem : ISubsystem
    {
        public string Name => "UI";
        public int Version => 1;
        public void Init(IServiceProvider services) { }
        public void Tick() { }
        public void Dispose() { }
    }
}
