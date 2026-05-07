# LMS Backend API

## Tech Stack
* ASP.NET Core 8
* Entity Framework Core 8
* SQL Server (Docker)


## Architectural & Security Notes
* **Password Hashing:** Implemented using BCrypt with automatic salting.
* **SQL Injection Prevention:** Enforced inherently via EF Core parameterized queries.
* **JWT Configuration:** The JWT Secret Key is placed in `appsettings.json` strictly for local evaluation and testing convenience. In a production environment, this must be securely injected via Environment Variables or a Secrets Manager.
* **Port Configuration:** The API is explicitly bound to `http://localhost:8081` to avoid common local port conflicts.

## Setup and Execution

### 1. Database Setup
Ensure Docker is running, then execute the following command to start the required SQL Server container:

```bash

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=LmsAdmin@123456" -p 1433:1433 --name sql_server_lms -d mcr.microsoft.com/mssql/server:2022-latest

```




2. Run the Application
Navigate to the project root directory and execute:

Bash

dotnet run

Testing & API Documentation
Once running, access the Swagger UI at: http://localhost:8081/swagger

Seeded Test Accounts
Use the following credentials at the POST /api/Auth/login endpoint to generate a Bearer token.

## Admin Account

Email: admin@lms.com

Password: 123

Permissions: Full access (Create, Update, Delete, View courses)

Trainee Account

Email: trainee@lms.com

Password: 123

Permissions: Read-only (View courses)
