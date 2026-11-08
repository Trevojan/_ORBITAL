# CHECKLIST — what has been done

**Derived, never authored.** Every line here comes from a task in a module's ladder
(`BLUEPRINT_M_##.md`, `LADDER_PLAN_M_##.md` once signed).
A line with no originating task is a process defect, not a shortcut.

This file exists separately from the ladder for one reason: **the ladder is what was
decided, the checklist is what was done.** Ticking a box means writing to a file, and a
frozen module is never rewritten (`PLANNING.md` rule 3).

## Ordering

Declared and reproducible, so two people regenerate the same list:

1. **Topological first** — nothing appears before what it depends on.
2. **Increasing difficulty** among whatever is unblocked at that point.
3. **`[late]` last** — non-functional items only surface when nothing else is free.

## Format

```markdown
- [ ] `M01.D01.T01` — write the norm that governs <thing>        [F0]      D1
- [x] `M01.D01.T02` — declare the <Concept> contract              [F1–F2]   D1
```

Milestones are marked on their own line and name what is true once reached — and, just as
importantly, what is still **not** true:

```markdown
⟦ MILESTONE — <name> ⟧  <what now works>.  Does NOT yet: <what it still cannot do>.
```

The second half is not pessimism. A milestone announced without it promises whatever the
reader hoped for.

---

## Modules

*(empty until `PROTOCOL.md` S8 derives it)*

| Module | Tasks | Done | Status |
|---|---:|---:|---|
| | | | |

---

## Tasks

*(empty until `PROTOCOL.md` S8 derives it)*
