# Monolith Reference

This directory is reserved for the ContosoUniversity brownfield monolith sourced from:

- `ckellywilson/day-in-the-life-copilot-lab`

## Expected architecture

The reference app follows a .NET 8 clean-architecture layout:

- Core
- Infrastructure
- Web
- Tests
- PlaywrightTests

## Domains emphasized in this workshop

- **Notification** (Day 1 showcase): clean extraction seam via `INotificationService`, notification model, and Service Bus integration.
- **Student** (Day 2 challenge): coupled aggregate through `Enrollment`, requiring explicit data-ownership and anti-corruption decisions.
