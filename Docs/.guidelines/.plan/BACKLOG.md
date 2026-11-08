# BACKLOG — the `B#` register

**Status:** `[TBD]` · **Owner:** PO · **Filled by:** `PROTOCOL.md` S3, maintained continuously.

Every functional and non-functional item, with a stable id. The backlog rules on **fact** —
what exists, what is missing, what it costs. It never holds a business rule; those live in
`.decisions/BUSINESS_RULES.md`, and when this file appears to state one it is quoting.

A `B#` is **stable for the life of the project**. An item that moves from missing to built
keeps its id, because `TRACE.md` refers to it and a renumbered backlog silently breaks the
trace.

## Difficulty

| # | Meaning |
|---|---|
| `D1` | Simple — few files, no schema change, no new surface |
| `D2` | Dense — a new surface, a new screen, or a localised restructuring |
| `D3` | Very dense — a whole module, a migration plus service plus surface, or an external integration |

A **task** is always `D1` by definition. An item larger than `D1` is decomposed into tasks by
`PROTOCOL.md` S7; the difficulty here estimates the *item*, not its parts.

## `[late]`

Marks a **non-functional** item. These never become modules: they anchor to phase F0 or F5 of
the first module that would suffer without them. If no module would suffer, the item stays
here and never enters the ladder — which is a legitimate outcome, not a failure.

---

## Part I — built

| `B#` | Name | Category | Difficulty |
|---|---|---|---|
| | | | |

## Part II — missing

| `B#` | Name | Category | Difficulty |
|---|---|---|---|
| | | | |

Each row is followed by a paragraph saying what happens today, what needs to happen, and what
it unblocks. An item nobody can describe that way is not understood well enough to estimate.

## Part III — categories

One entry per category, each answering three questions: what it is today, what it must
become, and which functions are still to be written.

Material here is **raw material for a module**, not a commitment. Work described in this part
that never became an item in Part II is exactly what `TRACE.md`'s backward direction exists to
catch.

### `<Category>`

**Today.** `[TBD]`

**Must become.** `[TBD]`

**To implement.** `[TBD]`
