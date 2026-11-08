---
role: TEAM
full_name: Common Standard of the Roles
type: structural_standard
related: [PO.md, DV.md, QA.md, DA.md]
---

# TEAM — the common standard

> Four roles, one format. What exists in one exists in the others, translated into that
> role's lens.

## 1. Why this file exists

Roles drift. A useful capability appears in one file and is missing from the other three,
and nobody notices because each file reads fine on its own.

This document fixes the **canonical template**, the **complete contract matrix** and the
**planning cycle**, so that each role describes only its own version of each section — never
inventing a section, never omitting one.

**Maintenance rule:** a structural change enters here first, then the four files. A role
with a different section count from the others is out of date by definition.

## 2. Canonical template — 18 sections

| # | Section | Answers |
|---|---|---|
| 1 | Identity and Mission | Why this role exists |
| 2 | Scope | What is mine and what is not |
| 3 | Core Responsibilities | What I do every day |
| 4 | Key Competencies | What I must know how to do |
| 5 | Workflow | In what order I work |
| 6 | Input and Output Contracts | What I receive and deliver, under what condition |
| 7 | Deliverables and Where They Live | Which artefact, at which path, versioned how |
| 8 | Role in the Plans | What I write in GRAND, MINIMUM and LADDER |
| 9 | Maturity Ladder | How I climb without rewriting the step below |
| 10 | Definition of Done | When I may say I finished |
| 11 | Handoff Triggers | What forces me to pass the ball, and to whom |
| 12 | Autonomy and Escalation | What I decide, consult on, or escalate |
| 13 | Interaction with Other Roles | How I relate to each one |
| 14 | Health Metrics | How I know the role is working |
| 15 | Quick Checklist | What I check before releasing |
| 16 | Anti-Patterns | What makes me fail predictably |
| 17 | Questions I Must Ask | What I ask before acting |
| 18 | Method Discipline | Which external discipline governs my rigour |

### Section 18 — why the discipline differs

The section exists in all four files; its content splits by the nature of the problem the
role faces.

| Roles | Discipline | Why |
|---|---|---|
| PO, DA | **Creative thinking** | They work on an **ill-defined** problem: the formulation *is* most of the work, and the first formulation is always the inherited one. They need techniques that generate options and break framing before converging. |
| DV, QA | **Object Calisthenics** | They work on a **defined** artefact: the problem is structural. They need rules that force decomposition and verifiability without depending on individual taste. |

The boundary is not rigid about *learning* — a developer benefits from inversion when
hunting a bug, a product owner benefits from unabbreviated names. It is rigid about
**obligation**: each role answers for its own discipline in its own artefact.

## 3. Contract matrix

Twelve edges. Each role delivers to each of the other three and receives from each of them.
No cell is empty.

| From → To | Artefact | Accepted only if… |
|---|---|---|
| PO → DV | Prioritised story | problem, acceptance criteria, out-of-scope and target module all declared |
| PO → QA | Acceptance criteria + criticality | objectively verifiable, with the maximum tolerable severity stated |
| PO → DA | Business question + success metric | has an associated decision and a date by which it is read |
| DV → PO | Estimate + risks + the cheaper alternative | premises explicit, maintenance cost declared |
| DV → QA | Testable build + notes on what changed | passes CI, deployed somewhere testable, rollback path named |
| DV → DA | Instrumented event or table | emits per the agreed schema, backfill defined |
| DA → PO | Analysis + data viability | conclusion first, limitation declared, method reproducible |
| DA → DV | Event specification + data contract | name, trigger, payload, types, optionality, key and granularity |
| DA → QA | Canonical metric definition + oracle | formula recorded, and an independent query QA can run alone |
| QA → PO | Risk opinion + version verdict | open bugs by severity, coverage gaps, type of increment |
| QA → DV | Bug report | reproduction steps, expected vs. observed, environment, evidence |
| QA → DA | Inconsistent-data signal | with a concrete example of a diverging record |

## 4. Planning cycle

Three living documents, always present: `.plan/GRAND_PLAN.md` (the ideal, the ceiling of
coherence), `.plan/MINIMUM_PLAN.md` (the shortest path to the MVP) and
`.plan/LADDER_PLAN_M_X.md` (one file per module).

Execution opens through the gate. **The gate formula is defined in
[`.plan/PLANNING.md`](../.plan/PLANNING.md#gate-formula) and nowhere else** — including here.
Restating it is how it drifts.

Responsibility: the **PO** owns the three files and the gate. The **DA** attests the data
line — what is reachable today. The **DV** attests the effort line — what fits in the
module. The **QA** attests the verifiability line — what can be proven to work. None of the
three attests alone; none is dispensable.

## 5. Maturity ladder — the shared heuristic

Every requirement has a scale of sophistication. The task is not to reach the top. It is to
choose **the most efficient step for the current business rules and the current state of the
project**, leaving the next step possible without demolition.

Canonical example — authentication:

```
identify by username → username + password → roles and permissions
  → protected internal network → SSO / external provider → auditing and managed sessions
```

Each role has its analogous ladder in its own section 9. The rules are the same for all:

1. Name the step you are on, and the next one.
2. Climb one step at a time, and only when a business rule asks.
3. A step that requires redoing the previous one was chosen wrong — back up and split.
4. Skipping a step is allowed only when the intermediate one is demonstrably disposable, and
   that goes in writing.

## 6. Fixed vocabulary

- **Module** — a coherent, deliverable, reversible increment; one step of the ladder.
- **Task** — one commit. Difficulty `D1` by definition.
- **Norm** — a citable rule: an `ADR-###`, `AR#` or `BR#`.
- **POC** — a disposable proof of viability; it never becomes production.
- **Risk opinion** — QA's written statement of what was not covered.
- **Version verdict** — QA's classification of an increment: correction, compatible
  addition, or break.
- **Oracle** — an independent path to confirming a result.
- **Data contract** — the schema agreed between DA and DV, with key, granularity and
  optionality.
- **Out of scope** — what was deliberately excluded, written down in the artefact.
