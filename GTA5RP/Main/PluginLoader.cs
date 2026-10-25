using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GTA;
using GTA5RP.Core.Contracts;
using GTA5RP.Core.DI;
using GTA5RP.Core.Events;

namespace GTA5RP.Main
{
    public class PluginLoader
    {
        private readonly ServiceRegistry _services;
        private readonly List<ISubsystem> _loaded = new List<ISubsystem>();
        private int _errors;

        public PluginLoader(ServiceRegistry services) { _services = services; }

        public void LoadAll(string baseDir)
        {
            // Manifest-driven: each subsystem ships manifest.json listing assembly + type.
            // Here we register built-in subsystems in priority order.
            var order = new[] { "Persistence", "Economy", "Environment", "UI" };
            foreach (var name in order)
            {
                try
                {
                    var type = Type.GetType("GTA5RP." + name + "." + name + "Subsystem, GTA5RP." + name);
                    if (type == null) continue;
                    var sub = (ISubsystem)Activator.CreateInstance(type);
                    sub.Init(_services);
                    _loaded.Add(sub);
                }
                catch (Exception e)
                {
                    GTA.UI.Notification.Show("~r~Subsystem failed: " + name + " - " + e.Message);
                }
            }
        }

        public void Tick()
        {
            foreach (var s in _loaded.ToArray())
            {
                try { s.Tick(); }
                catch (Exception e)
                {
                    _errors++;
                    GTA.UI.Notification.Show("~r~" + s.Name + " tick error: " + e.Message);
                    if (_errors > 10) { s.Dispose(); _loaded.Remove(s); }
                }
            }
        }

        public void Dispose()
        {
            foreach (var s in _loaded) { try { s.Dispose(); } catch { } }
            _loaded.Clear();
        }
    }
}
