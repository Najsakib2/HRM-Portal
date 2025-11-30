🏗️ Architectural Highlights

This project moves beyond simple CRUD operations by implementing strict separation of concerns and decoupled logic.

Clean Architecture: The codebase is divided into independent layers (Domain, Application, Infrastructure, API). The Core domain is isolated from external frameworks, making the system testable and easy to refactor.

CQRS (Command Query Responsibility Segregation): Reads and Writes are separated. This allows for optimized query performance and complex command validation without tangling logic.

Mediator Pattern (via MediatR): Handles in-process messaging between objects, ensuring loose coupling between request objects and their handlers.

🛠️ Technical Features & Best Practices

🔐 Security First	Implements robust JWT Authentication & Authorization, ensuring granular access control for Admins, HR, and Employees.
⚡ Performance	Integrates Redis Caching to reduce database load and serve frequently accessed data (e.g., Designation lists) instantly.
💾 Data Integrity	Uses Entity Framework Core (Code-First) with SQL Server. Includes database migrations for seamless schema updates.
🛡️ Robust Validation	FluentValidation is used for strong, separate validation rules, keeping the controllers and domain entities clean.
⚙️ Error Handling	Implements the ErrorOr pattern for a functional approach to error handling, eliminating exceptions for control flow and returning unified result responses.
🧩 Object Mapping	Utilizes AutoMapper to manage clean transformations between Domain Entities and DTOs (Data Transfer Objects).


👥 Core Functional Modules The system provides a comprehensive suite of endpoints to manage HR operations:

User & Role Management

Organization Structure

Employee Lifecycle

Time & Attendance

Leave Management

Payroll System

🧰 Technology Stack

Core Framework Language: C#

Framework: ASP.NET Core 8 Web API

Architecture & Patterns Pattern: Clean Architecture + CQRS

Messaging: MediatR

Validation: FluentValidation

Error Handling: ErrorOr

Data & Infrastructure Database: SQL Server

ORM: Entity Framework Core

Caching: Redis

Mapping: AutoMapper
