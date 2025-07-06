# Angular PWA Demo - Gestión de Usuarios y Productos

Una aplicación web progresiva (PWA) desarrollada con **Angular 17** que incluye gestión completa de usuarios y productos, con funcionalidad de cámara, formularios modales, y un sistema de estilos compartido unificado.

## 🚀 Características Principales

### 📱 **PWA (Progressive Web App)**
- ✅ Service Worker configurado para cache offline
- ✅ Manifest para instalación en dispositivos
- ✅ Iconos en múltiples tamaños
- ✅ Estrategia de cache optimizada
- ✅ Funciona offline

### 🏗️ **Arquitectura Modular**
- ✅ **Módulos Standalone** con lazy-loading
- ✅ **UserModule**: Gestión completa de usuarios
- ✅ **ProductModule**: Gestión completa de productos
- ✅ **SharedModule**: Componentes reutilizables
- ✅ **PreloadAllModules**: Estrategia de precarga

### 👥 **Gestión de Usuarios**
- ✅ **Lista de usuarios** con DataGrid responsive
- ✅ **Formulario modal** para crear/editar usuarios
- ✅ **Campos**: nombre, email, edad, teléfono, ciudad
- ✅ **Funcionalidad de cámara** integrada
- ✅ **Captura de fotos** con selector de cámara (frontal/trasera)
- ✅ **Previsualización** de imágenes capturadas
- ✅ **Validaciones** completas de formularios

### 📦 **Gestión de Productos**
- ✅ **Lista de productos** con DataGrid responsive
- ✅ **Formulario modal** para crear/editar productos
- ✅ **Campos**: nombre, descripción, precio, stock, categoría, código
- ✅ **Validaciones** completas de formularios
- ✅ **Formateo de precios** con pipes personalizados

### 🎨 **Sistema de Estilos Unificado**
- ✅ **Archivo de estilos compartido** (`shared-styles.scss`)
- ✅ **Sistema de variables CSS** con colores unificados
- ✅ **Diseño responsive** para móviles y desktop
- ✅ **Modales centrados** con animaciones suaves
- ✅ **Consistencia visual** en toda la aplicación

### 📷 **Funcionalidad de Cámara**
- ✅ **Acceso a cámara** del dispositivo (PC/móvil)
- ✅ **Selector de cámara** (frontal/trasera si hay múltiples)
- ✅ **Captura de fotos** en tiempo real
- ✅ **Previsualización** de imagen capturada
- ✅ **Eliminación** de fotos
- ✅ **Integración** con formulario de usuario

## 📦 Instalación

```bash
# Clonar el repositorio
git clone <url-del-repositorio>
cd angular-pwa-demo

# Instalar dependencias
npm install
```

## 🛠️ Desarrollo

```bash
# Servidor de desarrollo
npm start

# Servidor de desarrollo con apertura automática
ng serve --open

# Servidor de desarrollo con live-reload
ng serve --poll=2000


```

## 🏗️ Build y Deploy

### Build de Desarrollo
```bash
ng build
```



## 📱 PWA

### Configuración del Service Worker
El archivo `ngsw-config.json` incluye:

- **Assets**: Precarga de archivos de la aplicación
- **API Cache**: Cache de respuestas de API con estrategia freshness
  - URL: `https://api.tu-dominio.com/**`
  - Timeout: 5 segundos
  - Máximo: 100 entradas
  - Caducidad: 1 hora



## 🏗️ Estructura del Proyecto

