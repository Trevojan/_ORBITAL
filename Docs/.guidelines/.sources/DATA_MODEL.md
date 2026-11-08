# DATA_MODEL

**Status:** `[TBD]` · **Owner:** DA · **Filled by:** `PROTOCOL.md` S2, then extended by each
module's F1.

Entities, types, keys and granularity. This file is a **fact**: it describes the shape that
exists, not the shape that was hoped for.

---

## Storage

| Item | Value |
|---|---|
| Engine | `[TBD]` |
| Naming convention | `[TBD]` |
| Credentials | never here — configuration or a vault (`AR24`) |

## Entities

Repeat per entity. `*` marks a field added beyond the original scope, with the reason
recorded in the decisions section.

### `<EntityName>`

**Table:** `[TBD]` · **Describes:** `[TBD]`

| Column | Storage type | Language type | Key | Nullable | Description |
|---|---|---|---|---|---|
| | | | | | |

## Enumerations

Closed sets, stored by name so that a reordering cannot silently change stored meaning.

```
<EnumName> : <Value> | <Value>
```

## Relationships

```
<table>.<column>  →  <table>.<column>   (cardinality, optionality)
```

## Metric dictionary

Every number that reaches a person. A metric with no entry here has no agreed meaning, which
means two reports may disagree and both be defensible.

| Metric | Formula | Granularity | Owner | Exclusions | Oracle |
|---|---|---|---|---|---|
| | | | | | |

## Modelling decisions

| # | Decision | Why | Reversible? |
|---|---|---|---|
| | | | |

## Key and granularity

Declared explicitly, because getting this wrong at a low step contaminates every step above
it — the most expensive mistake available to the DA role.

| Entity | Natural key | Granularity — one row is… |
|---|---|---|
| | | |
