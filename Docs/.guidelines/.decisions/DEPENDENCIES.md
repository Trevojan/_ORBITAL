# DEPENDENCIES

**Status:** `[TBD]` · **Owner:** DV · **Filled by:** each module, as a dependency is added.

What this solution needs from outside, and which project holds each one.

## Norms

**AR23 applies here:** third-party code is a **package reference, never vendored source**.
Vendoring turns every upstream fix into a manual merge, and it misattributes the resulting
binary to whoever ships it.

Shared settings — target framework, nullability, language version, test tooling — come from
`Directory.Build.props` (`AR22`, `AR25`). A project file carries only what makes it different.

Adding a dependency is a **consult-first** decision for the DV. Two questions decide it:

1. What does this cost when it stops being maintained?
2. What would we write ourselves instead, and how long would that take?

If the honest answer to the second is "an afternoon", write the afternoon.

## By project

| Project | Package | Version | Why it is needed |
|---|---|---|---|
| | | | |

## Frameworks

| Project | Target | Reason |
|---|---|---|
| | | |

> A consumer's target **MUST** be at least the target of what it references. When two parts of
> the solution must move at different speeds, the shared piece targets the lowest common
> denominator — or the two stop sharing a project reference and communicate over a wire
> instead (`AR6`).

## Boundaries to external systems

Every external system sits behind an interface in a `_DRIVE`, so that its unavailability is an
outcome the caller handles rather than an exception that escapes.

| System | Interface | Implementation | Swap cost |
|---|---|---|---|
| | | | |

## Secrets

None in source, none in a committed file, none in a document (`AR24`). Committed configuration
holds placeholders, and a placeholder **MUST** be obviously inert — something that could not
be mistaken for a working value.

| Secret | Where it comes from |
|---|---|
| | |
