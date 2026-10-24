# Workout Assistant

> ASP.NET Core MVC application for creating, organizing, and managing workout plans, workouts, and exercises.

![.NET Version](https://img.shields.io/badge/.NET-10.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-blue)

---

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Features](#features)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## 📖 About the Project

Workout Assistant is a .NET 10 ASP.NET Core MVC project designed for managing workout plans. It supports creating workout routines, ordering workouts within a plan, and handling exercise data through a layered architecture built around EF Core and service abstractions.

The application is organized into separate projects for the web app, data access, business services, shared constants, and web view models to keep responsibilities clearly separated.

---

## 🛠️ Technologies Used

| Technology | Version | Purpose |
| --- | --- | --- |
| ASP.NET Core MVC | 10.0 | Web application framework |
| Entity Framework Core | 10.0 | Data access and database mapping |
| SQL Server | - | Relational database |
| Razor Views | - | UI rendering |
| Bootstrap | - | Styling and layout |
| .NET 10 SDK | 10.0 | Build and runtime |

---

## ✅ Prerequisites

Before running the project, make sure you have:

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) available locally or a compatible SQL Server instance
- [Git](https://git-scm.com/)

---

## 📁 Project Structure

```text
WorkoutAssistant/
├── WorkoutAssistant/                     # ASP.NET Core MVC web application
│   ├── Controllers/                     # Home, Workout, WorkoutPlan, Exercise controllers
│   ├── Views/                          # Razor views
│   ├── wwwroot/                        # Static assets (CSS, JS, images)
│   ├── Program.cs                      # Application startup and service registration
│   ├── appsettings.json                # Default app configuration
│   ├── appsettings.Development.json   # Local development overrides
│   └── Properties/                     # Project launch settings
├── WorkoutAssistant.Data/               # EF Core data layer
│   ├── Configuration/                  # Model configuration classes
│   ├── Migrations/                     # EF Core migrations
│   ├── Models/                         # Domain entities (WorkoutPlan, Workout, Exercise)
│   ├── AppDbContext.cs                 # Database context
│   └── WorkoutAssistant.Data.csproj
├── WorkoutAssistant.Services.Core/      # Application services and abstractions
│   ├── Contracts/                      # Service interfaces
│   ├── Services/                       # Business logic implementation
│   ├── ServiceCollectionExtensions.cs # Dependency injection registration
│   └── WorkoutAssistant.Services.Core.csproj
├── WorkoutAssistant.Shared/              # Shared constants and enums
│   ├── Enums/
│   └── ValidationConstants.cs
├── WorkoutAssistant.Web.Models/         # View models and input models
│   ├── Exercise/
│   ├── Workout/
│   ├── WorkoutPlan/
│   └── ErrorViewModel.cs
├── examples/                           # Reference/example code snippets
├── WorkoutAssistant.slnx                # Solution file
├── README.md
└── .gitignore
```

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/yordanihl/WorkoutAssistant.git
cd WorkoutAssistant
```

### 2. Restore dependencies and tools

```bash
dotnet restore
dotnet tool restore
```

### 3. Update the connection string

The default connection string is defined in `WorkoutAssistant/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=WorkoutAssistantDb;Trusted_Connection=True;Encrypt=False"
  }
}
```

If needed, update it to match your local SQL Server instance.

### 4. Apply database migrations

```bash
dotnet ef database update --project .\WorkoutAssistant.Data\WorkoutAssistant.Data.csproj --startup-project .\WorkoutAssistant\WorkoutAssistant.Web.csproj
```

### 5. Run the application

```bash
dotnet run --project .\WorkoutAssistant\WorkoutAssistant.Web.csproj
```

The app will run from the ASP.NET Core project and is typically available at the default development ports provided by ASP.NET Core.

---

## ✨ Features

- Create and manage workout plans
- Add, edit, and delete workouts within plans
- Organize workout order with reorder functionality
- Manage exercises associated with workouts
- Use database-backed persistence through EF Core
- Validate input using shared constants and model validation rules
- Follow a layered architecture for cleaner separation of concerns

---

## ⚙️ Configuration

Application configuration is stored in the web project under `WorkoutAssistant/appsettings.json` and `WorkoutAssistant/appsettings.Development.json`.

Key settings include:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=WorkoutAssistantDb;Trusted_Connection=True;Encrypt=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 📄 License

A root license file has not been added to this repository yet. If you plan to publish or distribute the project, add an appropriate license before release.

---

## 📬 Contact

**Yordan Iliev** – [@yordanihl](https://github.com/yordanihl)

Project Link: [https://github.com/yordanihl/WorkoutAssistant](https://github.com/yordanihl/WorkoutAssistant)
