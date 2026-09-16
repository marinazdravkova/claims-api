Claims & Covers API 

This is implementation for the Claims API technical assessment.

Key Improvements & Refactoring

1. Architecture & Clean Code
- Refactored controller actions by extracting business logic into service layers (`ClaimsService`, `CoversService`).
- Created "ClaimsContext.cs" file and data context access (`ClaimsContext`) across the application.
- Integrated "FluentValidation" (`ClaimValidator`, `CoverValidator`) to enforce domain rules (e.g., claim date constraints, damage cost).
2. Asynchronous Auditing
- Decoupled auditing from request processing using `System.Threading.Channels` (`AuditQueue`).
- Processed background auditing via `AuditBackgroundService` (`IHostedService`) to ensure non-blocking HTTP responses.
3. Business Logic & Premium Computation (Task 4 & 5)
- Implemented multi-tier yacht insurance premium calculations inside `PremiumComputeService`.
- Added unit tests (`PremiumComputeServiceTests`) testing period discounting logic.
