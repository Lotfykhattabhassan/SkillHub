# SkillHub AI

Multi-tenant SaaS platform for talent discovery, skill development, evaluation, and recruitment.

## Architecture
- Modular Monolith
- Clean Architecture inside modules
- CQRS where it provides clear value
- DDD-inspired domain modeling
- Shared Database / Shared Schema / TenantId (initial strategy)

## Solution layout
```text
SkillHub/
├── BuildingBlocks/
├── Host/
├── Modules/
└── Tests/
```

`Host` is the single ASP.NET Core composition root. Module API projects are module endpoint/registration layers, not separate web applications.

See `docs/PROJECT_CONTEXT.md` for the full project context.
