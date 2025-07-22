# 🌸 Azaliq.WebApp

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet&logoColor=white)
![Platform](https://img.shields.io/badge/platform-ASP.NET%20Core%20MVC-lightgrey?logo=visualstudio)
![Status](https://img.shields.io/badge/status-in%20development-orange)
![License](https://img.shields.io/badge/license-Apache--2.0-green)

Azaliq.WebApp is a full-stack **ASP.NET Core MVC** application for a modern online flower shop.  
It includes user-friendly storefronts, shopping cart functionality, and a robust admin panel.  
🌿 Built following the layered architecture of your CinemaApp project.

---

## 📦 Tech Stack

```csharp
🖥️ ASP.NET Core MVC (.NET 7)
🛢️ Entity Framework Core + SQL Server
🎨 Razor Pages + Bootstrap
🔐 ASP.NET Identity (roles: Admin, Manager, User)
```

---

## 🚀 Features

- 🛍️ Flower product catalog (with category filtering)
- 🛒 Shopping cart + checkout flow
- 👤 Login/Register (via ASP.NET Core Identity)
- 🛠️ Admin panel for managing products/categories/orders
- 📦 Order history and archived users/orders
- 🌐 Responsive UI for desktop and mobile

---

## 🧰 Project Structure

```plaintext
Azaliq.WebApp.sln
│
├── Azaliq.WebApp              // MVC UI Layer (Controllers, Views)
├── Azaliq.Services.Core       // Business logic layer (DI services)
├── Azaliq.Data.Models         // Entity Models and EF Configurations
├── Azaliq.ViewModels          // DTOs used for Views
├── Azaliq.GCommon             // Utilities and constants
└── Azaliq.Configurations      // AutoMapper, service extensions, etc.
```

---

## ⚙️ Prerequisites

```bash
✅ .NET 7 SDK or later
✅ Visual Studio 2022 or VS Code
✅ SQL Server (Express/Developer)
```

---

## 🛠️ Setup Instructions

<details>
<summary>🧪 1. Clone the repository</summary>

```bash
git clone https://github.com/edvinhubbyy/Azaliq.WebApp.git
cd Azaliq.WebApp
```
</details>

<details>
<summary>📦 2. Restore dependencies</summary>

```bash
dotnet restore
```
</details>

<details>
<summary>🏗️ 3. Build the solution</summary>

```bash
dotnet build
```
</details>

<details>
<summary>⚙️ 4. Configure your database</summary>

- Open `appsettings.json` or `appsettings.Development.json`
- Edit the `DefaultConnection` string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AzaliqDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
</details>

<details>
<summary>🧱 5. Apply EF migrations</summary>

```bash
dotnet ef database update
```
</details>

<details>
<summary>🚀 6. Run the application</summary>

```bash
dotnet run --project Azaliq.WebApp
```

## 👥 User Roles

```plaintext
🔑 Admin     → Full access to everything
🧰 Manager   → Can manage products & orders
👤 User      → Can shop, view orders
```

---

## 📌 Roadmap

| Feature                     | Status       |
|----------------------------|--------------|
| Product image upload       | 🟡 In progress
| Cart & Checkout flow       | ✅ Done
| Archive user/orders        | ✅ Done
| Order re-purchase          | 🟡 Working on it
| Admin Dashboard UI polish  | 🟠 Needs improvement
| CI/CD                      | ⏳ Not yet

---

## 📄 License

```txt
Apache License 2.0
© 2025 Edvin Hubbyy
