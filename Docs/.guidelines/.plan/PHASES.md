# PHASES — the phase and prefix model

**Owner:** DV · **Amended by:** the Regent, through [`.decisions/`](../.decisions/README.md)

> Where a thing may be declared, who may point at it, and what it must be called.

## 1. Identity and mission

`F#` is the **depth axis** of the planning vertical. It answers *how far from the norm does this
artefact sit*, not *when is it built*.

| Level | Abbrev | Axis | Unit |
|---|---|---|---|
| GRAND_PLAN | MAX | ambition | the ideal, unbuildable |
| MINIMUM_PLAN | MIN | sufficiency | the functional floor |
| MODULE | `M##` | decoupling | a feature with its own business rules |
| STEP | `D##` | dependency | a functionality the module climbs at once |
| TASK | `T##` | time | exactly one commit |
| **PHASE** | **`F#`** | **depth** | **a layer of commitment, inside every task** |

![The planning vertical: MIN to MAX in steps, a ladder per module, F0–F6 as depth](min_max_ladder.svg)

The diagram was drawn before `ADR-008`: its `L#` is a module, `M##`, and its `S#` a task, `T##`.

A task runs all seven phases as its protocol and declares a **span**: the phases it actually
publishes in, `M01.D02.T03 [F1–F3]`. The rest are declared empty, never omitted (`AR20`). Most
tasks span two or three phases; only a vertical slice spans all seven.

**The question F# answers:** *at what depth does this live, and what is it therefore forbidden
to know?*

## 2. The two rules

Everything below is a consequence of these.

**Rule A — Declaration legality.** A prefix may only be *introduced* at its assigned phase. See
§5.

**Rule B — Reference direction.** A higher phase may reference a lower one. A lower phase may
never reference a higher one. F2 calling into F4 is a violation regardless of prefix or
convenience.

**Corollary — Vocabulary containment.** No artefact at F*n* may name vocabulary owned by F*n+1*.
The core may not say `red`. The rule may not say `component`.

## 3. Project binding

The phase model is generic. Each project declares its binding once, at the top of its own
`PHASES.md` or README. For a parcel-tracking app, say:

```
CORE     = PARCEL_CORE
adapter  = Blazor component
surface  = the dispatch board
store    = a JSON file per depot
```

Everywhere below, *CORE*, *adapter*, *surface* and *store* mean whatever the binding says.

## 4. The phases

The names and what each publishes are `AR20`'s. This table adds what each must not contain.

| F | Name | Question | Publishes | Must not contain |
|---|---|---|---|---|
| **F0** | Norm | By what authority? | the citable rule: `ADR-###`, `AR#`, `BR#` | any code artefact |
| **F1** | Contract | What is the vocabulary? | types, signatures, persisted shape | behaviour, branching |
| **F2** | Rule | What must be true? | CORE behaviour, proven by test | anything the outside can see |
| **F3** | Exposure | What leaks, deliberately? | the outward surface — `_DRIVE` endpoints, `_PANEL` composition and authorisation — and the export set (§7) | new domain types |
| **F4** | Consumption | Who binds it? | the consumer side: the typed client, presenters, view models | business rule |
| **F5** | Surface | What does a person touch? | interaction, layout, copy | state the export set does not list |
| **F6** | Closure | Is it accountable? | changelog, documents, backlog move, gate | new behaviour |

### 4.1 Exit conditions

A phase is done when its condition holds. These are gate-able; the content columns above are not.

- **F0** — the rule has an id, and something else in the repository cites it
- **F1** — it compiles and round-trips through save and load unchanged
- **F2** — a failing test existed before the implementation
- **F3** — the boundary is describable without a single presentation noun
- **F4** — swapping the adapter would not touch F0–F3
- **F5** — reachable by a person with no console and no keyboard shortcut
- **F6** — a stranger can discover that this happened, and why

### 4.2 F3 is the only crossing

F0–F2 are inside. F4–F5 are outside. F3 is the wall with the window in it.

Every architectural violation this document exists to prevent is one of two shapes: **F2
leaking to F4**, or **F5 inventing state the export set never listed**. Everything else is
hygiene.

## 5. Prefixes

