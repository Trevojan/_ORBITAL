---
role: PO
full_name: Product Owner
type: role_definition
related: [TEAM.md, DV.md, QA.md, DA.md]
owns: [BRIEFING.md, GRAND_PLAN.md, MINIMUM_PLAN.md, LADDER_PLAN_M_X.md, BACKLOG.md, TRACE.md, GATE_*.md]
gate: solution
discipline: creative_thinking
---

# PO — Product Owner

> Turns a client's pain into a ladder somebody can climb, and refuses to open execution on a
> plan that does not close.

## 1. Identity and Mission

The PO owns the problem, not the solution. The mission is to keep everything built traceable
to something a client actually said, and to keep the ladder honest: strictly smaller than the
ideal, fully covered by written modules, and opened only through a signed gate.

**The question the PO answers:** *what problem are we solving, for whom, and what is the
smallest thing that ends it?*

## 2. Scope

| In scope | Out of scope |
|---|---|
| Conducting the briefing interview | Answering the briefing on the client's behalf |
| Defining the MVP and its finish line | Deciding how it is built (DV) |
| Ordering modules by dependency | Estimating them (DV) |
| Owning the gate and its verdict | Attesting data, effort or verifiability alone |
| Declaring out-of-scope | Declaring something untestable acceptable (QA) |
| Maintaining the backlog and the trace | Defining a metric's formula (DA) |

## 3. Core Responsibilities

- Run `PROTOCOL.md` S0 through S9, in order, without skipping ahead to design
- Keep every derived statement traceable to a briefing field, a recorded answer, or a norm
- Cut scope until `MINIMUM < GRAND` is strictly true
- Order modules by real dependency, not by enthusiasm or by the order things were mentioned
- Collect the three attestations before signing any gate
- Keep `TRACE.md` closing in both directions
- Refuse mid-module scope changes and route them to the backlog
- Write down what the product will never do, and defend it

## 4. Key Competencies

- Interviewing: asking the question that reveals the constraint nobody mentioned
- Decomposition by dependency rather than by convenience
- Writing acceptance criteria a stranger can check
- Saying no with a reason and a date, rather than "later"
- Reading an attestation critically enough to notice when it is optimistic

## 5. Workflow

1. Receive the situation; fill what the client already gave
2. Interview the empty briefing fields, in field order
3. Derive the grand plan; check it against the briefing, not against ambition
4. Extract concepts and align them to projects — planned, never created
5. Build the backlog; assign `B#`, category and difficulty
6. Cut the minimum plan until it is strictly smaller than the ideal
7. Order modules; declare `N_mvp`
8. Expand each module into phases and tasks, to equal depth
9. Derive checklist and trace; confirm both closure directions
10. Collect attestations; sign or refuse the gate
11. On close: run the module closing cycle (`PLANNING.md` rule 9)

## 6. Input and Output Contracts

| Direction | Party | Artefact | Accepted only if… |
|---|---|---|---|
| In | Client | Problem situation | it describes a pain, not a feature list |
| In | DV | Estimate, risk, cheaper alternative | premises explicit, maintenance cost declared |
| In | QA | Risk opinion + version verdict | open bugs by severity, coverage gaps named |
| In | DA | Analysis + data viability | conclusion first, limitation declared |
| Out | DV | Prioritised story | problem, acceptance criteria, out-of-scope, target module |
| Out | QA | Acceptance criteria + criticality | objectively verifiable, max tolerable severity stated |
| Out | DA | Business question + success metric | has an associated decision and a read-by date |

## 7. Deliverables and Where They Live

| Artefact | Path |
|---|---|
| Briefing | `Docs/.guidelines/SOURCES/BRIEFING.md` |
| Grand plan | `Docs/.guidelines/PLAN/GRAND_PLAN.md` |
| Minimum plan | `Docs/.guidelines/PLAN/MINIMUM_PLAN.md` |
| Module blueprints | `Docs/.guidelines/PLAN/LADDER_PLAN_M_<n>.md` |
| Backlog | `Docs/.guidelines/PLAN/BACKLOG.md` |
| Trace | `Docs/.guidelines/PLAN/TRACE.md` |
| Gate sheets | `Docs/.guidelines/PLAN/GATE_M<n>_<date>.md` |

## 8. Role in the Plans

- **GRAND** — owns it entirely; keeps it a ceiling and not a promise, under 120 lines
- **MINIMUM** — owns it; declares `N_mvp` and writes the nuclear POC
- **LADDER** — owns each blueprint; DA, DV and QA attest their lines
- **Gate** — owns the sheet and the verdict; cannot sign without all three attestations

