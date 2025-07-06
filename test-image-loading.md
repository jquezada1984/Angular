# Prueba de Carga de Imágenes

## Pasos para Probar

### 1. Ejecutar los Proyectos
```bash
start-projects.bat
```

### 2. Crear un Usuario con Foto
1. Ir a http://localhost:4200
2. Hacer clic en "Agregar Usuario"
3. Llenar los datos del formulario
4. Hacer clic en "Tomar foto"
5. Capturar una foto
6. Guardar el usuario

### 3. Verificar en la Lista
- La foto debe aparecer en la lista de usuarios
- Si no aparece, verificar la consola del navegador para errores

### 4. Editar el Usuario
1. Hacer clic en "Editar" en el usuario creado
2. **VERIFICAR**: La foto debe aparecer en el formulario
3. Revisar la consola del navegador para los logs

### 5. Logs Esperados en la Consola
```
Editando usuario: {id: 1, name: "...", photo: "/img/usuario/..."}
Obteniendo usuario con foto, ID: 1
Usuario obtenido: {id: 1, name: "...", photo: "/img/usuario/..."}
Convirtiendo foto del usuario: /img/usuario/...
Convirtiendo URL a Data URL: https://localhost:7000/img/usuario/...
URL convertida exitosamente a Data URL
Usuario con foto convertida: {id: 1, name: "...", photo: "data:image/png;base64,..."}
Usuario con foto obtenido para editar: {id: 1, name: "...", photo: "data:image/png;base64,..."}
Usuario recibido en formulario: {id: 1, name: "...", photo: "data:image/png;base64,..."}
Foto asignada al formulario: data:image/png;base64,...
```

### 6. Posibles Problemas

#### Problema: No se muestra la foto en la lista
**Solución**: Verificar que el backend esté sirviendo archivos estáticos correctamente

#### Problema: Error 404 al cargar la imagen
**Solución**: Verificar que la imagen exista en `back/src/API/img/usuario/`

#### Problema: Error CORS al cargar la imagen
**Solución**: Verificar la configuración de CORS en el backend

#### Problema: La foto no aparece al editar
**Solución**: Revisar los logs en la consola para identificar dónde falla el proceso

### 7. Debugging

#### Verificar Backend
1. Abrir https://localhost:7000/swagger
2. Probar el endpoint GET /api/Users/{id}
3. Verificar que devuelva la URL de la foto

#### Verificar Imagen Directamente
1. Copiar la URL de la foto del usuario
2. Pegar en el navegador: `https://localhost:7000/img/usuario/nombre-archivo.png`
3. Verificar que la imagen se cargue

#### Verificar Frontend
1. Abrir DevTools (F12)
2. Ir a la pestaña Network
3. Editar un usuario
4. Verificar que se haga la petición para obtener la imagen 