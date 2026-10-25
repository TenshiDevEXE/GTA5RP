# GTA5RP

Modular Roleplay environment for GTA V Single Player (offline), built on ScriptHookV + ScriptHookVDotNet v3.x (C#).

## Structure

- `GTA5RP/Core` — contracts, event bus, service registry
- `GTA5RP/Main` — SHVDN entry point and plugin loader
- `GTA5RP/Persistence` — player save (SQLite/JSON)
- `GTA5RP/Economy` — jobs and money flow
- `GTA5RP/Environment` — needs, wanted level, spawning
- `GTA5RP/UI` — HUD and menus
- `GTA5RP/config/manifest.json` — subsystem load order

## Install

Copy built assemblies to `GTA V/scripts/GTA5RP/`.

## License

MIT
