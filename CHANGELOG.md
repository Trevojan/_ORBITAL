# CHANGELOG

The record of what changed, newest first. The version rule is in
[`Docs/.guidelines/.changelog/`](Docs/.guidelines/.changelog/README.md): four digits,
`rule.structure.surface.record`, and no digit resets another.

## [Unreleased]

## 1.2.0.02 — 2026-11-07

The ladder is planned the way the projects built on this template already plan it, and the code
conventions that identify the work can now be cited.

- **Module, step, task.** A module climbs in steps, each step holds tasks, and a task is
  addressed `M01.D02.T03`. A step opens only when the one before it has closed; a task is one
  commit, undone whole or not at all.
- **The seven phases live inside every task.** Each task lists all seven and declares empty the
  ones it does not touch, so a reviewer sees at a glance how deep a change goes.
- **A plan stays editable until it is signed.** A module's plan is a blueprint until its gate is
  signed, and is renamed — and frozen — in the commit that signs it.
- **Three conventions became norms.** Reuse by composition, never by inheritance; names that say
  a responsibility, never `Helper` or `Manager`; and one register of file prefixes, where a
  repository is `Repo` and `R` stays the record.
- **Every norm has an address** another project can link to.

## 0.1.0.01 — 2026-11-07

The guidelines are reorganised so that a person, or an agent, resuming the work finds where it
stopped in one file instead of rebuilding it from the history.

- **One entry point.** `Docs/.guidelines/.shortcuts/README.md` says where the template stands, what
  comes next, and which document answers which question. The root `CLAUDE.md` is now published and
  only points there, so a fresh clone needs nothing pasted back in.
- **Every decision has a date, an author and a successor.** The register in `.decisions/` lists each
  decision with the Regent's own words as its reason, whether it can be reversed, and what it
  replaced. A replaced decision stays visible, struck through, instead of being rewritten.
- **What the project forbids itself is in one place.** `.constraints/` holds the prohibitions that
  were scattered across three documents, split into what this template forbids and the method every
  project shares.
- **A version that keeps what each part earned.** The number has four digits — rule, structure,
  surface, record — and changing the screens can no longer reset the number the rules earned.
- **The plan, the facts, the roles and the Orders each have their own folder**, named the way the
  projects built from this template already name them.

## 0.0.0.00

Everything before the four-digit scheme: the conventions, the derivation protocol, the planning
ladder, the four roles, and five working app templates. The template carried SemVer `0.1.0` and kept
no changelog entries.
