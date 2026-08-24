# SkillHub AI — Project Context
Version: 1.0
Date: 2026-08-21
Status: Day 1 — Implementation Ready

## 1. Project Vision

SkillHub AI is a multi-tenant SaaS platform for discovering, evaluating, training, and recruiting junior/early-career talent.

The platform connects:
- Students / candidates
- Companies
- Startups
- Training centers
- Recruiters / HR teams
- Evaluators / instructors

The product is designed to be commercially viable, not merely a CRUD/demo project.

Core value:
1. Help students prove and improve their skills.
2. Help organizations discover and evaluate talent.
3. Use AI for analysis, recommendations, evaluation assistance, and CV generation.
4. Provide isolated tenant workspaces with tenant-specific content and users.
5. Allow students to exist at platform level and participate in multiple tenants.

---

## 2. Primary Product Model

### Platform
SkillHub itself is the SaaS platform.

### Tenant
A tenant is an organization/workspace such as:
- Company
- Startup
- Training Center

Each tenant has its own:
- Members
- Roles/permissions
- Challenges
- Courses
- Jobs/recruitment data
- Settings
- Analytics
- Tenant-specific content

### Student
A student is a platform-level user.

A student is NOT owned by one tenant.

A student can have memberships in multiple tenants:

Student
 ├── Membership → Tenant A
 ├── Membership → Tenant B
 └── Membership → Tenant C

Memberships are independent and have their own status/role/permissions.

---

## 3. Actors

### Student
Platform-level candidate who can:
- Manage profile
- Add education, experience, skills, projects, certifications
- Manage CVs
- Generate CV using AI
- Receive AI profile/skill analysis
- Receive learning recommendations
- Practice AI-generated tasks
- Browse challenges
- Submit solutions
- View evaluations/results
- Join tenant memberships
- Access public and permitted tenant courses
- Participate in recruitment opportunities

### Platform Admin
Manages SkillHub globally:
- Tenants
- Platform users
- Platform content
- Moderation
- Platform configuration
- Subscription plans
- Feature flags
- Platform analytics

### Tenant Staff
A logical actor group whose permissions are role-based.

Potential roles:
- Tenant Owner
- Tenant Admin
- HR
- Recruiter
- Manager
- Evaluator
- Instructor

A user may have different roles in different tenants.

### AI / External AI Provider
AI is a system component, not a human user or authorization actor.

AI may:
- Analyze profiles
- Estimate skill levels
- Recommend learning topics
- Generate practice tasks
- Analyze submissions
- Generate CV content
- Suggest candidate/job matches

AI output is advisory unless an explicit business workflow makes it authoritative.

---

## 4. Core Modules

We are building a Modular Monolith.

Proposed business modules:

1. Identity
2. Tenancy
3. StudentProfile
4. Talent
5. Challenges
6. Submissions
7. Evaluation
8. Learning
9. Courses
10. AI
11. Recruitment
12. Notifications
13. Gamification
14. Reporting
15. PlatformManagement

We will NOT implement all modules at once. Modules will be introduced according to the roadmap and business dependencies.

---

## 5. Architectural Style

Primary architecture:

### Modular Monolith
The application is one deployable system, but business capabilities are isolated into modules.

### Clean Architecture inside modules
Each significant module follows separation of concerns similar to:

Module
 ├── Domain
 ├── Application
 ├── Infrastructure
 └── API

### CQRS
Commands and Queries are separated where useful.

Example:

Challenges
 └── Application
      ├── Commands
      └── Queries

CQRS must solve a real problem; it should not be applied blindly.

### DDD-inspired modeling
We will use practical domain-driven concepts where they add value:
- Entities
- Value Objects
- Aggregates
- Aggregate Roots
- Domain Services
- Domain Events
- Business Rules

We are not trying to implement every DDD pattern just for the sake of it.

---

## 6. Proposed Solution Shape

High-level structure:

SkillHub
│
├── Modules
│   ├── Identity
│   ├── Tenancy
│   ├── StudentProfile
│   ├── Talent
│   ├── Challenges
│   ├── Submissions
│   ├── Evaluation
│   ├── Learning
│   ├── Courses
│   ├── AI
│   ├── Recruitment
│   ├── Notifications
│   ├── Gamification
│   ├── Reporting
│   └── PlatformManagement
│
└── BuildingBlocks

BuildingBlocks may contain truly shared technical/domain abstractions such as:
- BaseEntity
- AggregateRoot
- ValueObject
- DomainEvent abstractions
- Result/Error abstractions
- Cross-cutting infrastructure abstractions

BuildingBlocks must NOT become a dumping ground for unrelated business logic.

---

## 7. Multi-Tenancy Strategy

Initial strategy:

### Shared Database + Shared Schema + TenantId

This is the starting architecture unless requirements later prove that another strategy is necessary.

Important principles:

1. Tenant-scoped data must be isolated.
2. TenantId must not be trusted from arbitrary client input.
3. Tenant context should be resolved from authenticated/request context.
4. Tenant-aware authorization is required.
5. Global query filters may be used where appropriate.
6. Cross-tenant operations must be explicit and privileged.
7. Student platform-level data is not automatically tenant-owned.
8. Tenant membership is a relationship between a platform user and a tenant.

