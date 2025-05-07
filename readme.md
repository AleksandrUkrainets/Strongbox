# Strongbox Document Access Approval System

## Project Overview

A system for managing access requests to internal documents with a multi-role workflow:

- **User**
- **Approver**
- **Admin**

Built according to Clean Architecture principles, organized into four layers:

- **Presentation**: ASP.NET Core Web API
- **Application**: Business logic and DTOs
- **Domain**: Entities and repository interfaces
- **Persistence**: EF Core + SQLite

## Use Cases

### User

- View list of documents (metadata only: ID, name, access level)
- Create an access request (specify reason and type: Read/Edit)
- Check status of own requests
- Retrieve and view all approved documents with content
- A user can change their account password

### Approver

- All User actions
- View list of pending requests
- Approve or reject requests with comments
- View and update existing decisions

### Admin

- All Approver actions
- Manage users and assign roles

## Project Structure & Key Design Decisions

- **Domain**

  - Entities: `User`, `Document`, `AccessRequest`, `Decision`
  - Enums: `PersonRole`, `AccessType`, `RequestStatus`
  - Repository interfaces

- **Persistence**

  - `AppDbContext` using EF Core + SQLite
  - Seed data (documents)

- **Application**

  - DTO definitions
  - Service interfaces and implementations:  
    `AccessRequestService`, `DecisionService`, `DocumentService`,  
    `UserService`, `AuthService`
  - AutoMapper profiles for entity ↔ DTO mappings

- **Presentation**

  - ASP.NET Core Web API controllers
  - JWT authentication and `[Authorize]`/role-based policies
  - Swagger/OpenAPI documentation

- **Tests**
  - Unit tests with xUnit and Moq

**Key Decisions:**

- JWT authentication + role-based authorization
- Controller-level authorization, service-level business logic
- AutoMapper for separation of concerns
- Strict adherence to SOLID and Clean Architecture

## Assumptions & Trade-offs

- Roles are issued in the JWT at login and not updated until re-login
- Seed data and User Secrets are for demonstration only
- Simple username/password registration (no external identity provider)

## Running the Application & Tests

1. Open the solution in Visual Studio.
2. Configure User Secrets for `AdminName`, `AdminPassword`, and the `JWT key` (provided via email)
3. Run the `Strongbox.Web` project—EF Core migrations will apply automatically.
4. Browse to Swagger UI at: `https://localhost:5001/swagger`.
5. Run the `Strongbox.Tests` project in Test Explorer to execute unit tests.
6. To create an account with the Admin role, you must register using the AdminName and AdminPassword provided in the email.
7. All accounts are created with the User role by default.
8. An administrator can change users’ roles.

## Improvements with More Time

- Implement the CQRS pattern
- Centralized exception handling with `try/catch` and `ExceptionMiddleware`
- Custom `LoggingMiddleware` and persistence of logs to database and files
- Extract secret management into `AesEncryptionService` and `TokenService`
- Containerize the application with Docker
- Add integration tests (In-Memory DB or test containers)
- Implement real-time notifications via SignalR for `Users`, `Approvers`, `Admins`
- Add email and SMS notification services for key events
