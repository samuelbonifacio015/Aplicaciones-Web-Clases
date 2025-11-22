# Guía de Desarrollo Backend - Learning Center Platform con C# y .NET

## Repositorio del proyecto:
Proyecto de práctica utilizando C# y .NET 9.0 con arquitectura DDD (Domain-Driven Design)

---

# Orden de pasos para crear un Backend con C# y .NET

## Paso 1: Configuración Inicial del Proyecto

### Crear proyecto con .NET CLI

```bash
# Crear un nuevo proyecto Web API
dotnet new webapi -n ACME.LearningCenterPlatform.API

# Navegar al directorio del proyecto
cd ACME.LearningCenterPlatform.API
```

### ACME.LearningCenterPlatform.API.csproj - Gestión de Dependencias

Lista de dependencias principales (NuGet packages):

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>

    <ItemGroup>
        <!-- Entity Framework Core para trabajar con MySQL -->
        <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.10" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="9.0.10" />
        <PackageReference Include="MySql.EntityFrameworkCore" Version="9.0.9" />
        
        <!-- Swagger para documentación de API -->
        <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.2"/>
        <PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.6" />
        <PackageReference Include="Swashbuckle.AspNetCore.Annotations" Version="9.0.6" />
        
        <!-- Seguridad y autenticación JWT -->
        <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.10" />
        <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="8.14.0" />
        <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.14.0" />
        
        <!-- Utilidades -->
        <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
        <PackageReference Include="Humanizer" Version="2.14.1" />
        <PackageReference Include="EntityFrameworkCore.CreatedUpdatedDate" Version="8.0.0" />
        
        <!-- Mediator para CQRS -->
        <PackageReference Include="Cortex.Mediator" Version="2.1.0" />
        <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.10" />
    </ItemGroup>
