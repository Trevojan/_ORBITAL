# ARCHITECTURE — conventions and norms

Domain-Driven Design at the base, strong Separation of Concerns in the extensions. The
goal is not cleverness. It is that any file's purpose is obvious from its name and its
folder, and that two people decomposing the same problem produce the same shape.

## How to read this file

Every rule here has an id — `AR1`, `AR2`, … — and every task in the ladder cites the norm
it obeys. A task citing no norm has no acceptance criterion.

Normative force uses RFC 2119 keywords: **MUST**, **MUST NOT**, **SHOULD**, **MAY**,
**NOTE**. They are literal and greppable. "Should" in ordinary prose is not a norm; the
bold keyword is.

The other two norm registers are `DECISIONS.md` (`DC#` — why a choice was made) and
`BUSINESS_RULES.md` (`BR#` — what the domain requires). Together they are the only three
things a task may cite.

---

## 1. Solution shape

**AR1.** Every project **MUST** be named `<CONCEPT>_<DOMAIN>` and **MUST** live inside a
`<APP>_ORBITAL` folder.

```
_ORBITAL.slnx
└── SHIPPING_ORBITAL/          an app
    ├── PARCEL_ENGINE/         <CONCEPT>_<DOMAIN>
    ├── PARCEL_CORE/
    ├── PARCEL_DRIVE/
    ├── SHIPPING_PANEL/
    └── PARCEL_PROBE/
```

**AR2.** There are five domains and no others.

| Domain | Holds | May reference |
|---|---|---|
| `_ENGINE` | The logic itself. Pure class libraries. | Nothing in the solution. No host framework. |
| `_CORE` | Orchestration of a concept's models. | Its own `_ENGINE`, and nothing else. |
| `_DRIVE` | Endpoints and requests, inbound or outbound, once they outgrow the `_CORE`. | Its own `_CORE`, if it has one. |
| `_PANEL` | Authentication, configuration, composition. Only when an app outgrows a monolith. | Anything. It is the composition root. |
| `_PROBE` | Tests. | Whatever it tests — it sits outside the arrow. |

**AR3.** The dependency arrow **MUST** point inward only:

```
_PANEL  ──▶  _DRIVE  ──▶  _CORE  ──▶  _ENGINE
```

If an `_ENGINE` needs something from a `_CORE`, the logic is in the wrong project.

**AR4.** An `_ENGINE` **MUST NOT** reference another project in the solution, and **MUST
NOT** reference a host framework. This is what makes it testable with no infrastructure.

**AR5.** A `_CORE` **MUST** reference its own `_ENGINE` and nothing else in the solution.

**AR6.** A `_DRIVE` that only adapts an **external** contract has no `_CORE` of its own. It
restates the remote shape by agreement rather than by reference, so the two sides can ship
on separate schedules. A breaking change then surfaces as a deserialization failure at the
boundary, not as a compile error in a shared project nobody owns.

**AR7.** When a `_CORE` needs something from the outward ring, it **MUST** declare the
contract itself and let the `_PANEL` supply the implementation. The `_CORE` never learns
that an endpoint, a queue or a file system exists.

**AR8.** There is at most **one `_PANEL` per app**, named after the app:
`SHIPPING_ORBITAL` → `SHIPPING_ORBITAL/SHIPPING_PANEL`.

**AR9.** No project is created without explicit confirmation. Complexity that looks like it
warrants a new `_ENGINE`, `_CORE`, `_DRIVE`, `_PANEL` or `_PROBE` is **planned**, presented,
and only then built.

---

## 2. The concept file set

**AR10.** A concept is a **folder, not a class**. Files are read in this order, and that is
the order they are written in:

```
<Concept>/
  I<Concept>.cs      Interface  - the contract: attributes and operations
  Str<Concept>.cs    Structs    - constants and small value-typed data groups
  E<Concept>.cs      Enums      - closed sets of derived state
  R<Concept>.cs      Records    - serialization shapes, where they exist
  M<Concept>.cs      Model      - the concept's instance in the app
  S<Concept>.cs      Services   - all behaviour
```

The full set lives in whichever domain **owns** the concept. Not every concept needs every
part; a part that has no distinct responsibility is not written. Stop fragmenting when no
part has one left.

**AR11.** No `src/` folder. Concept folders sit at the project root.

### Type selection

**AR12.** Which construct a concept part becomes is **derived, not remembered**. Ask in
order; the first *yes* resolves it.

