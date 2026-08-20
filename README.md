# RecipeHub 🍳

RecipeHub is a full-stack recipe management and meal planning web application built with **ASP.NET Core MVC** and **Microsoft SQL Server**. It allows users to discover, create, organize, and manage recipes along with their ingredients and categories.

---

## 🚀 Features

- **Recipe Management**: Create, edit, view, and delete recipes with detailed instructions, cooking/prep times, difficulty levels, and cuisine types.
- **Ingredient & Measurement Tracking**: Manage ingredient catalogs and map them to recipes with quantities and units.
- **Category Organization**: Categorize recipes for structured browsing and filtering.
- **User Authentication**: Integrated ASP.NET Core Identity for secure user registration and login.
- **Meal Planning & Favorites**: Data models ready for user meal plans, meal items, ratings, and favorites.

---

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 10.0 (MVC & Razor Pages)
- **ORM / Data Access**: Entity Framework Core 10.0 (SQL Server Provider)
- **Database**: Microsoft SQL Server
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Razor Views, HTML5, CSS3, JavaScript, Bootstrap

---

## 📁 Project Structure

```text
RecipeHub/
├── Database/               # Database creation and verification SQL scripts
│   ├── RecipeHub_Database.sql
│   └── RecipeHub_Database_Verification.sql
├── Documents/              # Agile sprint plans, requirement specs, and design docs
├── RecipeHub.Mvc/          # ASP.NET Core MVC web application
│   ├── Controllers/        # MVC Controllers (Recipe, Ingredient, RecipeIngredient, Home)
│   ├── Models/             # Domain and View Models
│   ├── Views/              # Razor Views (.cshtml)
│   ├── Data/               # ApplicationDbContext & EF Core Migrations
│   ├── wwwroot/            # Static assets (CSS, JS, libraries)
│   ├── appsettings.json    # Application configuration & connection strings
│   └── Program.cs          # Application entry point & service configurations
└── RecipeHub.slnx          # Solution file
```

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Microsoft SQL Server](https://www.microsoft.com/sql-server) or LocalDB
- [Visual Studio 2022 / 2025](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Setup & Run

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Sanchet1234/RecipeHub.git
   cd RecipeHub
   ```

2. **Configure Database Connection:**
   Update the connection string in `RecipeHub.Mvc/appsettings.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=RecipeHubDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   ```

3. **Apply Migrations / Setup Database:**
   Apply EF Core migrations:
   ```bash
   cd RecipeHub.Mvc
   dotnet ef database update
   ```
   *(Alternatively, execute `Database/RecipeHub_Database.sql` on your SQL Server instance).*

4. **Run the Application:**
   ```bash
   dotnet run
   ```
   Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`.
