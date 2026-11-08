# BUSINESS_RULES — the `BR#` register

**Status:** `[TBD]` · **Owner:** PO, written with the client · **Filled by:** each module's
phase F0.

Every rule the domain requires, numbered so a task can cite it. A rule that is not here is
not a rule — it is somebody's habit, and the code will disagree with it eventually.

> **A business rule lives in a service, never in an exposure layer, never in a model, never
> in a screen** (`AR17`, `AR18`). This file records *what* the rule is; the service is *where*
> it is.

## Where rules must not live

- Not in a controller, endpoint or command handler — that is exposure, not decision.
- Not in a model — a model holds state, not policy.
- Not in a screen — a screen may validate input for the typist's benefit, and the service
  validates it for the system's. Those are two different checks and both exist.
- Not in two places at once. Two implementations of one rule will disagree, and the disagreement
  will be discovered by whoever is harmed by it.

---

## Domain

`[TBD]` — one paragraph: what this system decides, and for whom.

## Calculated values

Repeat per value. A calculated value is one the system derives rather than stores as given.

### BR1 — `<name>`

| Field | Content |
|---|---|
| Describes | `[TBD]` |
| Formula | `[TBD]` |
| Return type | `[TBD]` |
| When it is absent | `[TBD]` — and what absent means, which is rarely zero |
| Edge cases | `[TBD]` |
| Proven by | `[TBD]` — test name |

## Mandatory filters

Constraints that apply to every read of a thing, not just to the obvious one.

### BR2 — `<name>`

`[TBD]`

> An empty period **SHOULD** return an empty result, not an error. Nothing found and something
> broken are different answers, and conflating them makes both harder to diagnose.

## Input validation

The message text is part of the contract: it is reused by the surface and by the service, so
that the same refusal reads the same way wherever it appears.

| Field | Required | Rule | Message |
|---|---|---|---|
| | | | |

## Visibility

Who may see what. Omission is the **service's** responsibility: the shape leaves the service
already lacking the field, rather than the surface filtering it afterwards. A surface that
filters is one forgotten line away from disclosing.

| Actor | Sees | Does not see |
|---|---|---|
| | | |

## State transitions

The legal moves, as a table rather than as prose. Prose grows exceptions; a table shows the
gap.

```
<state> → <state>          <who may> <under what condition>
```

## Representative test data

A small, controlled set that the engine's tests are built on. Worked by hand, so that a
failing test can be checked against arithmetic somebody did on paper.

| # | Inputs | Expected | Scenario |
|---|---|---|---|
| | | | |

## Rules with a change history

When a formula changes, the previous one is recorded. Not for nostalgia: results produced
under the old rule still exist, and somebody will ask why they do not reproduce.

| Rule | Version | Date | Formula | Reason for the change |
|---|---|---|---|---|
| | | | | |

## How a rule changes

1. Change it **here** first.
2. Update the test — it **MUST** go red before any service is touched.
3. Make the service green.
4. Record the observable difference in `CHANGELOG.md`.
5. If the decision was not obvious, write the `ADR-###`.

Documents before code, always. A rule changed in code first is a rule nobody agreed to.
