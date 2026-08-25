# 🎮 Bullet Brawl - Core Backend API

This repository contains the backend server architecture for **Bullet Brawl**, a multiplayer action game developed with Unity. The system is designed as a RESTful Web API using ASP.NET Core (.NET 9) to handle player authentication, secure state management, and in-game economic transactions independently from the game client.

## 🏗️ Architecture & Tech Stack

To ensure scalability and maintainability, the project is built upon a **Layered Architecture** utilizing the **Repository Pattern** for database operations.

- **Backend Framework:** C#, ASP.NET Core Web API (.NET 9)
- **Database & ORM:** SQL Server, Entity Framework Core (Code-First Migration)
- **Security:** JWT (JSON Web Token) Authentication, HMACSHA512 Password Hashing
- **Data Transfer:** DTOs (Data Transfer Objects) are strictly used to prevent over-posting and secure sensitive entity data.
- **Documentation:** Swagger / OpenAPI integration for endpoint testing.

## 🎯 Core Modules & Business Logic

### 1. Authentication & Security Module
Handles secure user registration and login processes. Passwords are never stored in plain text; they are secured using advanced hashing algorithms. Upon a successful login, a JWT is generated containing specific user claims (like Player ID) to maintain a stateless and secure connection with the Unity client.
> <img width="1430" height="935" alt="SwaggerWebAPI" src="https://github.com/user-attachments/assets/585fe964-32ad-41e8-bcde-390de00165f5" />

### 2. Economy & Market Engine
The market system is entirely server-authoritative to prevent client-side cheating (e.g., memory manipulation via Cheat Engine). The backend strictly validates:
- If the player has a sufficient coin balance.
- If the requested weapon already exists in the player's inventory (preventing duplicate purchases).
- Secure deduction of coins and asynchronous updating of the inventory database.
> <img width="1200" height="683" alt="UnityWebAPI3" src="https://github.com/user-attachments/assets/15f07c93-7abe-48dc-a62e-0256837e6c27" />


## 📡 API Endpoints Reference

The following table details the core endpoints communicating with the Unity client.

| HTTP Method | Route (Endpoint) | Module Description | Authorization |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/register` | Registers a new player into the database. | 🔓 Public |
| `POST` | `/api/auth/login` | Authenticates player and returns Bearer JWT. | 🔓 Public |
| `POST` | `/api/market/add-coins` | Updates the authorized player's wallet balance. | 🔒 JWT Required |
| `POST` | `/api/market/buy` | Executes server-side validation for item purchase. | 🔒 JWT Required |
| `GET`  | `/api/market/inventory` | Fetches the protected inventory of the current player. | 🔒 JWT Required |

## ⚙️ Local Development Setup

To test the backend logic locally:
1. Update the `ConnectionStrings` in `appsettings.json` with your local SQL Server credentials.
2. Apply the EF Core migrations to generate the database schema (`Update-Database`).
3. Run the API. Swagger UI will launch automatically. For secured endpoints (🔒), authenticate via `/api/auth/login` and inject the Bearer token into the Authorization header.
