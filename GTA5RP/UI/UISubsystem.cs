using System;
using GTA;
using GTA5RP.Core.Contracts;
using GTA5RP.Economy;
using GTA5RP.Environment;

namespace GTA5RP.UI
{
    public class UISubsystem : ISubsystem
    {
        private DateTime _last = DateTime.UtcNow;
        private bool _menuOpen = false;
        private Keys _toggle = Keys.F5;
        private EconomySubsystem _econ;
        private EnvironmentSubsystem _env;

        public string Name => "UI";
        public int Version => 1;

        public void Init(IServiceProvider services)
        {
            try { _econ = services is GTA5RP.Core.DI.ServiceRegistry r && r.TryGet(out EconomySubsystem e) ? e : null; } catch { }
        }

        public void Tick()
        {
            try
            {
                if (Game.IsKeyPressed(_toggle))
                {
                    _menuOpen = !_menuOpen;
                    GTA.UI.Notification.Show(_menuOpen ? "~g~Menu abierto" : "~r~Menu cerrado");
                    System.Threading.Thread.Sleep(200);
                }
                if (_menuOpen && (DateTime.UtcNow - _last).TotalSeconds >= 3)
                {
                    _last = DateTime.UtcNow;
                    GTA.UI.Notification.Show("Dinero: $" + Game.Player.Money);
                }
            }
            catch { }
        }

        public void Dispose() { }
    }
}
