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
| The ladder documents still speak `M<n>.F<k>.T<nn>`, with the phases as sections of a module | `AR20`, `AR21`, [`PLANNING.md`](PLANNING.md), [`LADDER_PLAN_M_1.md`](LADDER_PLAN_M_1.md), `PROTOCOL` S6–S7; `ADR-008` decided the replacement |
| The code-style conventions that identify the work have no `AR#`: no inheritance for reuse, the banned names, services accepting `I` and never `M`, the full prefix register | [`PHASES.md`](PHASES.md) §5, §6, §8; `ARCHITECTURE.md` knows six prefixes |
| `R` means **record** in `AR10` and **repository** in `PHASES.md` §5.1 | both |
| No norm is checked by a machine. Five apps ship ten probe files, and none reads a `.csproj` | AutoRpg's `Seta_aponta_para_dentro` turns `AR3`–`AR5` and `AR8` into a test |
| Nothing checks that a guideline link resolves | Glyph's `link-graph.js` refuses a dead link and a document more than two links from the entry point |
| Every commit warns that LF will become CRLF | `.gitattributes` holds `* text=auto` and nothing else; AutoRpg pins its oracles to LF |
| The Razor apps have no gate on their markup | AutoRpg adds bUnit to the `_PROBE` tooling for that reason |
| `PROTOCOL.md` starts only from a briefing | AutoRpg was adopted from an existing codebase by measuring it first |
| The descendants cite `AR1`–`AR27` without copying them | AutoRpg's and instituto-conforme's `.sources/`; the template is now a source, and nothing says what a clone inherits and what it cites |

## 2. Decisions

Answered on 2026-11-07, and recorded in the register: `ADR-004` to `ADR-011`.

Still owed by the Regent:

| | decision | recommendation | blocks |
|---|---|---|---|
| D3 | the `R` prefix | keep `R` = record, as `AR10` has it; `PHASES.md`'s repository takes another prefix | module 2 |
| D6 | language | English with en-EU spelling, as the template is written; in a project whose prose is another language, technical terms stay English (Glyph's constraint) | module 2 |
| D8 | the class of each new norm (`AR26`) | inheritance and names: invariant, so machine-checkable; the rest as stated per task | module 2 |
| D10 | what a clone inherits and what it cites | a clone keeps `ARCHITECTURE`, `PROTOCOL`, `.team/` and the three method tables of `.constraints/`; starts `.decisions/`, `.orders/`, `.plan/ROADMAP.md` and `CHANGELOG.md` empty; and records in its `.sources/` the template version it started from | module 5 |
| D11 | what `F3`–`F5` publish | `PHASES.md` reads `F3` as the export manifest, `F4` as adapters and presenters, `F5` as the surface; AutoRpg reads `F3` as the external surface (`_DRIVE`, composition, authorisation) and `F4` as the typed client. One reading, written once | module 2 |

## 3. Modules

Each task is one commit, cites a norm and names its proof (`AR20`).

### Module 1 — the Dotted layout and the registers — **built 2026-11-07, in review**

`PLAN/`, `DECISIONS/`, `SOURCES/`, `TEAM/` became `.plan/`, `.decisions/`, `.sources/`, `.team/`;
`AGENTS.md` became [`.shortcuts/README.md`](../.shortcuts/README.md); `.constraints/`, `.changelog/`,
`.orders/` and `.history/` were born; `DC1`–`DC3` became `ADR-001`–`ADR-003` in a dated register; the
root `CLAUDE.md` points at the entry; the version moved to `0.1.0.01`.

### Module 2 — the ladder restated, and the conventions promoted to norms

**Objective.** A ladder written today reads `M##.D##.T##` everywhere, and the conventions that
identify the work are citable `AR#`, not folklore in a plan file. New ids start at `AR28`.

1. `AR20` and `AR21` restated for `ADR-008`: the step is the dependency boundary, the task the
   reversibility boundary, `F0`–`F6` inside every task. *Proof:* no `M<n>.F<k>` id left outside acts.
2. `PLANNING.md` and the exemplar: `LADDER_PLAN_M_1.md` becomes `BLUEPRINT_M_01.md`, steps holding
   tasks, each task listing its seven phases. `PROTOCOL` S6–S7, `CHECKLIST`, `TRACE`, the role files and
   the README follow.
3. **No inheritance for reuse** — interface composition only; presentation reuse by composition or a
   presenter. Kind: invariant (D8).
4. **Banned names**, with `Core` narrowed to "a type or namespace named Core", since `_CORE` is a domain.
5. **Interface-first signatures**: services accept `I`, never `M`. Kind: invariant.
6. **The prefix register**: the full active set and the reserved ones with their triggers; `R` settled
   (D3). Amends `AR10`.
7. `PHASES.md` made generic (a binding block, no card-game examples, `S#` read as `T##`) and its phase
   reading settled (D11).
8. Every norm heading carries an anchor (`§AR28`), so a descendant's citation resolves.

### Module 3 — the gates, as `_PROBE` tests

**Objective.** The norms a machine can check are checked, each gate seen failing first, and
`dotnet test` stays the only toolchain.

1. **The arrow**: each app's `_PROBE` reads the `.csproj` files and refuses `AR3`, `AR4`, `AR5` and
   `AR8`. *Proof:* a planted reference fails it.
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

The `F3`–`F5` reading of AutoRpg (D11) was taken from its ladder's legend, not from its code.
Glyph's gate scripts were read from their headers, not run. instituto-conforme's own gate,
`scripts/check_guidelines.py`, was not read.
