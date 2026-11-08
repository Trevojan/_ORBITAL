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

**Decision.** `Docs/.guidelines/` at the repository root is the only guideline set. Every path
referenced from a guideline document **MUST** resolve inside this repository.

**Consequences.**

- Duplication cannot drift, because there is nothing to duplicate.
- A broken reference becomes a reportable defect rather than an invitation to improvise.

---

## DC3 — `.guidelines` is published, `.scope` is not

`accepted`

**Context.** A template that hides its own rules teaches nothing: the conventions, the roles
and the planning machinery are the product, and a reader who cannot see them has been handed
a folder structure and told to guess. But the same repository, once cloned, accumulates
material that belongs to whoever commissioned the work — briefs, contracts, exports,
handovers. Those two kinds of document have opposite defaults and were previously governed by
one rule, which meant one of them was always wrong.

**Decision.** The split is by **ownership, not by secrecy**:

| Folder | Holds | Published |
|---|---|---|
| `Docs/.guidelines/` | conventions, roles, planning machinery — the template | **yes** |
| `**/.scope/` | briefs, contracts, exports, handovers — the engagement | **never** |

The ignore rule matches `.scope` at **any depth**, so it cannot be defeated by moving a
folder, and it lives in the root `.gitignore` rather than in a per-app file that disappears
along with the app it sits in.

**Consequences.**

- The template is readable by anyone, which is the point of publishing it.
- Client material has exactly one home, and that home is never a judgement call.
- A clone that becomes a real project keeps its own history in its own repository; the
  template does not follow it there.
- Anything placed outside `.scope` is published by default. That default is deliberate — the
  alternative, hiding by default, is how a template stops being one.

---

*(new decisions start at `DC4`)*
