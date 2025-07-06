# Estructura de Arquitectura Hexagonal en C#.NET

Este proyecto implementa la estructura base de una arquitectura hexagonal (también conocida como arquitectura de puertos y adaptadores) en C#.NET 8.0.

## Estructura del Proyecto

```
src/
├── Domain/                 # Capa de dominio (núcleo de negocio)
│   ├── Entities/          # Entidades de dominio
│   │   └── BaseEntity.cs  # Clase base para entidades
│   └── Repositories/      # Interfaces de repositorios (puertos de salida)
│       └── IBaseRepository.cs # Interfaz base para repositorios
├── Application/           # Capa de aplicación (casos de uso)
│   └── UseCases/         # Casos de uso de la aplicación
│       └── IBaseUseCase.cs # Interfaz base para casos de uso
├── Infrastructure/        # Capa de infraestructura (adaptadores de salida)
│   ├── Data/             # Contexto de Entity Framework
│   │   └── BaseDbContext.cs # Clase base para contexto
│   └── Repositories/     # Implementaciones de repositorios
│       └── BaseRepository.cs # Implementación base para repositorios
└── API/                  # Capa de presentación (adaptadores de entrada)
    ├── Controllers/      # Controladores de la API
    │   └── BaseController.cs # Controlador base
    ├── Program.cs        # Configuración de la aplicación
    └── appsettings.json  # Configuración
```

## Arquitectura Hexagonal

### Principios Aplicados

1. **Separación de Responsabilidades**: Cada capa tiene una responsabilidad específica
2. **Inversión de Dependencias**: Las dependencias apuntan hacia el dominio
3. **Puertos y Adaptadores**: Interfaces definen contratos, implementaciones son adaptadores
4. **Independencia de Tecnologías**: El dominio no depende de tecnologías externas

### Capas

#### Domain (Dominio)
- **BaseEntity**: Clase base para todas las entidades con propiedades comunes
- **IBaseRepository**: Interfaz base para operaciones CRUD genéricas
- **Reglas de Negocio**: Lógica central de la aplicación

#### Application (Aplicación)
- **IBaseUseCase**: Interfaz base para casos de uso genéricos
- **Casos de Uso**: Orquestación de operaciones de negocio
- **DTOs**: Objetos para transferencia de datos

#### Infrastructure (Infraestructura)
- **IDatabaseConnection**: Interfaz para conexión a base de datos
- **SqlServerConnection**: Implementación de conexión SQL Server
- **Persistencia**: Dapper y Stored Procedures para optimización

#### API (Presentación)
- **BaseController**: Controlador base con funcionalidades comunes
- **Configuración**: Inyección de dependencias y configuración de la aplicación

## Tecnologías Utilizadas

- **.NET 8.0**: Framework de desarrollo
- **Dapper**: Micro-ORM para mapeo de objetos
- **Stored Procedures**: Para operaciones de base de datos optimizadas
- **SQL Server**: Base de datos
- **ASP.NET Core**: Framework web
- **Swagger**: Documentación de API

## Configuración de Base de Datos

1. **Ejecutar** `Database/Scripts/CreateDatabase.sql` para crear la base de datos
2. **Ejecutar** `Database/Scripts/StoredProcedures.sql` para crear los Stored Procedures
3. **Verificar** la cadena de conexión en `appsettings.json`
4. **Ejecutar** la aplicación

## Ventajas del Rendimiento

- **Stored Procedures**: Ejecución optimizada y planes de consulta reutilizables
- **Dapper**: Micro-ORM ligero y rápido
- **Sin Entity Framework**: Eliminación de overhead de tracking y cambio de estado
- **Conexiones directas**: Control total sobre las consultas de base de datos

## Ventajas de la Arquitectura Hexagonal

1. **Testabilidad**: Fácil mockeo de dependencias
2. **Mantenibilidad**: Cambios en una capa no afectan otras
3. **Flexibilidad**: Fácil cambio de tecnologías
4. **Escalabilidad**: Estructura clara para crecimiento
5. **Independencia**: El dominio no depende de frameworks externos 