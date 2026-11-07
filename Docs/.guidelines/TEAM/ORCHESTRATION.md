---
artifact: ORCHESTRATION
nome_completo: Agent & Subagent Orchestration
versao: 1.0
tipo: definicao_de_processo
relacionados: [PHASES.md, GRAND_PLAN.md, MINIMUM_PLAN.md, PO.md, DV.md, QA.md, DA.md]
---

# Orchestration — Agents over the Planning Vertical

> The vertical already is an orchestration spec. This document only makes that explicit.

## 0. The Premise

Nothing here invents new structure. `MAX → MIN → L# → S# → F# → Rule A/B → file`
is already a decomposition of work into units with declared inputs, declared
outputs and declared boundaries. That is the same thing an agent topology needs.

The single design constraint that governs everything below:

> **Human review capacity is the binding resource.** Agents make production
> cheap and review expensive. An orchestration that produces more output than
> can be reviewed has not increased throughput — it has moved the queue.

Therefore the objective is not *more agents*. It is **review compression**: the
system must make a unit of work verifiable without reading all of it.

## 1. What Each Level Becomes

| Level | Nature | Orchestration meaning | Who may author |
|---|---|---|---|
| **MAX** | Ambition | Never assigned. Context only. | Human |
| **MIN** | Sufficiency | The termination condition of the whole system | Human + PO |
| **L#** | Decoupling | **Unit of parallelism** — one isolated workspace each | PO |
| **S#** | Time | **Unit of agent invocation** — one session, one commit | PO, refined by DV |
| **F#** | Depth | **Unit of role assignment** — which role owns this layer | Fixed by PHASES.md |
| **Rule A/B** | Legality | **Unit of automated verification** — the gate before any human looks | Machine |
| **file** | Artifact | The reviewable object | Agent |

Read downward, each level narrows what the next one is allowed to decide. That
narrowing *is* the safety property. An agent operating at S# cannot renegotiate
L#; an agent operating at F2 cannot invent an F0 norm.

### 1.1 MAX is never given to an agent

MAX is the ideal that will not be built. It exists to make MIN defensible by
contrast. Given to an agent it produces scope inflation with no counterweight,
because the agent has no cost model and no stake in the maintenance burden.

MAX may be *quoted* to an agent as rationale. It may never be the target.

### 1.2 MIN is the only global termination condition

Every agent run terminates on its step. Only the human stops the system, and
MIN is what tells them when. Without MIN there is no completion, only fatigue.

### 1.3 L# is where parallelism is legal — and only there

Ladders are decoupled by construction: separate modules, separate business
rules. That is exactly the precondition for concurrent agents without merge
pathology. One workspace per ladder, one branch per ladder.

**Two agents inside the same ladder is a false economy.** They will contend on
the same F1 vocabulary, and the reconciliation cost exceeds the parallel gain.
If a ladder feels big enough to need two agents, it is two ladders that have not
been separated yet.

### 1.4 S# is the session boundary

One step, one agent session, one commit. This is the most load-bearing mapping
in the document, and it holds for a practical reason: **the commit is the review
object.** If a session produces two commits, review granularity has decoupled
from planning granularity and the plan stops describing the repository.

A reopened step is a new session labelled as a reopening, not a continuation of
the old context. Fresh context, explicit statement of what is being re-entered.

### 1.5 F# is the role assignment axis

Phases are not sequential tasks handed down a chain. They are *depths*, and each
depth has an owner. A step declares a span — `S7 [F1–F3]` — and the span
determines which roles participate.

### 1.6 Rules A and B are the gate

Rule A (declaration legality) and Rule B (reference direction) are mechanical.
They run before a human or a QA agent sees anything. Their purpose in an agent
context is different from their purpose in a human context:

For humans they prevent drift. For agents they are **the compression
mechanism** — a machine-checkable claim that the output sits where it says it
sits, which removes the largest category of review work from the human.

An agent whose output fails Rule A or B is not reviewed. It is returned.

### 1.7 The file is the only thing that crosses

Agents do not hand each other reasoning, plans or intentions. They hand each
other **artifacts**: a manifest, a signature, a bug report, a test.

This is not a stylistic preference. Reasoning transferred between agents is
unverifiable and accumulates error silently. An artifact is checkable.

## 2. Roles as Agents

The four role documents are already agent specifications. They declare identity,
scope, in/out contracts with acceptance conditions, DoD, anti-patterns. That is
a complete interface. Reuse it directly — do not write parallel "agent prompts"
that will drift from the role docs.