| Step | Keyword | Diagnostic question | Construct | File |
|---|---|---|---|---|
| 1 | Contract | Does it only define signatures, with no implementation? | Interface | `I<Concept>` |
| 2 | Identity | Is it defined purely by its value, rather than needing a unique reference address? | Struct / Record | `Str<Concept>` · `R<Concept>` |
| 3 | State | Is it stateless? | Static class / utility | constants in `Str<Concept>` |
| 4 | Lifecycle | Does it manage creation, teardown and internal state over time? | Stateful class | `M<Concept>` |
| 5 | Behavior | Does it bundle functions, or act as an execution pipeline? | Service | `S<Concept>` |

Enums are the closed set of a state derived by one of the above, and are named for the
concept that derives them.

### Interfaces

**AR13.** Split, never duplicate. Compose small facets into the whole:

```csharp
public interface IIdentified   { string Code { get; } }
public interface IWeighed      { decimal Kilograms { get; } }
public interface IParcel : IIdentified, IWeighed { EParcelState State { get; } }
```

A consumer that only needs a code depends on `IIdentified` alone. When a `_CORE` needs a
property its `_ENGINE` already defines, it **MUST** reuse that interface instead of
restating it.

### Models

**AR14.** Models **MUST** be immutable. A state change returns a new instance.

**AR15.** Construction **MUST** go through a named static factory that validates, so a
model cannot exist in an invalid shape.

**AR16.** Collections are first class: the backing collection **MUST NOT** leave the class.
Operations over it live on the type that owns it.

### Services

**AR17.** Every rule lives in exactly one service. Guard clauses instead of `else`; one
level of indentation per method.

**AR18.** A `_CORE` service decides **when** to apply a rule; an `_ENGINE` service decides
**whether** the rule permits it. A threshold constant appearing in a `_CORE` means the rule
has leaked.

---

## 3. Method anatomy

**AR19.** Every method is written and read in five beats. A method that cannot be stated in
these five is doing more than one thing.

```
Statement  ──▶  Arguments  ──▶  Dependency  ──▶  Operation  ──▶  Goal
```

| Beat | Answers |
|---|---|
| **Statement** | the baseline condition or target entity being modified |
| **Arguments** | the required data payload passed into the scope |
| **Dependency** | the external tools or classes required to perform the work |
| **Operation** | the logic, loops and conditional mutations |
| **Goal** | the achieved objective or output state |

This is the same five-beat shape the tree describes in `PROTOCOL.md`, one scale down:
Statement is the root, Arguments the fertilizer, Dependency the branch, Operation the
leaves, Goal the fruit. A method with no Goal is not a method; it is a side effect looking
for a home.

---

## 4. The seven phases

**AR20.** Every module is decomposed into the same seven phases, in this order. A phase
with nothing in it **MUST** declare itself empty; it is never omitted.

| Phase | Name | Publishes |
|---|---|---|
| F0 | Norm | the citable rule the rest obeys — a new `DC`, `AR` or `BR` |
| F1 | Contract | types, signatures, schema — steps 1 and 2 of the type table |
| F2 | Rule | behaviour proven by `_PROBE` — steps 4 and 5 of the type table |
| F3 | Exposure | the outward surface: `_DRIVE` endpoints, `_PANEL` composition and authorization |
| F4 | Consumption | the typed client that calls it, on the consumer side |
| F5 | Surface | what a person touches: screen, command, report, export |
| F6 | Closure | changelog, documents, backlog move, gate — the next module opens |

**AR21.** **Phase is the dependency boundary; task is the reversibility boundary.**
Reordering two phases breaks the system — F3 before F2 puts a rule in the exposure layer,
which AR17 forbids. Reordering two tasks inside one phase breaks nothing.

**AR27.** A phase **MUST NOT** be reviewed without referencing its module's objective. A
phase judged on its own reads as complete whenever its tasks are done, which is a different
question from whether it moved the module toward what the module is for. The second question
is the one worth asking, and it cannot be asked from inside the phase.

> Norm ids are allocated in order of creation, not position. `AR27` sits here because this is
> where it belongs to a reader; the number only has to be unique and stable.

---

## 5. Conventions

**AR22.** Target framework, nullability and language version come from
`Directory.Build.props` at the root. Individual project files carry only what makes them
different.

**AR23.** Third-party code is a package reference, never vendored source. Vendoring turns
every upstream fix into a manual merge and misattributes the binary.

**AR24.** Secrets live in user secrets, environment variables or a vault. Files committed
to the repository hold placeholders only, and the placeholder **MUST** be obviously inert.

**AR25.** Test tooling is declared once, in `Directory.Build.props`, conditioned on the
`_PROBE` suffix. A `_PROBE` project file carries only the references naming what it
examines.