</Project>
```

**Comando para restaurar dependencias:**
```bash
dotnet restore
```

**Comando para compilar:**
```bash
dotnet build
```

---

## Paso 2: Setup de Configuración

### appsettings.json

Archivo de configuración para conectar con la base de datos y configurar la aplicación:

```json
{
  "TokenSettings": {
    "Secret" : "Place here you secret for token generation"
  },
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;user=root;password=12345678;database=learning-center-wa-7452;"
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

### appsettings.Development.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

**Notas importantes:**
- **ConnectionStrings**: Cadena de conexión a MySQL
- **TokenSettings**: Configuración para JWT (si se usa autenticación)
- **Logging**: Niveles de log para desarrollo y producción

---

## Paso 3: Estructura del Proyecto (Bounded Context)

### Organización de carpetas según DDD:

```
|📂 ACME.LearningCenterPlatform.API/
│
├── 📂 Profiles/                          # Bounded Context (Contexto de Negocio)
│   │
│   ├── 📂 Domain/                        # Capa de Dominio (Lógica de Negocio)
│   │   └── 📂 Model/
│   │       ├── 📂 Aggregates/            # Entidades principales (Aggregate Roots)
│   │       │   ├── Profile.cs            # Entidad raíz del agregado
│   │       │   └── ProfileAudit.cs       # Parte del agregado (auditoría)
│   │       ├── 📂 Command/               # Comandos (escritura/modificación)
│   │       │   └── CreateProfileCommand.cs
│   │       ├── 📂 Query/                 # Consultas (solo lectura)
│   │       │   ├── GetAllProfilesQuery.cs
│   │       │   ├── GetProfileByIdQuery.cs
│   │       │   └── GetProfileByEmailQuery.cs
│   │       └── 📂 ValueObjects/          # Objetos de valor (inmutables)
│   │           ├── PersonName.cs
│   │           ├── EmailAddress.cs
│   │           └── StreetAddress.cs
│   │   │
│   │   ├── 📂 Services/                  # Interfaces de servicios
│   │   │   ├── IProfileCommandService.cs
│   │   │   └── IProfileQueryService.cs
│   │   │
│   │   └── 📂 Repositories/              # Interfaces de repositorios
│   │       └── IProfileRepository.cs
│   │
│   ├── 📂 Application/                   # Capa de Aplicación (Casos de Uso)
│   │   └── 📂 Internal/
│   │       ├── 📂 CommandServices/       # Implementación de comandos
│   │       │   └── ProfileCommandService.cs
│   │       └── 📂 QueryServices/         # Implementación de consultas
│   │           └── ProfileQueryService.cs
│   │
│   ├── 📂 Infrastructure/                # Capa de Infraestructura (Persistencia)
│   │   └── 📂 Persistence/
│   │       └── 📂 EFC/                   # Entity Framework Core
│   │           ├── 📂 Repositories/
│   │           │   └── ProfileRepository.cs
│   │           └── 📂 Configuration/
│   │               └── 📂 Extensions/
│   │                   └── ModelBuilderExtensions.cs
│   │
│   └── 📂 Interfaces/                    # Capa de Presentación (API REST)
│       └── 📂 REST/
│           ├── ProfilesController.cs     # Controlador REST
│           ├── 📂 Resources/             # DTOs de entrada/salida
│           │   ├── CreateProfileResource.cs
│           │   └── ProfileResource.cs
│           └── 📂 Transform/             # Ensambladores (Mappers)
│               ├── CreateProfileCommandFromResourceAssembler.cs
│               └── ProfileResourceFromEntityAssembler.cs
│
├── 📂 Shared/                            # Recursos compartidos entre contextos
│   ├── 📂 Domain/
│   │   ├── 📂 Model/
│   │   │   └── 📂 Events/
│   │   │       └── IEvent.cs
│   │   └── 📂 Repositories/
│   │       ├── IBaseRepository.cs
│   │       └── IUnitOfWork.cs
│   │
│   ├── 📂 Application/
│   │   └── 📂 Internal/
│   │       └── 📂 EventHandlers/
│   │           └── IEventHandler.cs
│   │
│   └── 📂 Infrastructure/
│       ├── 📂 Persistence/
│       │   └── 📂 EFC/
│       │       ├── 📂 Configuration/
│       │       │   ├── AppDbContext.cs
│       │       │   └── 📂 Extensions/
│       │       │       ├── ModelBuilderExtension.cs
│       │       │       └── StringExtensions.cs
│       │       └── 📂 Repositories/
│       │           ├── BaseRepository.cs
│       │           └── UnitOfWork.cs
│       │
│       ├── 📂 Interfaces/
│       │   └── 📂 ASP/
│       │       └── 📂 Configuration/
│       │           └── KebabCaseRouteNamingConvention.cs
│       │
│       └── 📂 Mediator/
│           └── 📂 Cortex/
│               └── 📂 Configuration/
│                   └── LoggingCommandBehavior.cs
│
├── Program.cs                            # Clase principal de la aplicación
├── appsettings.json
└── appsettings.Development.json
```

---

## Paso 4: Crear el Bounded Context

**Un Bounded Context representa un módulo de negocio específico.** En este caso: `Profiles`

Crear la estructura de carpetas manualmente o con comandos:
```bash
mkdir -p Profiles/Domain/Model/{Aggregates,Command,Query,ValueObjects}
mkdir -p Profiles/Domain/{Services,Repositories}
mkdir -p Profiles/Application/Internal/{CommandServices,QueryServices}
mkdir -p Profiles/Infrastructure/Persistence/EFC/{Repositories,Configuration/Extensions}
mkdir -p Profiles/Interfaces/REST/{Resources,Transform}
```

Cada Bounded Context debe tener estas capas:
1. **Domain** - Lógica de negocio pura
2. **Application** - Casos de uso
3. **Infrastructure** - Persistencia y servicios externos
4. **Interfaces** - API REST y presentación

---

## Paso 5: Shared Layer - Crear Infrastructure Base

### 📂 Shared/Domain/Repositories/IBaseRepository.cs

**Repositorio base con operaciones CRUD comunes:**

```csharp
namespace ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;

/// <summary>
///     Base repository interface for all repositories.
/// </summary>
/// <typeparam name="TEntity">The Entity Type</typeparam>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    ///     Add entity to the repository.
    /// </summary>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    ///     Updates an entity.
    /// </summary>
    void Update(TEntity entity);
    
    /// <summary>
    ///     Removes an entity.
    /// </summary>
    void Remove(TEntity entity);
    
    /// <summary>
    ///     Find entity by Id.
    /// </summary>
    Task<TEntity?> FindByIdAsync(int id);
    
    /// <summary>
    ///     Gets all entities.
    /// </summary>
    Task<IEnumerable<TEntity>> ListAsync();
}
```

---

### 📂 Shared/Domain/Repositories/IUnitOfWork.cs

**Unit of Work para manejo de transacciones:**

```csharp
namespace ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;

/// <summary>
///     Unit of work interface
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Commit all changes to the database.
    /// </summary>
    Task CompleteAsync();
}
```

---

### 📂 Shared/Infrastructure/Persistence/EFC/Configuration/AppDbContext.cs

**Contexto de base de datos con Entity Framework Core:**

```csharp
using ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Habilita interceptor para CreatedDate y UpdatedDate automáticos
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Aplicar configuraciones de cada Bounded Context
        builder.ApplyProfilesConfiguration();
        
        // Aplicar convención de nomenclatura snake_case
        builder.UseSnakeCaseNamingConvention();
    }
}
```

---

### 📂 Shared/Infrastructure/Persistence/EFC/Repositories/BaseRepository.cs

**Implementación base del repositorio:**

```csharp
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Base repository for all repositories
/// </summary>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;

    protected BaseRepository(AppDbContext context)
    {
        Context = context;
    }
    
    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
}
```

---

### 📂 Shared/Infrastructure/Persistence/EFC/Repositories/UnitOfWork.cs

**Implementación del Unit of Work:**

```csharp
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Unit of Work implementation for managing transactions
/// </summary>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}
```

---

### 📂 Shared/Infrastructure/Persistence/EFC/Configuration/Extensions/StringExtensions.cs

**Extensiones para convertir nombres a snake_case:**

```csharp
using Humanizer;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string text)
    {
        return text.Underscore();
    }
    
    public static string ToPlural(this string text)
    {
        return text.Pluralize();
    }
}
```

---

### 📂 Shared/Infrastructure/Persistence/EFC/Configuration/Extensions/ModelBuilderExtension.cs

**Convención de nomenclatura snake_case para la base de datos:**

```csharp
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extension
/// </summary>
public static class ModelBuilderExtension
{
    /// <summary>
    ///     Use snake case naming convention
    /// </summary>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            // Convertir nombres de tablas a snake_case y pluralizarlas
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName)) 
                entity.SetTableName(tableName.ToPlural().ToSnakeCase());
            
            // Convertir nombres de columnas a snake_case
            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());

            // Convertir nombres de claves a snake_case
            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrEmpty(keyName)) 
                    key.SetName(keyName.ToSnakeCase());
            }

            // Convertir nombres de foreign keys a snake_case
            foreach (var key in entity.GetForeignKeys())
            {
                var foreignKeyName = key.GetConstraintName();
                if (!string.IsNullOrEmpty(foreignKeyName)) 
                    key.SetConstraintName(foreignKeyName.ToSnakeCase());
            }
            
            // Convertir nombres de índices a snake_case
            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(indexName)) 
                    index.SetDatabaseName(indexName.ToSnakeCase());
            }
        }
    }
}
```

**Características:**
- Convierte nombres de clases C# (PascalCase) a snake_case en BD
- Pluraliza nombres de tablas automáticamente
- Ejemplo: `Profile` → `profiles` en la BD

---

## Paso 6: Domain Layer - Crear Value Objects

### 📂 Profiles/Domain/Model/ValueObjects/

**Los Value Objects son objetos inmutables que representan conceptos del dominio.**

**Ejemplo: PersonName.cs**
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value Object representing a Person's Name
/// </summary>
public record PersonName(string FirstName, string LastName)
{
    public PersonName() : this(string.Empty, string.Empty) { }
    
    public PersonName(string firstName) : this(firstName, string.Empty) { }
    
    public string FullName => $"{FirstName} {LastName}";
}
```

**Ejemplo: EmailAddress.cs**
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Email Address Value Object
/// </summary>
public record EmailAddress(string Address)
{
    public EmailAddress(): this(string.Empty) { }
}
```

**Ejemplo: StreetAddress.cs**
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Street Address Value Object
/// </summary>
public record StreetAddress(
    string Street, 
    string Number, 
    string City, 
    string PostalCode, 
    string Country)
{
    public StreetAddress() : this(string.Empty, string.Empty, string.Empty, 
        string.Empty, string.Empty) { }
    
    public string FullAddress => $"{Street} {Number}, {City}, {PostalCode}, {Country}";
}
```

**Características:**
- Usan `record` de C# para inmutabilidad
- Contienen lógica de negocio relacionada con el valor
- Propiedades computadas (FullName, FullAddress)
- Constructores por defecto para EF Core

---

## Paso 7: Domain Layer - Crear Commands y Queries (CQRS)

### 📂 Profiles/Domain/Model/Command/

**Los Commands son objetos que representan intenciones de modificar el estado del sistema.**

**Ejemplo: CreateProfileCommand.cs**
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Command;

public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string EmailAddress,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country);
```

**Características:**
- Usan `record` de C# para inmutabilidad
- Representan acciones de escritura (CREATE, UPDATE, DELETE)
- Contienen solo los datos necesarios para ejecutar la acción

---

### 📂 Profiles/Domain/Model/Query/

**Los Queries son objetos que representan consultas al sistema (solo lectura).**

**Ejemplo: GetProfileByIdQuery.cs**
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Query;

public record GetProfileByIdQuery(int ProfileId);
```

**Ejemplo: GetAllProfilesQuery.cs**
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Query;

public record GetAllProfilesQuery();
```

**Ejemplo: GetProfileByEmailQuery.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Query;

public record GetProfileByEmailQuery(EmailAddress Email);
```

**Características:**
- También usan `record` para inmutabilidad
- Representan consultas (GET/READ)
- Parámetros de búsqueda o filtrado

---

## Paso 8: Domain Layer - Crear Aggregates (Entidades)

### 📂 Profiles/Domain/Model/Aggregates/

**Los Aggregates son las entidades principales del dominio (raíz del agregado).**

**Ejemplo: Profile.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Command;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;

public partial class Profile
{
    public int Id { get; set; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public StreetAddress Address { get; private set; }
    
    // Propiedades computadas para facilitar acceso
    public string FullName => Name.FullName;
    public string FullAddress => Address.FullAddress;
    public string EmailAddress => Email.Address;
    
    // Constructor vacío para EF Core
    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Address = new StreetAddress();
    }
    
    // Constructor con parámetros individuales
    public Profile(string firstName, string lastName, string emailAddress,
        string street, string number, string city, string postalCode, string country)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(emailAddress);
        Address = new StreetAddress(street, number, city, postalCode, country);
    }
    
    // Constructor con Command (patrón recomendado)
    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.EmailAddress);
        Address = new StreetAddress(command.Street, command.Number, command.City, 
            command.PostalCode, command.Country);
    }
}
```

**Ejemplo: ProfileAudit.cs** (partial class para auditoría)
```csharp
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Profile aggregate audit properties
/// </summary>
public partial class Profile : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}
```

**Características importantes:**
- `partial class` permite dividir la entidad en múltiples archivos
- Propiedades con `private set` para encapsulamiento
- Value Objects como propiedades complejas
- Constructor que recibe un Command para crear la entidad
- Implementa `IEntityWithCreatedUpdatedDate` para auditoría automática

---

## Paso 9: Infrastructure Layer - Configurar Entity Framework

### 📂 Profiles/Infrastructure/Persistence/EFC/Configuration/Extensions/ModelBuilderExtensions.cs

**Configuración de EF Core para el Bounded Context:**

```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Configuración de la entidad Profile
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
        
        // Configuración del Value Object PersonName (Owned Entity)
        builder.Entity<Profile>().OwnsOne(p => p.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.FirstName).HasColumnName("FirstName");
                n.Property(p => p.LastName).HasColumnName("LastName");
            });
        
        // Configuración del Value Object EmailAddress
        builder.Entity<Profile>().OwnsOne(p => p.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(p => p.Address).HasColumnName("EmailAddress");
            });

        // Configuración del Value Object StreetAddress
        builder.Entity<Profile>().OwnsOne(p => p.Address,
            a =>
            {
                a.WithOwner().HasForeignKey("Id");
                a.Property(p => p.Street).HasColumnName("AddressStreet");
                a.Property(p => p.Number).HasColumnName("AddressNumber");
                a.Property(p => p.City).HasColumnName("AddressCity");
                a.Property(p => p.PostalCode).HasColumnName("AddressPostalCode");
                a.Property(p => p.Country).HasColumnName("AddressCountry");
            });
    }
}
```

**Características:**
- `OwnsOne` marca los Value Objects como entidades propias (owned entities)
- Se mapean las propiedades de los Value Objects a columnas específicas
- Los Value Objects no tienen su propia tabla, se almacenan en la tabla del agregado

---

## Paso 10: Domain Layer - Crear Repository Interfaces

### 📂 Profiles/Domain/Repositories/IProfileRepository.cs

**Interface del repositorio con métodos específicos del dominio:**

```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;

namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Repositories;

/// <summary>
///     Profile repository interface
/// </summary>
public interface IProfileRepository : IBaseRepository<Profile>
{
    /// <summary>
    ///     Find profile by email
    /// </summary>
    Task<Profile?> FindProfileByEmailAsync(EmailAddress email);
}
```

**Características:**
- Extiende `IBaseRepository<Profile>` heredando métodos CRUD básicos
- Define métodos personalizados específicos del dominio
- Retorna `Task<Profile?>` para operaciones asíncronas

---

## Paso 11: Infrastructure Layer - Implementar Repository

### 📂 Profiles/Infrastructure/Persistence/EFC/Repositories/ProfileRepository.cs

**Implementación del repositorio:**

```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///  Profile Repository Implementation
/// </summary>
public class ProfileRepository(AppDbContext context)
    : BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().FirstOrDefault(p => p.Email == email);
    }
}
```

**Características:**
- Extiende `BaseRepository<Profile>` para operaciones CRUD básicas
- Implementa `IProfileRepository` con métodos personalizados
- Usa el `AppDbContext` inyectado por constructor
- Accede al DbSet mediante `Context.Set<Profile>()`

---

## Paso 12: Domain Layer - Crear Service Interfaces

### 📂 Profiles/Domain/Services/

**Las interfaces de servicios definen el contrato de operaciones del dominio.**

**Ejemplo: IProfileCommandService.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Command;

namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Services;

/// <summary>
///     Profile Command Service Interface
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    ///     Handle Create Profile Command
    /// </summary>
    Task<Profile?> Handle(CreateProfileCommand command);
}
```

**Ejemplo: IProfileQueryService.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Query;

namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Services;

/// <summary>
///     Profile Query Service Interface
/// </summary>
public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    Task<Profile?> Handle(GetProfileByIdQuery query);
    Task<Profile?> Handle(GetProfileByEmailQuery query);
}
```

