@echo off
echo Iniciando proyectos Angular y C#...

echo.
echo 1. Iniciando Backend C#...
start "Backend C#" cmd /k "cd back\src\API && dotnet run"

echo.
echo 2. Esperando 5 segundos para que el backend inicie...
timeout /t 5 /nobreak > nul

echo.
echo 3. Iniciando Frontend Angular...
start "Frontend Angular" cmd /k "cd angular-pwa-demo && npm start"

echo.
echo Proyectos iniciados:
echo - Backend: https://localhost:7000
echo - Frontend: http://localhost:4200
echo.
echo Presiona cualquier tecla para cerrar esta ventana...
pause > nul 