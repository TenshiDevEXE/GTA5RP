# GTA5RP

Modular Roleplay environment for GTA V Single Player (offline), built on ScriptHookV + ScriptHookVDotNet v3.x (C#).

## Structure

- `GTA5RP/Core` — contracts, event bus, service registry, logger
- `GTA5RP/Main` — SHVDN entry point and plugin loader
- `GTA5RP/Persistence` — player save (SQLite/JSON)
- `GTA5RP/Economy` — jobs and money flow
- `GTA5RP/Environment` — needs, wanted level, spawning
- `GTA5RP/UI` — HUD and menus
- `GTA5RP/Tests` — unit tests scaffold
- `GTA5RP/config/manifest.json` — subsystem load order

## Dependencies

- ScriptHookV, SHVDN v3.x
- `GTA`, `GTA.Native`, `GTA.Math`
- System.Data.SQLite, Newtonsoft.Json, LemonUI

## Persistence

- SQLite at `scripts/GTA5RP/player.db` with versioned migrations.
- Autosave every 60s plus on dispose; loads position/money on start.

## Economy

- EconomyService synced to player money every 5s.
- JobManager with Delivery, Mechanic, Emergency, Illegal jobs; payouts, cooldowns, reputation gating.
- Keys: F6 start delivery, F7 complete/payout.

## Environment

- NeedsSystem (hunger/thirst/fatigue) decays over time with health/stamina effects.
- WantedSystem decays wanted level every 5s.
- SpawnService for vehicles.

## UI

- Keybinds: F5 toggle menu; HUD notifications for money and job status.

## Tests

- Scaffold in `GTA5RP/Tests` (unit tests pending wiring).

## Install

Copy built assemblies to `GTA V/scripts/GTA5RP/`.

## License

MIT