**Características:**
- Definen métodos `Handle()` para cada Command o Query
- Retornan `Task<Profile?>` para resultados únicos (puede no existir)
- Retornan `Task<IEnumerable<Profile>>` para múltiples resultados
- Separan Commands (escritura) de Queries (lectura)

---

## Paso 13: Application Layer - Implementar Services

### 📂 Profiles/Application/Internal/CommandServices/

**Implementación de los servicios de comandos (escritura).**

**Ejemplo: ProfileCommandService.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Command;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Services;
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;

namespace ACME.LearningCenterPlatform.API.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating profile: {e.Message}");
            return null;
        }
    }
}
```

**Características importantes:**
- Constructor con inyección de dependencias (primary constructor de C# 12)
- Crea la entidad a partir del Command
- Usa el Repository para persistir
- Usa UnitOfWork para confirmar cambios
- Manejo de excepciones con try-catch

---

### 📂 Profiles/Application/Internal/QueryServices/

**Implementación de los servicios de consultas (lectura).**

**Ejemplo: ProfileQueryService.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Query;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Services;

namespace ACME.LearningCenterPlatform.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) 
    : IProfileQueryService
{
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }

    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await profileRepository.FindProfileByEmailAsync(query.Email);
    }
}
```

**Características:**
- Inyección de dependencias por constructor
- Delegan la lógica de persistencia al Repository
- No modifican el estado (solo lectura)
- Retornan entidades del dominio

