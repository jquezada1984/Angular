# Sistema de Manejo de Imágenes - Archivos

## Descripción General

El sistema ha sido actualizado para manejar imágenes como archivos físicos en lugar de almacenarlas como base64 en la base de datos. Las imágenes se guardan en la carpeta `img/usuario/` y solo se almacena la URL en la base de datos.

## Estructura de Carpetas

```
src/API/img/
└── usuario/
    ├── .gitkeep
    ├── [guid1].jpg
    ├── [guid2].png
    └── [guid3].gif
```

## Características del Sistema

### 1. Validación de Archivos
- **Extensiones permitidas**: .jpg, .jpeg, .png, .gif, .bmp
- **Tamaño máximo**: 5MB por archivo
- **Validación MIME**: Solo archivos de tipo imagen

### 2. Nomenclatura de Archivos
- Se genera un GUID único para cada imagen
- Se mantiene la extensión original del archivo
- Formato: `{GUID}.{extension}`

### 3. Almacenamiento
- **Ubicación**: `src/API/img/usuario/`
- **URL de acceso**: `/img/usuario/{filename}`
- **Base de datos**: Solo se almacena la URL relativa

## Servicios Implementados

### IFileService
```csharp
public interface IFileService
{
    Task<string> SaveImageAsync(IFormFile imageFile, string folderName);
    void DeleteImage(string imagePath);
    bool IsValidImage(IFormFile file);
}
```

### FileService
- Guarda imágenes en la carpeta especificada
- Genera nombres únicos para evitar conflictos
- Elimina imágenes anteriores al actualizar
- Valida tipos y tamaños de archivo

## Endpoints Actualizados

### Crear Usuario con Imagen
```http
POST /api/users
Content-Type: multipart/form-data

{
    "name": "Juan Pérez",
    "email": "juan@example.com",
    "age": 30,
    "phone": "123456789",
    "city": "Madrid",
    "photoFile": [archivo de imagen]
}
```

### Actualizar Foto de Usuario
```http
PUT /api/users/{id}/photo
Content-Type: multipart/form-data

{
    "photoFile": [archivo de imagen]
}
```

## Configuración del Servidor

### Program.cs
```csharp
// Registrar el servicio de archivos
builder.Services.AddScoped<IFileService, FileService>();

// Configurar archivos estáticos
app.UseStaticFiles();
```

### Base de Datos
- Se eliminó la columna `PhotoBase64`
- Solo se mantiene `PhotoUrl` (NVARCHAR(500))

## Scripts de Migración

### Eliminar Columna PhotoBase64
```sql
-- Database/Scripts/RemovePhotoBase64Column.sql
USE HexagonalArchitectureDb;
GO

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PhotoBase64')
BEGIN
    ALTER TABLE Users DROP COLUMN PhotoBase64;
END
GO
```

### Actualizar Stored Procedures
- Se actualizaron todos los SP para eliminar referencias a PhotoBase64
- Solo se mantienen las operaciones con PhotoUrl

## Configuración de CORS

El sistema mantiene la configuración CORS para permitir peticiones desde Angular:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
```

## Manejo de Errores

### Errores Comunes
1. **Archivo no válido**: Se valida tipo MIME y extensión
2. **Archivo muy grande**: Máximo 5MB
3. **Archivo vacío**: Se verifica que el archivo tenga contenido
4. **Error de guardado**: Se maneja excepciones de I/O

### Respuestas de Error
```json
{
    "message": "El archivo no es una imagen válida"
}
```

## Seguridad

### Validaciones Implementadas
- Verificación de tipo MIME
- Validación de extensiones permitidas
- Límite de tamaño de archivo
- Generación de nombres únicos para evitar conflictos

### Consideraciones
- Las imágenes se sirven como archivos estáticos
- No se ejecuta código en las imágenes subidas
- Se eliminan archivos anteriores al actualizar

## Uso desde Angular

### Subir Imagen
```typescript
const formData = new FormData();
formData.append('name', user.name);
formData.append('email', user.email);
formData.append('age', user.age.toString());
formData.append('phone', user.phone);
formData.append('city', user.city);
formData.append('photoFile', imageFile);

this.http.post<UserDto>('/api/users', formData).subscribe(...);
```

### Mostrar Imagen
```html
<img [src]="user.photoUrl" alt="Foto de usuario" />
```

## Mantenimiento

### Limpieza de Archivos
- Los archivos se eliminan automáticamente al actualizar fotos
- Se puede implementar un job de limpieza para archivos huérfanos

### Backup
- Las imágenes se pueden respaldar junto con la aplicación
- Se recomienda backup regular de la carpeta `img/`

## Ventajas del Nuevo Sistema

1. **Mejor rendimiento**: No se cargan imágenes grandes en memoria
2. **Menor uso de base de datos**: Solo URLs, no datos binarios
3. **Mejor escalabilidad**: Las imágenes se pueden servir desde CDN
4. **Facilidad de mantenimiento**: Archivos separados de la lógica
5. **Mejor UX**: Carga más rápida de imágenes

## Migración desde Base64

Si tienes datos existentes en base64:

1. Ejecutar script para eliminar columna PhotoBase64
2. Actualizar Stored Procedures
3. Migrar imágenes existentes a archivos (si es necesario)
4. Actualizar frontend para usar FormData 