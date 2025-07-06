# Pruebas de la API de Usuarios

## Endpoints Disponibles

### 1. Obtener todos los usuarios
```bash
GET https://localhost:7000/api/Users
```

### 2. Obtener usuario por ID
```bash
GET https://localhost:7000/api/Users/{id}
```

### 3. Crear usuario (con foto)
```bash
POST https://localhost:7000/api/Users
Content-Type: multipart/form-data

FormData:
- Name: "Juan Pérez"
- Email: "juan@email.com"
- Age: 25
- Phone: "+34 123 456 789"
- City: "Madrid"
- PhotoFile: [archivo de imagen]
```

### 4. Actualizar usuario (solo datos de texto)
```bash
PUT https://localhost:7000/api/Users/{id}
Content-Type: application/json

{
  "name": "Juan Pérez Actualizado",
  "email": "juan.nuevo@email.com",
  "age": 26,
  "phone": "+34 987 654 321",
  "city": "Barcelona"
}
```

### 5. Actualizar foto de usuario
```bash
PUT https://localhost:7000/api/Users/{id}/photo
Content-Type: multipart/form-data

FormData:
- PhotoFile: [archivo de imagen]
```

### 6. Eliminar usuario
```bash
DELETE https://localhost:7000/api/Users/{id}
```

## Flujo de Prueba Recomendado

1. **Crear usuario con foto**
2. **Verificar que se guarde correctamente**
3. **Editar usuario (cambiar datos de texto)**
4. **Editar usuario (cambiar foto)**
5. **Verificar que las imágenes se muestren correctamente**

## Notas Importantes

- **Crear**: Usa `multipart/form-data` para incluir la foto
- **Actualizar datos**: Usa `application/json` para solo datos de texto
- **Actualizar foto**: Usa `multipart/form-data` para la nueva foto
- **Imágenes**: Se guardan en `/img/usuario/` con nombres únicos 