---

## Paso 14: Interfaces Layer - Crear Resources (DTOs)

### 📂 Profiles/Interfaces/REST/Resources/

**Los Resources son DTOs (Data Transfer Objects) para la API REST.**

**Ejemplo: CreateProfileResource.cs** (Request DTO)
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country
);
```

**Ejemplo: ProfileResource.cs** (Response DTO)
```csharp
namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

public record ProfileResource(int Id, string FullName, string Email, string StreetAddress);
```

**Características:**
- Son `record` inmutables para transferencia de datos
- Request Resources: reciben datos del cliente
- Response Resources: envían datos al cliente
- No contienen lógica de negocio
- Se mapean desde/hacia entidades usando Assemblers

---

## Paso 15: Interfaces Layer - Crear Assemblers (Mappers)

### 📂 Profiles/Interfaces/REST/Transform/

**Los Assemblers transforman entre Resources (DTOs) y Entidades de Dominio.**

**Ejemplo: CreateProfileCommandFromResourceAssembler.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Command;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Street,
            resource.Number,
            resource.City,
            resource.PostalCode,
            resource.Country
        );
    }
}
```

**Ejemplo: ProfileResourceFromEntityAssembler.cs**
```csharp
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(
            entity.Id, 
            entity.FullName, 
            entity.EmailAddress, 
            entity.FullAddress
        );
    }
}
```

