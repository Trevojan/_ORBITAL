# PLAN — the ladder

Every file here answers one question and has one owner. When two of them disagree, the rule
at the bottom of this page decides.

| File | Question it answers | Owner | When it changes |
|---|---|---|---|
| `PLANNING.md` | What shape does a plan take, and when may execution open? | PO | By amendment, appended and dated |
| `GRAND_PLAN.md` | What is the ideal system? | PO | On a change of premise, never per sprint |
| `MINIMUM_PLAN.md` | What is the shortest path to the MVP? | PO | Freezes when execution opens; changing it needs a new gate |
| `BACKLOG.md` | What exists, what is missing, what does it cost? | PO | Continuously |
| `LADDER_PLAN_M_<n>.md` | What is built in this module, and how? | PO, attested by DA/DV/QA | Until its gate is signed; never after |
| `CHECKLIST.md` | What has been done? | whoever closes a task | Every task |
| `TRACE.md` | Did anything get lost or invented? | PO | Whenever the backlog or a blueprint changes |
| `GATE_M<n>_<date>.md` | May the next module open? | PO, signed by DA/DV/QA | Once per opening, never edited afterwards |

## Reading order

1. `GRAND_PLAN.md` — the ceiling
2. `MINIMUM_PLAN.md` — the path
3. `LADDER_PLAN_M_<current>.md` — the module in flight
4. `CHECKLIST.md` — where the work stands
5. `BACKLOG.md` — what is waiting

`PLANNING.md` is read when writing any of them. `TRACE.md` is read when something feels
missing.

## Precedence

- **`BACKLOG.md` rules on fact** — what exists, what is missing, what it costs.
- **`GRAND` / `MINIMUM` / `LADDER` rule on decision** — what gets built, in what order,
  under what criterion.
- **Business rules live in neither.** They live in `DECISIONS/BUSINESS_RULES.md`, and when
  a plan appears to state one, the plan is quoting, not deciding.

## Gates issued

| Date | Module | Type | Verdict |
|---|---|---|---|
| | | | |
