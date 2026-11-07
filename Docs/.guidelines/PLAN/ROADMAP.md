# ROADMAP — expanding the template through virtual paths and shortcuts

> **Proposal, not norm.** An agent does not ratify a norm it wrote, so every row marked
> *Regent* waits for a decision. Measured 2026-10-13. Ledger: `.shortcuts/.virtual/ongoing.json`.

This is step S5 of `PROTOCOL.md`: modules ordered by real dependency. Phases (S6) and tasks
(S7) are derived for one module at a time, when its Order opens — one Order open at a time.

## 1. Where the template stands

| Finding | Evidence |
|---|---|
| The set uses the **Classic** layout; the current Regent standard is **Dotted** | `AGENTS.md`, `PLAN/`, `DECISIONS/`, `SOURCES/`, `TEAM/` against `.shortcuts`, `.plan`, `.decisions`, `.sources`, `.constraints`, `.orders`, `.changelog`, `.history`, `.team` (regency `library/layouts.md` §2) |
| Glyph already runs the Dotted template, so it is the reference instance | `glyph-markup/.guidelines/` — `.shortcuts`, `.plan`, `.decisions`, `.constraints`, `.changelog`, `.orders` (series `EMS-###/ORD-####`), `.sources`, `.history` |
| `main` is an **ancestor** of `ORBITAL_TEMPLATE`, which is four commits ahead | `git log main..ORBITAL_TEMPLATE`; the last one (`ff90abd`, WEB_ORBITAL) is not on `origin` |
| Those four carry norms `main` lacks: `AR26` (invariant vs gate), `AR27` (phase review cites the module objective), the §7 vocabulary, `DC3`, the task lifecycle A–F, and the five app templates | `git diff main ORBITAL_TEMPLATE -- Docs` |
| `README.md` on `main` says the two branches "diverge on purpose and are not merged"; git says one contains the other | `README.md` §Branches |
| 11 uncommitted files on `main`. `PROTOCOL.md` differs only in line endings | `git diff` prints a CRLF warning and no hunks |
| `flow.pgml/.hgml/.min.hgml/.xml` encode the lifecycle A–F and `AR27`, which already exist as prose on `ORBITAL_TEMPLATE` | `flow.hgml` steps A–F, "never review a phase without referencing its module's objective" |
| `flow.hgml` still carries empty slots (`[ctx[/ctx]`, `[cnst[/cnst]`). Glyph reads them as `<needs>`, not errors | `SIGNATURES.md` §Arity |
| One stash, on `DESKTOP_ORBITAL`: a Tailwind `output.css`. Outside this roadmap | `git stash show` |
| No `.shortcuts/.virtual`, no manifest, no `.orders/` | `vp.py trace` |

**Conventions against norms.** The `.csproj` conventions (`net10.0`, nullable, implicit usings,
documentation encouraged, `0.1.0`, no vendoring, `_PROBE` tooling, no `src/`) are fully covered by
`AR11`, `AR22`–`AR25`. The code-style conventions are not:

| Convention | Where it lives today | Gap |
|---|---|---|
| Interfaces split and composed | `AR13` | covered |
| Immutable models, factories, guard clauses, five-beat methods | `AR14`–`AR19` | covered |
| **No inheritance for reuse**, presentation included | `PLAN/PHASES.md` §5.3, §6, §8, §13 (untracked) | no `AR` forbids it |
| **Banned names** (`Mgr`, `Helper`, `Common`, `Base`, `Utils`, `Abs`/`A`, `DTO` bucket) | `PLAN/PHASES.md` §5.3 | no `AR`; also lists `Core`, which collides with the `_CORE` domain |
| **Services accept `I`, never `M`** | `PLAN/PHASES.md` §6, lint 8 | no `AR` |
| Prefix set `Ev Cmd X U C Vm P Cfg` and reserved `H Fct Pol Spec Vs` | `PLAN/PHASES.md` §5 | `AR10` knows six prefixes |
| `R` means **record** in `AR10` and **repository** in `PHASES.md` §5.1 | both | collision |
| `ADR#` in `PHASES.md` F0; `S#` as "one commit" against `PROTOCOL` S0–S9 | `PHASES.md` | ids that collide with `DC#` and the protocol steps |
| `PHASES.md` examples are a card game (`GAME_CORE`, `Card`, `Player`) | `PHASES.md` §3, §7 | not generic |

