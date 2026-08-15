# TRACE — nothing is lost, nothing appears from nowhere

**Derived, never authored.** Regenerated whenever the backlog or a module blueprint changes.

The trace is the only thing standing between a plan and two failure modes that are invisible
from inside it: a backlog item quietly dropped, and work quietly invented. It **MUST** close
in **both** directions.

## Forward — every backlog item has a fate

One line per item in `BACKLOG.md`. Exactly one of four states. **No empty cell.**

| State | Means | Must also record |
|---|---|---|
| `covered` | a task builds it | the task id |
| `deferred` | it is planned, later | the target module |
| `off-ladder` | it never enters the ladder | the reason |
| `dropped` | it is abandoned | the reason |

**Closure check:** the number of lines here equals the number of items in `BACKLOG.md`.

| `B#` | Item | State | Task / module / reason |
|---|---|---|---|
| | | | |

## Backward — every task has an origin

Every task in every module declares where it came from. Three legal origins:

| Origin | Means |
|---|---|
| `B<n>` | it builds a backlog item |
| `origin: GRAND` | it comes from the ideal, and the backlog never recorded it |
| `origin: norm` | it exists because a `DC#`, `AR#` or `BR#` requires it |

**Closure check:** no task without one of the three.

This direction is the one that earns its keep. It catches work promised in prose — a
paragraph describing something that should exist — which never became a backlog item and so
would otherwise be built without anyone deciding to build it, or not built without anyone
noticing.

| Task | Origin | Note |
|---|---|---|
| | | |

---

## Regression

An item **MUST NOT** move backwards between states without a recorded reason. `covered`
returning to `deferred` means a module lost scope, and a module in execution does not lose
scope (`PLANNING.md` rule 10). The module gate checks this before opening the next one.
