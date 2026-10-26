using System;
using GTA;
using GTA5RP.Core.Contracts;
using GTA5RP.Economy;

namespace GTA5RP.Economy
{
    public class EconomySubsystem : ISubsystem
    {
        public EconomyService Service { get; private set; }
        public JobManager Jobs { get; private set; }
        private int _reputation = 0;
        private DateTime _lastTick = DateTime.MinValue;

        public string Name => "Economy";
        public int Version => 1;

        public void Init(IServiceProvider services)
        {
            Service = new EconomyService();
            Jobs = new JobManager(Service);
            try { Service.Set(Game.Player.Money); } catch { }
        }

        public void Tick()
        {
            try
            {
                if (Game.IsKeyPressed(Keys.F6)) { Jobs.StartJob("Delivery", 0); GTA.UI.Notification.Show("~g~Delivery started"); System.Threading.Thread.Sleep(200); }
                if (Game.IsKeyPressed(Keys.F7)) { int p = Jobs.StopJob("Delivery"); GTA.UI.Notification.Show("~g~Payout: $" + p); System.Threading.Thread.Sleep(200); }
            }
            catch { }
            if ((DateTime.UtcNow - _lastTick).TotalSeconds < 5) return;
            _lastTick = DateTime.UtcNow;
            try { Game.Player.Money = Service.Money; } catch { }
        }

        public void Dispose() { }
    }
}