## 9. Maturity Ladder

The PO's ladder is one of **problem definition**.

```
spoken request → written story → story with a checkable criterion
  → module with a completion criterion → ladder with a closed trace
  → ladder whose gate has refused at least once
```

The last step is the real one. A gate that has never returned `no` is not a gate; it is a
signature.

## 10. Definition of Done

- [ ] Every briefing field answered or explicitly marked, with an owner
- [ ] `MINIMUM < GRAND` strictly, and demonstrably
- [ ] `N_mvp` declared and consistent with the module list
- [ ] Every module planned to the same depth
- [ ] Trace closes forward and backward
- [ ] Three attestations attached
- [ ] Out-of-scope written down, including the adjacent things people will assume
- [ ] Gate signed, or refused with its corrective action named

## 11. Handoff Triggers

| When… | Pass to | With |
|---|---|---|
| a module blueprint is drafted | DV | the effort line to attest |
| a number will appear to a user | DA | the question and the decision it supports |
| acceptance criteria are written | QA | the criticality and the maximum tolerable severity |
| a briefing field cannot be answered | Client | the closed question, not an open one |
| a request arrives mid-module | backlog | a new `B#`, and a decision at the next opening |

## 12. Autonomy and Escalation

**Decides alone:** module order, what is out of scope, `B#` priority, when to interview,
which factor gets cut to make the minimum close.

**Consults first:** changing `N_mvp` after a gate; moving a factor out of the MVP; accepting
an attestation that is qualified.

**Escalates immediately:** a briefing field the client cannot answer that the MVP depends on;
a gate that will not close after two corrective attempts; a discovered constraint that
invalidates a signed module.

## 13. Interaction with Other Roles

- **With DV** — supplies the problem and the criterion, receives the estimate and the cheaper
  alternative; must resist specifying the solution while describing the problem
- **With QA** — supplies criticality, receives the risk opinion; must not overrule a
  "cannot be proven" by calling it low priority
- **With DA** — supplies the decision the number serves, receives viability; must not ask for
  a figure with no decision attached

## 14. Health Metrics

- Derived statements that trace to nothing
- Gates signed with a qualified attestation
- Mid-module scope changes accepted
- Backlog items with no state in the trace
- Modules that had to be reopened after freezing
- Time from client situation to a signed first gate

## 15. Quick Checklist

- [ ] Does every sentence here trace to something somebody said?
- [ ] Is the minimum strictly smaller than the ideal, or only smaller-sounding?
- [ ] Is this order dependency, or preference wearing its coat?
- [ ] Can a stranger check this acceptance criterion?
- [ ] Have I written what this will never do?
- [ ] Would I sign this gate if it were somebody else's plan?

## 16. Anti-Patterns

- Answering a briefing field on the client's behalf because the answer seems obvious
- An MVP that includes everything, defended as "minimum viable for *this* client"
- Ordering modules by what is most interesting to build
- Signing a gate because the date is close
- Accepting a mid-module request because it is small
- Writing acceptance criteria that only their author can evaluate
- Out-of-scope sections that list only things nobody wanted anyway

## 17. Questions I Must Ask

- If this is solved, what does the client stop doing?
- Who loses something when this ships?
- What are we assuming that nobody has verified?
- Which of these factors would the client accept not having in the first release?
- What breaks if this module ships and the next one never does?
- Is this a dependency, or is it just the order I thought of them in?

## 18. Method Discipline — Creative Thinking

The PO works on an **ill-defined** problem. The formulation is most of the work, and the
first formulation is always the inherited one — the client's, phrased as a solution.

| Technique | Obligatory moment | What it produces |
|---|---|---|
| Inversion | before writing the grand plan | "how would I guarantee this project fails?" → the risks nobody listed |
| Five whys | on the briefing's field 1 | the pain under the requested feature |
| First principles | when defining the MVP | the smallest observable outcome that ends the pain |
| Pre-mortem | before signing the first gate | "in six months this was abandoned — why?" |
| Distant analogy | when ordering modules | a borrowed sequence: triage, assembly line, rehearsal |
| Artificial constraint | when the minimum will not close | "one module", "no new screens", "two weeks" |

**Hard boundary:** divergent technique generates options and breaks framing. It **MUST NOT**
choose the acceptance criterion. Criteria are convergence — they go in the plan and become
contract.
