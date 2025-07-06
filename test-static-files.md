# Prueba de Archivos Estáticos

## Problema
- URL: `https://localhost:7000/img/usuario/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png`
- Archivo existe en: `src\API\img\usuario\841c32f5-96ef-45cc-a0e0-b88b933f95bb.png`
- Pero no se puede acceder desde el navegador

## Solución Implementada

### 1. Configuración de Archivos Estáticos
```csharp
// Configurar archivos estáticos para la carpeta img
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "img")),
    RequestPath = "/img"
});
```

### 2. Controlador de Prueba
- Endpoint: `GET /api/Test/files` - Lista todos los archivos
- Endpoint: `GET /api/Test/image/{fileName}` - Verifica archivo específico

## Pasos para Probar

### 1. Reiniciar el Backend
```bash
cd back/src/API
dotnet run
```

### 2. Probar Endpoints de Diagnóstico

#### A. Listar archivos
```bash
GET https://localhost:7000/api/Test/files
```

**Respuesta esperada:**
```json
{
  "imgPath": "C:\\...\\src\\API\\img",
  "exists": true,
  "files": [
    {
      "fileName": "841c32f5-96ef-45cc-a0e0-b88b933f95bb.png",
      "fullPath": "C:\\...\\src\\API\\img\\usuario\\841c32f5-96ef-45cc-a0e0-b88b933f95bb.png",
      "size": 577000,
      "url": "/img/usuario/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png"
    }
  ]
}
```

#### B. Verificar archivo específico
```bash
GET https://localhost:7000/api/Test/image/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png
```

### 3. Probar Acceso Directo a la Imagen
```bash
GET https://localhost:7000/img/usuario/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png
```

**Debería devolver la imagen directamente**

### 4. Probar desde el Frontend
1. Ir a http://localhost:4200
2. Crear un usuario con foto
3. Editar el usuario
4. Verificar que la imagen se cargue

## Posibles Problemas y Soluciones

### Problema: Error 404 en archivos estáticos
**Solución**: Verificar que la configuración esté correcta en `Program.cs`

### Problema: Error de CORS
**Solución**: Verificar configuración de CORS para archivos estáticos

### Problema: Archivo no encontrado
**Solución**: Verificar que el archivo exista en la ruta correcta

## Debugging

### 1. Verificar Configuración
```bash
# En Swagger
GET /api/Test/files
```

### 2. Verificar Archivo
```bash
# En Swagger
GET /api/Test/image/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png
```

### 3. Verificar Acceso Directo
```bash
# En navegador
https://localhost:7000/img/usuario/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png
```

### 4. Verificar Logs del Backend
- Revisar consola donde se ejecuta `dotnet run`
- Buscar errores relacionados con archivos estáticos 