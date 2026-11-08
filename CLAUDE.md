# _ORBITAL

A template for starting software projects: conventions, a derivation protocol, a planning ladder,
and five working app templates. A project's source of truth is its briefing; the plans, the ladder
and the code derive from it, in that order.

## Start here — always

**[`Docs/.guidelines/.shortcuts/README.md`](Docs/.guidelines/.shortcuts/README.md)**

It is the index: where the work stopped, what comes next, and which document answers which
question. It **points and never copies**, and this file does the same on purpose. Do not rebuild by
inference what is one shortcut away.

| axis | holds |
|---|---|
| [`.decisions/`](Docs/.guidelines/.decisions/README.md) | what has been decided, by whom, and why; the `AR#` and `BR#` norms |
| [`.plan/`](Docs/.guidelines/.plan/README.md) | what comes next, and what was put off |
| [`.orders/`](Docs/.guidelines/.orders/README.md) | the Orders — the contract that dispatches the work. **One open at a time** |
| [`.constraints/`](Docs/.guidelines/.constraints/README.md) | what the project forbids itself — **read before proposing** |
| [`.changelog/`](Docs/.guidelines/.changelog/README.md) | how the version number moves |
| [`.sources/`](Docs/.guidelines/.sources/README.md) | the facts a project derives from, and the references that decided something |

## Verify

```bash
dotnet build _ORBITAL.slnx
```

Builds the five apps. Tests run app by app, never through the solution:
`dotnet test <APP>_ORBITAL/<CONCEPT>_PROBE`. **One step per commit, green at each one.**

## Two constraints that catch every new agent

The full set is in [`.constraints/`](Docs/.guidelines/.constraints/README.md). These two change what
you do in the first hour:

- **No project is created without explicit confirmation** (`AR9`). Complexity that looks like it
  needs a new `_CORE`, `_ENGINE`, `_DRIVE`, `_PANEL` or `_PROBE` is planned, presented, and only then
  built.
- **State the present, never the correction.** Text that said "this is A" **becomes** "this is B";
  it does not gain a "no longer A, now B". Accumulated micro-corrections become residue the reader
  infers and throws away, because they do not help the final state.

## Vocabulary

- **Regent** — who decides. An agent measures, proposes and refuses; it does not ratify.
- The rest — norm, invariant, gate, phase, module, the five domains — is defined once, in
  [`ARCHITECTURE.md`](Docs/.guidelines/.decisions/ARCHITECTURE.md) §7.
