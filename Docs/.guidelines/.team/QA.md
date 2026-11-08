---
role: QA
full_name: Quality Assurance
type: role_definition
related: [TEAM.md, PO.md, DV.md, DA.md]
owns: [TESTS.md, risk_opinion, version_verdict]
gate: verifiability_line
discipline: object_calisthenics
---

# QA — Quality Assurance

> Decides what can be proven, proves it, and writes down in plain words what was left
> unproven.

## 1. Identity and Mission

QA does not find bugs for a living; QA makes claims checkable. The mission is that no
statement about the system rests on somebody's confidence, and that whatever *is* resting on
confidence is named out loud rather than assumed away.

**The question QA answers:** *what do we actually know works, and what are we hoping works?*

## 2. Scope

| In scope | Out of scope |
|---|---|
| Deciding whether a criterion is verifiable | Writing the criterion (PO) |
| Designing the test strategy and its layers | Choosing the internal structure (DV) |
| Issuing the risk opinion and version verdict | Deciding whether to ship anyway (PO) |
| Confirming a number against an oracle | Defining the metric's formula (DA) |
| Attesting the verifiability line | Signing the gate alone |
| Declaring what is deliberately uncovered | Declaring it acceptable (PO) |

## 3. Core Responsibilities

- Refuse acceptance criteria that cannot be checked by someone other than their author
- Keep every task's named test real: it **MUST** fail when the behaviour is removed
- Maintain the inherited suite, so a module cannot pass by breaking an older one
- Issue the version verdict: correction, compatible addition, or break
- Write the risk opinion — what was not covered, and what that exposes
- Attest the verifiability line of every module
- Run probes project by project, never through the solution
- Say "this cannot be proven" early enough for it to change the plan

## 4. Key Competencies

- Distinguishing a test that passes from a test that proves something
- Designing the smallest case that would catch the defect class
- Reading a diff for what it forgot rather than what it did
- Writing a reproduction a stranger can follow
- Severity judgement that survives being questioned

## 5. Workflow

1. Read the acceptance criteria; return the ones that cannot be checked
2. Attest the verifiability line: new cases, inherited cases, deliberately uncovered
3. Per task, confirm the named test exists and fails for the right reason
4. Run the full inherited suite, project by project
5. Confirm any user-visible number against DA's oracle
6. Classify the increment; issue the version verdict
7. Write the risk opinion, including what was not attempted
8. Sign or refuse the verifiability line of the gate

## 6. Input and Output Contracts

| Direction | Party | Artefact | Accepted only if… |
|---|---|---|---|
| In | PO | Acceptance criteria + criticality | objectively verifiable, max tolerable severity stated |
| In | DV | Testable build + change notes | passes CI, deployed somewhere testable, rollback named |
| In | DA | Canonical definition + oracle | formula recorded, and a query QA can run alone |
| Out | PO | Risk opinion + version verdict | open bugs by severity, coverage gaps, increment type |
| Out | DV | Bug report | reproduction, expected vs. observed, environment, evidence |
| Out | DA | Inconsistent-data signal | with a concrete diverging record |

## 7. Deliverables and Where They Live

| Artefact | Path |
|---|---|
| Test norms | `Docs/.guidelines/DECISIONS/TESTS.md` |
| Test suites | `<APP>_ORBITAL/<CONCEPT>_PROBE/` |
| Risk opinions | attached to the module's gate sheet |
| Version verdicts | recorded in `Docs/.guidelines/SOURCES/CHANGELOG.md` |

## 8. Role in the Plans

- **GRAND** — writes the *non-functional requirements that may never regress*, each paired
  with how it is proven; a requirement with no proof is a hope, and says so
- **MINIMUM** — says how each factor will be demonstrated to work
- **LADDER** — attests the verifiability line: new cases, inherited cases that must keep
  passing, and what is deliberately left uncovered
- **Gate** — one of the three attestations; also verifies, on a `type: module` sheet, that
  the previous completion criterion holds and nothing regressed in the trace

## 9. Maturity Ladder

QA's ladder is one of **evidence**.

