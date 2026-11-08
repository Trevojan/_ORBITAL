# TESTS — how behaviour is proven

Owner: **QA**. The norms here are cited by tasks the same way `AR#` norms are; they are
referenced as `TESTS §<section>`.

## What is proven, and where

| Layer | Probed? | Why |
|---|---|---|
| `_ENGINE` | **Yes, exhaustively** | Pure logic with no dependencies. It is the cheapest thing in the repository to test and the place every business rule lives. |
| `_CORE` | **Yes** | Orchestration over the engine. Bugs here are about *when* a rule is applied, and they do not show up in engine tests. |
| `_DRIVE` | By running it | Mostly translation. A live call against the real counterpart proves more than a mock of it. |
| `_PANEL` | By running it | Composition and wiring. Dependency-injection faults are runtime faults; they do not appear at compile time and a mock will not find them. |
| `_PROBE` | n/a | It is the prover. |

One `_PROBE` per app, named for the app's concept, covering that app's `_ENGINE` and `_CORE`.

## Norms

**T1.** Tooling is declared once, in `Directory.Build.props`, conditioned on the `_PROBE`
suffix. A probe's project file carries only the references naming what it examines (`AR25`).

**T2.** Tests run **project by project**, never through the solution. A solution-wide run
drags in projects whose pre-build steps require toolchains the test does not need.

**T3.** A test **MUST** fail when the behaviour it names is removed. A test that has never
been seen red is an assertion about its author's intent.

**T4.** Arrange, Act, Assert — in that order, visibly separated.

**T5.** One reason to fail per test. Two assertions about unrelated things report the first
failure and hide the second.

**T6.** No conditional logic in a test. A branch means the test does not know what it
expects.

**T7.** Names state the scenario and the outcome, unabbreviated, in whatever casing the
language conventionally uses for test methods. `Silence_beyond_the_grace_period_marks_it_lost`
tells a failure reader what broke; `Test3` does not.

**T8.** Test data comes from a named builder when it appears more than twice. A literal
repeated six times is a fixture that has not been given its name yet.

**T9.** Every task in a module blueprint **names its test**, and the named test exists before
the task is considered done.

**T10.** A number displayed to a person **MUST** be confirmed against an oracle — an
independent path that shares no code with the one that produced it. The DA supplies it.

## Coverage expectations

- Every rule in an `_ENGINE` has at least three cases: the normal one, the boundary, and the
  one that is refused.
- Every derived state has a test that produces it. A state no test can reach is either dead
  or unreachable through the surface, and both are worth knowing.
- Every immutability guarantee has a test that the original is unchanged after a
  transformation.
- Every validation has a test that the invalid input is refused, asserting the refusal — not
  merely that nothing was written.

## What is deliberately not covered

Recorded rather than omitted, per the QA attestation. Typical entries: native window
behaviour, third-party client internals, and anything whose failure a running smoke check
catches faster than a unit test would.

A deliberately uncovered area **MUST** appear in the module's QA attestation. Silence is not
a coverage decision.