**Características:**
- Clases estáticas con métodos de conversión
- `ToCommandFromResource()` - De Resource a Command
- `ToResourceFromEntity()` - De Entity a Resource
- Separan la lógica de transformación del controlador

---

## Paso 16: Interfaces Layer - Crear Controller (API REST)

### 📂 Profiles/Interfaces/REST/

**El Controller expone los endpoints REST de la API.**

**Ejemplo: ProfilesController.cs**
```csharp
using System.Net.Mime;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Query;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Services;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile endpoints")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService) : ControllerBase
{
    /// <summary>
    /// GET /api/v1/profiles
    /// Obtener todos los perfiles
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Profiles",
        Description = "Get All Profiles",
        OperationId = "GetAllProfiles"
    )]
    [SwaggerResponse(200, "The profiles were found", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(404, "The profiles were not found")]
    public async Task<IActionResult> GetAllProfiles()
    {
        var profiles = await profileQueryService.Handle(new GetAllProfilesQuery());
        var profileResources = profiles
            .Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }
    
    /// <summary>
    /// GET /api/v1/profiles/{profileId}
    /// Obtener un perfil por ID
    /// </summary>
    [HttpGet("{profileId:int}")]
    [SwaggerOperation(Summary = "Get Profile by Id")]
    [SwaggerResponse(200, "The profile was found", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    /// <summary>
    /// POST /api/v1/profiles
    /// Crear un nuevo perfil
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Profile",
        Description = "Create a new Profile",
        OperationId = "CreateProfile"
    )]
    [SwaggerResponse(201, "The profile was created", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile could not be created")]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileResource resource)
    {
        var createProfileCommand = 
            CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var profile = await profileCommandService.Handle(createProfileCommand);
        
        if (profile is null) return BadRequest();
        
        var profileResource = 
            ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        
        return CreatedAtAction(
            nameof(GetProfileById), 
            new { profileId = profile.Id }, 
            profileResource);
    }
}
```

