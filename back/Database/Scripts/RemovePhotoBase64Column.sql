USE HexagonalArchitectureDb;
GO

-- Eliminar la columna PhotoBase64 de la tabla Users
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PhotoBase64')
BEGIN
    ALTER TABLE Users DROP COLUMN PhotoBase64;
    PRINT 'Columna PhotoBase64 eliminada exitosamente de la tabla Users.';
END
ELSE
BEGIN
    PRINT 'La columna PhotoBase64 no existe en la tabla Users.';
END
GO 