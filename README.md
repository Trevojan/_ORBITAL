# _ORBITAL

Orbital is a mix of DDD + Medallion pattern, Photino + Blazor and heavy SoC. The goal is multi-platform modularized user-friendly and consistent app. In resume: true organized files for ease of development.

## Layout

Projects are named `<CONCEPT>_<DOMAIN>` and grouped into `<APP>_ORBITAL` folders.

| App | Projects | Role |
|---|---|---|
| `DESKTOP_ORBITAL` | `VIEW_ENGINE`, `VIEW_CORE`, `UPLINK_DRIVE`, `DESKTOP_PANEL`, `VIEW_PROBE` | Photino + Blazor desktop client |
| `API_ORBITAL` | `SATELLITE_ENGINE`, `SATELLITE_CORE`, `SATELLITE_DRIVE`, `API_PANEL`, `SATELLITE_PROBE` | HTTP API, auth, composition root |
| `MIDDLEWARE_ORBITAL` | `RELAY_ENGINE`, `RELAY_CORE`, `RELAY_PROBE` | Decoupled relay — carries opaque payloads |
| `DATA_ORBITAL` | `VAULT_ENGINE`, `VAULT_CORE`, `VAULT_PROBE` | Medallion tiers: bronze → silver → gold |

`_ENGINE` is pure logic. `_CORE` orchestrates its engine's models. `_DRIVE` handles endpoints
and outbound requests. `_PANEL` — one per app — owns authentication and wiring. `_PROBE` holds
tests and sits outside the arrow. Dependencies point inward only.

`MIDDLEWARE_ORBITAL` and `DATA_ORBITAL` reference no other app; `API_PANEL` composes them.
`UPLINK_DRIVE` calls the API without referencing it, mirroring its wire shapes by agreement.

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

## Build and test

```bash
dotnet build _ORBITAL.slnx
```

```bash
dotnet test _ORBITAL.slnx
```

## Run

Start the API first — the desktop reports `Offline` without it.

```bash
dotnet run --project API_ORBITAL/API_PANEL --urls http://localhost:5199
```

```bash
dotnet run --project DESKTOP_ORBITAL/DESKTOP_PANEL
```

Routes sit behind an `X-Orbital-Key` header. Scopes (`Read`, `Write`, `Operator`) are
configured under `Access:Keys` — use user-secrets for anything real. The desktop reads its
key from the `ORBITAL_KEY` environment variable.

```bash
curl -H "X-Orbital-Key: replace-me-local-only" http://localhost:5199/api/health
```
