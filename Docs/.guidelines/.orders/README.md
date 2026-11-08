# Orders — the contract that dispatches the work

> An Order is neither a plan nor a decision. A decision **authorises**, a plan **schedules**, an Order
> **commissions**: it says what is to be done, under which constraints, and which deliverables come
> back.
>
> **An open Order is immutable except by version.** New material that arrives while an Order is open
> becomes *intake*, never an amendment. **Two concurrent Orders are forbidden:** exactly one stays open
> at a time.

## Numbering

Flat and sequential, like an `ADR-###`: the next one is the largest that already exists here, plus
one. No branching by subject, no reusing the number of a cancelled Order.

```
ORD-####
```

An Order may be written in Glyph: the `.pgml` is the source written by hand, and the `.hgml`, `.xml`
and `.json` beside it are what the engine answers. Edit the `.pgml`, never a projection.

## The open Order

None. The template has not issued one.

## Closed Orders

> They stay. A closed Order is the contract of what was delivered, and it is how "was this asked for
> this way?" gets answered — so it does **not** leave this folder when it closes, unlike what is in
> [`.plan/`](../.plan/README.md).

| id | commissioned | where the result is |
|---|---|---|

## Intake — what arrived while an Order was open

> Not a queue of ideas: material that **would have amended** an immutable Order. Each line waits to
> become an Order of its own or to enter [`.plan/`](../.plan/README.md).

| | |
|---|---|
