@echo off
echo Reiniciando Backend C#...

echo.
echo 1. Deteniendo procesos existentes...
taskkill /f /im dotnet.exe 2>nul

echo.
echo 2. Limpiando build anterior...
cd back\src\API
dotnet clean

echo.
echo 3. Restaurando paquetes...
dotnet restore

echo.
echo 4. Compilando proyecto...
dotnet build

echo.
echo 5. Iniciando backend...
dotnet run

echo.
echo Backend iniciado en https://localhost:7000
echo.
echo Para probar:
echo 1. Abrir https://localhost:7000/swagger
echo 2. Probar GET /api/Test/files
echo 3. Probar GET /api/Test/image/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png
echo 4. Probar https://localhost:7000/img/usuario/841c32f5-96ef-45cc-a0e0-b88b933f95bb.png 