---
description: Define the test strategy and quality gates for an extracted service before rollout.
agent: "agent"
---

Define the test strategy for **{{service_name}}**.

Use these references:
- `docs/architecture/microservice-principles.md`
- `.github/copilot-instructions.md`

Output:
1. Contract tests covering approved API and event contracts.
2. Event-handler idempotency and retry-behavior tests.
3. Integration tests for the service seam, adapters, and datastore interactions.
4. Quality-gate checklist, including fresh-clone `dotnet build` as the smoke test when CI is unavailable.
