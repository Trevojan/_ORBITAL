# SOURCES — the facts

What the system **is** and what has been done to it. Everything here is descriptive: if one
of these files disagrees with reality, reality is right and the file is a documentation
defect.

Rules live next door in `DECISIONS/`. If one of *those* disagrees with reality, that is a
process defect, and it is repaired differently — by changing the code back, or by writing the
decision that changes the rule.

| File | Question it answers | Owner | Filled by |
|---|---|---|---|
| `BRIEFING.md` | What problem are we solving, for whom? | PO | `PROTOCOL` S0 |
| `DATA_MODEL.md` | What are the entities, and what shape do they have? | DA | `PROTOCOL` S2, then per module |
| `API_SURFACE.md` | What can be called, and what comes back? | DV | per module, phase F3 |
| `CHANGELOG.md` | What changed, and when? | DV | per module, phase F6 |

## Reading order

`BRIEFING.md` first, always. It is the root everything else descends from, and reading a data
model before the problem it serves is how people end up defending a schema nobody needs.
