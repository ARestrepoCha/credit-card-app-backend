# credit-card-app-backend
## Backend (Core API)
- **Framework:** .NET 8 / Web API.
- **Arquitectura:** Clean Architecture / Hexagonal Architecture.
- **Persistencia:** Entity Framework Core con SQL Server.
- **Patrones:** Repository Pattern, DTOs (Data Transfer Objects), Unit of work pattern.
- **Paginación:** Implementación de paginación por cursor y metadatos de navegación.

### Pasos para el Backend
1. Clonar el repositorio.
2. Actualizar la `ConnectionString` en `appsettings.json`.
3. Ejecutar `dotnet ef database update` para aplicar migraciones.
4. `dotnet run` para iniciar la API.
