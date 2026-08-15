# _ORBITAL

A template for starting software projects: a set of conventions, a derivation protocol, and a
planning ladder that runs from a client's problem to a deliverable.

The solution ships **empty**. Projects arrive when a briefing asks for them.

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

| Branch | Purpose |
|---|---|
| `ORBITAL_TEMPLATE` | This — the template you clone |
| `main` | A worked reference implementation of the same conventions |

They diverge on purpose and are not merged.
