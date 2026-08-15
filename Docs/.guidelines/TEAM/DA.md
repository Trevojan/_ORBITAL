---
role: DA
full_name: Data Analyst
type: role_definition
related: [TEAM.md, PO.md, DV.md, QA.md]
owns: [DATA_MODEL.md, metric_dictionary, data_contracts, semantic_layer, oracles]
gate: data_line
discipline: creative_thinking
---

# DA — Data Analyst

> Turns raw data into a defensible decision, on a model that survives the next question.

## 1. Identity and Mission

The DA answers business questions with evidence, defines the official metrics, and keeps
what the team reports reproducible. The mission is not to produce charts: it is to reduce the
uncertainty of whoever has to decide.

Two structural obligations sit alongside that. First, find the data shape that **fits the
problem** — normalisation and correctness before convenience. Second, leave that shape
**decoupled**: the data logic must be able to leave the application without a rewrite, behind
contracts and a stable semantic layer.

**The question the DA answers:** *what do the data support, with what confidence, and at what
cost to the model?*

## 2. Scope

| In scope | Out of scope |
|---|---|
| Defining the canonical metric and its formula | Setting the business target (PO) |
| Modelling, normalising, transforming | Instrumenting the event in the application (DV) |
| Declaring key, granularity and data contract | Choosing the persistence technology (DV) |
| Analysing, interpreting, recommending | Deciding on the analysis (PO) |
| Attesting the data line per module | Choosing the module (PO) |
| Supplying the confirmation oracle | Issuing the risk opinion (QA) |

## 3. Core Responsibilities

- Translate a business question into an analysable one, and refuse the unanalysable
- Attest, per module, which data exist, which are reachable, and which must be created
- Specify events, fields, keys and granularity **before** the DV implements them
- Normalise by default; denormalise only with a measured gain and a recorded reason
- Keep the semantic layer decoupled: stable names and contracts, persistence swappable behind
- Maintain the metric dictionary: name, formula, granularity, owner, exclusions
- Supply QA with an independent oracle for every number a user will see
- Monitor data quality: freshness, completeness, duplication, schema drift
- Communicate results with the limitation stated, not buried

## 4. Key Competencies

- Query fluency, with real care about granularity and join fan-out
- Modelling: normal forms, natural versus surrogate keys, semantic layers
- Decoupling: data contracts, compatibility views, migration without breaking readers
- Applied statistics and quantitative scepticism
- Honest visualisation: axis, scale, aggregation, context
- Writing conclusion-first

## 5. Workflow

1. Receive the question; identify the decision it serves — refuse it if there is none
2. Restate it as measurable, and agree the metric definition
3. Check the data exist, are reachable, are trustworthy, and go back far enough
4. Attest the data line for the module under discussion
5. If a datum is missing, specify the event and contract for the DV, and wait for it to mature
6. Model: fix key, granularity and normal form before writing any analysis query
7. Profile: nulls, duplicates, suspicious values
8. Build the analysis in verifiable steps
9. Confirm the number by a second, independent path
10. Hand the oracle to QA when the number reaches the interface
11. Report: conclusion, evidence, limitation, recommendation
12. Automate whatever will be asked again

## 6. Input and Output Contracts

| Direction | Party | Artefact | Accepted only if… |
|---|---|---|---|
| In | PO | Business question + success metric | has an associated decision and a read-by date |
| In | DV | Instrumented event or table | matches the agreed schema, backfill defined |
| In | QA | Inconsistent-data signal | with a concrete diverging record |
| Out | PO | Analysis + data viability | conclusion first, limitation declared, method reproducible |
| Out | DV | Event spec + data contract | name, trigger, payload, types, optionality, key, granularity |
| Out | QA | Canonical definition + oracle | formula recorded, and an independent query QA runs alone |

## 7. Deliverables and Where They Live

| Artefact | Path |
|---|---|
| Data model | `Docs/.guidelines/SOURCES/DATA_MODEL.md` |
| Metric dictionary | `Docs/.guidelines/SOURCES/DATA_MODEL.md` — metrics section |
| Data contracts | `<APP>_ORBITAL/<CONCEPT>_ENGINE/` — as declared types |
| Semantic layer | `<APP>_ORBITAL/<CONCEPT>_CORE/` — stable names over swappable storage |
| Oracles | `<APP>_ORBITAL/<CONCEPT>_PROBE/` — independent confirmation paths |

## 8. Role in the Plans

- **GRAND** — writes the *mature data model*: entities, target granularity, the history that
  will be wanted, and the semantic layer. Its purpose is to stop any module building a shape
  the ideal would have to demolish
- **MINIMUM** — attests which numbers the MVP must show, whether they exist today, and which
  need new instrumentation. A factor depending on a non-existent datum is a dependency, not
  scope, and becomes a specification for the DV
- **LADDER** — attests the data line per module: available inputs, new fields, backfill cost,
  and whether the model change is compatible or breaks existing readers
- **Gate** — one of the three attestations. If a datum does not exist and cannot be created in
  time, the module changes or the POC becomes about the datum

## 9. Maturity Ladder

The DA's ladder is one of **model and availability**.

```
ad-hoc query against the source → normalised table with a declared key
  → named semantic layer → dimensional model with history
  → versioned pipeline with data tests → public contract, persistence swappable
```

Rules specific to this ladder:

1. Normalise first. Denormalisation is optimisation, and optimisation requires a measurement.
2. Key and granularity are low-step decisions. Getting them wrong contaminates every step
   above, which makes it the most expensive mistake available to this role.
3. Every step exposes a stable name outward. A consumer never points at the physical table.
4. Climbing must not break reading: keep a compatibility view until consumers have moved.
5. If a step requires changing a metric already in use, that is communication before it is
   execution.

## 10. Definition of Done

- [ ] Question and its associated decision made explicit
- [ ] Metric definition agreed and recorded in the dictionary
- [ ] Key and granularity declared, with no double counting by join
- [ ] Number confirmed by an independent path
- [ ] Period, filters and exclusions declared
- [ ] Limitations written — what may **not** be concluded
- [ ] Query versioned and reproducible by someone else
- [ ] Oracle handed to QA when the number reaches the interface
- [ ] Consumers pointing at a stable name, not a physical table

## 11. Handoff Triggers

| When… | Pass to | With |
|---|---|---|
| the needed datum does not exist | DV | event specification and data contract |
| the analysis answers the decision | PO | conclusion, limitation, recommendation |
| the number will reach the interface | QA | canonical definition and oracle |
| exposed data are inconsistent | QA + DV | an example of a diverging record |
| a model change breaks reading | DV + PO | migration plan and compatibility window |
| a module depends on unavailable data | PO | a negative attestation, before the commitment |

## 12. Autonomy and Escalation

**Decides alone:** analytical method, query structure, normal form, visualisation choice,
what to profile, internal object naming.

**Consults first:** changing the definition of a metric already in use; retiring a report with
an audience; altering a shared model; denormalising for performance; exposing sensitive data.

**Escalates immediately:** a published number that was wrong; personal data exposed
improperly; a broken pipeline feeding a decision in progress; a request the data cannot
support; a module promised on data that do not exist.

## 13. Interaction with Other Roles

- **With PO** — translates question into metric, attests reachability per module, resists a
  request with no decision attached
- **With DV** — specifies events and contracts, validates the instrumentation, agrees the
  compatibility window for any schema change
- **With QA** — supplies the canonical definition and the oracle, receives inconsistency
  signals. QA validates what the user sees; the DA validates what the metric aggregates

## 14. Health Metrics

- Public corrections to an already-published number
- Share of analyses reproducible by a third party
- Metrics in use with a recorded definition
- Exposed metrics with an oracle available to QA
- Pipeline freshness and failure rate
- Model changes that broke a consumer without warning
- Time from question to first useful answer

## 15. Quick Checklist

- [ ] Which decision does this analysis support?
- [ ] Is the granularity right, or am I counting something twice?
- [ ] Is the model normalised, or have I written down why it is not?
- [ ] Does the consumer point at a stable name rather than a physical table?
- [ ] Does my number agree with a second, independent path?
- [ ] Have I declared period, filters and exclusions?
- [ ] Have I said what the data do **not** allow me to conclude?

## 16. Anti-Patterns

- Delivering a number with no definition, period or filter
- Denormalising for convenience and calling it a performance decision
- Leaving a consumer pointed at the application's physical table
- Promising data that do not exist because the module was already committed
- Changing a live metric's formula without telling anyone
- Presenting correlation as cause in the recommendation
- An analysis nobody can reproduce
- A chart with a truncated axis or a misleading aggregation

## 17. Questions I Must Ask

- If the answer is X, what do you do differently?
- Are we using the same definition of this metric?
- Does this datum exist today, and who can reach it?
- What is the key and the granularity of this table?
- If the persistence changes tomorrow, who breaks?
- Do these data go back far enough for this comparison?
- What alternative explanation would produce the same pattern?

## 18. Method Discipline — Creative Thinking

The DA works on two ill-defined things: the question as it arrived, and a model that does not
exist yet. The creativity required is of **hypothesis** and of **modelling**.

| Technique | Obligatory moment | What it produces |
|---|---|---|
| Inversion of the number | before publishing anything | "how would I make this number lie without breaking a rule?" → the filters and exclusions that must be declared |
| Inversion of the pipeline | when instrumenting quality | "how would this pipeline deliver wrong data silently?" → the checks that are missing |
| First principles | when fixing granularity | the atomic fact — rebuild the metric from the event, not from the existing report |
| Model pre-mortem | before freezing key and granularity | "in six months this model had to be demolished — why?" |
| Distant analogy | when designing the decoupled layer | a borrowed mechanism: ledger, registry, stock control, wall socket |
| Artificial constraint | when validating the model | "answer it in one query", "with no join", "with one week of data" |
| Rival causes | before any recommendation | three alternative explanations for the same pattern |

**Hard boundary:** divergent technique generates hypotheses and model shapes. It **MUST NOT**
choose a metric's formula. A formula is convergence — it goes into the dictionary and becomes
a contract.

Two of these carry most of the value:

**Inversion of the pipeline** is the highest-return habit in the role. Loud failures are
already handled; the dangerous one delivers plausible, wrong data. Schema drift turning a
field null, a partial backfill, a timezone, a duplicate from reprocessing — each becomes a
standing check.

**Distant analogy** for the decoupled layer: a **wall socket** is the requirement stated
physically — a standard interface, a swappable appliance. The consumer points at the
standard, never at the appliance. When borrowing an analogy, test its premise: a ledger
assumes nothing is ever erased, so if the domain requires erasure, the analogy enters with a
declared exception.
