# 🎬 PeliculasAPI - Movies Management System

A comprehensive RESTful API built with **ASP.NET Core 9.0** demonstrating modern backend development practices, designed as a showcase of advanced .NET concepts and patterns.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-512BD4?style=flat)](https://docs.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat&logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.txt)

## 📋 Table of Contents

- [Overview](#-overview)
- [Key Features](#-key-features)
- [Technologies & Patterns](#-technologies--patterns)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [API Endpoints](#-api-endpoints)
- [Database Schema](#-database-schema)
- [Authentication & Authorization](#-authentication--authorization)
- [Testing](#-testing)
- [Project Structure](#-project-structure)
- [Configuration](#-configuration)
- [Learning Outcomes](#-learning-outcomes)

## 🎯 Overview

PeliculasAPI is a production-ready REST API for managing a movie database system. It demonstrates enterprise-level implementation of ASP.NET Core concepts including authentication, authorization, file management, spatial data, caching strategies, and comprehensive CRUD operations.

**Perfect for demonstrating:**
- Modern .NET development skills
- Clean architecture principles
- RESTful API design
- Security best practices
- Database design and Entity Framework Core mastery

## ✨ Key Features

### Core Functionality
- 🎥 **Complete Movie Management** - CRUD operations for movies, actors, genres, and theaters
- 🔐 **JWT Authentication** - Secure token-based authentication with ASP.NET Core Identity
- 👥 **User Management** - Registration, login, and role-based access control
- ⭐ **Rating System** - Users can rate movies with personalized vote tracking
- 🔍 **Advanced Filtering** - Search and filter movies by title, genre, release date, and theater availability
- 📄 **Pagination** - Efficient data retrieval with pagination support
- 📸 **File Upload** - Image handling for actors and movie posters (Azure Storage/Local)
- 🗺️ **Spatial Data** - Geographic location support for movie theaters using NetTopologySuite
- 🚀 **Output Caching** - Performance optimization with cache invalidation strategies
- 🌐 **CORS Support** - Configured for cross-origin requests

### Advanced Patterns
- **Many-to-Many Relationships** - Complex entity relationships (Movies-Actors, Movies-Genres, Movies-Theaters)
- **AutoMapper Integration** - Clean object-to-object mapping
- **Custom Validation Attributes** - Business rule validation at the model level
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Full IoC container utilization
- **DTOs Pattern** - Separation of domain models and API contracts

## 🛠️ Technologies & Patterns

### Backend Technologies
```
- ASP.NET Core 9.0 Web API
- Entity Framework Core 9.0
- SQL Server with NetTopologySuite (Spatial Data)
- ASP.NET Core Identity
- JWT Bearer Authentication
- AutoMapper 14.0
- Azure Blob Storage
- Swagger/OpenAPI
```

### Key NuGet Packages
```xml
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite
- AutoMapper
- Azure.Storage.Blobs
- Swashbuckle.AspNetCore
```

### Testing Framework
```
- MSTest
- NSubstitute (Mocking)
- Entity Framework Core InMemory Database
```

## 🏗️ Architecture

### Project Structure
```
PeliculasAPI/
├── Controllers/         # API endpoints
├── DTOs/               # Data Transfer Objects
├── Entidades/          # Domain entities
├── Servicios/          # Business logic services
├── Utilidades/         # Helper classes and extensions
├── Validaciones/       # Custom validation attributes
├── Migrations/         # EF Core migrations
└── wwwroot/           # Static files (uploaded images)

PeliculasAPIPruebas/
├── Controller/         # Controller unit tests
└── Dobles/            # Test doubles (mocks/fakes)
```

### Entity Relationship Diagram

```
┌─────────────┐       ┌──────────────────┐       ┌─────────────┐
│   Pelicula  │───────│ PeliculaGenero   │───────│   Genero    │
└─────────────┘       └──────────────────┘       └─────────────┘
       │
       │              ┌──────────────────┐       ┌─────────────┐
       ├──────────────│ PeliculaCine     │───────│    Cine     │
       │              └──────────────────┘       └─────────────┘
       │
       │              ┌──────────────────┐       ┌─────────────┐
       ├──────────────│ PeliculaActor    │───────│    Actor    │
       │              └──────────────────┘       └─────────────┘
       │
       │              ┌──────────────────┐       ┌─────────────┐
       └──────────────│     Rating       │───────│    User     │
                      └──────────────────┘       └─────────────┘
```

## 🚀 Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or Full)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- (Optional) [Azure Storage Account](https://azure.microsoft.com/services/storage/) for cloud file storage

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/PeliculasAPI.git
   cd PeliculasAPI
   ```

2. **Update the connection string**

   Edit `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=PeliculasAPI;Integrated Security=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd PeliculasAPI
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access Swagger UI**
   ```
   https://localhost:5001/swagger
   ```

### Docker Support (Optional)

```dockerfile
# Coming soon - Docker configuration for containerized deployment
```

## 📡 API Endpoints

### Authentication & Users
```http
POST   /api/usuarios/registrar          # Register new user
POST   /api/usuarios/login               # User login (returns JWT)
GET    /api/usuarios/ListadoUsuarios     # List all users (Admin only)
POST   /api/usuarios/HacerAdmin          # Grant admin role (Admin only)
POST   /api/usuarios/RemoverAdmin        # Revoke admin role (Admin only)
```

### Movies
```http
GET    /api/peliculas/landing            # Get featured movies (in theaters & upcoming)
GET    /api/peliculas/{id}               # Get movie details with ratings
GET    /api/peliculas/filtrar            # Filter movies with advanced criteria
GET    /api/peliculas/PostGet            # Get form data (genres, theaters)
POST   /api/peliculas                    # Create new movie (Admin only)
PUT    /api/peliculas/{id}               # Update movie (Admin only)
DELETE /api/peliculas/{id}               # Delete movie (Admin only)
GET    /api/peliculas/PutGet/{id}        # Get movie edit data (Admin only)
```

### Genres
```http
GET    /api/generos                      # List all genres (cached)
GET    /api/generos/{id}                 # Get genre by ID
POST   /api/generos                      # Create genre (Admin only)
PUT    /api/generos/{id}                 # Update genre (Admin only)
DELETE /api/generos/{id}                 # Delete genre (Admin only)
```

### Actors
```http
GET    /api/actores                      # List actors with pagination
GET    /api/actores/BuscarPorNombre      # Search actors by name
GET    /api/actores/{id}                 # Get actor by ID
POST   /api/actores                      # Create actor with photo (Admin only)
PUT    /api/actores/{id}                 # Update actor (Admin only)
DELETE /api/actores/{id}                 # Delete actor (Admin only)
```

### Movie Theaters
```http
GET    /api/cines                        # List all theaters (cached)
GET    /api/cines/cercanos               # Find nearby theaters (spatial query)
GET    /api/cines/{id}                   # Get theater by ID
POST   /api/cines                        # Create theater with location (Admin only)
PUT    /api/cines/{id}                   # Update theater (Admin only)
DELETE /api/cines/{id}                   # Delete theater (Admin only)
```

### Ratings
```http
POST   /api/ratings                      # Rate a movie (Authenticated users)
```

### Example Request: Login
```bash
curl -X POST "https://localhost:5001/api/usuarios/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "YourPassword123!"
  }'
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiracion": "2026-01-19T00:00:00Z"
}
```

### Example Request: Create Movie
```bash
curl -X POST "https://localhost:5001/api/peliculas" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: multipart/form-data" \
  -F "titulo=Inception" \
  -F "fechaLanzamiento=2010-07-16" \
  -F "poster=@inception.jpg" \
  -F "generosIds=1" \
  -F "cinesIds=1,2,3"
```

## 🗄️ Database Schema

### Main Entities

**Pelicula (Movie)**
```csharp
- Id: int (PK)
- Titulo: string (Required, MaxLength: 300)
- Trailer: string (nullable)
- FechaLanzamiento: DateTime
- Poster: string (URL)
```

**Actor**
```csharp
- Id: int (PK)
- Nombre: string (Required, MaxLength: 120)
- FechaNacimiento: DateTime
- Foto: string (URL)
```

**Genero (Genre)**
```csharp
- Id: int (PK)
- Nombre: string (Required, MaxLength: 40)
```

**Cine (Theater)**
```csharp
- Id: int (PK)
- Nombre: string (Required, MaxLength: 120)
- Ubicacion: Point (Spatial data - Latitude/Longitude)
```

**Rating**
```csharp
- Id: int (PK)
- Puntuacion: int (1-5)
- PeliculaId: int (FK)
- UsuarioId: string (FK - ASP.NET Identity User)
```

### Junction Tables (Many-to-Many)
- **PeliculaGenero** - Movies ↔ Genres
- **PeliculaCine** - Movies ↔ Theaters
- **PeliculaActor** - Movies ↔ Actors (with Order property for billing order)

## 🔐 Authentication & Authorization

### JWT Configuration
- **Token Lifetime**: 1 year
- **Algorithm**: HMAC SHA-256
- **Claims**: Email, custom claims (admin role)

### Authorization Policies
```csharp
[Authorize]                    // Requires authenticated user
[Authorize(Policy = "esadmin")] // Requires admin role
[AllowAnonymous]               // Public access
```

### Security Features
- Password hashing with ASP.NET Core Identity
- Role-based access control (RBAC)
- JWT token validation
- Secure password requirements

## 🧪 Testing

The project includes comprehensive unit tests:

### Test Coverage
- **Controller Tests** - API endpoint behavior
- **Validation Tests** - Custom attribute validation
- **Integration Tests** - Database operations with InMemory provider

### Running Tests
```bash
cd PeliculasAPIPruebas
dotnet test
```

### Test Example
```csharp
[TestMethod]
public void IsValid_DebeRetornarExitoso_SiLaPrimeraLetraEsMayuscula()
{
    // Preparar (Arrange)
    var attribute = new PrimeraLetraMayusculaAttribute();
    var context = new ValidationContext(new object());

    // Probar (Act)
    var resultado = attribute.GetValidationResult("Dairan", context);

    // Verificar (Assert)
    Assert.AreEqual(ValidationResult.Success, resultado);
}
```

## 📁 Project Structure

### Key Files

**Program.cs** - Application configuration and middleware pipeline
```csharp
- Dependency Injection setup
- Authentication & Authorization configuration
- Entity Framework DbContext registration
- CORS policy configuration
- Output caching configuration
- AutoMapper configuration
```

**ApplicationDbContext.cs** - Database context
```csharp
- Entity configurations
- Many-to-many relationship setup
- DbSet definitions
```

**AutoMapperProfiles.cs** - Object mapping configuration
```csharp
- Entity ↔ DTO mappings
- Spatial data transformations
- File upload handling
```

## ⚙️ Configuration

### App Settings Structure
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "SQL Server connection string",
    "AzureStorageConnection": "Azure Storage connection string"
  },
  "llavejwt": "Your secure JWT secret key",
  "origenesPermitidos": "http://localhost:4200,https://yourdomain.com"
}
```

### Storage Options
The API supports two storage providers:

1. **Local Storage** (Default in code)
   ```csharp
   builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
   ```

2. **Azure Blob Storage**
   ```csharp
   builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
   ```

## 📚 Learning Outcomes

This project demonstrates proficiency in:

### ASP.NET Core Concepts
✅ RESTful API design and implementation
✅ Dependency Injection and IoC containers
✅ Middleware pipeline configuration
✅ Authentication & Authorization (JWT, Identity)
✅ Model validation and custom validators
✅ File upload and management
✅ CORS configuration
✅ Output caching strategies

### Entity Framework Core
✅ Code-First approach with migrations
✅ Complex relationship mapping (1-to-Many, Many-to-Many)
✅ Query optimization and eager/lazy loading
✅ Spatial data types (NetTopologySuite)
✅ Database seeding and configuration

### Software Engineering
✅ Clean Architecture principles
✅ Repository and Service patterns
✅ DTO pattern for API contracts
✅ SOLID principles
✅ Unit testing with MSTest
✅ Mocking and test doubles

### Security
✅ JWT token generation and validation
✅ Role-based access control
✅ Secure password storage
✅ Input validation and sanitization

### DevOps & Tools
✅ Git version control
✅ NuGet package management
✅ API documentation with Swagger
✅ Configuration management

## 🤝 Contributing

This is a learning/portfolio project. Suggestions and improvements are welcome!

1. Fork the project
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## 👤 Author

**Daira** - [GitHub Profile](https://github.com/DairaDoo)

## 🙏 Acknowledgments

- Built as a comprehensive learning project to master ASP.NET Core
- Inspired by modern enterprise application architecture
- Demonstrates real-world development patterns and practices

---

⭐ **Star this repository** if you find it helpful for learning ASP.NET Core!

📧 **Contact**: For questions or opportunities, feel free to reach out.

🔗 **Portfolio**: [https://dairadoo.github.io/Portafolio.github-io/]

---

*Last Updated: July 2025*
