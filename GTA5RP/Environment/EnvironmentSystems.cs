using System;
using GTA;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Environment
{
    public class Needs
    {
        public float Hunger = 100f;
        public float Thirst = 100f;
        public float Fatigue = 100f;
    }

    public class NeedsSystem
    {
        public Needs State = new Needs();
        private DateTime _last = DateTime.UtcNow;
        public void Tick()
        {
            if ((DateTime.UtcNow - _last).TotalSeconds < 10) return;
            _last = DateTime.UtcNow;
            State.Hunger = Math.Max(0, State.Hunger - 0.5f);
            State.Thirst = Math.Max(0, State.Thirst - 0.8f);
            State.Fatigue = Math.Max(0, State.Fatigue - 0.3f);
            try
            {
                var p = Game.Player.Character;
                if (State.Hunger <= 0 || State.Thirst <= 0) p.Health = Math.Max(1, p.Health - 1);
                if (State.Fatigue <= 0) Game.Player.Stamina = Math.Max(0, Game.Player.Stamina - 5);
            }
            catch { }
        }
    }

    public class WantedSystem
    {
        private DateTime _last = DateTime.UtcNow;
        public void Tick()
        {
            if ((DateTime.UtcNow - _last).TotalSeconds < 5) return;
            _last = DateTime.UtcNow;
            try
            {
                int w = Game.Player.WantedLevel;
                if (w > 0) Game.Player.WantedLevel = Math.Max(0, w - 0); // decay handled natively; placeholder for custom logic
            }
            catch { }
        }
        public void SetWanted(int level) { try { Game.Player.WantedLevel = Math.Max(0, Math.Min(5, level)); } catch { } }
    }

    public class SpawnService
    {
        public void SpawnVehicle(GTA.Model model, GTA.Math.Vector3 pos)
        {
            try { World.CreateVehicle(model, pos); } catch { }
        }
    }
}
