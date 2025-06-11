# 🧪 AppUnitTest

This repository demonstrates a well-structured unit testing suite for a .NET application using **xUnit** and **Entity Framework Core In-Memory Database**. The focus is on verifying both **database-level** operations and **service-layer** logic for a `User` entity.

---

## 📂 Project Structure

```
AppUnitTest/
├── Models/
│   └── User.cs
├── Services/
│   └── UserService.cs
├── DbContextService.cs
├── Test/
│   ├── DbTest/
│   │   └── UserUnitTest.cs         # Direct DB tests (CRUD)
│   └── ServiceTest/
│       └── UserServiceUnitTest.cs  # Business logic tests (via service layer)
```

---

## 🧰 Technologies Used

- [.NET 9 or later](https://dotnet.microsoft.com/)
- [xUnit](https://xunit.net/)
- [Entity Framework Core InMemory](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/)

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed

### Run Tests

Use the following command to run all unit tests:

```bash
dotnet test
```

---

## ✅ What’s Covered?

### 🔹 `UserServiceUnitTest.cs`

- `CreateUserAsync` with valid and invalid input
- `GetAllUsersAsync` with and without seed data
- `GetUserByIdAsync` success and not found cases
- `UpdateUserAsync` success, null input, and not found
- `DeleteUserAsync` success and not found

### 🔹 `UserUnitTest.cs`

- Direct DB validation for:
  - `Add`, `Get`, `Update`, and `Delete` operations
  - Handling of null and missing data

---

## ⚙️ GitHub Actions (CI/CD)

This project includes GitHub Actions to automatically build and test your code on every push and pull request to `main`.

```yaml
# .github/workflows/dotnet.yml

name: .NET Build and Test

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0.x'

    - name: Restore dependencies
      run: dotnet restore

    - name: Build
      run: dotnet build --no-restore

    - name: Test
      run: dotnet test --no-build --verbosity normal
```

---

## 📸 Sample Output

```
Restore complete (0.9s)
  AppUnitTest succeeded (7.3s) → AppUnitTest\bin\Debug\net9.0\AppUnitTest.dll
  AppUnitTest.Test succeeded (2.5s) → AppUnitTest.Test\bin\Debug\net9.0\AppUnitTest.Test.dll
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.8.2+699d445a1a (64-bit .NET 9.0.3)
[xUnit.net 00:00:00.16]   Discovering: AppUnitTest.Test
[xUnit.net 00:00:00.18]   Discovered:  AppUnitTest.Test
[xUnit.net 00:00:00.19]   Starting:    AppUnitTest.Test
[xUnit.net 00:00:00.67]   Finished:    AppUnitTest.Test
  AppUnitTest.Test test succeeded (1.6s)

Test summary: total: 21, failed: 0, succeeded: 21, skipped: 0, duration: 1.6s
Build succeeded in 12.7s
```

---

## 🙋‍♂️ Author

**Nawaf AL-Maqbali**  
📧 [LinkedIn](https://github.com/nawaf91maqbali)
