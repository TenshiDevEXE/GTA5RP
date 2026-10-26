using System;
using System.IO;
using GTA;

namespace GTA5RP.Core
{
    public static class Logger
    {
        private static readonly string Path = "scripts/GTA5RP/GTA5RP.log";
        public static void Log(string msg)
        {
            try
            {
                Directory.CreateDirectory("scripts/GTA5RP");
                File.AppendAllText(Path, DateTime.UtcNow.ToString("o") + " " + msg + Environment.NewLine);
            }
            catch { }
        }
        public static void Error(string msg, Exception e = null)
        {
            Log("ERROR: " + msg + (e != null ? " :: " + e : ""));
            try { GTA.UI.Notification.Show("~r~" + msg); } catch { }
        }
    }
}
