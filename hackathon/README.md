# Day 2 Hackathon: Student Service Challenge

Teams extract the Student aggregate from the monolith, applying the same GHCP workflow used in the Day-1 showcase.

The challenge is intentionally scoped to a single-day event (~6–7 hours): teams should deliver strong architecture/design artifacts and one thin vertical implementation slice, not a fully productionized service.

## Goals

- Convert a coupled aggregate into an independently deployable service.
- Resolve data ownership around `Enrollment` without shared tables.
- Demonstrate event-driven integration and anti-corruption boundaries.

## Challenge brief

- [`docs/student-service-challenge.md`](docs/student-service-challenge.md)

## Suggested day flow

- Kickoff/context and scope confirmation
- Bounded-context + data ownership decisions
- API/event contract design
- Thin implementation slice
- Verification and final presentation

See the detailed **Timebox & Milestones** in [`docs/student-service-challenge.md`](docs/student-service-challenge.md).
