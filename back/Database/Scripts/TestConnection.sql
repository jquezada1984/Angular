-- Script para probar la conexión al servidor JOHN
-- Ejecutar este script para verificar que la conexión funciona correctamente

-- Verificar que podemos conectarnos al servidor
SELECT @@SERVERNAME as ServerName, 
       @@VERSION as Version,
       GETDATE() as CurrentDateTime;

-- Verificar si la base de datos existe
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'HexagonalArchitectureDb')
BEGIN
    PRINT 'La base de datos HexagonalArchitectureDb existe.'
    
    USE HexagonalArchitectureDb;
    
    -- Verificar si la tabla Users existe
    IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
    BEGIN
        PRINT 'La tabla Users existe.'
        SELECT COUNT(*) as TotalUsers FROM Users;
    END
    ELSE
    BEGIN
        PRINT 'La tabla Users NO existe. Ejecutar CreateDatabase.sql primero.'
    END
    
    -- Verificar Stored Procedures
    DECLARE @spCount INT = 0;
    SELECT @spCount = COUNT(*) FROM sys.procedures WHERE name LIKE 'sp_%';
    PRINT 'Stored Procedures encontrados: ' + CAST(@spCount AS VARCHAR(10));
    
    -- Listar Stored Procedures
    SELECT name as StoredProcedureName 
    FROM sys.procedures 
    WHERE name LIKE 'sp_%'
    ORDER BY name;
END
ELSE
BEGIN
    PRINT 'La base de datos HexagonalArchitectureDb NO existe. Ejecutar CreateDatabase.sql primero.'
END 