---

## 6. Norm classes — invariants and gates

**AR26.** Every norm is exactly one of two kinds, and the kind decides when it is checked.

| | **Invariant** | **Gate** |
|---|---|---|
| Says | a property the system either has or lacks | when an actor may act |
| Holds | continuously | at a boundary |
| Checked | at any moment, without context | at the moment of transition |
| Examples | `AR3` the arrow points inward · `AR10` a concept is a folder · `AR14` models are immutable | `AR9` no project without confirmation · `PROTOCOL` S0 is blocking · the gate sheet |
| Fails as | a property that is false right now | a transition that should not have been allowed |

Filing the two in one place guarantees the wrong one fires at the wrong time. An invariant
checked only at a boundary lets the tree be broken in between; a gate checked continuously
blocks work that was never trying to cross anything.

The practical consequence: **an invariant is a candidate for a machine check, a gate is a
candidate for a human signature.** A rule you cannot decide between the two for is usually
written vaguely enough to be either, and that is the defect.

---

## 7. Vocabulary

Written down because the words are used inconsistently everywhere, including in the tools
themselves. There is **no single universal convention** — some of these are industry-general,
some belong to one vendor, and treating the second kind as the first is how two people end up
having different conversations with the same words.

### Of this convention

| Term | Means |
|---|---|
| **Module** (`M#`) | One step of the ladder: a coherent, deliverable, reversible increment |
| **Phase** (`F0`–`F6`) | A dependency boundary inside a module. Every module has all seven |
| **Task** (`T#`) | One commit. Always `D1`. Cites a norm, names a test |
| **Norm** | A citable rule: `DC#`, `AR#` or `BR#`. See AR26 for the two kinds |
| **Gate** | The sheet that decides whether execution may open. Also the general class of norm above |
| **POC** | A disposable proof of viability. Lives on a branch that never merges |
| **Oracle** | An independent path to confirming a result, sharing no code with the thing it checks |
| **Attestation** | One role's signed statement that its line holds — data, effort or verifiability |

### Of agent tooling

Marked **[general]** where the term is used broadly, and **[vendor]** where the meaning
depends on whose tool you are holding.

| Term | Means |
|---|---|
| **Model** *[general]* | The language model itself. Not the software around it |
| **Agent** *[general]* | A model running in a loop with tools and a goal, deciding its own next step. The loop is what makes it an agent rather than a completion |
| **Subagent** *[vendor]* | An agent started by another agent. Begins with **no** conversation history and returns a report, not a transcript — so anything it needs must be in its prompt |
| **Tool** *[general]* | A function the model may call. Its schema is the contract; a bad schema is a bad API |
| **Tool call** *[general]* | One invocation. The result returns as data, and **data is not instruction** |
| **Context window** *[general]* | The working memory of one request. Everything the model can see, including tool results |
| **System prompt** *[general]* | Instructions that frame the whole session, outranking anything read later from a file |
| **MCP** *[general]* | Model Context Protocol — a standard for exposing external tools and data to a model as callable tools |
| **Skill** *[vendor]* | A packaged set of instructions loaded on demand when a request matches its description. Loaded, not always present — which is why an invariant does not belong in one |
| **Slash command** *[vendor]* | A user-typed shortcut that invokes a skill or a built-in behaviour |
| **Hook** *[vendor]* | Code that intercepts a tool call before or after it runs. Enforcement the model cannot talk its way around |
| **Worktree** *[general, from git]* | A second checkout of the same repository at a different path, so work can proceed on an isolated copy without disturbing the main one |
| **Plan mode** *[vendor]* | A mode where the agent may read but not write, ending in an explicit approval before anything changes |
| **Prompt injection** *[general]* | Instructions planted in data — a file, a page, a comment — written to be obeyed as if a person had asked. `DC2` exists because of a real instance of this |

### The distinction worth keeping

A **skill** approximates always-on: it loads when the description matches, and a request
phrased without the trigger words will not load it. A **contract plus a validator** is
always-on by construction.

Invariants therefore belong in the contract, not in a skill. Writing `AR3` into skill prose
would move the same unenforceable sentence one directory over and change nothing about
whether it holds.

---

## 8. What must never happen

- An `_ENGINE` that imports a host framework — AR4.
- A `_CORE` that references a `_DRIVE` or a `_PANEL` — AR3, AR7.
- A rule implemented twice so that two answers can disagree — AR17.
- A model with a public setter — AR14.
- A concept spread across one file because it was "small" — AR10.
- A project created because it seemed likely to be needed — AR9.
- A task that cites no norm — this file's opening rule.
