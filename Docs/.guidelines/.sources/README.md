# Sources — what the project is built from, and what came from outside

> Two kinds of material, both **descriptive**: the facts a project derives everything from, and the
> external references that **decided something**. If one of these disagrees with reality, reality is
> right and the file is a documentation defect.
>
> Rules live next door in [`.decisions/`](../.decisions/README.md). If one of *those* disagrees with
> reality, that is a process defect, and it is repaired differently — by changing the code back, or by
> writing the decision that changes the rule.

## The facts

| file | question it answers | owner | filled by |
|---|---|---|---|
| [`BRIEFING.md`](BRIEFING.md) | What problem are we solving, for whom? | PO | `PROTOCOL` S0 |
| [`DATA_MODEL.md`](DATA_MODEL.md) | What are the entities, and what shape do they have? | DA | `PROTOCOL` S2, then per module |
| [`API_SURFACE.md`](API_SURFACE.md) | What can be called, and what comes back? | DV | per module, phase F3 |

`BRIEFING.md` first, always. It is the root everything else descends from, and reading a data model
before the problem it serves is how people end up defending a schema nobody needs.

## The references that decided something

> A list of links that decided nothing is a bibliography, not a shortcut. The **decided** column is
> required.

The template's own descendants are its primary source: they ran the pattern on real work and changed
it where it did not hold. Each was measured on 2026-11-07, in the Regent's repositories.

| reference | what it is | decided |
|---|---|---|
| **Glyph** (`glyph/.guidelines/`) | the Regent's markup language, a JS engine being ported to Rust | the Dotted layout and its entry point (`ADR-007`); the four constraint tables; the four-digit version with no reset (`ADR-010`); gates that check the plan, the links and the dependency arrows, each seen failing before it counts — roadmap module 3 |
| **AutoRpg** (`heranca-orbital/AutoRpg/`) | a C# application migrated onto Orbital from a single project | the ladder as `M.D.T` with the phases inside each task (`ADR-008`); the decision register with superseded rows marked (`ADR-009`); the arrow as a `_PROBE` test that reads the `.csproj` files, and recognisers for retired constructs — module 3; adopting an existing codebase by measuring it against every `AR#` and recording its output as an oracle first — module 4. It cites `AR1`–`AR27` from its own `.sources/` without renumbering them |
| **instituto-conforme** (`instituto-conforme/Docs/.guidelines/`) | a project started from this template's app set | two-digit ids `M##.D##.T##`, and an unsigned ladder kept as a `BLUEPRINT_M_##` until its gate is signed (`ADR-008`); the *reversible* and *supersedes* columns of the register (`ADR-009`) |