### 2.1 Phase ownership

| F | Owner | Contributors | Never |
|---|---|---|---|
| **F0** Norm | **Human + PO** | QA (ambiguity), DA (measurability) | Any agent authoring alone |
| **F1** Contract | DV | DA (`Ev` / `Cmd` shape), QA (testability) | PO deciding types |
| **F2** Rule | DV | QA (coverage, before implementation) | PO, DA |
| **F3** Exposure | DV | QA, DA | PO widening it |
| **F4** Consumption | DV | QA | — |
| **F5** Surface | DV | QA, PO (acceptance) | DA |
| **F6** Closure | PO decides, DV+QA+DA supply | — | An agent self-accepting |

Two entries carry the weight.

**F0 is human-anchored.** An agent that may author norms will author whatever
norm justifies the code it already wrote. Norms are the appeal court; the
defendant does not staff it. Agents may *draft* a norm and *propose* an ID; a
human ratifies before any F2 cites it.

**F6 is never self-accepted.** The role that produced the increment does not
declare it done. PO accepts, on QA's risk opinion.

### 2.2 Context isolation is the point

The reason to use subagents is **not** speed. It is that each role must be
unable to see what would corrupt its judgment.

| Agent | Receives | Must not receive |
|---|---|---|
| PO | Norms, backlog, metrics, QA risk opinion | Implementation reasoning |
| DV | Story + acceptance criteria, PHASES.md, F3 manifest, prior code | The PO's internal priority debate |
| QA | Acceptance criteria, the artifact, the diff, the manifest | **DV's reasoning or justification** |
| DA | Business question + associated decision, schema, event spec | The desired answer |

The QA row is the critical one. A QA agent that reads the DV agent's
explanation of why the code is correct will agree with it. Agreement is the
default failure mode of language models reviewing language-model output. QA
receives **artifacts only** — the diff, the tests, the acceptance criteria, the
lint result. Never the narrative.

The DA row is the same failure in a different costume: an analysis agent told
which result is hoped for will find it.

### 2.3 The orchestrator is not a role

The orchestrator selects the ladder, sequences steps, dispatches role agents,
runs the gate and routes returns. It has no opinion on product, code, quality or
data. It owns no phase and authors no artifact.

Keeping the orchestrator opinionless matters because an orchestrator that starts
making product judgments has become an unaccountable fifth role, outside the
contract tables, invisible to the DoD.

## 3. The Workflow

### 3.1 Topology

```
                       MIN  (termination condition, human-held)
                        │
        ┌───────────────┼───────────────┐
        │               │               │
       L1              L2              L3          ← parallel, isolated workspaces
        │               │               │
   S1 S2 S3 …      S1 S2 …         S1 …            ← serial within a ladder
        │
   ┌────┴────┐
   │  STEP   │
   └────┬────┘
        │
   span [F1–F3]
        │
   ┌────┴──────────────────────────────┐
   │  PO → DV → QA → PO                │  ← role agents, artifact-passing only
   └────┬──────────────────────────────┘
        │
   GATE: Rule A, Rule B, tests, manifest diff
        │
   HUMAN REVIEW  ← the scarce resource
        │
   one commit
```

### 3.2 The step cycle

**1 · Frame (PO).** Emits the story: problem, acceptance criteria, out-of-scope,
the declared F-span, and the citation of the F0 norm it serves. Rejected if the
span is unstated or the norm is uncited.

**2 · Challenge (QA).** Reads the criteria *before* implementation and returns
ambiguities. This is the cheapest defect removal in the cycle and the one most
often skipped. Ambiguity found here costs a sentence; found at F5 it costs a
step.

**3 · Shape (DA, conditional).** If the step touches `Ev`, `Cmd` or anything
measured, DA specifies name, trigger, payload, types and obligation *before* DV
implements. Instrumentation retrofitted after the fact produces data with no
history.

**4 · Build (DV).** Implements strictly inside the declared span. Any need to
exceed the span **halts the step** and returns to PO — it does not silently
widen. Span violation is the primary containment failure of agent coding.

**5 · Gate (machine).** Rule A, Rule B, tests, F3 manifest diff. Failure returns
to DV without consuming QA or human attention. This is the review-compression
step; everything about the system's economics depends on it being strict.

