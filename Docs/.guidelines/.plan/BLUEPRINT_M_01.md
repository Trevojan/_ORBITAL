# BLUEPRINT_M_01 — `[TBD: module name]`

**Status:** `[TBD]` · **Owner:** PO · **Attested by:** DA, DV, QA · **Filled by:**
`PROTOCOL.md` S5–S7

This is where the effort lives. `GRAND_PLAN` says what the ideal is, `MINIMUM_PLAN` says what
the shortest path is, and this file says what actually gets typed.

Copy this file for each module: `BLUEPRINT_M_02.md`, `M_03`, and so on. **Never overwrite one
that has been frozen** — a frozen module is the record of what was decided and when.

> **Freezing.** This file is a blueprint until its gate is signed `SOLUTION: yes`. Before
> that, it may be corrected by whatever the earlier modules taught. The commit that signs the
> gate renames it `LADDER_PLAN_M_01.md`, and from then on rule 3 of `PLANNING.md` applies in
> full (amendment A1).
>
> **All modules are planned to this depth before execution opens** — the whole blueprint
> before the first wall. What waits for its moment is the content: the bodies of the
> functions.

---

## Why this module now

`[TBD]` — the business rule that forces it, and the **cost of not climbing**. The second half
is the one that gets skipped, and it is the half that justifies the order.

## Functions affected

| Function | Previous module | This module | Next module |
|---|---|---|---|
| | | | |

## Scope

**In:**

- `[TBD]`

**Out:**

- `[TBD]` — and where it returns

## Attestations

Three lines, three owners. None of them is optional, and a qualified attestation is not a
signature.

- **Data (DA)** — available: `[TBD]`; new: `[TBD]`; backfill: `[TBD]`; read compatibility:
  `[TBD]`
- **Effort (DV)** — starting pattern: `[TBD]`; effort: `[TBD]`; risk: `[TBD]`; rollback:
  `[TBD]`; accepted debt: `[TBD: ADR-###]`
- **Verifiability (QA)** — new cases: `[TBD]`; inherited cases that must keep passing:
  `[TBD]`; deliberately uncovered: `[TBD]`

---

## Steps and tasks

A module climbs in **steps** (`D##`): each is a functionality the module climbs at once, and it
opens only when the previous step has closed (`AR21`). A step holds **tasks** (`T##`), and every
task runs the seven phases as its protocol (`AR20`). **A phase with nothing to publish in a task
declares itself empty there** — omitting it hides a decision, declaring it records one.

Each task is one commit and satisfies all seven conditions of the definition of done
(`PROTOCOL.md` S7). What is written now: the file path, the function signature, the norm, the
test name and the commit subject. What is not written now: the function bodies.

### `M01.D01` — `[TBD: step name]`

*The functionality this step climbs at once, and why it comes first.*

#### `M01.D01.T01` — `[TBD: one imperative sentence, no "and"]`

| Covers | Span | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `[TBD: B#]` | `[TBD: F1–F2]` | `[TBD]` | `[TBD]` | `AR10` | `[TBD]` | `[TBD]` |

| Phase | Publishes in this task |
|---|---|
| F0 Norm | `[TBD]` — or *empty*: the citable rule the rest obeys |
| F1 Contract | `[TBD]` — types, signatures, schema; steps 1 and 2 of the type table (`AR12`) |
| F2 Rule | `[TBD]` — behaviour proven by a `_PROBE`; steps 4 and 5 of the type table |
| F3 Exposure | *empty* — the outward surface and the export set |
| F4 Consumption | *empty* — the typed client, its presenters and view models |
| F5 Surface | *empty* — what a person touches |
| F6 Closure | `[TBD]` — the changelog line; in the module's last task, the gate |

### `M01.D02` — `[TBD: step name]`

*Opens when `M01.D01` has closed.*

#### `M01.D02.T01` — `[TBD]`

| Covers | Span | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `[TBD: B#]` | `[TBD]` | `[TBD]` | `[TBD]` | `[TBD: BR#]` | `[TBD]` | `[TBD]` |

| Phase | Publishes in this task |
|---|---|
| F0 Norm | |
| F1 Contract | |
| F2 Rule | |
| F3 Exposure | |
| F4 Consumption | |
| F5 Surface | |
| F6 Closure | |

---

## POC required

Where the blueprint is necessarily less firm, it says so here rather than faking precision.

| Unproven risk | Question the POC answers | Discardable? |
|---|---|---|
| | | |

> A POC lives on a branch that never reaches the main line, and the only artefact that
> survives is a document. If something in the code must survive, it was a module wearing the
> wrong label.

## Completion criterion

What must be **true** — not "done", true and checkable — before `M02` may open.

1. `[TBD]`
2. The build produces no new warnings and the probes are green, project by project.
3. `TRACE.md` shows no item regressed.
4. QA's version verdict is issued.

Once all of them hold, run the closing cycle in `PLANNING.md` rule 9 and open the next gate.
