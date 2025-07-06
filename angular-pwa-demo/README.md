# Administracion de usuarios

Una aplicación web progresiva (PWA) desarrollada con Angular que incluye módulos con lazy-loading, formularios con funcionalidad de cámara, y configuración completa de PWA.

## 🚀 Características

### Módulos y Lazy-Loading
- **UserModule**: Gestión de usuarios con formulario y funcionalidad de cámara
- **ProductModule**: Gestión de productos con formulario completo
- **SharedModule**: Componentes, directivas y pipes reutilizables
- **PreloadAllModules**: Estrategia de precarga para mejor rendimiento

### Formulario de Usuario
- Campos: `firstName`, `lastName`
- Selector de cámara (frontal/trasera)
- Vista previa y captura de imagen usando MediaDevices API
- Almacenamiento de foto en el formulario

### Formulario de Producto
- Campos: `name`, `price`, `description`, `category`, `stock`, `image`
- Vista previa de descripción y imagen
- Formateo de precios con pipes personalizados

### PWA (Progressive Web App)
- Service Worker configurado
- Manifest para instalación
- Cache de assets y API
- Estrategia de cache: freshness con timeout de 5s

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
ng serve

# Servidor de desarrollo con apertura automática
ng serve --open

# Servidor de desarrollo con SSL (para PWA)
ng serve --ssl
```

## 🏗️ Build y Deploy

### Build de Desarrollo
```bash
ng build
```

### Build de Producción
```bash
ng build --configuration production
```

### Build de Producción con Optimizaciones
```bash
ng build --configuration production --optimization
```

### Análisis del Bundle
```bash
ng build --configuration production --stats-json
npx webpack-bundle-analyzer dist/angular-pwa-demo/stats.json
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

### Testing PWA
```bash
# Build de producción
ng build --configuration production

# Servir archivos estáticos
npx http-server dist/angular-pwa-demo -p 8080

# Verificar PWA
npx lighthouse http://localhost:8080 --view
```

## 🏗️ Estructura del Proyecto

```
src/
├── app/
│   ├── features/
│   │   ├── user/
│   │   │   ├── components/
│   │   │   │   ├── user-form/
│   │   │   │   └── user-list/
│   │   │   └── user.module.ts
│   │   └── product/
│   │       ├── components/
│   │       │   ├── product-form/
│   │       │   └── product-list/
│   │       └── product.module.ts
│   ├── shared/
│   │   ├── components/
│   │   │   ├── alert/
│   │   │   ├── button/
│   │   │   └── loading-spinner/
│   │   ├── directives/
│   │   │   ├── click-outside/
│   │   │   ├── highlight/
│   │   │   └── tooltip/
│   │   ├── pipes/
│   │   │   ├── currency-format/
│   │   │   ├── format-date/
│   │   │   └── truncate/
│   │   └── shared-module.ts
│   ├── app.routes.ts
│   ├── app.config.ts
│   └── app.ts
├── public/
│   ├── manifest.webmanifest
│   └── icons/
└── ngsw-config.json
```

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

### Firebase Hosting
```bash
# Instalar Firebase CLI
npm install -g firebase-tools

# Login
firebase login

# Inicializar proyecto
firebase init hosting

# Build y deploy
ng build --configuration production
firebase deploy
```

### GitHub Pages
```bash
# Instalar angular-cli-ghpages
npm install -g angular-cli-ghpages

# Build y deploy
ng build --configuration production --base-href "https://tu-usuario.github.io/angular-pwa-demo/"
npx angular-cli-ghpages --dir=dist/angular-pwa-demo
```

## 📊 Performance

### Lighthouse Score Objetivo
- **Performance**: 90+
- **Accessibility**: 90+
- **Best Practices**: 90+
- **SEO**: 90+
- **PWA**: 90+

### Optimizaciones Incluidas
- Lazy-loading de módulos
- PreloadAllModules para mejor UX
- Service Worker para cache
- Compresión de imágenes
- Minificación de CSS/JS
- Tree shaking

## 🧪 Testing

```bash
# Unit tests
ng test

# E2E tests
ng e2e

# Coverage
ng test --code-coverage
```

## 📝 Scripts NPM

```json
{
  "scripts": {
    "ng": "ng",
    "start": "ng serve",
    "build": "ng build",
    "build:prod": "ng build --configuration production",
    "watch": "ng build --watch --configuration development",
    "test": "ng test",
    "test:coverage": "ng test --code-coverage",
    "e2e": "ng e2e",
    "lint": "ng lint",
    "analyze": "ng build --configuration production --stats-json && npx webpack-bundle-analyzer dist/angular-pwa-demo/stats.json"
  }
}
```

## 🔍 Troubleshooting

### Errores Comunes

1. **Service Worker no se registra**
   - Verificar que esté en HTTPS o localhost
   - Revisar la configuración en app.config.ts

2. **Lazy-loading no funciona**
   - Verificar las rutas en app.routes.ts
   - Comprobar que los módulos exporten correctamente

3. **Cámara no funciona**
   - Verificar permisos del navegador
   - Asegurar que esté en HTTPS

4. **Build falla**
   - Limpiar cache: `npm run clean`
   - Reinstalar node_modules: `rm -rf node_modules && npm install`

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📞 Soporte

Para soporte, email: soporte@ejemplo.com o crear un issue en el repositorio.
