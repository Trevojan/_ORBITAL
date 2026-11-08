# Plan — what comes next

> Only what is **open**. What closed leaves this file and becomes a line in
> [`.changelog/`](../.changelog/README.md). A plan that keeps what was already done stops being a plan
> and becomes a report.

The template's own work is in [`ROADMAP.md`](ROADMAP.md). The rest of this folder is the planning
machinery every project fills in — [below](#the-plan-files-a-project-fills).

## Authorised by the Regent, not yet built

### 1. The ladder as `M##.D##.T##` — `ADR-008`

Module, step, task, with `F0`–`F6` inside every task, and an unsigned ladder kept as a blueprint until
its gate is signed.

The material already exists: the decision, and the wording AutoRpg and instituto-conforme run on.
Missing: `AR20`, `AR21`, `PLANNING.md`, the exemplar ladder and `PROTOCOL` S6–S7 restated —
[roadmap](ROADMAP.md) module 2.

## Proposed, waiting for the Regent

[Roadmap](ROADMAP.md) modules 2 to 6 and their order. The register lists them as an agent's proposal.

## Open, waiting for the Regent

| | |
|---|---|
| D3 | the `R` prefix — blocks module 2, task 6 |
| D6 | the language rule — blocks module 2 |
| D8 | the class of each new norm — blocks module 2, tasks 3 to 5 |
| D10 | what a clone inherits and what it cites — blocks module 5 |
| D11 | what `F3`–`F5` publish — blocks module 2, task 7 |

Each is stated with its recommendation in [`ROADMAP.md`](ROADMAP.md) §2.

## Put off by decision, not by oversight

Nothing yet.

---

## The plan files a project fills

Every file answers one question and has one owner. When two of them disagree, the precedence below
decides.

| file | question it answers | owner | when it changes |
|---|---|---|---|
| [`PLANNING.md`](PLANNING.md) | What shape does a plan take, and when may execution open? | PO | by amendment, appended and dated |
| [`GRAND_PLAN.md`](GRAND_PLAN.md) | What is the ideal system? | PO | on a change of premise, never per sprint |
| [`MINIMUM_PLAN.md`](MINIMUM_PLAN.md) | What is the shortest path to the MVP? | PO | freezes when execution opens; changing it needs a new gate |
| [`BACKLOG.md`](BACKLOG.md) | What exists, what is missing, what does it cost? | PO | continuously |
| `LADDER_PLAN_M_<n>.md` | What is built in this module, and how? | PO, attested by DA/DV/QA | until its gate is signed; never after |
| [`CHECKLIST.md`](CHECKLIST.md) | What has been done? | whoever closes a task | every task |
| [`TRACE.md`](TRACE.md) | Did anything get lost or invented? | PO | whenever the backlog or a blueprint changes |
| `GATE_M<n>_<date>.md` | May the next module open? | PO, signed by DA/DV/QA | once per opening, never edited afterwards |
| [`PHASES.md`](PHASES.md) | At what depth does an artefact live, and what may it be called? | DV | by amendment |

The exemplar ladder is [`LADDER_PLAN_M_1.md`](LADDER_PLAN_M_1.md).

### Reading order

1. `GRAND_PLAN.md` — the ceiling
2. `MINIMUM_PLAN.md` — the path
3. `LADDER_PLAN_M_<current>.md` — the module in flight
4. `CHECKLIST.md` — where the work stands
5. `BACKLOG.md` — what is waiting

`PLANNING.md` is read when writing any of them. `TRACE.md` is read when something feels missing.

### Precedence

- **`BACKLOG.md` rules on fact** — what exists, what is missing, what it costs.
- **`GRAND` / `MINIMUM` / `LADDER` rule on decision** — what gets built, in what order, under what
  criterion.
- **Business rules live in neither.** They live in
  [`BUSINESS_RULES.md`](../.decisions/BUSINESS_RULES.md), and when a plan appears to state one, the
  plan is quoting, not deciding.

### Gates issued

| date | module | type | verdict |
|---|---|---|---|
| | | | |
