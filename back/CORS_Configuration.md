# Configuración de CORS

Este documento explica la configuración de CORS (Cross-Origin Resource Sharing) en el WebService para permitir peticiones desde aplicaciones frontend como Angular.

## Problema Resuelto

El error original era:
```
Access to XMLHttpRequest at 'https://localhost:7000/api/Users' from origin 'http://localhost:4200' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.
```

## Configuración Implementada

### 1. Configuración en Program.cs

```csharp
// CORS configuration
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:4200" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// En el pipeline HTTP
app.UseCors("AllowAngularApp");
```

### 2. Configuración en appsettings.json

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200",
      "https://localhost:4200"
    ]
  }
}
```

### 3. Configuración en appsettings.Development.json

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200",
      "https://localhost:4200",
      "http://localhost:3000",
      "https://localhost:3000",
      "http://localhost:8080",
      "https://localhost:8080"
    ]
  }
}
```

## Orígenes Permitidos

### Desarrollo
- `http://localhost:4200` - Angular CLI por defecto
- `https://localhost:4200` - Angular CLI con HTTPS
- `http://localhost:3000` - React por defecto
- `https://localhost:3000` - React con HTTPS
- `http://localhost:8080` - Otros frameworks
- `https://localhost:8080` - Otros frameworks con HTTPS

### Producción
- Configurar según los dominios de producción

## Headers Permitidos

- ✅ **AllowAnyHeader()** - Todos los headers
- ✅ **AllowAnyMethod()** - Todos los métodos HTTP (GET, POST, PUT, DELETE, etc.)
- ✅ **AllowCredentials()** - Permite cookies y headers de autenticación

## Endpoint de Prueba

Se ha creado un controlador de prueba para verificar CORS:

```
GET  https://localhost:7000/api/test
POST https://localhost:7000/api/test
```

### Ejemplo de respuesta:
```json
{
  "message": "CORS está funcionando correctamente",
  "timestamp": "2024-01-15T10:30:00.000Z",
  "origin": "http://localhost:4200"
}
```

## Verificación desde Angular

### 1. Servicio Angular
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  private apiUrl = 'https://localhost:7000/api';

  constructor(private http: HttpClient) { }

  testCors() {
    return this.http.get(`${this.apiUrl}/test`);
  }

  getUsers() {
    return this.http.get(`${this.apiUrl}/users`);
  }
}
```

### 2. Componente Angular
```typescript
import { Component, OnInit } from '@angular/core';
import { TestService } from './test.service';

@Component({
  selector: 'app-test',
  template: '<div>{{ message }}</div>'
})
export class TestComponent implements OnInit {
  message = '';

  constructor(private testService: TestService) { }

  ngOnInit() {
    this.testService.testCors().subscribe(
      (response: any) => {
        this.message = response.message;
        console.log('CORS funcionando:', response);
      },
      (error) => {
        console.error('Error CORS:', error);
      }
    );
  }
}
```

## Solución de Problemas

### 1. Verificar que CORS esté habilitado
- Asegúrate de que `app.UseCors("AllowAngularApp")` esté en el pipeline HTTP
- Debe estar antes de `app.UseAuthorization()`

### 2. Verificar orígenes permitidos
- Confirma que el origen de tu aplicación esté en la lista de `AllowedOrigins`
- Para desarrollo, usa `http://localhost:4200` o `https://localhost:4200`

### 3. Verificar certificados HTTPS
- Si usas HTTPS, asegúrate de que el certificado sea válido
- Para desarrollo, puedes usar `TrustServerCertificate=true` en la cadena de conexión

### 4. Headers adicionales
Si necesitas headers específicos, puedes configurarlos:

```csharp
policy.WithOrigins(allowedOrigins)
      .WithHeaders("Authorization", "Content-Type")
      .WithMethods("GET", "POST", "PUT", "DELETE")
      .AllowCredentials();
```

## Seguridad

### Desarrollo
- Se permiten múltiples orígenes para facilitar el desarrollo
- Se incluyen tanto HTTP como HTTPS

### Producción
- Limitar orígenes a dominios específicos
- Usar solo HTTPS
- Considerar políticas más restrictivas según necesidades de seguridad

## Comandos de Verificación

### 1. Probar desde navegador
```bash
# Abrir DevTools -> Console
fetch('https://localhost:7000/api/test')
  .then(response => response.json())
  .then(data => console.log(data));
```

### 2. Probar desde curl
```bash
curl -H "Origin: http://localhost:4200" \
     -H "Access-Control-Request-Method: GET" \
     -H "Access-Control-Request-Headers: Content-Type" \
     -X OPTIONS \
     https://localhost:7000/api/test
```

### 3. Verificar headers de respuesta
```bash
curl -I -H "Origin: http://localhost:4200" \
     https://localhost:7000/api/test
```

Deberías ver:
```
Access-Control-Allow-Origin: http://localhost:4200
Access-Control-Allow-Credentials: true
Access-Control-Allow-Methods: GET, POST, PUT, DELETE
Access-Control-Allow-Headers: *
``` 