## 2. Decisions the Regent owes (R0)

| | Decision | Recommendation | Blocks |
|---|---|---|---|
| D1 | Branch of record | `ORBITAL_TEMPLATE` is the line: push `ff90abd`, then move `main` to it. Correct the README table either way | R1, R4 |
| D2 | Adopt the Dotted layout at `Docs/.guidelines/` | yes; becomes `DC4` | R1 |
| D3 | `R` prefix | keep `R` = record (it is in `AR10`); re-prefix the repository | R2 |
| D4 | Version scheme | keep SemVer for the .NET deliverable (`Version` is `0.1.0`, a SemVer ordering). Glyph's `release.frontend.rules.minor` never resets a digit; SemVer does | R3 |
| D5 | Root `CLAUDE.md` | the Regency template tracks it; your rule gitignores it. Keep your rule; the index lives in `.shortcuts/README.md` | R5 |
| D6 | Language | English, en-EU spelling (`behaviour` is already used); import the pt-BR template text translated | R1 |
| D7 | Canonical form of lifecycle A–F and `AR27` | prose in `ARCHITECTURE.md` is the norm; `flow.pgml` is its encoding, `.hgml`/`.xml` are projections | R3 |
| D8 | Class of each new norm (`AR26`) | inheritance and names: invariant (machine-checkable); the rest as stated per task | R2 |
| D9 | Orphan work | which item owns the 11 files; whether `min_max_ladder.zip`, `inspection.pgml`, `production_level_flow.xml` are sources, history or discards | R1 |

## 3. Modules

Each task is one commit, cites a norm, names its proof (`AR20`).

### R1 — Migrate to the Dotted layout

**Objective.** A stranger opens `.shortcuts/README.md` and reaches any document by pointer;
nothing is lost, and every path still resolves (`DC2`).

| Classic | Dotted |
|---|---|
| `AGENTS.md` | `.shortcuts/README.md` (where I am, five-line map, question table) |
| `PLAN/` | `.plan/`, with the "open / authorised / deferred" sections |
| `DECISIONS/` | `.decisions/` (norms stay; the prohibitions are pointed at from `.constraints/`) |
| `SOURCES/` | `.sources/` (facts) |
| `SOURCES/CHANGELOG.md` | `.changelog/README.md` (the rule) and root `CHANGELOG.md` (the record) |
| `TEAM/` | `.team/` — a project spec overrides the library (`layouts.md` §3) |
| — | `.constraints/`, `.orders/`, `.history/` |

1. T01 — Baseline: list every internal link. *Proof:* all resolve before anything moves.
2. T02 — `DC4`: adopt the Dotted layout. *Cites:* `DC2`. *Proof:* supersedes nothing silently.
3. T03 — One `git mv` per axis, links fixed in the same commit. *Cites:* `DC2`. *Proof:* link list from T01 still resolves.
4. T04 — `.shortcuts/README.md` from the template; id grammar and "rules that do not bend" move to `.constraints/`. *Proof:* no fact in two places.
5. T05 — `.constraints/` "Do artefato" points at `AR3`, `AR4`, `AR9`, `AR14` and the never-happen list. *Proof:* it points and never copies.
6. T06 — README layout block and branch table corrected (D1). *Proof:* matches `git branch -vv`.

### R2 — Promote the code conventions to norms, born anchored

**Objective.** The conventions that identify the work are citable `AR#`, not folklore in an
untracked file. Ids go to the next free number, `AR28` on `ORBITAL_TEMPLATE` (`AR26`/`AR27` exist there).

1. T01 — **No inheritance for reuse**: interface composition only; presentation reuse by composition or a Presenter. Kind: invariant. *Proof:* `PHASES.md` lint 7–8 reworded as a checklist.
2. T02 — **Banned names**, with `Core` narrowed to "a type or namespace named Core". *Proof:* a grep list that returns nothing.
3. T03 — **Interface-first signatures**: services accept `I`, never `M`. Kind: invariant.
4. T04 — **Prefix register**: the full set, reserved ones with their triggers; `R` resolved (D3). Amends `AR10`.
5. T05 — `PHASES.md` made generic (binding block, no card-game examples); `ADR#` becomes `DC#`; `S#` mapped through regency `vocabulary-map.md`.
6. T06 — Each norm carries a heading anchor `§AR28`, and the existing `DC#` headings gain `§DC#`. *Proof:* every citation resolves (`virtualpath.md` §2: anchors are exact).

