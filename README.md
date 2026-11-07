# _ORBITAL

A template for starting software projects: a set of conventions, a derivation protocol, a
planning ladder that runs from a client's problem to a deliverable, and four working app
templates to start from.

| App | Projects | What it is |
|---|---|---|
| `DESKTOP_ORBITAL` | 5 | Native desktop window hosting a web UI, with a typed client to an API |
| `WEB_ORBITAL` | 5 | Browser app that measures a viewport it does not control |
| `API_ORBITAL` | 5 | HTTP API with authentication and a composition root |
| `MIDDLEWARE_ORBITAL` | 3 | A relay that carries opaque payloads and references no other app |
| `DATA_ORBITAL` | 3 | A refinement pipeline over tiered records |

Twenty-one projects, 124 tests, building clean. They are a **starting point, not a demo**:
clone, rename the concepts to your domain, delete what you do not need.

Each app is **independently deletable**. No app references another, so removing three of them
leaves the fourth working. That costs some duplication — `UPLINK_DRIVE` and `BEACON_DRIVE`
are near-identical — and it is the right trade, because the unit anyone reuses here is a
whole app, not a class.

## What this is for

Starting a project usually means an improvised conversation, a folder structure invented on
the spot, and a plan that exists mostly in somebody's head. The setup work gets redone from
nothing every time, and the parts that were decided verbally get re-decided differently three
weeks later.

Orbital replaces that with one input and a fixed derivation. You describe the problem; the
protocol produces the ideal, the backlog, the modules, the phases, the individual commits,
and a gate that refuses to open execution on a plan that does not close.

## Start here

```bash
git clone <this repo> <your-project>
```

Then, locally — this file is gitignored on purpose, because agent working setup is personal
and not published content:

```bash
echo "Read \`Docs/.guidelines/AGENTS.md\` before acting." > CLAUDE.md
```

Then fill in `Docs/.guidelines/SOURCES/BRIEFING.md` with whatever you know about the problem,
and run `Docs/.guidelines/PROTOCOL.md` from S0. The gaps in the briefing get interviewed
before anything is derived — that is the one rule the whole scaffold rests on.

## Layout

```
Docs/.guidelines/          the only published guideline set
├── AGENTS.md              entry point and reading order
├── PROTOCOL.md            the derivation algorithm, S0 to S9
├── SOURCES/               facts    - what the system is
├── DECISIONS/             rules    - what may and may not
├── TEAM/                  roles    - who decides what
└── PLAN/                  the ladder - what gets built, in what order
```

The split is by **nature**, not by subject. An out-of-date fact is a documentation defect; an
out-of-date rule is a process defect. They are repaired differently, so they are stored
separately.

## The conventions, in brief

Projects are named `<CONCEPT>_<DOMAIN>` and grouped in `<APP>_ORBITAL` folders. There are
five domains, and the dependency arrow points inward only:

```
_PANEL  ──▶  _DRIVE  ──▶  _CORE  ──▶  _ENGINE          _PROBE sits outside
```

A concept is a folder, not a class — interface, structs, enums, records, model, services —
and which construct each part becomes is **derived** from a five-question table rather than
remembered.

The same five-beat shape recurs at three scales, which is what lets two people decompose the
same problem the same way:

| Scale | Beats |
|---|---|
| Journey | root → fertilizer → trunk → branches → leaves → fruit |
| Task | why it exists → clarification → the commit → the files → the edits → the test that proves it |
| Method | Statement → Arguments → Dependency → Operation → Goal |

Full detail: [`ARCHITECTURE.md`](Docs/.guidelines/DECISIONS/ARCHITECTURE.md).

## The ladder

Work is cut into **modules** (`M#`), each with the same seven **phases** (`F0`–`F6`), each
phase holding **tasks** (`T#`). One task is one commit, always the smallest difficulty, always
citing a norm and naming the test that proves it.

Execution opens only through a gate, and the gate is a formula rather than an opinion. It is
defined once, in [`PLANNING.md`](Docs/.guidelines/PLAN/PLANNING.md#gate-formula).

## Build

The solution is empty until a project is added, so this warns that it found nothing to
restore. That is expected, and it stops with the first project.

```bash
dotnet build _ORBITAL.slnx
```

## Branches

`main` is the template you clone. Every other branch converges to it.
