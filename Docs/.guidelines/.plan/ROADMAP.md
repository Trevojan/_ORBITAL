# ROADMAP — the template, reinvented from its descendants

> **Proposal, not norm.** An agent does not ratify a norm it wrote: the module order below waits for
> the Regent, and only what [`.decisions/`](../.decisions/README.md) records is decided. Measured on
> 2026-11-07 against Glyph, AutoRpg and instituto-conforme ([`.sources/`](../.sources/README.md)).
> Ledger: `.shortcuts/.virtual/ongoing.json`.

This is step S5 of [`PROTOCOL.md`](../PROTOCOL.md) applied to the template itself: modules ordered by
real dependency. Steps and tasks are derived for one module at a time, when it opens.

## 1. Where the template stands

| finding | evidence |
|---|---|
| The guidelines run the Dotted layout, with the decision register, the constraints and the four-digit version | module 1, below; `ADR-007` to `ADR-011` |
| The ladder is `M##.D##.T##`, with the phases inside every task, and the exemplar is a blueprint | module 2, below; `AR20`, `AR21`, [`BLUEPRINT_M_01.md`](BLUEPRINT_M_01.md) |
| No inheritance for reuse, the banned names and the prefix register are norms; every norm has an anchor | `AR28`, `AR29`, `AR31` in [`ARCHITECTURE.md`](../.decisions/ARCHITECTURE.md) |
| "Services accept `I`, never `M`" has no norm, and the five apps break it by design: each service transforms its own immutable model (`MLedger Ingest(MLedger, …)`, `MEnvelope Seal(MEnvelope)`) | `PHASES.md` §6 carried the rule; measured on 2026-11-07 across the five apps' services |
| No norm is checked by a machine. Five apps ship ten probe files, and none reads a `.csproj` | AutoRpg's `Seta_aponta_para_dentro` turns `AR3`–`AR5` and `AR8` into a test |
| Nothing checks that a guideline link resolves | Glyph's `link-graph.js` refuses a dead link and a document more than two links from the entry point |
| Every commit warns that LF will become CRLF | `.gitattributes` holds `* text=auto` and nothing else; AutoRpg pins its oracles to LF |
| The Razor apps have no gate on their markup | AutoRpg adds bUnit to the `_PROBE` tooling for that reason |
| `PROTOCOL.md` starts only from a briefing | AutoRpg was adopted from an existing codebase by measuring it first |
| The descendants cite `AR1`–`AR27` without copying them | AutoRpg's and instituto-conforme's `.sources/`; the template is now a source, and nothing says what a clone inherits and what it cites |

## 2. Decisions

Answered on 2026-11-07, and recorded in the register: `ADR-004` to `ADR-015`.

Still owed by the Regent:

| | decision | recommendation | blocks |
|---|---|---|---|
| AR30 | how "services accept `I`, never `M`" reads, given that every app service takes its own model | narrow it: a service takes its **own** concept's `M` and returns a new one (`AR14`); it **MUST NOT** take another concept's `M` — that crosses through the other concept's `I`. Then it is an invariant the apps already keep | module 3, task 1 |
| D10 | what a clone inherits and what it cites | a clone keeps `ARCHITECTURE`, `PROTOCOL`, `.team/` and the three method tables of `.constraints/`; starts `.decisions/`, `.orders/`, `.plan/ROADMAP.md` and `CHANGELOG.md` empty; and records in its `.sources/` the template version it started from | module 5 |

## 3. Modules

Each task is one commit, cites a norm and names its proof (`AR20`).

### Module 1 — the Dotted layout and the registers — **built 2026-11-07, in review**

`PLAN/`, `DECISIONS/`, `SOURCES/`, `TEAM/` became `.plan/`, `.decisions/`, `.sources/`, `.team/`;
`AGENTS.md` became [`.shortcuts/README.md`](../.shortcuts/README.md); `.constraints/`, `.changelog/`,
`.orders/` and `.history/` were born; `DC1`–`DC3` became `ADR-001`–`ADR-003` in a dated register; the
root `CLAUDE.md` points at the entry; the version moved to `0.1.0.01`.

### Module 2 — the ladder restated, and the conventions promoted to norms — **built 2026-11-07, in review**

`AR20` and `AR21` restated for `ADR-008`, with `PLANNING.md` amendment A1; `LADDER_PLAN_M_1.md`
became [`BLUEPRINT_M_01.md`](BLUEPRINT_M_01.md), steps holding tasks, each task listing its seven
phases; `PROTOCOL` S5–S9, the checklist, the role files, `ORCHESTRATION.md` and the README moved to
`M##.D##.T##`. `AR28` (composition, never inheritance) and `AR29` (the banned names) were born as
invariants, and `AR31` (the prefix register, `R` the record and `Repo` the repository) as a gate.
`PHASES.md` became generic and folds into `AR20` (`ADR-013`). Every norm carries an anchor,
`ARCHITECTURE.md#ar28`. Left out: the signature rule, which waits as `AR30` in §2.

### Module 3 — the gates, as `_PROBE` tests

**Objective.** The norms a machine can check are checked, each gate seen failing first, and
`dotnet test` stays the only toolchain.

1. **The arrow and the invariants**: each app's `_PROBE` reads the `.csproj` files and refuses
   `AR3`, `AR4`, `AR5` and `AR8`, and reads the sources for `AR28`, `AR29` and, once decided,
   `AR30`. *Proof:* a planted violation of each fails it.
2. **Recognisers** for the retired forms: `DC#` and the Classic paths outside the acts and the attic.
3. **The link gate**: every internal link resolves, and every guideline is within two links of the
   entry point. Where it lives is a question for the Regent: each app is deletable, so a
   repository-level probe is a new project (`AR9`).
4. `.gitattributes` pins LF for documents and recorded outputs.
5. bUnit joins the `_PROBE` tooling in `Directory.Build.props`, so the Razor markup has a gate.

### Module 4 — adopting an existing codebase

**Objective.** `PROTOCOL.md` has a second way in: a codebase that already exists is measured before
it is moved, and its current output is the oracle every change is checked against.

1. A map template: one line per file, the `AR#` it answers to, a verdict (*conforms*, *partial*,
   *violates*, *empty*, *disappears*) and the module that repairs it.
2. The oracle: the output recorded before the first change; known defects pinned with their reason and
   not fixed during the migration.
3. `PROTOCOL` gains the entry, ending where S5 begins.

### Module 5 — the template as a source, and the operating machinery

1. What a clone inherits and what it cites (D10), written in `.shortcuts/` and the README.
2. The lineage manifest: from a code file to the norms that govern it, and back.
3. The ledger kept true at every boundary.

### Module 6 — template expansion (Regent, `AR9`)

Nothing is created without confirmation. Each candidate runs `PROTOCOL` S2 first and returns as a
plan.

| candidate | evidence |
|---|---|
| Conformance sweep of the five app templates against `AR1`–`AR10` | five apps with `_CORE`, `_ENGINE`, `_DRIVE`, `_PANEL` and `_PROBE`; module 3's arrow gives the first measure |
| Frontend documentation (`.ux/`) for the Blazor panels | `DESKTOP_PANEL`, `WEB_PANEL`; `/regency:ux-init` |
| Run `BLUEPRINT_M_01` to a signed gate as the worked example | the exemplar exists |

## 4. Order

```
M1 ──▶ M2 ──▶ M3 ──▶ M4
         └──────▶ M5 ──▶ M6
```

## 5. Not verified

Glyph's gate scripts were read from their headers, not run. instituto-conforme's own gate,
`scripts/check_guidelines.py`, was not read.