Potential future evolution:
- Separate schema
- Database per tenant
- Hybrid tenancy

We will not prematurely implement these.

---

## 8. Student / Tenant Data Boundary

### Platform-level student data
Examples:
- User/account
- Student profile
- Education
- Experience
- Skills
- Projects
- Certifications
- CVs
- Career preferences
- AI-derived profile insights

### Tenant-scoped data
Examples:
- Tenant members
- Tenant roles
- Tenant challenges
- Tenant courses
- Tenant jobs
- Tenant recruitment pipeline
- Tenant settings
- Tenant analytics

### Cross-boundary relationship
Student ↔ Tenant Membership

Membership may have statuses such as:
- Pending
- Active
- Suspended
- Revoked

---

## 9. Candidate/Talent Visibility

A tenant should NOT automatically see every student's private data.

Talent discovery must respect:
- Student visibility/privacy settings
- Tenant permissions
- Consent/access rules

Possible visibility concepts:
- Private
- Platform Members
- Recruiters Only
- Specific Tenants
- Public

Exact rules will be finalized during the Talent/Recruitment design phase.

---

## 10. Challenge Model — Business Intent

Challenges can be:
- Platform-owned
- Tenant-owned

Examples:
- Coding problems
- MCQ/assessment challenges
- Real-world tasks
- API implementation tasks
- Bug-fixing tasks
- Projects

Challenge lifecycle may include:
- Draft
- Published/Open
- Closed
- Archived

Challenge may define:
- Track
- Required skills
- Minimum skill levels
- Prerequisites
- Experience requirements
- Submission window
- Maximum attempts
- Eligibility rules
- Evaluation strategy

We will model Challenge Requirements as a proper domain concept rather than scattering many unrelated properties across controllers.

---

## 11. Submission Business Rules

Before accepting a submission, the system should evaluate relevant rules such as:

### Access / authorization
- User is authenticated.
- User has permission to submit.
- Tenant context is valid when applicable.

### Tenant
- Tenant is active when the challenge is tenant-owned.
- Membership is valid/active when membership is required.

### Challenge
- Challenge exists.
- Challenge is in a state that accepts submissions.
- Current time is inside the submission window.
- Student has remaining attempts.
- Student has not violated challenge restrictions.

### Student eligibility
- Student is not banned/suspended where relevant.
- Student satisfies track requirements.
- Student satisfies required skill thresholds.
- Student satisfies prerequisites.
- Student satisfies experience requirements if configured.

### Submission state
- Existing submissions do not violate retry/concurrency rules.
- Submission content is valid for the challenge type.

Exact placement of each rule will be decided later:
- Authorization
- Application validation
- Domain business rules
- Database constraints

We must avoid putting all rules in controllers.

---

## 12. Evaluation

Submission and Evaluation are separate concepts.

Submission:
- What the student submitted.

Evaluation:
- What the system/ evaluator concluded about it.

Evaluation may include:
- Score
- Criteria
- Test results
- Feedback
- Code quality
- Skill impact
- AI feedback
- Human evaluator feedback

Potential evaluation strategies:
- Automated/code evaluation
- MCQ evaluation
- Project evaluation
- AI-assisted evaluation
- Human evaluation

Strategy Pattern may be appropriate, but will be introduced only when the actual variation requires it.

---

## 13. AI Architecture

AI should be abstracted behind application/domain-appropriate interfaces.

Conceptual example:

IAIService / specialized AI abstractions

Potential providers:
- OpenAI
- Azure OpenAI
- Ollama/local models
- Other providers

The business domain should not depend directly on a provider SDK.

AI use cases:
- Student profile analysis
- Skill estimation
- Learning recommendations
- Practice task generation
- Submission feedback
- CV generation
- Candidate/job matching

Important:
AI does not directly write business data to the database.

AI produces structured results/proposals; application/domain workflows validate and persist them.

---

## 14. Learning

Learning may include:
- Skills
- Tracks
- Topics
- Learning paths
- Recommendations
- Practice tasks
- Progress

Example:

Student
 → Skill Assessment
 → Skill Gap
 → Recommended Topics
 → Learning Path
 → Practice Tasks
 → Reassessment

Track/skill hierarchy may be supported later.

---

## 15. Courses

Courses may be:
- Public
- Tenant-members-only
- Private

Potential tenant offerings:
- Free for everyone
- Free for tenant members
- Paid (future capability)

Course domain may contain:
- Course
- Sections
- Lessons
- Resources
- Enrollment
- Progress
- Completion

---

## 16. Recruitment

Potential recruitment capabilities:
- Jobs
- Applications
- Candidates
- Hiring pipeline
- Shortlisting
- Interviews
- Candidate evaluation
- Candidate/job matching

Talent discovery is separate from the student's profile ownership.

---

## 17. Gamification

Potential capabilities:
- Points
- Badges
- Achievements
- Streaks
- Ranks
- Leaderboards

Possible leaderboards:
- Platform
- Tenant
- Challenge

---

## 18. Notifications

Potential channels:
- In-app
- Email
- Real-time
- Push (future)