### R3 — Apply Glyph's current specs

**Objective.** `_ORBITAL` inherits what Glyph learnt, without importing Glyph's domain.

1. T01 — **Method rows** into `.constraints/`: a gate is trusted only after it is seen failing; a retired construct becomes a recogniser; a known loss is pinned with its reason and inverted, never deleted; nothing disappears silently; the prose declares the state, never the correction.
2. T02 — **Source and projection** for the flow files (D7). `flow.pgml` is the readable source; `.hgml`, `.min.hgml` and `.xml` are regenerated, never edited. The shortcut records source, copy and rebuild command. *Proof:* the round trip Glyph guarantees (`diag`) passes.
3. T03 — Fill or mark every empty slot in `flow.pgml` as `[TBD — owner]`; none is "fixed" by guessing.
4. T04 — **Link gate**: every internal link resolves and every document is within two links of the index. *Proof:* planted broken link, gate fails, then green.
5. T05 — Version scheme recorded in `.changelog/README.md` (D4); `Directory.Build.props` comment points at it.
6. T06 — Language rule recorded (D6).

### R4 — Lineage and ledger

**Objective.** From a code file to the norms that govern it, and from a norm to its files, with
no search. Adoption order is cheapest first: anchors resolve, manifest valid, staleness fires.

| Slice | Governs | `on_mutation` (proposal) |
|---|---|---|
| `AR22`, `AR25` | `Directory.Build.props` | `mark_stale` |
| `AR3`, `AR4`, `AR5` | every `*.csproj` `ProjectReference` | `lock` |
| `AR1`, `AR8`, `AR10`, `AR11` | project and concept folders | `mark_stale` |
| lifecycle A–F, `AR27` | `flow.pgml` blocks (`glyph_block` selector) | `mark_stale` |

On `main` the targets are `Directory.Build.props` and `_ORBITAL.slnx` only; the app projects exist
on `ORBITAL_TEMPLATE`. That is why D1 comes first.

1. T01 — Plant a citation that points nowhere; `vp.py check` must fail, then pass. Also learn whether `refers_to` accepts folders.
2. T02 — `manifesto.json` born listing, not failing.
3. T03 — Baseline commit recorded; turn staleness on.
4. T04 — The ledger kept true on every boundary; `done` leaves at handoff.

### R5 — Operating machinery

1. T01 — `.orders/`: one Order per module, `ORD-####`, one open at a time; material that arrives mid-Order becomes `INTAKE-*`.
2. T02 — `.shortcuts/README.md` "Onde estou" kept true by the agent.
3. T03 — `.sources/` stays honest: only references that decided something. Empty is allowed.
4. T04 — `.history/` records every retirement (`AR9` is not touched).

### R6 — Template expansion (Regent, `AR9`)

Nothing is created without confirmation. Each candidate runs `PROTOCOL.md` S2 first and returns as a plan.

| Candidate | Evidence |
|---|---|
| Conformance sweep of the five app templates against `AR1`–`AR10` | five apps with `_CORE/_ENGINE/_DRIVE/_PANEL/_PROBE` seen in build output; the tracked tree was not listed |
| Frontend documentation (`.ux/`) for the Blazor/Tailwind panels | `DESKTOP_PANEL/wwwroot/output.css`; `/regency:ux-init` |
| Run `LADDER_PLAN_M_1` to a closed gate as the worked example | `PLAN/LADDER_PLAN_M_1.md` exists |
| Orders authored in Glyph (`germinate`, `handoff`, `resume`) | `glyph-markup/.scope/generics/*.pgml` |

## 4. Order

```
R0 decide ──▶ R1 layout ──▶ R2 norms (anchored) ──▶ R4 lineage ──▶ R6 expansion
                   └──────▶ R3 Glyph specs ────────────┘
                   └──────▶ R5 machinery (parallel after R1)
```

## 5. Not verified

`library/team/references/vocabulary-map.md` and `TEAM/ORCHESTRATION.md` were not read.
`inspection.pgml`, `production_level_flow.xml` and `min_max_ladder.zip` were not opened.
The `ORBITAL_TEMPLATE` norms were read from a diff, not from a checkout. Whether `vp.py`
resolves folders in `refers_to` is unknown until R4.T01.