**6 · Verify (QA).** Artifacts only. Emits a **risk opinion**, not a verdict:
open defects, severity, and explicitly *what was not covered*. "Approved" is
not a QA output. "Approved with residual risk X" is.

**7 · Accept (PO).** Decides on the risk opinion. May accept with known
defects — that is a legitimate PO decision, and recording it is the point.

**8 · Close (F6).** Changelog, docs, backlog delta, manifest change if the
export set widened. Then the commit.

### 3.3 What the human actually reviews

Not the code. The **review packet**:

- the F-span declared vs. the F-span actually touched
- the gate result
- the F3 manifest diff — usually empty, and *loud* when not
- the QA risk opinion
- the diff itself, read last and selectively

If the packet is trustworthy, most steps are approved from the first four items.
That is the throughput mechanism. When the packet stops being trustworthy, the
correct response is to tighten the gate, not to read more code — reading more
code does not scale and the agents will outproduce it.

## 4. Failure Modes

| Failure | Signal | Countermeasure |
|---|---|---|
| Span creep | Diff touches phases outside the declared span | Gate check: declared span vs. touched phases |
| Norm invention | An `AR#`/`BR#` appears with no human ratification | F0 requires a human signature |
| Sycophantic review | QA approves everything | QA never receives DV reasoning; track escape rate |
| Self-acceptance | Producing role closes its own step | F6 requires a different role |
| Ladder contention | Two agents editing the same F1 vocabulary | One agent per ladder, always |
| Silent manifest widening | F3 grows inside an F4 commit | Manifest diff in every packet |
| Context bleed | An agent cites information it was never given | Isolated contexts; treat as a containment bug |
| Review debt | Merged-unreviewed backlog grows | **Stop dispatching.** The queue is the ceiling. |
| Plan drift | Commits stop matching steps | One session, one commit, no exceptions |

The last two are the ones that actually kill the system. The rest are noisy and
therefore survivable.

## 5. Definition of Done — one orchestrated step

- [ ] Step cites an F0 norm ratified by a human
- [ ] F-span declared before dispatch
- [ ] QA challenged the criteria before implementation
- [ ] DA specified instrumentation before DV, where applicable
- [ ] Touched phases ⊆ declared span
- [ ] Rule A and Rule B pass
- [ ] Tests pass; a failing test preceded any F2 change
- [ ] F3 manifest diff is empty or explicitly declared
- [ ] QA risk opinion issued, naming what was **not** covered
- [ ] PO accepted, with any known defects recorded
- [ ] Human reviewed the packet
- [ ] Exactly one commit

## 6. Health Metrics

- **Review latency** — dispatch → human decision. The real throughput number.
- **Merged-unreviewed count** — must trend to zero. If it rises, dispatch less.
- **Span violation rate** — steps touching undeclared phases.
- **QA escape rate** — defects that passed QA and failed at human review or in
  production. Rising means the QA agent has gone sycophantic.
- **Gate return rate** — high is fine and cheap; it means the gate is working.
- **Steps per ladder before integration** — long runs hide divergence.
- **Ratio of norms drafted by agents to norms ratified by humans** — a rising
  ratio means the appeal court is being packed.

## 7. Anti-Patterns

- Giving an agent MAX as a target
- Parallelising inside a ladder
- Letting an agent author and cite its own norm
- Passing reasoning between agents instead of artifacts
- Showing QA the implementer's justification
- Telling DA the hoped-for answer
- An orchestrator that develops product opinions
- A QA verdict of "approved" with no residual risk statement
- Widening F3 as a side effect
- Scaling agent count while review latency is already climbing
- Reopening a step by continuing the old session context
- Writing agent prompts that duplicate the role docs and then diverge from them

## 8. Adoption Order

Do not start with the topology. Start with the gate.

1. **Gate first, no agents.** Rule A, Rule B, manifest diff, span check, running
   by hand. If the gate is not trustworthy, agents only produce faster garbage.
2. **One role.** DV only, human as PO/QA/DA. Learn where span creep happens.
3. **Add QA, isolated.** Artifacts only. Watch the escape rate; if it is zero
   across several steps, suspect sycophancy rather than celebrate.
4. **Add DA** when instrumentation retrofits start hurting.
5. **Parallelise ladders** last, and only after review latency has been stable
   at one ladder.

Each stage must hold its metrics before the next is added. The system's ceiling
is review capacity at every stage; adding agents before the gate is strict just
raises the queue.
