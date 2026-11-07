---
artifact: PHASES
nome_completo: Phase & Prefix Model
versao: 1.0
tipo: definicao_de_estrutura
relacionados: [GRAND_PLAN.md, MINIMUM_PLAN.md, PO.md, DV.md, QA.md, DA.md]
---

# F# — Phase & Prefix Model

> Where a thing may be declared, who may point at it, and what it must be called.

## 1. Identity and Mission

`F#` is the **depth axis** of the planning vertical. It answers *how far from the
norm does this artifact sit*, not *when is it built*.

| Level | Abbrev | Axis | Unit |
|---|---|---|---|
| GRAND_PLAN | MAX | ambition | the ideal, unbuildable |
| MINIMUM_PLAN | MIN | sufficiency | the functional floor |
| LADDER_N | L# | decoupling | a module with its own business rules |
| STEP_N | S# | time | exactly one commit |
| **PHASE** | **F#** | **depth** | **a layer of commitment** |

![The planning vertical: MIN to MAX in steps, a ladder per module, F0–F6 as depth](min_max_ladder.svg)

A step declares a **span**, not a position: `S7 [F1–F3]`. Nothing obliges a step
to walk F0→F6. Most steps touch two or three phases; only a vertical slice
touches all seven.

**The question F# answers:** *at what depth does this live, and what is it
therefore forbidden to know?*

## 2. The Two Rules

Everything below is a consequence of these.

**Rule A — Declaration legality.** A prefix may only be *introduced* at its
assigned phase. See §5.

**Rule B — Reference direction.** A higher phase may reference a lower one.
A lower phase may never reference a higher one. F2 calling into F4 is a
violation regardless of prefix or convenience.

**Corollary — Vocabulary containment.** No artifact at F*n* may name vocabulary
owned by F*n+1*. The core may not say `red`. The rule may not say `component`.

## 3. Project Binding

The phase model is generic. Each repo declares its binding once, at the top of
its own `PHASES.md` or `README`:

```
CORE     = GAME_CORE
adapter  = Blazor component
surface  = the deck
store    = LocalStorage save slot
```

Everywhere below, *CORE*, *adapter*, *surface* and *store* mean whatever the
binding says.

## 4. The Phases

| F | Name | Question | Owns | Must not contain |
|---|---|---|---|---|
| **F0** | Norm | By what authority? | The citable rule: `AR#`, `BR#`, `ADR#` | Any code artifact |
| **F1** | Contract | What is the vocabulary? | Types, signatures, persisted shape | Behaviour, branching |
| **F2** | Rule | What must be true? | CORE behaviour, proven by test | Anything the outside can see |
| **F3** | Exposure | What leaks, deliberately? | The export manifest | New declarations of any kind |
| **F4** | Consumption | Who binds it? | Adapters, presenters, view models | Business rule |
| **F5** | Surface | What does the user touch? | Interaction, layout, copy | State not enumerated at F3 |
| **F6** | Closure | Is it accountable? | Changelog, docs, backlog delta, gate | New behaviour |

### 4.1 Exit conditions

A phase is done when its condition holds. These are gate-able; the content
columns above are not.

- **F0** — the rule has an ID, and something else in the repo cites it
- **F1** — it compiles and round-trips through save/load unchanged
- **F2** — a failing test existed before the implementation
- **F3** — the boundary is describable without a single presentation noun
- **F4** — swapping the adapter would not touch F0–F3
- **F5** — reachable by a user with no console and no keyboard shortcut
- **F6** — a stranger can discover that this happened, and why

### 4.2 F3 is the only crossing

F0–F2 are inside. F4–F5 are outside. F3 is the wall with the window in it.

Every architectural violation this document exists to prevent is one of two
shapes: **F2 leaking to F4**, or **F5 inventing state that F3 never
enumerated**. Everything else is hygiene.

## 5. Prefixes

### 5.1 Active

