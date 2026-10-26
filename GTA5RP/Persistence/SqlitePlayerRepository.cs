using System;
using System.Data.SQLite;
using GTA;
using GTA5RP.Core.Contracts;

namespace GTA5RP.Persistence
{
    public class PlayerData
    {
        public string Id { get; set; } = "player";
        public int Money { get; set; }
        public float Health { get; set; }
        public float Armor { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Heading { get; set; }
        public int WantedLevel { get; set; }
    }

    public interface IPlayerRepository
    {
        void Init(string dbPath);
        PlayerData Load(string id);
        void Save(PlayerData data);
        void Migrate();
    }

    public class SqlitePlayerRepository : IPlayerRepository
    {
        private string _connStr;
        private const int SchemaVersion = 1;

        public void Init(string dbPath)
        {
            _connStr = "Data Source=" + dbPath + ";Version=3;";
            using (var c = new SQLiteConnection(_connStr)) { c.Open(); }
            Migrate();
        }

        public void Migrate()
        {
            using (var c = new SQLiteConnection(_connStr))
            {
                c.Open();
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS schema_version(version INTEGER);
CREATE TABLE IF NOT EXISTS players(id TEXT PRIMARY KEY, money INTEGER, health REAL, armor REAL, x REAL, y REAL, z REAL, heading REAL, wanted INTEGER, updated_at TEXT);";
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM schema_version";
                    long n = (long)cmd.ExecuteScalar();
                    if (n == 0)
                    {
                        cmd.CommandText = "INSERT INTO schema_version(version) VALUES(" + SchemaVersion + ")";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public PlayerData Load(string id)
        {
            using (var c = new SQLiteConnection(_connStr))
            {
                c.Open();
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT money,health,armor,x,y,z,heading,wanted FROM players WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new PlayerData
                            {
                                Id = id,
                                Money = r.GetInt32(0),
                                Health = (float)r.GetDouble(1),
                                Armor = (float)r.GetDouble(2),
                                X = (float)r.GetDouble(3),
                                Y = (float)r.GetDouble(4),
                                Z = (float)r.GetDouble(5),
                                Heading = (float)r.GetDouble(6),
                                WantedLevel = r.GetInt32(7)
                            };
                        }
                    }
                }
            }
            return new PlayerData { Id = id };
        }

        public void Save(PlayerData data)
        {
            using (var c = new SQLiteConnection(_connStr))
            {
                c.Open();
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO players(id,money,health,armor,x,y,z,heading,wanted,updated_at)
VALUES(@id,@money,@health,@armor,@x,@y,@z,@heading,@wanted,@u)
ON CONFLICT(id) DO UPDATE SET money=@money,health=@health,armor=@armor,x=@x,y=@y,z=@z,heading=@heading,wanted=@wanted,updated_at=@u;";
                    cmd.Parameters.AddWithValue("@id", data.Id);
                    cmd.Parameters.AddWithValue("@money", data.Money);
                    cmd.Parameters.AddWithValue("@health", data.Health);
                    cmd.Parameters.AddWithValue("@armor", data.Armor);
                    cmd.Parameters.AddWithValue("@x", data.X);
                    cmd.Parameters.AddWithValue("@y", data.Y);
                    cmd.Parameters.AddWithValue("@z", data.Z);
                    cmd.Parameters.AddWithValue("@heading", data.Heading);
                    cmd.Parameters.AddWithValue("@wanted", data.WantedLevel);
                    cmd.Parameters.AddWithValue("@u", DateTime.UtcNow.ToString("o"));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
