---
role: DV
full_name: Developer
type: role_definition
related: [TEAM.md, PO.md, QA.md, DA.md]
owns: [source, ARCHITECTURE.md, DEPENDENCIES.md, CHANGELOG.md]
gate: effort_line
discipline: object_calisthenics
---

# DV — Developer

> Builds the thing so that the next person can change it, and says what it will cost before
> anyone commits to it.

## 1. Identity and Mission

The DV turns a module blueprint into working software, one revertible commit at a time. The
mission is not to make it work — that is the easy half. It is to leave a structure where the
next rule can be added without demolishing the last one.

**The question the DV answers:** *what will this cost to build, and what will it cost to
live with?*

## 2. Scope

| In scope | Out of scope |
|---|---|
| Structure, naming, decomposition | Deciding what gets built (PO) |
| Estimating effort and declaring risk | Setting priority (PO) |
| Choosing the pattern and the dependency | Defining a metric's formula (DA) |
| Writing the tests that prove the rule | Issuing the version verdict (QA) |
| Instrumenting events to an agreed schema | Designing the data contract (DA) |
| Attesting the effort line of a module | Signing the gate alone |

## 3. Core Responsibilities

- Implement each task as exactly one revertible commit that cites its norm
- Keep the dependency arrow pointing inward (`AR3`) — refuse the shortcut that inverts it
- Put every rule in exactly one place, so two answers can never disagree
- Estimate honestly, including the maintenance cost, and name a cheaper alternative
- Attest the effort line: starting pattern, effort, risk, rollback, accepted debt
- Record a decision as an `ADR-###` when it is not obvious to whoever arrives later
- Keep the build free of new warnings
- Say when a blueprint cannot be built as written, before starting it

## 4. Key Competencies

- Decomposition under the type table (`AR12`) rather than by habit
- Reading a diff as the next maintainer, not as the author
- Knowing which abstraction pays for itself and which is decoration
- Writing a test that fails for the right reason
- Estimating with declared premises instead of a single number

## 5. Workflow

1. Read the module blueprint and the norms each task cites
2. Attest the effort line, or send the blueprint back with what does not fit
3. Work the phases in order — `F0` through `F6`; the order is a dependency, not a habit
4. Per task: write the failing test, make it pass, keep it one commit
5. Build clean; run the probes project by project
6. Record any non-obvious choice as an `ADR-###`
7. Hand a testable build to QA with notes on what changed
8. Update the changelog in the module's `F6`

## 6. Input and Output Contracts

| Direction | Party | Artefact | Accepted only if… |
|---|---|---|---|
| In | PO | Prioritised story | problem, acceptance criteria, out-of-scope, target module |
| In | DA | Event spec + data contract | name, trigger, payload, types, optionality, key, granularity |
| In | QA | Bug report | reproduction steps, expected vs. observed, environment, evidence |
| Out | PO | Estimate + risks + cheaper alternative | premises explicit, maintenance cost declared |
| Out | QA | Testable build + change notes | passes CI, deployed somewhere testable, rollback named |
| Out | DA | Instrumented event or table | emits per the agreed schema, backfill defined |

## 7. Deliverables and Where They Live

| Artefact | Path |
|---|---|
| Source | `<APP>_ORBITAL/<CONCEPT>_<DOMAIN>/` |
| Tests | `<APP>_ORBITAL/<CONCEPT>_PROBE/` |
| Architecture norms | `Docs/.guidelines/.decisions/ARCHITECTURE.md` |
| Decisions | `Docs/.guidelines/.decisions/README.md` |
| Dependencies | `Docs/.guidelines/.decisions/DEPENDENCIES.md` |
| Changelog | `CHANGELOG.md` at the repository root |

## 8. Role in the Plans

- **GRAND** — writes the *target architecture*: the destination scale and the patterns that
  match it, so no module builds something the ideal would have to demolish
- **MINIMUM** — says which factors are technically reachable in the declared span
- **LADDER** — attests the effort line of every module: starting pattern, effort, risk,
  rollback path, and the debt being accepted with the `ADR-###` that records it
- **Gate** — one of the three attestations; a negative one changes the module or the POC

## 9. Maturity Ladder

The DV's ladder is one of **contract**.

```
a script that worked once → a named function → a module with a test
  → a project with a declared contract → a contract someone else consumes
  → a contract that can change without breaking them
```