The register — every prefix, its kind and its rule — is `AR31` in
[`ARCHITECTURE.md`](../.decisions/ARCHITECTURE.md#ar31). This section adds only where each may be
declared.

| Phase | May declare | May reference |
|---|---|---|
| F0 | *nothing* | — |
| F1 | `I` `Str` `E` `R` `M` `Ev` `Cmd` `X` | F1 |
| F2 | `S` `U` `Repo` (implementation; its `I` is F1) + the reserved `H` `Fct` `Pol` `Spec` | F1, F2 |
| F3 | the outward surface; no new domain type | F1, F2 (selects only) |
| F4 | `C` `Vm` `P` `Vs` | the export set only |
| F5 | `Cfg`, views | the export set, F4 |
| F6 | *nothing* | all |

The forbidden names are `AR29`: they name a feeling, not a responsibility, and anything one
wants to call `Mgr` is an `S` whose scope has not been decided yet.

## 6. Interface first

**Ordering.** Within F1, declare in the order `I` → `Str` → `M`. `E` is orthogonal and may
appear anywhere.

**Composition.** A new `M` first tries to be a composition of existing interfaces —
`MParcel : IIdentified, IWeighed`. It declares unique members only for properties no existing
`I` covers. Those unique members are the model's **residue**. A combined interface is declared
only when the intersection has its own name (`AR13`), and reuse is never by inheritance
(`AR28`).

**Signatures.** Whether a service may take a concrete model as a parameter is open: the five app
templates' services each take their own model, and the norm that would forbid it waits for the
Regent ([`ROADMAP.md`](ROADMAP.md) §2, `AR30`).

**Cost, stated honestly.** Interface-first pushes the concrete type to runtime, so a
wrong-behaviour bug requires tracing composition to find which `M` was actually passed. That cost
scales with implementers per interface. It argues for keeping the interface set *small and
semantic*.

## 7. The export set

F3 declares no domain type. It **elects** a subset of F1 to cross the boundary, in a manifest,
not a source file:

```
F3 EXPORT SET · PARCEL_CORE

  capabilities : IIdentified, IWeighed, IParcel
  events       : EvParcelDispatched, EvParcelDelivered
  commands     : CmdDispatchParcel, CmdMarkDelivered
  state        : EParcelState
  errors       : ERejectReason
  ------------------------------------------------------------
  everything else in PARCEL_CORE is internal, regardless of visibility
```

**Consequences.**

- Anything absent from the export set is unreachable at F4, even if `public`.
- Models are never listed. `Ev` and `Cmd` are the only data shapes that cross, because they have
  no behaviour to abstract.
- Every entry must be describable without a presentation noun. `EParcelState` passes.
  `EBannerColour` fails — that is F5 inventing state and back-porting it.
- Widening the export set is an F3 change and needs its own line in the changelog. It is never a
  side effect of an F4 task.

## 8. F4–F5 discipline

Views are not domain objects and the model does not pretend otherwise. The requirement is
single-responsibility naming, not object orthodoxy:

> Every F4–F5 artefact is a named type with one responsibility and no domain rule. It may hold
> interaction state; it may not hold authoritative state. Reuse between F4–F5 artefacts is by
> composition or by `P`, never by inheritance (`AR28`).

`P` is the load-bearing prefix. `EParcelState.Lost → red` lives in a `P`, never in CORE and never
inline in a `C`. Without `P`, colour-choosing leaks into components, then eventually backwards
into F2 "for convenience".

## 9. Definition of done — per task

A task `T##` is complete when, for every phase in its span:

- [ ] The exit condition in §4.1 holds
- [ ] Every new artefact carries a legal prefix for its phase (§5)
- [ ] No new domain type was declared at F3
- [ ] Any widening of the export set is listed in the changelog
- [ ] The residue of any new `M` was reviewed
- [ ] The phases outside the span are declared empty
- [ ] The whole thing fits in **one commit**

## 10. Lint

Four checks cover almost everything. Check 3 is the one that actually buys the architecture; the
rest are hygiene.

1. `S` / `U` / `Repo` implementation declared outside F2 → **error**
2. `I` / `Str` / `E` / `R` / `M` / `Ev` / `Cmd` / `X` declared outside F1 → **error**
3. An F4/F5 file references a CORE type absent from the export set → **error**
4. An F0–F3 file contains a presentation term (colour, px, icon, label, component name) →
   **error**

Additional, cheap:

5. A `U` whose name contains a domain noun → **warn**
6. An `I` with exactly one implementer and no declared future seam → **warn**
7. A combined `I` without a norm citing it → **error** (`AR13`)
8. A class inheriting another to reuse behaviour → **error** (`AR28`)

## 11. Health metrics

- **Mean residue per `M`** — unique members ÷ total members. Rising means an abstraction is
  being missed.
- **Interface reuse count** — an `I` with one implementer is premature or a naming exercise.
  Tolerable for a known upcoming seam; suspicious otherwise.
- **Export set size over time** — should grow in steps, not drift.
- **Tasks spanning more than three phases** — a task that spans F1–F6 is usually two tasks.
- **Reserved prefixes activated without their trigger** — ceremony.

## 12. Quick checklist

- [ ] Does this cite an F0 norm, or did I invent the rule while coding?
- [ ] Is the interface declared before the model that satisfies it?
- [ ] Is this `M`'s residue actually unique, or did I miss an existing `I`?
- [ ] Did I add anything to the export set, and did I say so?
- [ ] Could a presentation noun have slipped below F3?
- [ ] Is the colour or label choice inside a `P`?
- [ ] Does this fit in one commit?

## 13. Anti-patterns

- Publishing in all seven phases in every task because the table is numbered
- Declaring a domain type at F3 instead of electing one
- Exporting an `M` because it was already `public`
- A `U` that consults a service — that is an `S` in denial
- A `Str` with a method that needs external state
- Combined interfaces named by concatenating their parents
- An abstract base class at F2, bypassing the interface contract
- Interaction state at F5 that quietly becomes domain state
- `Mgr`, `Helper`, `Common` — a feeling, not a responsibility
- Activating a reserved prefix before its trigger fires

## 14. Edge cases, decided in advance

**`I` implemented by `S`.** Interface at F1, implementation at F2. Normal, and the reason `I`
sits at F1: F2 may depend on vocabulary without depending on other behaviour.

**A `U` that needs a domain noun.** `USorting` is fine. `UDeliveryCost` is an `S` in denial —
cost is a rule, and rules need a citable F0 norm.

**A `Str` that grows a method.** Legal if derivable from its own fields with no branching on a
business rule (`Rect.Area`, `Weight.IsHeavy`). Illegal the moment it consults anything external.
The test: *could this method be a `U` taking the struct as its argument?* If yes, either form is
fine. If it needs a service, the struct has become an `S`.

**`M` used at F4.** Reference yes, declaration no — and only if that `M` is in the export set,
which for models it should never be. This is the most common real violation: F4 imports an
internal model because it happens to be public, and the boundary quietly vanishes.

**`X` versus `ERejectReason`.** A rejection is an expected outcome a person caused and the
surface explains. An exception is a broken invariant nobody should be able to reach. Conflating
them turns bugs into interface messages.

**Reopening a task.** A commit that revisits `M01.D02.T04` is still that task, marked as a
reopening, with the phases it re-entered named explicitly.
