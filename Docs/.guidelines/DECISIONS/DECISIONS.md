# DECISIONS — the `DC#` register

A decision record is a short note of an architectural choice, the context that forced it,
and what it costs. It exists so that whoever arrives later can tell a deliberate choice from
an accident.

**Never contradict an accepted `DC#` without writing the one that supersedes it.**

Status values: `proposed` · `accepted` · `deprecated` · `superseded by DC<n>`

## When to write one

- A technical choice that is important and not obvious to a newcomer
- An alternative that was considered and rejected — record why
- An ambiguous business rule where a reading was chosen
- A convention deliberately broken in this project

Do **not** write one for trivia, naming, or formatting.

---

## DC1 — Adopt the Orbital convention

`accepted`

**Context.** Projects that grow without a naming and layering convention accumulate files
whose purpose is only knowable by reading them. The cost is not felt at the start; it is felt
when a second person joins, or when the first person returns after three months.

**Decision.** Adopt Orbital: Domain-Driven Design at the base, strong Separation of Concerns
in the extensions. Projects named `<CONCEPT>_<DOMAIN>` grouped in `<APP>_ORBITAL` folders,
five domains with an inward-pointing dependency arrow, and a concept expressed as a folder of
files rather than a single class. The full set of norms is `ARCHITECTURE.md`.

**Consequences.**

- A file's purpose is derivable from its name and its folder.
- The type table makes the file set derivable rather than memorised, so two people decompose
  the same concept the same way.
- More projects than a conventional layout would produce. That is the price of the arrow
  being enforced by project references instead of by discipline.

---

## DC2 — One canonical guideline location

`accepted`

**Context.** Guideline files copied into several folders drift apart, and the copies are
believed because they look official. A worse variant exists: a copied "navigation hub" that
points at canonical documents which do not exist anywhere. An agent reading it follows the
pointers, finds nothing, and improvises — while appearing to be well-informed. This defect
was found in this repository's own history and is what motivated the rule.

**Decision.** `Docs/.guidelines/` at the repository root is the only published guideline set.
Per-app `<APP>_ORBITAL/Docs/` folders are personal scratch space, are gitignored, and are
never published. Every path referenced from a guideline document **MUST** resolve inside this
repository.

**Consequences.**

- Duplication cannot drift, because there is nothing to duplicate.
- A broken reference becomes a reportable defect rather than an invitation to improvise.
- Personal working notes stay personal, which is why the ignore rule lives in the root
  `.gitignore` and not in a file that disappears with the app it sits in.

---

*(new decisions start at `DC3`)*
