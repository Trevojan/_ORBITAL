# PLANNING — templates, the gate, and the rules that govern them

Owner: **PO**. Attesters: **DA** (data), **DV** (effort), **QA** (verifiability).

This file holds the templates for every planning document and **the single statement of the
gate formula**. Everywhere else links here. That is deliberate: a formula restated in three
places is a formula that will disagree with itself, and it will do so quietly.

---

## The gate formula

<a id="gate-formula"></a>

```
((MINIMUM < GRAND) & (MINIMUM <= (Σ LADDER_M_1..N_mvp + POC))) = SOLUTION
```

`N_mvp` is declared in `MINIMUM_PLAN.md`, section *Reach of the MVP*, and is the number of
modules the MVP spans.

Three readings, all mandatory:

| Condition | Means | If it fails |
|---|---|---|
| `MINIMUM < GRAND` | the minimum is **strictly** smaller than the ideal | You did not slice. You are building the ideal system and calling it an MVP. Cut scope from `MINIMUM`. |
| `MINIMUM <= Σ LADDER_M_1..N_mvp + POC` | every factor of the minimum is covered by a written module or by a delimited proof | Either `MINIMUM` is inflated, or the modules are under-specified. Adjust one until it closes. |
| both true | there is a **SOLUTION**: a plan that is buildable and defensible | Without both there is no plan, only intention. **Execution does not open.** |

The `POC` term is what the modules do not yet prove: technical risk, availability of data,
viability of an integration. It is **discardable by construction** — if a proof needs to
survive into production, it was a module wearing the wrong label.

No single role signs this. The PO owns the sheet; DA attests what data is reachable, DV what
effort fits, QA what can be proven. None of the three is optional.

---

## `GRAND_PLAN.md` — the ceiling of coherence

At most **120 lines**. Revised by a change of premise, never by a sprint.

```markdown
# GRAND_PLAN — <product>

## Client problem
<one sentence: what hurts, for whom>

## Ideal system
<the destination, with no schedule>

## Domain
| Entity | Role in the system | Target module |
|---|---|---|

## Mature business rules
- <rule> — target module: <M#>

## Integrations and target scale
<external systems, volume, concurrency, response time>

## Non-functional requirements that may never regress   <!-- QA -->
- <requirement> — proof: <how it is demonstrated>

## Mature data model   <!-- DA -->
<entities, target granularity, history, semantic layer>

## Target architecture   <!-- DV -->
<destination scale and the patterns that match it>

## Definition of a top-line deliverable
<the complete functional scope, plus only those non-functional conventions that
 make the core faster to build rather than merely safer>

## Outside the ideal
<what this product will never do>
```

---

## `MINIMUM_PLAN.md` — the path to the MVP

At most **90 lines**. Freezes when execution opens; changing it afterwards requires a new
gate sheet.

```markdown
# MINIMUM_PLAN — path to the MVP

## Definition of MVP
<the problem solved end to end, for one user, common cases only>
<if there is more than one milestone, name them and say what each does NOT do>

## Common use cases included
1. <actor> <action> <result>

## Factor sequence (ordered by dependency)
| # | Factor | Depends on | Target module |
|---|---|---|---|

## Reach of the MVP
`N_mvp = <n>` — the number of modules the MVP spans. Consumed by the gate formula.

## Nuclear POC
| Field | Content |
|---|---|
| Unproven risk | |
| Closed question it answers | |
| How it is answered | |
| Discard criterion | |
| What changes if the answer is no | |

## Outside the MVP (deliberately)
- <item> — reason, and the module where it returns

## Deferred exceptions
<recognised exception rules, postponed past the MVP>

## Risks accepted without proof
<what is being built on an unverified premise, and when it gets verified>
```

---

## `LADDER_PLAN_M_X.md` — where the effort lives

One file per module. Never overwritten once frozen.

