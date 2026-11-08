# Plan — what comes next

> Only what is **open**. What closed leaves this file and becomes a line in
> [`.changelog/`](../.changelog/README.md). A plan that keeps what was already done stops being a plan
> and becomes a report.

The template's own work is in [`ROADMAP.md`](ROADMAP.md). The rest of this folder is the planning
machinery every project fills in — [below](#the-plan-files-a-project-fills).

## Authorised by the Regent, not yet built

### 1. Roadmap module 3 — the gates, as `_PROBE` tests

The arrow, the recognisers for the retired forms, the link gate, LF for documents, bUnit.

The material already exists: AutoRpg's arrow probe and recognisers, Glyph's link gate; the
invariants `AR28` and `AR29` to check. Missing: where the guideline-level gates live — each app
is deletable, so a repository-level probe is a new project (`AR9`).

### 2. Roadmap modules 4 to 6

The adoption path, the template as a source, the expansion candidates — [`ROADMAP.md`](ROADMAP.md) §3.

## Open, waiting for the Regent

| | |
|---|---|
| AR30 | services accept `I`, never `M` — the five apps' services all take their own model; held until the Regent decides how the norm reads |
| D10 | what a clone inherits and what it cites — blocks module 5 |

Each is stated in [`ROADMAP.md`](ROADMAP.md) §2.

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
| `BLUEPRINT_M_##.md` → `LADDER_PLAN_M_##.md` | What is built in this module, and how? | PO, attested by DA/DV/QA | a blueprint until its gate is signed; then renamed, and never after |
| [`CHECKLIST.md`](CHECKLIST.md) | What has been done? | whoever closes a task | every task |
| [`TRACE.md`](TRACE.md) | Did anything get lost or invented? | PO | whenever the backlog or a blueprint changes |
| `GATE_M##_<date>.md` | May the next module open? | PO, signed by DA/DV/QA | once per opening, never edited afterwards |
| [`PHASES.md`](PHASES.md) | At what depth does an artefact live, and what may it be called? | DV | by amendment |

The exemplar is [`BLUEPRINT_M_01.md`](BLUEPRINT_M_01.md).

### Reading order

1. `GRAND_PLAN.md` — the ceiling
2. `MINIMUM_PLAN.md` — the path
3. the current module's blueprint or ladder — the module in flight
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
