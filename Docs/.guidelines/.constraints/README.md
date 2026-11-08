# Constraints — what the project forbids itself

> A constraint is cheaper than the decision it spares. These hold without a new discussion;
> changing one is the Regent's decision and goes to [`.decisions/`](../.decisions/README.md).
>
> The tables **Of the method**, **Of writing** and **Of the process** come filled in: they are the
> method, and they do not change from project to project. **Of the artefact** is the one each
> project writes. A clone of this template keeps the three and rewrites the first.

The architecture norms `AR1`–`AR27` are **not repeated here**. They live in
[`ARCHITECTURE.md`](../.decisions/ARCHITECTURE.md), and its §8 lists what must never happen; a
fact in two places is a fact one of whose copies is wrong.

## Of the artefact

| | |
|---|---|
| **No project is created without explicit confirmation** | `AR9`. Complexity that looks like it needs one is planned, presented, and only then built |
| **Nothing is invented silently** | every derived fact traces to a briefing field, a recorded answer or a norm — [`PROTOCOL.md`](../PROTOCOL.md) |
| **Every app is independently deletable** | no app references another, so removing three leaves the rest working. The near-duplicates that costs (`UPLINK_DRIVE`, `BEACON_DRIVE`) are the price, because the unit anyone reuses here is a whole app, not a class |
| **Every path a guideline names resolves inside this repository** | `ADR-002`. A pointer that resolves nowhere is a defect to report, never a gap to improvise around |
| **Client material lives in `.scope/`, and `.scope/` is never published** | `ADR-003` |
| **A task cites a norm and names its test** | or it is not ready to start: a task with no norm has no acceptance criterion, and a task with no test is a side effect looking for a home |
| **A module in execution does not change scope** | new requests become backlog items and are decided at the next module's opening |

## Of the method

| | |
|---|---|
| **Mastery spares Synthesis** | whatever fits in code, a schema or a table **never** has to be inferred again |
| **The gate comes before the thing it guards** | and every gate must have been **seen failing** before it counts |
| **A retired construct becomes a recogniser** | it is never deleted in silence: whoever writes the old form gets a refusal that names the new one |
| **A known loss is pinned with its reason** | and a pin that stops holding is **inverted**, not deleted — a suite that drops a retired pin loses the memory of what it guarded |
| **An agent never ratifies a norm it wrote** | it may measure, propose and refuse. Ratifying is the Regent's |
| **The simplest sufficient** | before building, stop at the first rung that solves it, and write in one line which simpler rung was refused and why; the unnecessary at medium cost is skipped, and validation at a trust boundary, errors that lose data, security and accessibility never count as unnecessary |
| **Documents before code** | a business rule changes in [`BUSINESS_RULES.md`](../.decisions/BUSINESS_RULES.md) first, then the test goes red, then the service goes green, then the changelog |

## Of writing

| | |
|---|---|
| **The index points, it never copies** | if a fact is in two places, one of them is wrong and nobody knows which |
| **Prose states the present, never the correction** | text that said "this is A" **becomes** "this is B"; it does not gain a "no longer A". Accumulated micro-corrections are residue the reader infers and throws away, because they do not help the final state. Where retirement has to act, it acts on the mechanism, not on the prose. The decision register is the one exception, and it marks a superseded row instead of rewriting it (`ADR-009`) |
| **Nothing disappears in silence** | what vanishes without a refusal is a defect, not a saving |
| **A diagnostic names the cause** | not the repair. A more detailed message does not help; position and structure do |
| **English, with European spelling** | `ADR-015`. The template and what it seeds are written in English, spelt the European way — *behaviour*, *authorisation*, *colour*. A project whose prose is in another language keeps its technical terms in English |

## Of the process

| | |
|---|---|
| **One step per commit** | with the verification green at each one |
| **Never contradict an accepted `ADR-###`** | without writing the one that supersedes it |
| **Regenerating the snapshot to turn the suite green defeats the snapshot** | it is regenerated when the change is a **decision**, and the decision is written down |
| **An open Order is immutable except by version** | and two concurrent Orders are forbidden — new material becomes *intake*, not an amendment. The Orders live in [`.orders/`](../.orders/README.md) |
