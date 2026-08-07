# _ORBITAL

Orbital is a mix of DDD + Medallion pattern, Photino + Blazor and heavy SoC. The goal is multi-platform modularized user-friendly and consistent app. In resume: true organized files for ease of development.

## Layout

Projects are named `<CONCEPT>_<DOMAIN>` and grouped into `<APP>_ORBITAL` folders.

| App | Projects | Role |
|---|---|---|
| `DESKTOP_ORBITAL` | `VIEW_ENGINE`, `VIEW_CORE` | Photino + Blazor desktop client |
| `API_ORBITAL` | `SATELLITE_ENGINE`, `SATELLITE_CORE`, `SATELLITE_DRIVE`, `API_PANEL` | HTTP API, auth, composition root |
| `MIDDLEWARE_ORBITAL` | `RELAY_ENGINE`, `RELAY_CORE` | Decoupled relay — carries opaque payloads |
| `DATA_ORBITAL` | `VAULT_ENGINE`, `VAULT_CORE` | Medallion tiers: bronze → silver → gold |

`_ENGINE` is pure logic. `_CORE` orchestrates its engine's models. `_DRIVE` publishes
endpoints. `_PANEL` — one per app — owns authentication and wiring. Dependencies point
inward only.

`MIDDLEWARE_ORBITAL` and `DATA_ORBITAL` reference no other app; `API_PANEL` composes them.

## Concepts

Every concept is a folder, not a class:

```
Satellite/
  ISatellite.cs      interface (attributes + service contract)
  StrSatellite.cs    constants and lesser data groups
  ESatelliteState.cs derived states
  MSatellite.cs      the model
  SSatellite.cs      the business logic
```

The full convention lives in [CLAUDE.md](CLAUDE.md).

## Build

```bash
dotnet build _ORBITAL.slnx
```

## Run the API

```bash
dotnet run --project API_ORBITAL/API_PANEL --urls http://localhost:5199
```

Routes sit behind an `X-Orbital-Key` header. Scopes (`Read`, `Write`, `Operator`) are
configured under `Access:Keys` — use user-secrets for anything real.

```bash
curl -H "X-Orbital-Key: replace-me-local-only" http://localhost:5199/api/health
```
