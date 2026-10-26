using System;
using GTA;
using GTA5RP.Core.Contracts;

namespace GTA5RP.UI
{
    public class UISubsystem : ISubsystem
    {
        private DateTime _last = DateTime.UtcNow;
        public string Name => "UI";
        public int Version => 1;

        public void Init(IServiceProvider services) { }

        public void Tick()
        {
            if ((DateTime.UtcNow - _last).TotalSeconds < 1) return;
            _last = DateTime.UtcNow;
            try
            {
                // Placeholder HUD: show money and needs? Minimal to avoid spam.
            }
            catch { }
        }

        public void Dispose() { }
    }
}
