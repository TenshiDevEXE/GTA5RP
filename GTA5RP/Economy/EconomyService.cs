using System;
using GTA;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Economy
{
    public class EconomyService
    {
        public int Money { get; private set; }
        public void Deposit(int amount) { if (amount > 0) Money += amount; }
        public bool Withdraw(int amount) { if (amount <= 0 || Money < amount) return false; Money -= amount; return true; }
        public void Set(int amount) { Money = Math.Max(0, amount); }
    }

    public interface IJob
    {
        string Name { get; }
        bool IsActive { get; }
        void Start();
        void Stop();
        int Payout();
        bool CanStart(int reputation);
    }

    public abstract class JobBase : IJob
    {
        public abstract string Name { get; }
        public bool IsActive { get; protected set; }
        public DateTime CooldownUntil { get; protected set; }
        protected int Reputation;
        public virtual bool CanStart(int reputation) { return !IsActive && DateTime.UtcNow >= CooldownUntil && reputation >= MinReputation(); }
        protected virtual int MinReputation() => 0;
        public virtual void Start() { if (!IsActive) IsActive = true; }
        public virtual void Stop() { if (IsActive) { IsActive = false; CooldownUntil = DateTime.UtcNow.AddSeconds(CooldownSeconds()); } }
        protected virtual int CooldownSeconds() => 30;
        public abstract int Payout();
    }

    public class DeliveryJob : JobBase
    {
        public override string Name => "Delivery";
        private int _earned;
        public override int Payout() { var p = _earned; _earned = 0; return p; }
        public void CompleteDelivery(int amount) { if (IsActive) _earned += Math.Max(0, amount); }
    }

    public class MechanicJob : JobBase
    {
        public override string Name => "Mechanic";
        private int _earned;
        public override int Payout() { var p = _earned; _earned = 0; return p; }
        public void RepairVehicle(int amount) { if (IsActive) _earned += Math.Max(0, amount); }
    }

    public class EmergencyJob : JobBase
    {
        public override string Name => "Emergency";
        private int _earned;
        protected override int MinReputation() => 10;
        public override int Payout() { var p = _earned; _earned = 0; return p; }
        public void Respond(int amount) { if (IsActive) _earned += Math.Max(0, amount); }
    }

    public class IllegalJob : JobBase
    {
        public override string Name => "Illegal";
        private int _earned;
        protected override int MinReputation() => 5;
        protected override int CooldownSeconds() => 60;
        public override int Payout() { var p = _earned; _earned = 0; return p; }
        public void Heist(int amount) { if (IsActive) _earned += Math.Max(0, amount); }
    }

    public class JobManager
    {
        private readonly EconomyService _economy;
        public System.Collections.Generic.List<IJob> Jobs { get; } = new System.Collections.Generic.List<IJob>();

        public JobManager(EconomyService economy)
        {
            _economy = economy;
            Jobs.Add(new DeliveryJob());
            Jobs.Add(new MechanicJob());
            Jobs.Add(new EmergencyJob());
            Jobs.Add(new IllegalJob());
        }

        public bool StartJob(string name, int reputation)
        {
            var job = Jobs.Find(j => j.Name == name);
            if (job == null || !job.CanStart(reputation)) return false;
            job.Start();
            return true;
        }

        public int StopJob(string name)
        {
            var job = Jobs.Find(j => j.Name == name);
            if (job == null || !job.IsActive) return 0;
            int payout = job.Payout();
            job.Stop();
            _economy.Deposit(payout);
            return payout;
        }
    }
}