| Prefix | Kind | Declared | Rule |
|---|---|---|---|
| `E` | Enum | F1 | Members only. No explicit values unless the number *is* the meaning. |
| `Str` | Struct | F1 | `readonly record struct`. Immutable once built. |
| `I` | Interface | F1 | Declared **before** any implementer. Avoids referencing concrete `M`. |
| `M` | Model | F1 | Data. No rule, no I/O, no RNG. **Internal — never exported at F3.** |
| `Ev` | Event | F1 | Outbound DTO. Past tense. Immutable. No methods. Exportable at F3. |
| `Cmd` | Command | F1 | Inbound intent. Imperative name. Immutable. Rejected with `ERejectReason`. |
| `X` | Exception | F1 | Broken invariant only. **Never** a player-caused outcome. |
| `S` | Service | F2 | Operations over the aggregate. No session state of its own. Signs against `I`. |
| `U` | Util | F2 | Static, pure, deterministic. No domain noun in the name. |
| `R` | Repository | `I` at F1, impl at F2 | The **only** I/O seam. `IRSaveStore` → `RJsonSaveStore`. |
| `C` | Component | F4 | Consumes the F3 manifest. Interaction state only. |
| `Vm` | View model | F4 | Derived fields only. Never authoritative state. |
| `P` | Presenter | F4 | `F3 type → Vm`. **The only legal place to choose a colour, icon or label.** |
| `Cfg` | Config | F5 | Palettes, copy tables, layout constants. |

### 5.2 Reserved

Named in advance so that nobody invents `Mgr` under pressure. Declare when the
trigger fires, not before.

| Prefix | Kind | Phase | Trigger |
|---|---|---|---|
| `H` | Handler | F2 | A single user intent requires two or more `S` in sequence. Owns the transaction boundary. |
| `Fct` | Factory | F2 | Construction itself carries a rule citable at F0. If it is just `new`, it is noise. |
| `Pol` | Policy | F2 | The ruleset varies at runtime (mode, variant). Keeps `if (variant == …)` out of every `S`. |
| `Spec` | Specification | F2 | The same predicate is asked in three or more places and must stay identical. |
| `Vs` | View state | F4 | A component's local state grows complex enough to need its own test. |

### 5.3 Forbidden

`Mgr`, `Helper`, `Common`, `Base`, `Core`, `Utils` (as a namespace dump),
`Abs`/`A` for abstract classes, and any general-purpose `DTO` bucket.

These name a feeling, not a responsibility. Anything you want to call `Mgr` is
an `S` whose scope has not been decided yet. Any abstract base at F2 pulls
behaviour into an inheritance chain that no interface describes, defeating §6.

### 5.4 Legality matrix

| Phase | May declare | May reference |
|---|---|---|
| F0 | *nothing* | — |
| F1 | `E` `Str` `I` `M` `Ev` `Cmd` `X` | F1 |
| F2 | `S` `U` `R`(impl) + reserved | F1, F2 |
| F3 | *nothing* | F1, F2 (selects only) |
| F4 | `C` `Vm` `P` `Vs` | F3 manifest only |
| F5 | `Cfg`, views | F3 manifest, F4 |
| F6 | *nothing* | all |

## 6. Interface-First

**Ordering.** Within F1, declare in the order `I` → `Str` → `M`. `E` is
orthogonal and may appear anywhere.

**Composition.** A new `M` must first attempt to be a composition of existing
interfaces — `MCard : IDiscardable, ITargetable`. It declares unique members
only for properties no existing `I` covers. Those unique members are the
model's **residue**.

**Never create a combined interface for structural reasons.** A class may
implement any number of interfaces; a combo type adds no semantic content.
Declare a combined `I` only when the intersection has its own name in the
domain and is cited by an F0 norm. Test: *can you name it without concatenating
the parents?* `IPlayable` passes. `IDiscardableTargetable` fails.

**Signature rule.** `S` and `U` accept `I`, never `M`. A service parameter
typed as a concrete model is F2 reaching into F1's nouns. This is the check
that makes interface-first enforceable rather than aspirational.

**Cost, stated honestly.** Interface-first pushes the concrete type to runtime,
so a wrong-behaviour bug requires tracing composition to find which `M` was
actually passed. That cost scales with implementers per interface. It argues
for keeping the interface set *small and semantic*.

## 7. The F3 Manifest

F3 declares nothing. It **elects** a subset of F1 to cross the boundary. It is
a manifest file, not a source file:

```
F3 EXPORT SET · CORE

  capabilities : IDiscardable, ITargetable, IPlayable
  events       : EvTurnEnded, EvCardDrawn
  commands     : CmdPlayCard, CmdEndTurn
  state        : EPhase, EPlayerStatus
  errors       : ERejectReason
  ------------------------------------------------------------
  everything else in CORE is internal, regardless of visibility
```

**Consequences.**

- Anything absent from the manifest is unreachable at F4, even if `public`.
- Models are never listed. `Ev` and `Cmd` are the only DTO shapes that cross,
  because a DTO has no behaviour to abstract.
- Every entry must be describable without a presentation noun. `EPhase` passes.
  `EBannerColour` fails — that is F5 inventing state and back-porting it.
- Widening the manifest is an F3 change and requires its own line in the
  changelog. It is never a side effect of an F4 commit.

