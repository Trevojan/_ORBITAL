# CHANGELOG

The record of what changed, newest first. The version rule is in
[`Docs/.guidelines/.changelog/`](Docs/.guidelines/.changelog/README.md): four digits,
`rule.structure.surface.record`, and no digit resets another.

## [Unreleased]

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