**Características del Controller:**
- `[ApiController]` - Habilita validaciones automáticas y binding de modelos
- `[Route("api/v1/[controller]")]` - Define la ruta base (api/v1/profiles)
- `[Produces]` - Especifica que la API devuelve JSON
- `[SwaggerOperation]` - Documentación de Swagger
- `[HttpGet]`, `[HttpPost]` - Verbos HTTP
- `[FromBody]` - Indica que el parámetro viene del body del request
- Retorna `IActionResult` con códigos HTTP apropiados
- Usa los Assemblers para transformar datos
- Inyección de servicios por constructor

---

## Paso 17: Program.cs - Configuración de la Aplicación

### Program.cs - Punto de entrada de la aplicación

```csharp
using ACME.LearningCenterPlatform.API.Profiles.Application.Internal.CommandServices;
using ACME.LearningCenterPlatform.API.Profiles.Application.Internal.QueryServices;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Services;
using ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar URLs en minúsculas con kebab-case
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Agregar controladores
builder.Services.AddControllers();

// Obtener la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null) 
    throw new InvalidOperationException("Connection string not found.");

// Configurar DbContext con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ACME Learning Center Platform API",
            Version = "v1",
            Description = "ACME Learning Center Platform API",
            Contact = new OpenApiContact
            {
                Name = "ACME Studios",
                Email = "contact@acme.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.EnableAnnotations();
});

// ===== DEPENDENCY INJECTION =====

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// ===== BUILD APP =====

var app = builder.Build();

// Verificar y crear la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar política de CORS
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

**Características importantes:**
- `AddDbContext` configura Entity Framework Core con MySQL
- `AddScoped` registra servicios con ciclo de vida por request
- `context.Database.EnsureCreated()` crea la BD automáticamente
- `UseSwagger()` habilita la UI de documentación
- Inyección de dependencias para cada Bounded Context

---

## Paso 18: Ejecutar la Aplicación

### Comandos para ejecutar:

```bash
# Compilar el proyecto
dotnet build

# Ejecutar la aplicación
dotnet run

