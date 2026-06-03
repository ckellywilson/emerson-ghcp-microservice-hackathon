# ADR 0003: Student Service Data Ownership

## Status
Accepted

## Context
Student is the prescribed Day-2 extraction target because it is the most weakly coupled aggregate in the monolith; the primary seam is `Enrollment -> Course`.
`Enrollment` is a join entity (`EnrollmentID`, `CourseID`, `StudentID`, nullable `Grade`) and its CRUD currently appears in `StudentsController`/`CoursesController` (no dedicated `EnrollmentsController`).

## Decision
Resolve Day-2 boundaries as follows:
- `Student` is the aggregate root extracted into `StudentService`.
- `Enrollment` is a child entity under `Student` (not a standalone aggregate for this workshop implementation).
- `Course` is a separate aggregate in a different bounded context and is out of extraction scope.
- Student-service enrollment records are owned in a sovereign datastore, including `Grade` and lifecycle transitions.
- Collaboration across service boundaries uses events plus anti-corruption layers (ACLs), never shared tables.

## Consequences
- `Enrollment.Course` object navigation must be replaced by a `CourseID` identity reference plus a read projection for Course-facing data.
- Cross-service consistency is eventual and implemented through event publication/consumption patterns.
- Teams spend build time implementing this boundary with GHCP rather than re-debating aggregate ownership.
- Optional stretch for presentations: argue why `Enrollment` might be promoted to its own aggregate in a future evolution.
