# LADDER_PLAN_M_1 — `[TBD: module name]`

**Status:** `[TBD]` · **Owner:** PO · **Attested by:** DA, DV, QA · **Filled by:**
`PROTOCOL.md` S5–S7

This is where the effort lives. `GRAND_PLAN` says what the ideal is, `MINIMUM_PLAN` says what
the shortest path is, and this file says what actually gets typed.

Copy this file for each module: `LADDER_PLAN_M_2.md`, `M_3`, and so on. **Never overwrite one
that has been frozen** — a frozen module is the record of what was decided and when.

> **Freezing.** This file is a blueprint until its gate is signed `SOLUTION: yes`. Before
> that, it may be corrected by whatever the earlier modules taught. After that, rule 3 of
> `PLANNING.md` applies in full.
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
  `[TBD]`; accepted debt: `[TBD: DC#]`
- **Verifiability (QA)** — new cases: `[TBD]`; inherited cases that must keep passing:
  `[TBD]`; deliberately uncovered: `[TBD]`

---

## Phases and tasks

Seven phases, always in this order (`AR20`). **A phase with nothing in it declares itself
empty** — omitting it hides a decision, declaring it records one.

Each task is one commit and satisfies all seven conditions of the definition of done
(`PROTOCOL.md` S7). What is written now: the file path, the function signature, the norm, the
test name and the commit subject. What is not written now: the function bodies.

### F0 — Norm

*Publishes the citable rule the rest of the module obeys.*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `M1.F0.T01` | `[TBD]` | `[TBD]` | — | `[TBD]` | — | `[TBD]` |

### F1 — Contract

*Types, signatures, schema. Steps 1 and 2 of the type table (`AR12`).*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `M1.F1.T01` | `[TBD]` | `[TBD]` | `[TBD]` | `AR10` | `[TBD]` | `[TBD]` |

### F2 — Rule

*Behaviour proven by a `_PROBE`. Steps 4 and 5 of the type table.*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `M1.F2.T01` | `[TBD]` | `[TBD]` | `[TBD]` | `[TBD: BR#]` | `[TBD]` | `[TBD]` |

### F3 — Exposure

*The outward surface: endpoints, composition, authorization.*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| | | | | | | |

### F4 — Consumption

*The typed client that calls it, on the consumer side.*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| | | | | | | |

### F5 — Surface

*What a person touches: screen, command, report, export.*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| | | | | | | |

### F6 — Closure

*Changelog, documents, backlog move, gate. The next module opens.*

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `M1.F6.T01` | `origin: norm` | `SOURCES/CHANGELOG.md` | — | `PLANNING §9` | — | `[TBD]` |

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

What must be **true** — not "done", true and checkable — before `M2` may open.

1. `[TBD]`
2. The build produces no new warnings and the probes are green, project by project.
3. `TRACE.md` shows no item regressed.
4. QA's version verdict is issued.

Once all of them hold, run the closing cycle in `PLANNING.md` rule 9 and open the next gate.