Likely future infrastructure:
- Background jobs
- Events
- SignalR
- Retry policies

---

## 19. Reporting

Potential read-heavy analytics:
- Student performance
- Challenge performance
- Tenant performance
- Recruitment analytics
- Learning analytics

This area is a strong candidate for CQRS/query optimization/caching later.

---

## 20. Design Patterns

We will NOT add patterns because they are on a checklist.

Patterns will be introduced when a real design problem appears.

Potential candidates:
- Strategy
- Factory
- Specification
- Decorator
- Adapter
- Builder
- State
- Repository
- Unit of Work

For every pattern we use, the developer must understand:
1. What problem it solves.
2. Why the simpler approach is insufficient.
3. What trade-offs it introduces.

---

## 21. Repository / Unit of Work Policy

Generic Repository and Unit of Work are learning goals and may be implemented where useful.

However, we will explicitly evaluate whether they provide value with EF Core.

We will discuss:
- DbContext already acting as Unit of Work
- Generic repository limitations
- Specific repositories
- IQueryable leakage
- Query specialization
- CQRS read-side alternatives

We should never create abstractions merely because they are common in tutorials.

---

## 22. Mapping

AutoMapper may be used where mapping complexity justifies it.

We will also understand manual mapping and decide per use case.

The goal is to understand:
- Entity ↔ DTO boundaries
- Projection
- Read models
- Mapping trade-offs

---

## 23. Identity & Authorization

Expected concepts:
- ASP.NET Core Identity
- JWT
- Refresh tokens
- Roles
- Claims
- Permissions
- Policies
- Tenant-aware authorization
- Resource-based authorization where appropriate

Important:
Role membership is tenant-aware where applicable.

A user may have different roles in different tenants.

---

## 24. Security Principles

Must consider:
- Tenant isolation
- Authorization
- Input validation
- Sensitive data protection
- Secure token handling
- Rate limiting
- Auditability
- File upload security
- AI prompt/input safety
- Preventing cross-tenant data access

Security is part of architecture, not a final-day patch.

---

## 25. Development Method

This project is an educational + portfolio + potentially commercial project.

Developer role:
- Lotfy writes the implementation.
- AI acts as system designer / technical lead / reviewer.
- AI should guide instead of blindly generating the entire implementation.
- Every major architectural decision must be explained.
- Problems should be solved from first principles when possible.

Daily schedule:
- ~4 hours focused study for the CURRENT day's implementation needs.
- ~6 hours implementation/debugging/testing.
- Do not pre-study future topics unless necessary.

Learning method:
Problem → Required concept → Learn → Implement → Debug → Review → Refactor

---

## 26. 21-Day Goal

Target:
A production-oriented, working backend for SkillHub AI.

The 21 days are a structured target, not a rigid deadline.

If a day is completed early and the developer still has energy, continue with the next planned work.

Priority order:
1. Correct domain/business model
2. Working core backend
3. Clean modular architecture
4. Security and tenant isolation
5. Testing
6. Performance
7. Advanced infrastructure
8. Commercial polish

We prefer depth and understanding over blindly maximizing the number of technologies.

---

## 27. Day 1 Status

Completed conceptually:
- Product vision
- Actors
- Initial roles
- Student/tenant relationship
- Initial module boundaries
- Modular Monolith decision
- Clean Architecture direction
- CQRS direction
- Multi-tenancy direction
- Challenge/submission business rules
- AI role and boundaries
- Talent/privacy direction

Next implementation phase:
1. Freeze the initial requirements.
2. Create solution/repository structure.
3. Create module/building-block boundaries.
4. Establish project conventions.
5. Begin Domain modeling with the first module(s).

---

## 28. Current Architecture Decision Summary

AD-001:
Use Modular Monolith as the initial architecture.

AD-002:
Use Clean Architecture principles inside modules.

AD-003:
Use Shared Database + Shared Schema + TenantId initially.

AD-004:
Students are platform-level users and can belong to multiple tenants through memberships.

AD-005:
Tenant staff roles are permission-driven and tenant-specific.

AD-006:
AI is an external/system capability behind abstractions and does not directly own/persist business decisions.

AD-007:
CQRS is used selectively where it provides clear value.

AD-008:
Design patterns are introduced because of concrete design problems, not as a checklist.

AD-009:
BuildingBlocks contain only genuinely shared abstractions.

---

## 29. Working Rules for Future Sessions

When starting a new chat:
1. Provide this file as project context.
2. Provide CURRENT_STATE.md if available.
3. Provide the latest DEVELOPMENT_LOG.md if needed.
4. State the current Day and current task.

At the end of each work session:
- Update current state.
- Record important architectural decisions.
- Record unresolved issues.
- Record next steps.
- Commit code to Git.

The Git repository + docs are the source of truth, not the chat history.

---

## 30. Immediate Next Task

Start implementation.

First technical milestone:
Create the initial solution and modular structure without prematurely implementing every module.

Before coding entities, establish:
- Naming conventions
- Project structure
- Dependency rules
- BuildingBlocks boundaries
- Module boundaries
- API composition approach
- Dependency Injection strategy

Then begin the first domain module.
