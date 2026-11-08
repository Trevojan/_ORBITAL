# Decisions — who decided, and why

> A decision with no recorded reason gets discussed again. A decision with no recorded author
> gets re-decided by someone who had no authority to.
>
> **Only the Regent decides.** An agent may measure, propose and refuse — never ratify a norm it
> wrote itself.

This is the `ADR-###` register, one of the three things a task may cite. The other two live
beside it: [`ARCHITECTURE.md`](ARCHITECTURE.md) (`AR#`) and
[`BUSINESS_RULES.md`](BUSINESS_RULES.md) (`BR#`).

## When to write one

- A technical choice that is important and not obvious to a newcomer
- An alternative that was considered and rejected — record why
- An ambiguous business rule where a reading was chosen
- A convention deliberately broken in this project

Do **not** write one for trivia, naming, or formatting.

## Format

One section per date, the newest on top. The reason, when it exists in the Regent's own words,
goes in **quoted** — the original sentence decides better than a paraphrase. Numbers are three
digits, sequential, never reused.

```
## YYYY-MM-DD

| ADR | decision | reason | reversible | supersedes |
|---|---|---|---|---|
| **ADR-###** | **<what was decided>** | <why — quoted when there is a sentence> | <yes · at a cost: what it costs · no> | <ADR-###, or —> |
```

**A superseded decision is never rewritten or deleted.** Its title gains `!` in front and
`-> superseded by ADR-###` at the end, and its body is struck through, so whoever reads it sees
what held, until when, and what replaced it. The decision that replaces it is born with the next
number and says what holds now.

```
| **ADR-###** | **!<the title, as it was> -> superseded by ADR-###** | ~~<the body, as it was>~~ | … | … |
```

**Never contradict an accepted `ADR-###` without writing the one that supersedes it.**

---

## 2026-11-07

