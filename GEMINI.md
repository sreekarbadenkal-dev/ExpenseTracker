I am building an ExpenseTracker application using ASP.NET Core microservices with Microsoft SQL Server (MSSQL).

IMPORTANT: THIS IS A LEARNING PROJECT

I am relatively new to:
- C#
- ASP.NET Core
- Entity Framework Core (EF Core)

However, I have some prior development experience with:
- NestJS (some experience)
- Spring (limited experience)
- ORMs such as Hibernate and Sequelize
- General backend/API and database concepts

Therefore, when explaining C#, ASP.NET Core, EF Core, or microservice concepts, relate them to concepts I may already know from NestJS, Spring, Hibernate, Sequelize, etc. when that comparison is useful.

MOST IMPORTANT LEARNING RULE:
If you recommend something that is different from what I currently have designed or implemented, DO NOT silently apply the change or simply replace my approach with yours.

Instead:
1. First explain what is different.
2. Explain why the different approach is used.
3. Explain the advantages/disadvantages or trade-offs.
4. Compare it with my current approach.
5. Where useful, relate it to NestJS/Spring/Hibernate/Sequelize concepts.
6. Then let me understand the difference so I can decide/apply the change myself.

Do not assume that the "best practice" automatically needs to be applied immediately. The goal is for me to understand the architecture and technologies while building the project.

Avoid unnecessary abstractions and premature complexity. If something can be implemented simply for the learning project, prefer explaining the simple approach first and then mention how it could evolve in a production system.

--------------------------------------------------
PROJECT ARCHITECTURE
--------------------------------------------------

The ExpenseTracker application is being designed as an ASP.NET Core microservices project.

Current planned services:

- Auth Service
- User Service
- Device & Consent Service
- Transaction Service
- Rules/Detection Service
- Future/Scheduled Transaction Service
- Notification Service

The database technology for the project is Microsoft SQL Server (MSSQL). Do not assume MySQL or introduce another database unless explicitly requested.

--------------------------------------------------
AUTH SERVICE
--------------------------------------------------

The Auth Service is partially implemented as:

ExpenseTracker.AuthService.Api

Its responsibility is identity and authentication, including:

- User registration
- Login
- Password hashing
- Email verification / OTP
- JWT authentication

Its Users table currently contains identity/authentication information such as:

- Id
- UserId (Guid)
- Email
- PasswordHash
- EmailVerifiedAt
- CreatedAt
- LastUpdatedAt
- DeletedAt

PhoneNumber should belong to Auth Service because it may be used for:
- OTP verification
- Account recovery
- Authentication
- Other security-related functionality

Do NOT duplicate PhoneNumber in UserProfile unless there is a specific architectural reason that is first explained.

The conceptual boundary is:

Auth Service:
"Who is this user and can they authenticate?"

--------------------------------------------------
USER SERVICE
--------------------------------------------------

The User Service is:

ExpenseTracker.UserService.Api

Its responsibility is the user's ExpenseTracker profile and application preferences.

It should NOT be responsible for authentication.

For now, we have decided that User Service contains two entities.

### 1. UserProfile

Fields:

- UserId
- UserName
- Description
- ProfilePicture
- DateOfBirth
- CreatedAt
- LastUpdatedAt
- DeletedAt

UserId links this profile to the identity owned by Auth Service.

Conceptually:

UserProfile answers:

"What information about the user should ExpenseTracker store as their profile?"

### 2. UserPreferences

Fields initially:

- UserId
- DefaultCurrency
- TimeZone
- Language
- DateFormat

UserPreferences answers:

"How does the user want ExpenseTracker to behave/display information?"

For example:

DefaultCurrency = INR
TimeZone = Asia/Kolkata
Language = en-IN
DateFormat = DD/MM/YYYY

--------------------------------------------------
MULTIPLE CURRENCIES
--------------------------------------------------

Currency conversion is OUT OF SCOPE for now.

Do not design or implement exchange-rate functionality yet.

Eventually, if the user needs multiple currencies, we may create:

UserCurrencies

Fields:

- Id
- UserId
- CurrencyCode
- IsDefault

The distinction is:

UserPreferences:
"What is my default/display currency?"

UserCurrencies:
"Which currencies have I enabled?"

Currency conversion and external exchange-rate APIs will be handled separately later.

--------------------------------------------------
WHAT DOES NOT BELONG IN USER SERVICE
--------------------------------------------------

Do NOT put these in User Service:

- PasswordHash
- Authentication data
- Phone authentication data
- Devices
- ConnectedSources
- Consents
- SyncSessions
- Transactions
- ScheduledTransactions
- Notifications
- Rules

These belong to other services.

Do not create additional User Service tables unless there is an actual requirement for them.

--------------------------------------------------
DEVICE & CONSENT SERVICE
--------------------------------------------------

After User Service, we will design the Device & Consent Service.

Planned entities:

- Devices
- ConnectedSources
- Consents
- SyncSessions

Its purpose is to establish:

User
  ↓
Device
  ↓
Data Source
  ↓
Consent

and manage historical synchronization.

The conceptual question this service answers is:

"What devices/sources does this user have, and what data has the user allowed ExpenseTracker to access/process?"

--------------------------------------------------
OTHER SERVICES
--------------------------------------------------

Transaction Service:
- Transactions

Rules/Detection Service:
- Rules / transaction detection logic

Future/Scheduled Transaction Service:
- ScheduledTransactions

Notification Service:
- Notifications

These services should own their respective data rather than putting everything into User Service.

--------------------------------------------------
ARCHITECTURAL PRINCIPLE
--------------------------------------------------

Use UserId as the common identity reference across services.

Do not share databases between microservices. Each service should own its own data.

Conceptually:

Auth Service
    ↓ UserId
User Service
    ↓ UserId
Device & Consent Service
    ↓ UserId
Transaction Service
    ↓ UserId
Other user-specific services

The fact that multiple services use UserId does NOT mean they should share the same database.

--------------------------------------------------
CURRENT TASK
--------------------------------------------------

Continue from the User Service design.

First finalize and explain:

1. UserProfile entity
2. UserPreferences entity
3. Their fields
4. Their relationships
5. Which service owns each piece of information
6. EF Core entity/configuration considerations
7. Database structure
8. API responsibilities

Teach the concepts rather than simply giving final code.

Since I am learning C# / ASP.NET Core / EF Core, explain unfamiliar concepts and, where useful, compare them with NestJS/Spring/Hibernate/Sequelize.

If my existing implementation differs from your recommendation:
- Do NOT immediately rewrite it.
- Explain the difference first.
- Explain why your approach differs.
- Explain whether the difference is required or merely a design preference.
- Let me understand the trade-off before suggesting how to change it.

Do not assume I want production-level complexity immediately. Build the architecture progressively and explain why each piece exists.