```markdown
# LADDER_PLAN_M_<X> — <module name>

## Why this module now
<the business rule that forces it; the cost of not climbing>

## Functions affected
| Function | Previous module | This module | Next module |
|---|---|---|---|

## Scope
**In:** <items>
**Out:** <items>

## Attestations
- **Data (DA)** — available: …; new: …; backfill: …; read compatibility: …
- **Effort (DV)** — starting pattern: …; effort: …; risk: …; rollback: …; accepted debt: <ADR-###>
- **Verifiability (QA)** — new cases: …; inherited cases that must keep passing: …;
  deliberately uncovered: …

## Phases and tasks

### F0 — Norm
| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `M<X>.F0.T01` | | | | | | |

### F1 — Contract
### F2 — Rule
### F3 — Exposure
### F4 — Consumption
### F5 — Surface
### F6 — Closure

<a phase with nothing in it says so here; it is never omitted>

## POC required
| Unproven risk | Question the POC answers | Discardable? |
|---|---|---|

## Completion criterion
<what must be true before M<X+1> may open>
```

---

## Gate sheet

Two kinds. Both live in `.plan/GATE_M<X>_<date>.md`.

```markdown
# GATE — <date> — opening M<X>
`type: triangulation | module`

| Condition | Check | Result |
|---|---|---|
| MINIMUM < GRAND | the minimum is strictly smaller in scope than the ideal | ✅ / ❌ |
| MINIMUM <= Σ LADDER + POC | every factor is covered by a written module or a delimited POC | ✅ / ❌ |

<for type: module, add>
| Previous completion criterion | every line of M<X-1> §Completion criterion verified | ✅ / ❌ |
| QA version verdict | issued: correction / compatible addition / break | ✅ / ❌ |
| No trace regression | no TRACE.md item moved backwards | ✅ / ❌ |

Attestations attached: DA ☐  DV ☐  QA ☐

**SOLUTION:** yes / no

If ❌ on any line, execution does not open. Corrective action:
- MINIMUM inflated → cut factor <…> to module <…>
- Module under-specified → absorb <…>
- Negative attestation → the module changes, or the POC becomes about <…>
```

**`type: triangulation`** is issued once, opening `M1`, with the two formula conditions and
the three attestations.
**`type: module`** is issued at every later opening, adding the three extra lines.

---

## Rules

1. `GRAND_PLAN.md` is revised by a change of premise, not by a sprint.
2. `MINIMUM_PLAN.md` freezes when execution opens. Changing it afterwards requires a new
   gate sheet.
3. `LADDER_PLAN_M_X.md` is **never overwritten** — one file per module preserves the record
   of what was decided and when.
4. No module opens without the previous module's completion criterion satisfied and QA's
   version verdict issued.
5. **The effort lives in the ladder.** `GRAND_PLAN` ≤ 120 lines, `MINIMUM_PLAN` ≤ 90.
   Content over the ceiling belongs to a module and moves there. The ceiling is numeric
   because an adjective does not survive haste.
6. **The whole blueprint is written before the first wall.** Every module is planned to the
   same depth before execution opens: file, function, norm and test for each task. Rule 4
   governs the *opening* of a module, not the existence of its blueprint. What waits for
   its moment is the content — the bodies of the functions.
7. **A module freezes only when its gate is signed `SOLUTION: yes`.** Before that it is a
   blueprint, and a blueprint is corrected by whatever the earlier modules taught. This is
   what allows a plan that never opened to be discarded without violating rule 3.
8. **Two kinds of POC.** *Nuclear* lives in `MINIMUM_PLAN` and answers whether the MVP is
   viable at all; it is the term the gate formula sums. *Module* lives in a
   `LADDER_PLAN_M_X` and answers whether that module is viable. In both cases the POC lives
   on a branch that never reaches the main line, and the only artefact that survives is a
   document.
9. **Module closing cycle** — all in one commit, in this order: verify every line of the
   completion criterion; mark the module done in `CHECKLIST.md`; move the covered items in
   `BACKLOG.md`, preserving their `B#`; update `TRACE.md`; issue the next gate sheet; revise
   the next module's blueprint with what was learned; and only then freeze the module that
   closed.
10. **A module in execution does not change scope.** A request arriving mid-module becomes a
    new `B#` in the backlog and is decided at the next opening. Without this, "the effort
    lives in the ladder" becomes "the ladder absorbs whatever appears".

---

## Amendments

> Append-only, dated. The rules above are not rewritten; what practice demands is added
> here. An amendment that contradicts a rule above **MUST** say so on its own line.

*(empty at opening)*