# O ejecutar en modo watch (recarga automática)
dotnet watch run
```

**La aplicación estará disponible en:**
```
https://localhost:5001
http://localhost:5000
```

**Swagger UI disponible en:**
```
https://localhost:5001/swagger
```

---

## Resumen del Orden de Creación

### Orden recomendado para crear un Bounded Context:

1. **Shared Layer (Infraestructura compartida):**
   - ✅ `Shared/Domain/Repositories/` - IBaseRepository, IUnitOfWork
   - ✅ `Shared/Infrastructure/Persistence/EFC/Configuration/` - AppDbContext
   - ✅ `Shared/Infrastructure/Persistence/EFC/Repositories/` - BaseRepository, UnitOfWork
   - ✅ `Shared/Infrastructure/Persistence/EFC/Configuration/Extensions/` - Naming conventions

2. **Domain Layer (Núcleo del negocio):**
   - ✅ `Domain/Model/ValueObjects/` - Crear Value Objects
   - ✅ `Domain/Model/Command/` - Crear Commands
   - ✅ `Domain/Model/Query/` - Crear Queries
   - ✅ `Domain/Model/Aggregates/` - Crear Entidades (Aggregates)
   - ✅ `Domain/Services/` - Crear interfaces de servicios
   - ✅ `Domain/Repositories/` - Crear interfaces de repositorios

3. **Infrastructure Layer (Persistencia):**
   - ✅ `Infrastructure/Persistence/EFC/Configuration/Extensions/` - Configuración EF Core
   - ✅ `Infrastructure/Persistence/EFC/Repositories/` - Implementar Repository

4. **Application Layer (Casos de uso):**
   - ✅ `Application/Internal/CommandServices/` - Implementar Command Services
   - ✅ `Application/Internal/QueryServices/` - Implementar Query Services

5. **Interfaces Layer (API REST):**
   - ✅ `Interfaces/REST/Resources/` - Crear DTOs (Resources)
   - ✅ `Interfaces/REST/Transform/` - Crear Assemblers
   - ✅ `Interfaces/REST/` - Crear Controller

6. **Program.cs:**
   - ✅ Configurar servicios
   - ✅ Configurar Dependency Injection
   - ✅ Configurar middleware

---

## Principios Arquitectónicos Aplicados

### 🏛️ DDD (Domain-Driven Design)
- Separación por Bounded Contexts
- Aggregates como raíz de consistencia
- Value Objects para conceptos inmutables
- Repositories para persistencia

### 📝 CQRS (Command Query Responsibility Segregation)
- Commands para escritura
- Queries para lectura
- Servicios separados por responsabilidad

### 🔄 Arquitectura en Capas
1. **Domain** - Lógica de negocio pura
2. **Application** - Casos de uso
3. **Infrastructure** - Detalles técnicos (EF Core, BD)
4. **Interfaces** - Puntos de entrada (API REST)

### 🧩 Dependency Inversion
- Las capas superiores no dependen de las inferiores
- Uso de interfaces para desacoplar
- Inyección de dependencias por constructor

### 🎯 Clean Architecture
- Las reglas de negocio no dependen de frameworks
- Las entidades no conocen la BD ni la API
- Independencia de UI, BD y frameworks externos

---

## Características Específicas de C# y .NET

### Records (C# 9+)
```csharp
// Inmutabilidad por defecto
public record PersonName(string FirstName, string LastName);
```

### Primary Constructors (C# 12)
```csharp
// Constructor conciso con inyección de dependencias
public class ProfileService(IProfileRepository repository)
{
    // repository está disponible automáticamente
}
```

### Nullable Reference Types
```csharp
// Indica explícitamente que puede ser nulo
public Task<Profile?> FindByIdAsync(int id);
```

### Value Objects con OwnsOne (EF Core)
```csharp
// Los Value Objects se almacenan en la misma tabla
builder.Entity<Profile>().OwnsOne(p => p.Name);
```

### Async/Await para operaciones asíncronas
```csharp
public async Task<Profile?> Handle(GetProfileByIdQuery query)
{
    return await profileRepository.FindByIdAsync(query.ProfileId);
}
```

---

## Endpoints de la API

### Base URL: `https://localhost:5001/api/v1`

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/profiles` | Obtener todos los perfiles |
| GET | `/profiles/{id}` | Obtener un perfil por ID |
| POST | `/profiles` | Crear un nuevo perfil |

### Ejemplo de Request (POST /profiles):
```json
{
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan.perez@example.com",
  "street": "Av. Universitaria",
  "number": "1801",
  "city": "Lima",
  "postalCode": "15088",
  "country": "Peru"
}
```

### Ejemplo de Response (201 Created):
```json
{
  "id": 1,
  "fullName": "Juan Pérez",
  "email": "juan.perez@example.com",
  "streetAddress": "Av. Universitaria 1801, Lima, 15088, Peru"
}
```

---

## Comandos Útiles

```bash
# Crear un nuevo proyecto Web API
dotnet new webapi -n MiProyecto.API

# Agregar paquetes NuGet
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package MySql.EntityFrameworkCore

# Restaurar dependencias
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la aplicación
dotnet run

# Ejecutar con recarga automática
dotnet watch run

# Limpiar archivos de compilación
dotnet clean

# Crear una solución
dotnet new sln -n MiSolucion

# Agregar proyecto a la solución
dotnet sln add MiProyecto.API/MiProyecto.API.csproj

# Ejecutar tests (si existen)
dotnet test
```

---

## Migraciones de Entity Framework Core

```bash
# Instalar herramientas de EF Core (global)
dotnet tool install --global dotnet-ef

# Crear una migración
dotnet ef migrations add InitialCreate

# Aplicar migraciones a la base de datos
dotnet ef database update

# Eliminar la última migración (si no se aplicó)
dotnet ef migrations remove

