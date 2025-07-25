# StenglonesApi

A modern ASP.NET Core MVC API built with best practices and leveraging the latest features from .NET 9 and C# 13.

## Main Features

- **FizzBuzz API:** Generates FizzBuzz results for a specified number range.  
- **MachineMetrics API:** Collects machine sensor data (temperature & rotation speed), supports adding, retrieving, and updating metrics, and stores them in a SQLite database.

---

## Features

- Clean MVC architecture with well-separated Controllers, Services, Models, and DTOs  
- SQLite database integration via Entity Framework Core  
- Swagger UI for interactive API documentation and testing, available at the root URL (`http://localhost:5065/`)  
- Robust validation with proper HTTP responses, including centralized exception handling middleware for consistent error management
- Comprehensive negative test cases to ensure API resilience and correct error responses
- **Behavior Driven Development (BDD):** Comprehensive tests using Gherkin feature files and step definitions (SpecFlow)  
- xUnit & BDD tests to ensure code quality and correctness  
- Utilizing modern C# 13 language features (Primary Constructors, Interpolated Strings and more)  
- CI/CD pipeline for automated builds and tests  
- Supports graceful cancellation via `CancellationToken` in controller actions and services  


---

## Quickstart

1. Install .NET 9 SDK  
2. Run `dotnet restore`  
3. Run `dotnet build`  
4. Run `dotnet test` (executes both xUnit and BDD tests)  
5. Start the API: `dotnet run --project StenglonesApi`  

Swagger UI will be available at the root URL: `http://localhost:5065/`.

---

## Let's Connect!

[![LinkedIn](https://img.shields.io/badge/LinkedIn-%230077B5.svg?&style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/bastian-stenglein/)  
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:stenglein.bastian@hotmail.com)
