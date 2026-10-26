using System;
using GTA;
using GTA5RP.Core.Contracts;
using GTA5RP.Persistence;

namespace GTA5RP.Persistence
{
    public class PersistenceSubsystem : ISubsystem
    {
        private IPlayerRepository _repo;
        private PlayerData _data;
        private DateTime _lastSave = DateTime.MinValue;
        private const int AutosaveSeconds = 60;

        public string Name => "Persistence";
        public int Version => 1;

        public void Init(IServiceProvider services)
        {
            _repo = new SqlitePlayerRepository();
            _repo.Init("scripts/GTA5RP/player.db");
            _data = _repo.Load("player");
            // apply loaded position
            try
            {
                if (_data.X != 0 || _data.Y != 0 || _data.Z != 0)
                {
                    Game.Player.Character.Position = new GTA.Math.Vector3(_data.X, _data.Y, _data.Z);
                    Game.Player.Character.Heading = _data.Heading;
                }
                Game.Player.Money = _data.Money;
            }
            catch (Exception e) { GTA.UI.Notification.Show("~r~Persist load err: " + e.Message); }
        }

        public void Tick()
        {
            if ((DateTime.UtcNow - _lastSave).TotalSeconds < AutosaveSeconds) return;
            _lastSave = DateTime.UtcNow;
            try
            {
                var p = Game.Player.Character;
                _data.Money = Game.Player.Money;
                _data.Health = p.Health;
                _data.Armor = p.Armor;
                _data.X = p.Position.X;
                _data.Y = p.Position.Y;
                _data.Z = p.Position.Z;
                _data.Heading = p.Heading;
                _data.WantedLevel = Game.Player.WantedLevel;
                _repo.Save(_data);
            }
            catch (Exception e) { GTA.UI.Notification.Show("~r~Persist save err: " + e.Message); }
        }

        public void Dispose()
        {
            try
            {
                Tick();
            }
            catch { }
        }
    }
}