| ADR | decision | reason | reversible | supersedes |
|---|---|---|---|---|
| **ADR-011** | **The root `CLAUDE.md` is tracked, and it only points at [`.shortcuts/README.md`](../.shortcuts/README.md).** The root `AGENTS.md` stays local and gitignored | the Regent's answer, *"Track it as a pointer"*, after Glyph and AutoRpg were measured tracking theirs the same way. A pointer carries no personal setup, so publishing it costs nothing, and a fresh clone reads the entry point without anyone pasting a line back in | yes | the rule that kept both root agent files local |
| **ADR-010** | **The version has four digits, `rule.structure.surface.record`, and no digit resets another.** The rule and the table of releases live in [`.changelog/`](../.changelog/README.md); the record is the root `CHANGELOG.md` | the Regent's answer, *"Four digits, no reset"*. Glyph, AutoRpg and instituto-conforme each moved to a scheme where touching the interface cannot throw away the number the core earned; SemVer zeroes everything to the right of the digit that moves | at a cost: every number re-derived | SemVer `MAJOR.MINOR.PATCH` |
| **ADR-009** | **Decisions are `ADR-###` rows in this register, dated, newest first**, with *reversible* and *supersedes* columns and the superseded rows marked as above. `DC1`–`DC3` are `ADR-001`–`ADR-003` | the Regent's answer, *"ADR-### dated table"*. The `DC#` headings had a status word and no date, no author and no mark for what replaced them; AutoRpg and instituto-conforme had both moved to this form | at a cost: ids rewritten again | the `DC#` register |
| **ADR-008** | **A ladder is `M##.D##.T##` — module, step (*degrau*), task — and the seven phases `F0`–`F6` run inside every task as its protocol**, a phase with nothing in it declared empty. The step is the dependency boundary; the task is the reversibility boundary. An unsigned ladder is a `BLUEPRINT_M_##.md`; the commit that signs its gate renames it `LADDER_PLAN_M_##.md`, and from then on it is an act | the Regent's answer, *"M##.D##.T##, F0–F6 per task"*. AutoRpg (its `ADR-79`) and instituto-conforme (its `ADR-007`, `ADR-018`, `ADR-019`) run their ladders this way. The norm text moves in [roadmap](../.plan/ROADMAP.md) module 2 | at a cost: every ladder id rewritten | `AR20` and `AR21` as worded today |
| **ADR-007** | **The guidelines take the Dotted layout at `Docs/.guidelines/`**: [`.shortcuts/`](../.shortcuts/README.md) is the entry point, and the axes are `.sources`, `.decisions`, `.plan`, `.orders`, `.constraints`, `.changelog`, `.team`, with `.history` as the attic | *"i've been using the pattern and upgrading them in other repositories, like 'glyph'. can you take a look in how is the app made and return here to reinvent the template?"* Glyph, AutoRpg and instituto-conforme all run this layout; it led the reinvention order the Regent answered | at a cost: every path moves back | — |
| **ADR-006** | **Of the eleven orphan files on `main`, five are increments and were committed** — `.plan/PHASES.md` with `.plan/min_max_ladder.svg`, `.team/ORCHESTRATION.md`, `.plan/ROADMAP.md` and the work ledger — **and seven were discarded**: `flow.pgml`, `flow.hgml`, `flow.min.hgml`, `flow.xml`, `inspection.pgml`, `production_level_flow.xml`, `WORKFLOW.txt` | *"read it all before and try to understand what is increment to the architecture and what is trash."* The classification was put to the Regent and answered *"Apply as classified"*; the reason for each file is in the [attic's record](../.history/README.md) | no — the seven were never committed | — |
| **ADR-005** | **The task lifecycle A–F and `AR27` live as prose only**, in [`PROTOCOL.md`](../PROTOCOL.md) and [`ARCHITECTURE.md`](ARCHITECTURE.md) | the `flow.*` files were a second copy of the same fact, already drifting from the prose (part Portuguese, a stale status line); answered with `ADR-006` | at a cost: an encoding rewritten from the prose | — |
| **ADR-004** | **`main` is the branch of record: every branch converges to it, and it is kept as the template** | *"all branches must converge to main and be maintained as a template."* `main` was an ancestor of `ORBITAL_TEMPLATE` and fast-forwarded to it; `DESKTOP_ORBITAL` was already inside it | yes — the other branches still exist | the README table that kept the branches apart |

## Earlier, still in force

> An old decision nobody revoked. It stays here so it does not come back to the table.

| ADR | decision | reason | reversible | supersedes |
|---|---|---|---|---|
| **ADR-003** | **`Docs/.guidelines/` is published; `**/.scope/` is not, at any depth.** The split is by ownership, not by secrecy: the guidelines are the template, `.scope` holds the engagement — briefs, contracts, exports, handovers | a template that hides its own rules teaches nothing, and client material has exactly one home that is never a judgement call. The ignore rule lives in the root `.gitignore` so moving a folder cannot defeat it. Anything outside `.scope` is published by default, deliberately | yes | the per-app `Docs/` ignore rule |
| **ADR-002** | **`Docs/.guidelines/` at the repository root is the only guideline set, and every path a guideline references MUST resolve inside this repository** | copied guideline files drift and are believed because they look official; a copied "navigation hub" pointing at documents that exist nowhere was found in this repository's own history — an agent followed it, found nothing, and improvised while looking well-informed. A broken reference is a reportable defect, not an invitation to improvise | at a cost: copies would drift again | — |
| **ADR-001** | **Adopt Orbital**: Domain-Driven Design at the base, strong Separation of Concerns in the extensions — projects named `<CONCEPT>_<DOMAIN>` in `<APP>_ORBITAL` folders, five domains with an inward-pointing arrow, a concept as a folder of files. The norms are [`ARCHITECTURE.md`](ARCHITECTURE.md) | without a naming and layering convention a file's purpose is knowable only by reading it, a cost felt when a second person joins or the first returns after three months. The price is more projects than a conventional layout, which is what it costs to have the arrow enforced by project references instead of by discipline | at a cost: every project renamed | — |

## Decisions an agent took that the Regent has not ratified yet

> While it is on this list it **is not a norm**, and nothing may cite it as binding. Leaving this
> list is the Regent's act, not the agent's.

- **The reinvention's module order** — [`ROADMAP.md`](../.plan/ROADMAP.md) §3, modules 2 to 6. Proposed on
  2026-11-07 from the three descendants; the Regent answered the four decisions that shape it
  (`ADR-008` to `ADR-011`), not the order itself.
