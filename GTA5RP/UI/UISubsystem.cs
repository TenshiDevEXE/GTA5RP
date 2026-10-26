using System;
using GTA;
using LemonUI;
using LemonUI.Menus;
using GTA5RP.Core.Contracts;

namespace GTA5RP.UI
{
    public class UISubsystem : ISubsystem
    {
        private ObjectPool _pool;
        private NativeMenu _mainMenu;
        private bool _visible = false;
        private DateTime _lastKey = DateTime.MinValue;

        public string Name => "UI";
        public int Version => 1;

        public void Init(IServiceProvider services)
        {
            _pool = new ObjectPool();
            _mainMenu = new NativeMenu("GTA5RP", "Menu principal", "Selecciona una opcion");
            _mainMenu.Add(new NativeItem("Dinero", "Muestra tu dinero actual"));
            _mainMenu.Add(new NativeItem("Trabajos", "Iniciar/terminar trabajos"));
            _mainMenu.Add(new NativeItem("Necesidades", "Estado de hambre/sed/fatiga"));
            _pool.Add(_mainMenu);
        }

        public void Tick()
        {
            try
            {
                if (Game.IsKeyPressed(Keys.F5) && (DateTime.UtcNow - _lastKey).TotalMilliseconds > 300)
                {
                    _lastKey = DateTime.UtcNow;
                    _visible = !_visible;
                    if (_visible) _mainMenu.Visible = true; else _mainMenu.Visible = false;
                }
                _pool.Process();
            }
            catch (Exception e) { GTA.UI.Notification.Show("~r~UI err: " + e.Message); }
        }

        public void Dispose()
        {
            try { if (_mainMenu != null) _mainMenu.Visible = false; } catch { }
        }
    }
}
