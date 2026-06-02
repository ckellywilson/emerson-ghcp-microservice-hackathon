# ADR 0003: Student Service Data Ownership

## Status
Proposed

## Context
Student extraction introduces coupling to Course through `Enrollment`, creating ownership and consistency trade-offs across bounded contexts.

## Decision
Teams must select and justify an ownership model for enrollment-related data that preserves database-per-service principles and uses events/ACLs for cross-service collaboration.

## Consequences
- Creates explicit architectural trade-off discussions for Day 2.
- Prevents shared-table coupling from reappearing in the target architecture.
- May introduce eventual consistency and projection complexity that must be tested.
