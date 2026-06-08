# Rehearsal Handoff

This handoff gives facilitators a stable runbook for Day 1 and Day 2.

## Workshop Intent

- `main` is the clean, pre-migration baseline for attendees.
- `rehearsal-day1-final` is the immutable finished Day 1 state.
- `rehearsal-day2-final` is the immutable finished Day 2 state.
- Day branches and tags are reference-only and should not be merged to `main`.

## Canonical References

- Day 1 final tag: `rehearsal-day1-final`
- Day 2 final tag: `rehearsal-day2-final`
- Day 1 feature branch: `feat/day1-notification-extraction`
- Day 2 feature branch: `feat/day2-student-extraction`

## Facilitator Quick Start

Run from repository root:

```bash
git fetch --all --tags --prune
git checkout main
git pull --ff-only origin main
```

Expected outcome:

- You are on `main`.
- Your local baseline matches remote.

## Start Attendees from Clean Baseline

Use this at the beginning of the session:

```bash
git checkout main
git pull --ff-only origin main
```

## Jump to Finished Example (No Rebuild of Migration Needed)

### Day 1 final example

```bash
git fetch --tags
git checkout rehearsal-day1-final
```

### Day 2 final example

```bash
git fetch --tags
git checkout rehearsal-day2-final
```

## Return to Baseline After Demo

```bash
git checkout main
git pull --ff-only origin main
```

## Recommended Delivery Pattern

1. Start all teams on `main`.
2. Let teams run the guided exercise in their own feature branch.
3. If teams get stuck, facilitator checks out the matching final tag to demonstrate expected outcome.
4. Return to `main` before continuing.

## Safety Rules

- Do not merge workshop branches into `main`.
- Keep `main` as the restart point for all cohorts.
- Preserve rehearsal tags as immutable references.

## Troubleshooting

### Local branch is behind or diverged

```bash
git fetch --all --tags --prune
git checkout main
git reset --hard origin/main
```

Use the hard reset command only in local facilitator environments where uncommitted work is intentionally disposable.

### Cannot switch branches due to local changes

```bash
git status
git stash push -u -m "facilitator-temp"
git checkout main
```

### Verify tag targets

```bash
git show --no-patch --decorate rehearsal-day1-final
git show --no-patch --decorate rehearsal-day2-final
```

## Optional Build Checks

```bash
dotnet build monolith/ContosoUniversity.sln
```