```
angular-pwa-demo/
├── src/
│   ├── app/
│   │   ├── features/
│   │   │   ├── user/
│   │   │   │   └── components/
│   │   │   │       ├── user-form/
│   │   │   │       │   ├── user-form.ts
│   │   │   │       │   ├── user-form.html
│   │   │   │       │   └── user-form.css
│   │   │   │       └── user-list/
│   │   │   │           ├── user-list.ts
│   │   │   │           └── user-list.html
│   │   │   └── product/
│   │   │       └── components/
│   │   │           ├── product-form/
│   │   │           │   ├── product-form.ts
│   │   │           │   ├── product-form.html
│   │   │           │   └── product-form.css
│   │   │           └── product-list/
│   │   │               ├── product-list.ts
│   │   │               └── product-list.html
│   │   ├── shared/
│   │   │   ├── components/
│   │   │   │   ├── alert/
│   │   │   │   ├── button/
│   │   │   │   ├── loading-spinner/
│   │   │   │   └── nav-bar/
│   │   │   ├── directives/
│   │   │   │   ├── click-outside/
│   │   │   │   ├── highlight/
│   │   │   │   └── tooltip/
│   │   │   ├── pipes/
│   │   │   │   ├── currency-format/
│   │   │   │   ├── format-date/
│   │   │   │   └── truncate/
│   │   │   └── styles/
│   │   │       ├── shared-styles.scss
│   │   │       └── list-styles.css
│   │   ├── app.routes.ts
│   │   ├── app.config.ts
│   │   └── app.ts
│   ├── public/
│   │   ├── manifest.webmanifest
│   │   └── icons/
│   │       ├── icon-72x72.png
│   │       ├── icon-96x96.png
│   │       ├── icon-128x128.png
│   │       ├── icon-144x144.png
│   │       ├── icon-152x152.png
│   │       ├── icon-192x192.png
│   │       ├── icon-384x384.png
│   │       └── icon-512x512.png
│   ├── styles.scss
│   └── main.ts
├── ngsw-config.json
├── angular.json
├── package.json
└── README.md
```

## 🎨 Sistema de Estilos

### Variables CSS Principales
```scss
:root {
  // Colores principales
  --primary-color: #667eea;
  --secondary-color: #764ba2;
  --primary-gradient: linear-gradient(135deg, #667eea, #764ba2);
  
  // Estados
  --success-color: #28a745;
  --danger-color: #dc3545;
  --info-color: #17a2b8;
  
  // Espaciado
  --spacing-xs: 0.25rem;
  --spacing-sm: 0.5rem;
  --spacing-md: 1rem;
  --spacing-lg: 1.5rem;
  --spacing-xl: 2rem;
}
```

### Clases Principales
- `.list-container` - Contenedor de listas
- `.datagrid-container` - Contenedor de tablas
- `.modal-form-container` - Modal de formularios
- `.form-input`, `.form-label` - Elementos de formulario
- `.btn`, `.btn-primary`, `.btn-secondary` - Botones

## 📷 Funcionalidad de Cámara

### Características
- **MediaDevices API** para acceso a cámara
- **Selector de dispositivo** (frontal/trasera)
- **Captura en tiempo real** con Canvas API
- **Previsualización** de imagen capturada
- **Almacenamiento** como Data URL

### Uso
1. Hacer clic en "Tomar foto" en el formulario de usuario
2. Seleccionar cámara si hay múltiples dispositivos
3. Capturar foto con el botón "Capturar foto"
4. La imagen se muestra en el formulario
5. Opción de eliminar y volver a tomar

## 🔧 Configuración

### Angular.json
```json
{
  "projects": {
    "angular-pwa-demo": {
      "architect": {
        "build": {
          "options": {
            "serviceWorker": true,
            "ngswConfigPath": "ngsw-config.json"
          }
        }
      }
    }
  }
}
```

### TypeScript (tsconfig.json)
```json
{
  "compilerOptions": {
    "target": "ES2022",
    "useDefineForClassFields": false,
    "lib": ["ES2022", "dom"],
    "module": "ES2022",
    "skipLibCheck": true,
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "resolveJsonModule": true,
    "isolatedModules": true,
    "noEmit": true,
    "jsx": "preserve",
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noFallthroughCasesInSwitch": true
  }
}
```

## 🚀 Deploy

### Netlify
```bash
# Build
ng build --configuration production

# Deploy
netlify deploy --prod --dir=dist/angular-pwa-demo
```

### Vercel
```bash
# Instalar Vercel CLI
npm i -g vercel

# Deploy
vercel --prod
```



## 📋 Roadmap

### Próximas Funcionalidades
- [ ] **Autenticación** con JWT
- [ ] **Backend API** C#
- [ ] **Base de datos** SQL Server
- [ ] **Filtros y búsqueda** en listas
- [ ] **Exportación** a PDF/Excel
- [ ] **Notificaciones push** para PWA
- [ ] **Tema oscuro/claro**
- [ ] **Internacionalización** (i18n)

## 🤝 Contribuir

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE.md](LICENSE.md) para detalles.

## 👨‍💻 Autor

**John Quezada** - [@tu-usuario](https://github.com/jquezada1984)