## 8. F4–F5 Discipline

Views are not domain objects and the model does not pretend otherwise. The
requirement is single-responsibility naming, not object orthodoxy:

> Every F4–F5 artifact is a named type with one responsibility and no domain
> rule. It may hold interaction state; it may not hold authoritative state.
> **Inheritance between F4–F5 artifacts is prohibited** — reuse is by
> composition or by `P`.

`P` is the load-bearing prefix. `EPlayerStatus.Eliminated → red` lives in a `P`,
never in CORE and never inline in a `C`. Without `P`, colour-choosing leaks into
components, then eventually backwards into F2 "for convenience".

## 9. Definition of Done — per step

A step `S#` is complete when, for every phase in its span:

- [ ] The exit condition in §4.1 holds
- [ ] Every new artifact carries a legal prefix for its phase (§5.4)
- [ ] No new type was declared at F3
- [ ] Any F3 widening is listed in the changelog
- [ ] No `S`/`U` signature names a concrete `M`
- [ ] Residue of any new `M` was reviewed
- [ ] The whole thing fits in **one commit**

## 10. Lint

Four checks cover almost everything. Check 3 is the one that actually buys the
architecture; the rest are hygiene.

1. `S` / `U` / `R`-impl declared outside F2 → **error**
2. `E` / `Str` / `I` / `M` / `Ev` / `Cmd` / `X` declared outside F1 → **error**
3. F4/F5 file references a CORE type absent from the F3 manifest → **error**
4. F0–F3 file contains a presentation term (colour, px, icon, label, component
   name) → **error**

Additional, cheap:

5. `U` whose name contains a domain noun → **warn**
6. `I` with exactly one implementer and no declared future seam → **warn**
7. Combined `I` without an F0 citation → **error**
8. Concrete-type parameter in an F2 signature → **error**

## 11. Health Metrics

- **Mean residue per `M`** — unique members ÷ total members. Rising means an
  abstraction is being missed.
- **Interface reuse count** — an `I` with one implementer is premature or a
  naming exercise. Tolerable for a known upcoming seam; suspicious otherwise.
- **F3 manifest size over time** — should grow in steps, not drift.
- **Commits touching more than three phases** — a step that spans F1–F6 is
  usually two steps.
- **Concrete-type parameters in F2** — should be zero.
- **Reserved prefixes activated without their trigger** — ceremony.

## 12. Quick Checklist

- [ ] Does this cite an F0 norm, or did I invent the rule while coding?
- [ ] Is the interface declared before the model that satisfies it?
- [ ] Is this `M`'s residue actually unique, or did I miss an existing `I`?
- [ ] Did I add anything to the F3 manifest, and did I say so?
- [ ] Could a presentation noun have slipped below F3?
- [ ] Is the colour/label choice inside a `P`?
- [ ] Does this fit in one commit?

## 13. Anti-Patterns

- Walking F0→F6 in every step because the table is numbered
- Declaring a type at F3 instead of electing one
- Exporting an `M` because it was already `public`
- A `U` that consults a service — that is an `S` in denial
- A `Str` with a method that needs external state
- Combined interfaces named by concatenating their parents
- An abstract base class at F2, bypassing the interface contract
- Interaction state at F5 that quietly becomes game state
- `Mgr`, `Helper`, `Common` — a feeling, not a responsibility
- Activating a reserved prefix before its trigger fires

## 14. Edge Cases, Pre-Decided

**`I` implemented by `S`.** Interface at F1, implementation at F2. Normal, and
the reason `I` sits at F1: F2 may depend on vocabulary without depending on
other behaviour.

**A `U` that needs a domain noun.** `USorting` is fine. `UScoreCalculator` is
an `S` in denial — score is a rule, and rules need a citable F0 norm.

**A `Str` that grows a method.** Legal if derivable from its own fields with no
branching on game rule (`Rect.Area`, `Card.IsFace`). Illegal the moment it
consults anything external. Test: *could this method be a `U` taking the struct
as its argument?* If yes, either form is fine. If it needs a service, the struct
has become an `S`.

**`M` used at F4.** Reference yes, declaration no — and only if that `M` is in
the manifest, which for models it should never be. This is the most common real
violation: F4 imports an internal model because it happens to be public, and the
boundary quietly vanishes.

**`X` versus `ERejectReason`.** A rejection is an expected outcome the player
caused and the surface explains. An exception is a broken invariant nobody
should be able to reach. Conflating them turns bugs into UI messages.

**Reopening a step.** A commit that revisits `S4` is still `S4`, marked as a
reopening, with the phases it re-entered named explicitly.