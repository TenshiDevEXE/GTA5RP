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

        public DeliveryManager Delivery { get; private set; }
...
        public void Init(IServiceProvider services)
        {
            Service = new EconomyService();
            Jobs = new JobManager(Service);
            Delivery = new DeliveryManager();
            try { Service.Set(Game.Player.Money); } catch { }
        }

        public void Tick()
        {
            try
            {
                if (Game.IsKeyPressed(Keys.F6)) { Jobs.StartJob("Delivery", 0); Delivery.SetTarget(Game.Player.Character.Position + new Vector3(50, 0, 0)); GTA.UI.Notification.Show("~g~Delivery started"); System.Threading.Thread.Sleep(200); }
                if (Game.IsKeyPressed(Keys.F7)) { int p = Jobs.StopJob("Delivery"); GTA.UI.Notification.Show("~g~Payout: $" + p); System.Threading.Thread.Sleep(200); }
                // auto-complete if near target
                var job = Jobs.Jobs.Find(j => j.Name == "Delivery");
                if (job != null && job.IsActive && Delivery.IsNearTarget(Game.Player.Character.Position))
                {
                    ((DeliveryJob)job).CompleteDelivery(100);
                    int p = Jobs.StopJob("Delivery");
                    GTA.UI.Notification.Show("~g~Delivered! Payout: $" + p);
                }
            }
            catch { }
            if ((DateTime.UtcNow - _lastTick).TotalSeconds < 5) return;
            _lastTick = DateTime.UtcNow;
            try { Game.Player.Money = Service.Money; } catch { }
        }

        public void Dispose() { }
    }
}
