# Changelog — how the number moves

> The record itself is the [`CHANGELOG.md`](../../../CHANGELOG.md) at the repository root, and stays
> there. This file is the **shortcut**: the version rule, and what each release decided, so nobody has
> to reread everything. `ADR-010`.

## The scheme

```
rule . structure . surface . record
```

| digit | moves when |
|---|---|
| **rule** | a norm the code obeys is born, falls or changes meaning — an `AR#` or a `BR#`, or an `ADR-###` that binds code. The most important event a project has, so it holds the highest digit |
| **structure** | a module of the ladder closes, a project or an app enters or leaves the solution, or the guideline layout changes |
| **surface** | what a person sees changes: a screen, a layout, what the program accepts as input |
| **record** | only the documentation moves — an `ADR-###` that binds no code, an axis filled in, an Order closed. Two digits, running past 99 freely |

**No digit resets another.** Digits move independently and keep their place: touching only the
interface cannot throw away the number the core earned.

**A change moves every digit it touches, by one each.** A commit that closes a module and writes its
decision moves `structure` and `record` together.

The number lives in [`Directory.Build.props`](../../../Directory.Build.props) as `<Version>`, the top
entry of `CHANGELOG.md` carries the same number, and the two change in the same commit.

## Who reads `CHANGELOG.md`

Developers, managers and clients. Write in **business language**: what changed and why it mattered,
not which method was renamed. An entry that only makes sense next to the diff belongs in the commit
message instead.

An entry states the observable difference. "Fixed `CalculateTotal`" says nothing; "a total that
included cancelled items no longer does, so the monthly figure stops disagreeing with the report" says
what someone will notice.

## What each release decided

> One line per release, and only what **moved the highest digit**. The detail is in `CHANGELOG.md`;
> this is what is remembered.

| version | what moved the highest digit |
|---|---|
| **0.1.0.01** | **structure** — the guidelines take the Dotted layout (`ADR-007`), with the decision register, the constraints and this rule. The scheme starts here: until this release the template carried SemVer `0.1.0`, and that history is counted as `0.0.0.00` |

## The rule learnt by rereading

**The root cause that repeats most in any repository is a single one:** *a decision taken in one place
and never carried to the others.*

When closing any change, the question is: **where else was this written?**