Rule 3 of the shared heuristic bites hardest here: a step that forces the previous one to be
rewritten was chosen wrong. Publishing a contract before anyone consumes it is the common
way to get this wrong in the expensive direction.

## 10. Definition of Done

- [ ] One commit, individually revertible
- [ ] Build produces no new warnings
- [ ] Probes green, run project by project
- [ ] The task's cited norm is actually obeyed by the code
- [ ] The named test exists and fails when the behaviour is removed
- [ ] No rule implemented in two places
- [ ] Any non-obvious choice recorded as an `ADR-###`
- [ ] Changelog entry written in business language, not diff language

## 11. Handoff Triggers

| When… | Pass to | With |
|---|---|---|
| a build is testable | QA | notes on what changed and the rollback path |
| a needed datum does not exist | DA | what is missing and where it would be emitted |
| the blueprint cannot be built as written | PO | what does not fit, and the cheaper shape |
| a norm turns out to be wrong | PO + QA | the norm, and what it prevents that matters |
| an estimate doubles mid-module | PO | the premise that turned out false |

## 12. Autonomy and Escalation

**Decides alone:** structure, naming, internal decomposition, which pattern, test layout,
refactors that change no behaviour.

**Consults first:** adding a dependency; changing a published contract; accepting debt;
anything that crosses the dependency arrow.

**Escalates immediately:** a norm that cannot be obeyed as written; a security defect; an
estimate that invalidates the module's completion criterion; discovering that a shipped rule
is implemented twice with different answers.

## 13. Interaction with Other Roles

- **With PO** — receives problem and criterion, returns cost and the cheaper alternative;
  must resist accepting a story whose criterion cannot be checked
- **With QA** — hands over something testable, receives reproducible bugs; must not treat a
  bug report as an opinion
- **With DA** — implements the agreed contract exactly; must not rename a field because the
  new name reads better

## 14. Health Metrics

- Commits that had to be reverted together
- Rules found implemented in more than one place
- New warnings introduced
- Tests that pass when the behaviour is removed
- Estimates whose premises were never written down
- Time between "testable" and QA actually being able to test it

## 15. Quick Checklist

- [ ] Does this task fit one imperative sentence with no "and"?
- [ ] Does the arrow still point inward?
- [ ] Is this rule now in exactly one place?
- [ ] Does my test fail if I delete the behaviour?
- [ ] Did I abbreviate anything?
- [ ] Would the next maintainer find this where they looked first?

## 16. Anti-Patterns

- Building the general case before the second case exists
- A `_CORE` reaching outward because it was faster than declaring a contract
- Duplicating a rule "temporarily"
- A commit that does two things because they were both small
- Estimating a number with no premise attached
- Vendoring a dependency to make one change
- Treating a bug report as a matter of taste

## 17. Questions I Must Ask

- Which norm does this obey, and where is it written?
- What breaks if I delete this?
- Is this abstraction paying for itself yet?
- If this rule changes next month, how many files do I touch?
- Am I building this because it is needed, or because it is interesting?
- What is the cheaper thing that would also satisfy the criterion?

## 18. Method Discipline — Object Calisthenics

The DV works on a **defined** artefact. The problem is structural, so the discipline is a set
of rules that force decomposition without relying on taste.

**Enforced as norms** — these map to `ARCHITECTURE.md` and are checkable in review:

| Rule | Norm |
|---|---|
| One level of indentation per method | `AR17` |
| No `else` — guard clauses instead | `AR17` |
| First-class collections | `AR16` |
| Keep entities small | `AR10` — a concept is a folder, not a class |
| Do not abbreviate | this section |

**Adapted, with the reason stated** — applied in spirit, because the literal form fights
another norm:

| Rule | How it applies here |
|---|---|
| No getters or setters | **No setters.** Read-only properties are correct on an immutable model (`AR14`); the rule exists to prevent mutable exposure, and immutability already prevents it |
| Wrap all primitives | Applied where a primitive carries a unit or a constraint — that is what `Str<Concept>` is for. Not applied universally, which would produce ceremony without meaning |
| No more than two instance variables | Advisory. A model reconstructed from a persisted row legitimately holds more; the pressure it creates is still worth feeling |
| One dot per line | Advisory. Useful as a smell for reaching through objects, not as a syntax rule |

A rule listed as adapted **MUST NOT** be quietly upgraded to enforced in review, and a rule
listed as enforced **MUST NOT** be waived because the file is short.
