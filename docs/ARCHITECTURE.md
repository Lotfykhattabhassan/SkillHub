# SkillHub Architecture

## Runtime shape

One ASP.NET Core host:
- `Host/SkillHub.Host.csproj`

The host is the composition root.

Modules are class libraries. Their `.API` projects contain endpoint/registration code and are referenced by the host. They are NOT independent web applications.

## Module shape

```text
Modules/<Module>/
├── SkillHub.Modules.<Module>.Domain/
├── SkillHub.Modules.<Module>.Application/
├── SkillHub.Modules.<Module>.Infrastructure/
└── SkillHub.Modules.<Module>.API/
```

Dependency direction:

```text
API → Application → Domain
API → Infrastructure → Application/Domain
Infrastructure → Domain/Application
Domain → BuildingBlocks.Domain only when genuinely needed
```

Cross-module dependencies should be explicit and minimized.

## Shared infrastructure

`BuildingBlocks` contains genuinely shared abstractions only. It does not own module business logic and does not contain an API project.

## Multi-tenancy

Initial strategy: Shared DB / Shared Schema / TenantId.

Tenant context must come from trusted request/authentication context, not arbitrary client-supplied tenant identifiers.

## Persistence

We will decide DbContext/repository boundaries while implementing the first persistence slice. No generic repository or Unit of Work will be added merely because it is common in tutorials.

## CQRS

Commands and queries are separated where they provide a concrete benefit. CQRS is not a requirement that every method must become a handler.

## Patterns

Patterns are introduced only when a real variation/problem justifies them.