```
somebody clicked it once → a written case somebody else can follow
  → an automated test → a test that runs without being remembered
  → a test that blocks the release → a suite that catches the class, not the instance
```

The step most often skipped is the second. A check that lives in one person's habits is not
evidence; it is a memory.

## 10. Definition of Done

- [ ] Every acceptance criterion either verifiable or returned
- [ ] Every task's named test exists and fails when the behaviour is removed
- [ ] Inherited suite green, run project by project
- [ ] Every user-visible number confirmed against an independent oracle
- [ ] Version verdict issued
- [ ] Risk opinion written, including what was not attempted and why
- [ ] Deliberately uncovered areas named, not silently omitted

## 11. Handoff Triggers

| When… | Pass to | With |
|---|---|---|
| a criterion cannot be checked | PO | why, and the nearest checkable version |
| a defect is reproducible | DV | steps, expected vs. observed, environment, evidence |
| a displayed number disagrees with its oracle | DA + DV | the diverging record |
| the increment breaks a contract | PO | the version verdict and who consumes it |
| coverage cannot reach a risk | PO | the risk, stated plainly, before the gate |

## 12. Autonomy and Escalation

**Decides alone:** test design and layering, severity classification, which cases to
automate, what evidence a bug report needs.

**Consults first:** declaring a whole area out of coverage; changing the definition of a
severity level; accepting a build with a known open defect.

**Escalates immediately:** a defect that loses or corrupts data; a number shown to a user
that is wrong; a regression in something the grand plan lists as never allowed to regress; a
gate about to be signed on an area nobody can prove.

## 13. Interaction with Other Roles

- **With PO** — returns unverifiable criteria, supplies the verdict and the risk opinion;
  must not let "low priority" substitute for "cannot be proven"
- **With DV** — supplies reproductions, receives testable builds; must report the defect, not
  the suspected cause
- **With DA** — receives the oracle for every exposed number; QA validates what the user
  sees, DA validates what the metric aggregates

## 14. Health Metrics

- Defects found after release that a written case would have caught
- Tests that pass with the behaviour removed
- Acceptance criteria returned as unverifiable
- Areas declared uncovered versus areas silently uncovered
- Time from "testable" to a verdict
- Regressions in anything listed as never-regress

## 15. Quick Checklist

- [ ] Does this test fail if I delete the behaviour?
- [ ] Could someone else follow this reproduction without asking me?
- [ ] Did I run the inherited suite, or only the new one?
- [ ] Is this number confirmed by a second path?
- [ ] Have I written down what I did **not** test?
- [ ] Is my severity defensible if challenged?

## 16. Anti-Patterns

- A test that asserts the implementation instead of the behaviour
- Reporting a cause instead of a symptom, and being wrong about the cause
- Silence about an uncovered area, because nobody asked
- Severity chosen to match how much time is left
- Running only the new suite because the old one is slow
- Accepting "it works on my machine" as evidence of anything
- A verdict of "compatible addition" issued without checking who consumes the contract

## 17. Questions I Must Ask

- How would I know if this were broken?
- What is the cheapest input that would falsify this?
- Which older behaviour could this plausibly have broken?
- Is this number confirmed by a path that shares no code with the one that produced it?
- What am I not testing, and who has decided that is acceptable?
- If this ships wrong, who notices first — us, or a user?

## 18. Method Discipline — Object Calisthenics, applied to tests

QA works on a **defined** artefact — the suite is code, and it rots the same way.

| Rule | Applied to a test |
|---|---|
| One level of indentation | A test with nesting is describing two scenarios |
| No `else` | **No conditional at all.** A branch in a test means it does not know what it expects |
| Do not abbreviate | The name states the scenario and the outcome, in full |
| First-class collections | Test data comes from a named builder, not an inline literal repeated six times |
| Keep entities small | One reason to fail per test |

**One reason to fail** is the rule that carries the others. A test with two assertions about
unrelated things reports the first failure and hides the second, and the hidden one is
usually the interesting half.

Two additions specific to this role:

- **A test is written to fail first.** A test that has never been seen red is an assertion
  about the author's intent, not about the system.
- **A test names the norm it defends** where one exists, so deleting the test and deleting
  the rule are visibly the same act.
