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

## Dependencies

- ScriptHookV, SHVDN v3.x
- `GTA`, `GTA.Native`, `GTA.Math`
- System.Data.SQLite, Newtonsoft.Json, LemonUI

## Persistence

- SQLite at `scripts/GTA5RP/player.db` with versioned migrations.
- Autosave every 60s plus on dispose; loads position/money on start.

## Install

Copy built assemblies to `GTA V/scripts/GTA5RP/`.

## License

MIT