# Ver el SQL que generará la migración
dotnet ef migrations script
```

**Nota:** En este proyecto usamos `context.Database.EnsureCreated()` para desarrollo rápido, pero en producción se recomienda usar migraciones.

---

## Estructura de la Base de Datos

### Tabla: `profiles`

| Columna | Tipo | Descripción |
|---------|------|-------------|
| `id` | int | Clave primaria (auto-increment) |
| `first_name` | varchar | Nombre |
| `last_name` | varchar | Apellido |
| `email_address` | varchar | Email |
| `address_street` | varchar | Calle |
| `address_number` | varchar | Número |
| `address_city` | varchar | Ciudad |
| `address_postal_code` | varchar | Código postal |
| `address_country` | varchar | País |
| `created_date` | datetime | Fecha de creación |
| `updated_date` | datetime | Fecha de actualización |

**Nota:** Los nombres en snake_case son generados automáticamente por la convención de nomenclatura.

---

## Testing (Opcional)

### Crear proyecto de pruebas:

```bash
# Crear proyecto de pruebas xUnit
dotnet new xunit -n ACME.LearningCenterPlatform.Tests

# Agregar referencia al proyecto principal
dotnet add reference ../ACME.LearningCenterPlatform.API/ACME.LearningCenterPlatform.API.csproj

# Agregar paquetes para testing
dotnet add package Moq
dotnet add package FluentAssertions
```

### Ejemplo de test unitario:

```csharp
public class ProfileCommandServiceTests
{
    [Fact]
    public async Task Handle_CreateProfileCommand_ReturnsProfile()
    {
        // Arrange
        var mockRepository = new Mock<IProfileRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new ProfileCommandService(mockRepository.Object, mockUnitOfWork.Object);
        
        var command = new CreateProfileCommand(
            "Juan", "Pérez", "juan@example.com",
            "Av. Test", "123", "Lima", "15001", "Peru");
        
        // Act
        var result = await service.Handle(command);
        
        // Assert
        result.Should().NotBeNull();
        result!.FullName.Should().Be("Juan Pérez");
    }
}
```

---

## Mejores Prácticas

### ✅ DO (Hacer):
- **Usar records** para Commands, Queries y Value Objects
- **Inyección de dependencias** por constructor
- **Separar Commands y Queries** (CQRS)
- **Value Objects** para conceptos del dominio
- **Async/await** para operaciones de I/O
- **Nullable reference types** habilitados
- **Documentación con XML comments** en interfaces
- **Swagger annotations** en controllers

### ❌ DON'T (No hacer):
- No poner lógica de negocio en los Controllers
- No usar `new` para crear dependencias (usar DI)
- No mezclar Commands y Queries
- No exponer entidades del dominio directamente en la API
- No usar `DbContext` directamente en los services (usar Repository)
- No hardcodear strings de conexión

---

## Diferencias Clave entre Java/Spring Boot y C#/.NET

| Aspecto | Java/Spring Boot | C#/.NET |
|---------|------------------|---------|
| **Archivo de configuración** | `application.properties` | `appsettings.json` |
| **Dependencias** | `pom.xml` (Maven) | `.csproj` (NuGet) |
| **ORM** | Hibernate (JPA) | Entity Framework Core |
| **Anotaciones** | `@Service`, `@Repository` | Registro explícito en `Program.cs` |
| **Inmutabilidad** | `record` (Java 14+) | `record` (C# 9+) |
| **Async** | `CompletableFuture` | `async/await` (nativo) |
| **Null safety** | Optional<T> | Nullable reference types (`?`) |
| **Naming convention** | snake_case manual | Extensión automática con Humanizer |

---

## Recursos Adicionales

### Documentación Oficial:
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)

### Librerías Útiles:
- **Swashbuckle** - Documentación Swagger
- **FluentValidation** - Validaciones complejas
- **AutoMapper** - Mapeo automático de objetos
- **MediatR** - Patrón Mediator para CQRS
- **Serilog** - Logging estructurado

---

## Notas Finales

### Orden de Implementación:
1. **Shared Layer primero** (infraestructura común)
2. **Luego Domain Layer** (Commands, Queries, Aggregates, Value Objects)
3. **Infrastructure Layer** (EF Core configuration, Repositories)
4. **Application Layer** (Services)
5. **Interfaces Layer** (Resources, Assemblers, Controllers)
6. **Program.cs** (Dependency Injection)

### Consejos:
- Usar **records** para inmutabilidad
- **Primary constructors** para inyección de dependencias
- **Value Objects** con `OwnsOne` de EF Core
- **snake_case** automático con extensiones
- **Async/await** en todas las operaciones de I/O
- **Swagger** para documentación interactiva

---

**📅 Creación de este README: 22/11/2025**

**Autor: GitHub Copilot**

**Basado en el proyecto Learning Center Platform con arquitectura DDD**

**Sujeto a actualizaciones según evolución del proyecto**

