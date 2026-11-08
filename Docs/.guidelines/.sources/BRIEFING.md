# BRIEFING — the problem situation

**Status:** `empty`  ·  **Completed on:** —  ·  **Interviewed by:** —

> Status values: `empty` → `in interview` → `complete`. Nothing downstream is derived while
> this reads anything other than `complete`. See `../PROTOCOL.md` S0.

This is the **root of the tree**. Everything else in this repository is derived from it, so
a guess made here becomes a wrong module six steps later — and by then it will no longer
look like a guess.

Fill in what is known. The rest is interviewed, in field order, before anything is derived.
A field that genuinely cannot be answered is marked, never invented:

- `[TBD — owner: <name>]` when someone can answer it later.
- Moved to field 11 when nobody can answer it yet and it needs a proof.

## What each field is for

Answer quality matters in proportion to what depends on it.

| # | Field | Consumed by | If it is weak |
|---|---|---|---|
| 1 | Problem | S1 — opens the grand plan | Everything built solves the wrong thing, coherently |
| 2 | Actors and roles | S1, S2 — domain and authorization | Permissions get retrofitted, which is the expensive way |
| 3 | How it is solved today | S1, S3 — what is being replaced | The replacement misses the habit people actually rely on |
| 4 | What "solved" looks like | S4 — **this is the MVP criterion** | The MVP has no finish line and grows until it stops |
| 5 | Deliverable shape | S2 — which apps and domains exist | The solution shape is chosen by reflex instead of need |
| 6 | Hard constraints | S1, S2 — target architecture | A constraint discovered late invalidates finished modules |
| 7 | Integrations | S2, S4 — `_DRIVE` projects and POCs | An external system turns out to be unavailable mid-ladder |
| 8 | Scale | S1 — target architecture | Over-engineering, which is expensive and looks like diligence |
| 9 | Non-negotiables | S1, S5 — anchored to F0 or F5 | Something regresses and nobody notices until a user does |
| 10 | Out of scope | S1 — the ceiling's negative space | Scope creep has no wall to hit |
| 11 | Unknowns | S4, S6 — the POCs | A risk is discovered after it is expensive |

---

## 1. Problem

One sentence: **what hurts, and for whom.** Not the solution, not the feature — the pain.

*Shape of a good answer:* `<who> cannot <do what>, because <obstacle>, and it costs <what>.`
A good answer would still be true if the software were never built.

>

## 2. Actors and roles

Who touches this, and what each one is allowed to do. Distinguish two axes that are often
confused: **what someone is** (their relationship to the organization) and **what someone
may do** (their permissions). They are rarely derivable from each other.

Also separate **who asked for this** from **who suffers the problem**. They are frequently
different people, and when they are, the second one is the user. Building for the requester
is the quietest way to deliver something nobody uses.

>

## 3. How it is solved today

The thing being replaced — a spreadsheet, a process, a person's memory, nothing at all.
Include the parts that **work**, because those are the ones people will miss.

Then say whether the current way is **replaced or supplemented**. A fallback that survives
alongside the new system is scope: it has to keep working, and something has to reconcile
the two.

>

## 4. What "solved" looks like

The observable outcome that ends the problem in field 1. This becomes the MVP's finish
line, so it **MUST** be checkable by someone other than its author.

*Shape of a good answer:* `<actor> can <do the thing> without <the old workaround>.`

**And how would you check it?** Name the observation that settles it — a number, a duration,
a thing someone stops doing. Without this, the finish line is an adjective, and an adjective
is argued about rather than reached.

>

## 5. Deliverable shape

Desktop, API, web, worker, pipeline, command line, or a mix. If unknown, say so — field 6
often decides it.

>

## 6. Hard constraints

Stack, deadline, budget, regulation, systems already in place, hosting that is already paid
for, languages the team actually knows. Anything that is not negotiable and not chosen by
us.

Two constraints that are almost never volunteered and change the architecture more than the
rest of this field combined — ask both explicitly:

- **Availability.** Must it work when the network, the server or the power is down? "It has
  to work in the basement" and "it can assume a connection" are different products, and the
  difference is not retrofittable.
- **Data lifespan.** How long must records be kept, and what happens to them afterwards —
  deleted, archived, anonymised? This decides whether history is a feature or a liability.

>

## 7. Integrations

External systems, and **which direction data flows** in each. "We integrate with X" is not
an answer; "we push invoices to X and read payment status back" is.

>

## 8. Scale

Users, records, concurrency, growth. Rough numbers beat adjectives — "about forty people,
a few hundred records a month" is worth more than "small". This field exists mainly to
prevent over-engineering.

>

## 9. Non-negotiables that may never regress

The properties that are worse than a missing feature if they break — correctness of a
number, privacy of a record, an audit trail. Each **SHOULD** be paired with how it would be
proven, because a non-negotiable with no proof is a hope.

>

## 10. Explicitly out of scope

What this product will **never** do. Writing it down is what gives scope creep a wall to
hit. Includes the adjacent things people will reasonably assume are included.

>

## 11. Unknowns

What nobody can answer yet. Each becomes a candidate POC in S4 or S6, with the closed
question it must answer and the criterion for discarding it.

| Unknown | Closed question it must answer | Who could answer it |
|---|---|---|
| | | |

---

## Interview log

Append-only. Each entry records what was asked, what came back, and which field it filled —
this is the **fertilizer** returning to the root, and it is what makes a derived document
auditable back to a sentence somebody actually said.

| Date | Field | Question asked | Answer |
|---|---|---|---|
| | | | |
