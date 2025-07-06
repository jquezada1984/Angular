-- Script para agregar el campo PhotoBase64 a la tabla Users existente
-- Ejecutar este script si ya tienes la tabla Users creada

USE HexagonalArchitectureDb;
GO

-- Verificar si el campo PhotoBase64 ya existe
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PhotoBase64')
BEGIN
    -- Agregar el campo PhotoBase64
    ALTER TABLE Users ADD PhotoBase64 NVARCHAR(MAX) NULL;
    PRINT 'Campo PhotoBase64 agregado exitosamente a la tabla Users.';
END
ELSE
BEGIN
    PRINT 'El campo PhotoBase64 ya existe en la tabla Users.';
END
GO

-- Verificar la estructura actualizada de la tabla
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Users'
ORDER BY ORDINAL_POSITION;
GO 