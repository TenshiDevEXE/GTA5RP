using System;
using GTA;
using GTA5RP.Core.DI;
using GTA5RP.Core.Events;
using GTA5RP.Main;

namespace GTA5RP.Main
{
    public class EntryPoint : Script
    {
        private readonly ServiceRegistry _services = new ServiceRegistry();
        private readonly EventBus _events = new EventBus();
        private PluginLoader _loader;

        public EntryPoint()
        {
            _services.Register<IServiceRegistry>(_services);
            _services.Register<IEventBus>(_events);
            _loader = new PluginLoader(_services);
            _loader.LoadAll("scripts/GTA5RP");
            Tick += OnTick;
            Aborted += OnAborted;
        }

        private void OnTick(object sender, EventArgs e)
        {
            try { _loader.Tick(); }
            catch (Exception ex) { GTA.UI.Notification.Show("~r~Fatal: " + ex.Message); }
        }

        private void OnAborted(object sender, EventArgs e)
        {
            try { _loader.Dispose(); } catch { }
        }
    }
}
