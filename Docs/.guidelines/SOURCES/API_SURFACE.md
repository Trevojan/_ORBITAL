# API_SURFACE

**Status:** `[TBD]` · **Owner:** DV · **Filled by:** each module's phase F3.

What can be called and what comes back. Covers any outward surface — HTTP routes, command
line verbs, queue messages, exported files. If a consumer can reach it, it belongs here.

---

## Base

| Item | Value |
|---|---|
| Address | `[TBD]` |
| Authentication | `[TBD]` |
| Error shape | `[TBD]` |

## Authorization

Who may call what, and where the caller's identity comes from.

> **Identity MUST come from the credential, never from a request parameter.** A caller-supplied
> identifier is not authorization; it is a suggestion.

## Surfaces

Repeat per resource.

### `<resource>`

```
<VERB> <path>          <who may call>        → <success> | <failure>
```

| Note | Detail |
|---|---|
| | |

## Consumption

How the client side calls this, and what it declares for itself.

> A consuming `_DRIVE` restates the wire shape **by agreement, not by reference** (`AR6`), so
> the two sides can ship on separate schedules. Divergence between the two declarations is a
> silent defect: when changing a shape here, check the consumer.

## Deliberate response choices

Where a response was chosen for a reason that is not obvious — a status code that hides
whether something exists, an error that refuses to distinguish two causes, a success returned
for an action that did nothing. Each entry records the reason, because the next person will
otherwise "fix" it.

| Surface | Choice | Reason |
|---|---|---|
| | | |
