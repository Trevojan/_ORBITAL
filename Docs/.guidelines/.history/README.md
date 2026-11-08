# History — the attic

> **Not an axis.** An axis answers a question someone has when resuming the work; this folder
> answers none. It exists so that **removing is not deleting**.
>
> It is the counterpart of two constraints: *a known loss is pinned with its reason* and *nothing
> disappears in silence*. A superseded document leaves the reading path without leaving the
> repository.

## What comes here

A document that **was true** and stopped being so: an old version's change list, a study that decided
something and was later absorbed, the draft a norm replaced.

**What does not come here:** a document that still decides something. If it still answers a question,
it belongs to that question's axis — and moving it here is hiding it, not archiving it.

## The record

Moving without recording is the same as deleting, with extra steps. One line per move, the newest on
top. A file discarded without ever being committed has no copy to move, so its line is the only trace
it leaves.

| when | what | why | what took its place |
|---|---|---|---|
| 2026-11-07 | `flow.pgml`, `flow.hgml`, `flow.min.hgml`, `flow.xml` — discarded, never committed | a Glyph encoding of the task lifecycle A–F and the phase-review norm, and three projections of it; the prose had already taken both over, and the encoding was drifting from it (`ADR-005`, `ADR-006`) | [`PROTOCOL.md`](../PROTOCOL.md) § The task lifecycle, and `AR27` in [`ARCHITECTURE.md`](../.decisions/ARCHITECTURE.md) |
| 2026-11-07 | `inspection.pgml` — discarded, never committed | a one-off Glyph prompt to an agent, pointing at a `Plans/` folder that never existed (`ADR-006`) | nothing: it decided nothing that stayed |
| 2026-11-07 | `production_level_flow.xml` — discarded, never committed | another project's system prompt, in Portuguese, with that project's paths (`ADR-006`) | nothing: it was not Orbital material |
| 2026-11-07 | `SOURCES/WORKFLOW.txt` — discarded, never committed | a generic diagram of orchestrator, harness and gate, not a fact about the system (`ADR-006`) | [`.team/ORCHESTRATION.md`](../.team/ORCHESTRATION.md) §3 |

## How to read what is here

Half of what looks abandoned was **reassigned**, not thrown away — and the note saying so is usually
somewhere else, with a reason and a date. Before concluding that something was forgotten, look for its
line in the table above.
