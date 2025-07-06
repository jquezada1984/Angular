# Pruebas del Sistema de Subida de Imágenes

## Prerequisitos

1. Base de datos configurada y ejecutándose
2. Scripts de migración ejecutados
3. Aplicación compilada y ejecutándose

## Pasos de Migración

### 1. Ejecutar Script de Migración de Base de Datos

```sql
-- Ejecutar en SQL Server Management Studio
USE HexagonalArchitectureDb;
GO

-- Eliminar columna PhotoBase64 si existe
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PhotoBase64')
BEGIN
    ALTER TABLE Users DROP COLUMN PhotoBase64;
    PRINT 'Columna PhotoBase64 eliminada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La columna PhotoBase64 no existe.';
END
GO

-- Ejecutar Stored Procedures actualizados
-- (Usar el archivo Database/Scripts/StoredProcedures.sql)
```

### 2. Verificar Estructura de Carpetas

```
src/API/img/usuario/
└── .gitkeep
```

## Pruebas con Postman

### 1. Crear Usuario con Imagen

**Endpoint**: `POST http://localhost:5000/api/users`

**Headers**:
- Content-Type: `multipart/form-data`

**Body** (form-data):
- `name`: `Juan Pérez`
- `email`: `juan@example.com`
- `age`: `30`
- `phone`: `123456789`
- `city`: `Madrid`
- `photoFile`: [Seleccionar archivo de imagen]

**Respuesta esperada**:
```json
{
    "id": "guid-generado",
    "name": "Juan Pérez",
    "email": "juan@example.com",
    "age": 30,
    "phone": "123456789",
    "city": "Madrid",
    "photoUrl": "/img/usuario/guid-generado.jpg",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null
}
```

### 2. Actualizar Foto de Usuario

**Endpoint**: `PUT http://localhost:5000/api/users/{id}/photo`

**Headers**:
- Content-Type: `multipart/form-data`

**Body** (form-data):
- `photoFile`: [Seleccionar nueva imagen]

**Respuesta esperada**:
```json
{
    "id": "guid-del-usuario",
    "name": "Juan Pérez",
    "email": "juan@example.com",
    "age": 30,
    "phone": "123456789",
    "city": "Madrid",
    "photoUrl": "/img/usuario/nuevo-guid.jpg",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T01:00:00Z"
}
```

### 3. Obtener Usuario y Verificar Imagen

**Endpoint**: `GET http://localhost:5000/api/users/{id}`

**Respuesta esperada**:
```json
{
    "id": "guid-del-usuario",
    "name": "Juan Pérez",
    "email": "juan@example.com",
    "age": 30,
    "phone": "123456789",
    "city": "Madrid",
    "photoUrl": "/img/usuario/guid-generado.jpg",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T01:00:00Z"
}
```

**Verificar imagen**: `GET http://localhost:5000/img/usuario/guid-generado.jpg`

## Pruebas desde Angular

### 1. Servicio de Usuario

```typescript
// user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5000/api/users';

  constructor(private http: HttpClient) { }

  createUser(userData: any, imageFile: File): Observable<any> {
    const formData = new FormData();
    formData.append('name', userData.name);
    formData.append('email', userData.email);
    formData.append('age', userData.age.toString());
    formData.append('phone', userData.phone);
    formData.append('city', userData.city);
    formData.append('photoFile', imageFile);

    return this.http.post(this.apiUrl, formData);
  }

  updateUserPhoto(userId: string, imageFile: File): Observable<any> {
    const formData = new FormData();
    formData.append('photoFile', imageFile);

    return this.http.put(`${this.apiUrl}/${userId}/photo`, formData);
  }
}
```

### 2. Componente de Subida

```typescript
// user-form.component.ts
import { Component } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-form',
  template: `
    <form (ngSubmit)="onSubmit()" #userForm="ngForm">
      <input [(ngModel)]="user.name" name="name" placeholder="Nombre" required>
      <input [(ngModel)]="user.email" name="email" type="email" placeholder="Email" required>
      <input [(ngModel)]="user.age" name="age" type="number" placeholder="Edad" required>
      <input [(ngModel)]="user.phone" name="phone" placeholder="Teléfono" required>
      <input [(ngModel)]="user.city" name="city" placeholder="Ciudad" required>
      
      <input type="file" (change)="onFileSelected($event)" accept="image/*">
      
      <button type="submit">Crear Usuario</button>
    </form>
  `
})
export class UserFormComponent {
  user = {
    name: '',
    email: '',
    age: 0,
    phone: '',
    city: ''
  };
  selectedFile: File | null = null;

  constructor(private userService: UserService) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (this.selectedFile) {
      this.userService.createUser(this.user, this.selectedFile).subscribe({
        next: (response) => {
          console.log('Usuario creado:', response);
          // Mostrar imagen
          const imageUrl = `http://localhost:5000${response.photoUrl}`;
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
  }
}
```

## Verificaciones

### 1. Verificar Archivos Creados

Después de crear un usuario con imagen, verificar que se creó el archivo:

```
src/API/img/usuario/
├── .gitkeep
└── [guid-generado].jpg  <- Nueva imagen
```

### 2. Verificar Base de Datos

```sql
SELECT Id, Name, Email, PhotoUrl, CreatedAt, UpdatedAt 
FROM Users 
WHERE Email = 'juan@example.com';
```

### 3. Verificar Acceso a Imagen

Abrir en navegador: `http://localhost:5000/img/usuario/[guid-generado].jpg`

## Errores Comunes y Soluciones

### 1. Error 404 en Imagen
- Verificar que `app.UseStaticFiles()` esté configurado en Program.cs
- Verificar que la carpeta `img/usuario/` existe
- Verificar que el archivo se guardó correctamente

### 2. Error de Validación de Archivo
- Verificar que el archivo es una imagen válida
- Verificar que el tamaño no excede 5MB
- Verificar que la extensión está permitida

### 3. Error de CORS
- Verificar configuración CORS en Program.cs
- Verificar que Angular está en el origen permitido

### 4. Error de Base de Datos
- Verificar que se ejecutó el script de migración
- Verificar que los Stored Procedures están actualizados
- Verificar conexión a la base de datos

## Logs de Depuración

Para habilitar logs detallados, agregar en `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Infrastructure.Services.FileService": "Debug"
    }
  }
}
``` 