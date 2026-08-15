# PROTOCOL — from a problem statement to a ladder

This is the derivation algorithm. It takes one input — a client's problem situation — and
produces everything else: the ideal, the backlog, the modules, the phases, the tasks and
the gate that decides whether execution may open.

It exists so that starting a project stops being an improvised conversation. Run it in
order. Each step declares what it consumes, what it produces, and the condition that must
hold before the next step may start.

---

## The tree

The shape of the whole journey, and of every task inside it. **We start at the root and we
must reach the fruit.**

```
                    ╭─── Fruit ───╮        the goal
                    │  🍎  🍎  🍎  │
                 ╭──┴─────────────┴──╮
                 │      Leaves       │     the actions
                 ╰────────┬──────────╯
                  ╭───────┴───────╮
                  │   Branches    │        the resources
                  ╰───────┬───────╯
                          │
                       Trunk                the effort
                          │
              ════════════╪════════════
                        Root               the problem
                          ▲
                     Fertilizer            more context for the root
```

| Part | System scale | Task scale (`T#`) |
|---|---|---|
| **Root** | the client's problem — `SOURCES/BRIEFING.md` | why this task exists: the norm it obeys |
| **Fertilizer** | context added to deepen the root — interview answers, new facts | clarification that refines the task before it starts |
| **Trunk** | the effort — the ladder `M1..Mn` | the single commit |
| **Branches** | the resources — projects and files | the files this task touches |
| **Leaves** | the actions | the concrete edits |
| **Fruit** | the goal — the deliverable | the done state, proven by a named test |

Two criteria fall straight out of it:

- **A task whose root is unnamed has no acceptance criterion.** That is why every task
  cites a norm.
- **A task with no fruit is not a task.** That is why every task names its test.

The same five beats govern a single method, one scale further down — see `ARCHITECTURE.md`
AR19. Root is the Statement, fertilizer the Arguments, branch the Dependency, leaves the
Operation, fruit the Goal.

---

## The steps

| Step | From → To | Stop condition |
|---|---|---|
| **S0** | client situation → `SOURCES/BRIEFING.md` | **blocking** — no field unanswered or unmarked |
| **S1** | briefing → `PLAN/GRAND_PLAN.md` | ≤120 lines; every section present |
| **S2** | domain nouns → concepts → projects + `AR#` | every concept resolved; **nothing created** |
| **S3** | scope → `PLAN/BACKLOG.md` | every item has `B#`, category, `D1`–`D3` |
| **S4** | backlog → `PLAN/MINIMUM_PLAN.md` | ≤90 lines; `N_mvp` declared; nuclear POC written |
| **S5** | backlog → modules `M1..Mn` | ordered by real dependency |
| **S6** | each module → phases `F0..F6` | seven phases present; empty ones declared empty |
| **S7** | each phase → tasks `T#` | every task meets the definition of done |
| **S8** | S5–S7 → `CHECKLIST.md`, `TRACE.md` | trace closes both ways |
| **S9** | all → `PLAN/GATE_M1_<date>.md` | `SOLUTION: yes` opens execution |

---

### S0 — Briefing

**Consumes** whatever the client said, however unstructured.
**Produces** `SOURCES/BRIEFING.md` with all eleven fields answered or explicitly marked.

Fill in what was given. Then interview the gaps **in field order**, one at a time, without
skipping ahead to design. An unanswered field becomes either `[TBD — owner: <name>]` or a
candidate POC recorded in field 11.

> **MUST NOT** proceed to S1 while any field is blank. Nothing is invented silently — an
> assumption made here becomes a wrong module six steps later, and it will not look like an
> assumption by then. This is the fertilizer returning to the root.

---

### S1 — Grand plan

**Consumes** the briefing. **Produces** `PLAN/GRAND_PLAN.md`, at most 120 lines.

The ideal system, with no schedule and no commitment. It is the **ceiling of coherence**:
nothing enters the ladder that contradicts it. It is not a promise of delivery.

Sections: problem, ideal summary, domain table, mature business rules, integrations and
target scale, non-functional requirements that may never regress, mature data model, target
architecture, definition of a top-line deliverable, and what this product will never do.

> The line ceiling is numeric because an adjective — "keep it light" — does not survive
> haste. Content over the ceiling belongs to the ladder and moves there.

---

### S2 — Concepts and projects

**Consumes** the domain section of the grand plan. **Produces** the concept list, each one
mapped to a `<CONCEPT>_<DOMAIN>` project and resolved through the type table, plus any new
`AR#` the app's shape requires.

This is the concept alignment the template exists for.

1. Extract the **nouns** of the domain. A noun with behaviour and identity is a concept; a
   noun that is only a value is a part of one.
2. Group concepts into apps. One app is one deployable thing.
3. Give each concept a home domain, using `ARCHITECTURE.md` AR2 and the arrow in AR3.
4. Run the type table (AR12) over each concept's parts to get its file set.

> **MUST NOT** create any project here. S2 *plans* them. Creating one is a task inside
> `M1.F1`, and AR9 requires explicit confirmation first.

---

### S3 — Backlog

**Consumes** the grand plan and the briefing's scope. **Produces** `PLAN/BACKLOG.md`.

Every functional and non-functional item becomes a row: `B#`, name, category, difficulty
`D1`–`D3`. Non-functional items are marked `[late]`.

