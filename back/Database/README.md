# Configuración de Base de Datos

Este proyecto utiliza **Stored Procedures** con **Dapper** para optimizar el rendimiento de las consultas a la base de datos.

## Tecnologías Utilizadas

- **SQL Server**: Base de datos
- **Dapper**: Micro-ORM para mapeo de objetos
- **Stored Procedures**: Para operaciones de base de datos optimizadas

## Ventajas de Stored Procedures

1. **Rendimiento**: Ejecución más rápida que consultas dinámicas
2. **Seguridad**: Prevención de SQL Injection
3. **Mantenimiento**: Lógica centralizada en la base de datos
4. **Optimización**: Planes de ejecución reutilizables

## Scripts de Base de Datos

### 1. Probar Conexión
```sql
-- Ejecutar: Database/Scripts/TestConnection.sql
```

### 2. Crear Base de Datos
```sql
-- Ejecutar: Database/Scripts/CreateDatabase.sql
```

### 3. Crear Stored Procedures
```sql
-- Ejecutar: Database/Scripts/StoredProcedures.sql
```

## Stored Procedures Implementados

| Procedimiento | Descripción |
|---------------|-------------|
| `sp_GetUserById` | Obtener usuario por ID |
| `sp_GetAllUsers` | Obtener todos los usuarios |
| `sp_GetUserByEmail` | Obtener usuario por email |
| `sp_GetUsersByCity` | Obtener usuarios por ciudad |
| `sp_CreateUser` | Crear nuevo usuario |
| `sp_UpdateUser` | Actualizar usuario existente |
| `sp_DeleteUser` | Eliminar usuario |
| `sp_UserExists` | Verificar si existe usuario |
| `sp_EmailExists` | Verificar si existe email |
| `sp_EmailExistsForOtherUser` | Verificar email para otro usuario |

## Configuración de Conexión

La cadena de conexión se configura en `appsettings.json` para conectarse al servidor SQL Server local "JOHN" con autenticación de Windows:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=JOHN;Database=HexagonalArchitectureDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

### Parámetros de Conexión:
- **Server=JOHN**: Servidor SQL Server local
- **Database=HexagonalArchitectureDb**: Base de datos del proyecto
- **Trusted_Connection=true**: Autenticación de Windows
- **MultipleActiveResultSets=true**: Permite múltiples consultas simultáneas
- **TrustServerCertificate=true**: Confía en el certificado del servidor (para desarrollo)

## Pasos para Configurar

1. **Verificar** que SQL Server "JOHN" esté ejecutándose
2. **Ejecutar** `Database/Scripts/TestConnection.sql` para probar la conexión
3. **Ejecutar** `Database/Scripts/CreateDatabase.sql` para crear la base de datos
4. **Ejecutar** `Database/Scripts/StoredProcedures.sql` para crear los Stored Procedures
5. **Verificar** la cadena de conexión en `appsettings.json`
6. **Ejecutar** la aplicación

## Estructura de la Tabla Users

```sql
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Age INT NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    PhotoUrl NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL
);
``` 