The backlog is the register of **fact** — what exists, what is missing, what it costs. It
never holds a business rule; those live in `BUSINESS_RULES.md`.

---

### S4 — Minimum plan

**Consumes** the backlog and briefing field 4. **Produces** `PLAN/MINIMUM_PLAN.md`, at most
90 lines.

The shortest path that solves the client's problem end to end, for one user, with common
cases only. Ordered by dependency, never by preference — each factor exists because without
it the MVP does not close.

It **MUST** declare `N_mvp`, the number of modules the MVP spans, because the gate formula
consumes it. It **MUST** carry the nuclear POC with its five fields: unproven risk, the
closed question it answers, how it is answered, discard criterion, and what changes if the
answer is no.

---

### S5 — Modules

**Consumes** the backlog and the minimum plan. **Produces** the module list `M1..Mn`.

A module is a **decoupled business rule** — one step of the ladder, coherent, deliverable
and reversible, that does not force the previous step to be rewritten.

Order by real dependency. The first module is the one with no input dependency. Preference,
enthusiasm and the order things were mentioned are not orderings.

> **Non-functional requirements never become modules.** They anchor to F0 or F5 of the
> first module that would suffer without them. If no module would suffer, the requirement
> stays `[late]` in the backlog and never enters the ladder.

---

### S6 — Phases

**Consumes** each module. **Produces** its seven phases, `F0` through `F6` (AR20).

A phase with nothing in it declares itself empty. Omitting it hides a decision; declaring it
records one.

---

### S7 — Tasks

**Consumes** each phase. **Produces** its tasks, as rows:

| Task | Covers | Files | Function / signature | Norm | Test | Commit |
|---|---|---|---|---|---|---|
| `M1.F2.T03` | `B7` | path | signature | `AR17` | test name | commit subject |

**Definition of done for a task — all seven, no exceptions:**

1. It is exactly **one commit**.
2. The build produces **no new warnings**.
3. The `_PROBE` projects are **green**, run project by project.
4. It fits **one imperative sentence with no "and"**.
5. It is **individually revertible**.
6. It is always **`D1`** on the difficulty scale. A task that is not is two tasks.
7. It **cites a norm** and **names its test**.

What is written now: the path of each file, the name and signature of each function, the
norm that governs it, the name of the test that proves it, and the commit subject. What is
**not** written now: the body of the functions. Blueprint, not wall.

> Where the blueprint is necessarily less firm, it **MUST** say so rather than fake
> precision — the uncertainty is recorded as a POC in that module, with the question it
> answers.

---

### S8 — Checklist and trace

**Derived, never authored.** A line with no originating task is a process defect.

`CHECKLIST.md` is the ramp: markable, ordered topologically first, then by increasing
difficulty among whatever is unblocked, with `[late]` last. It carries the milestone
markers. It lives outside the ladder files because ticking a box means writing to a file,
and a frozen ladder is not rewritten — **the ladder is what was decided, the checklist is
what was done.**

`TRACE.md` **MUST** close in both directions:

- **Forward** — one line per backlog item, each with exactly one of `covered` (naming the
  task), `deferred` (naming the target module), `off-ladder` (with a reason) or `dropped`
  (with a reason). No empty cell.
- **Backward** — every task declares an origin: a `B#`, `origin: GRAND`, or `origin: norm`.

The backward direction is the one that earns its keep: it catches work promised in prose
that never became a backlog item.

---

### S9 — Gate

**Consumes** everything. **Produces** `PLAN/GATE_M1_<date>.md`.

The formula lives in `PLAN/PLANNING.md` and is stated **there and nowhere else**. Verify
its two conditions, attach the three attestations, and record the verdict.

`SOLUTION: yes` opens execution at `M1.F0.T01`.
`SOLUTION: no` does not open anything — take the corrective action the sheet names and
return to S4 or S6.

---

## Rules that do not bend

1. **Nothing is invented silently.** Every fact in every derived document traces to a
   briefing field, a client answer, or a norm. If it traces to none of those, it is a
   guess, and it gets a `[TBD]` and an owner instead.
2. **No project is created without explicit confirmation** — AR9.
3. **The blueprint is written before the first wall.** All modules are planned to the same
   depth before execution opens. What waits for its moment is the content, not the plan.
4. **A module in execution does not change scope.** A request arriving mid-module goes to
   the backlog with a new `B#` and is decided at the next module's opening. Without this,
   "the effort lives in the ladder" becomes "the ladder absorbs whatever appears".
5. **A module freezes only when its gate is signed** `SOLUTION: yes`. Before that it is a
   blueprint, and a blueprint is corrected by whatever the earlier modules taught.

## When a step fails

| Symptom | Cause | Action |
|---|---|---|
| A briefing field cannot be answered | The client does not know | Mark `[TBD]` with an owner, or move it to field 11 as a POC. Do not guess. |
| The grand plan will not fit 120 lines | Execution detail leaked into the ideal | Move the excess to the module it belongs to. |
| The minimum plan covers everything | It was not sliced | Cut factors out to later modules until `MINIMUM < GRAND` is strictly true. |
| A task will not fit one sentence | It is two tasks | Split it. |
| A task cites no norm | The norm was never written | Go back to that module's F0 and write it. |
| The trace has an empty cell | An item was neither covered nor consciously excluded | Decide, then record the decision. |
| The gate returns `no` | Either side of the formula | Follow the corrective action on the sheet. Execution stays shut. |
