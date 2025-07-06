-- Crear base de datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HexagonalArchitectureDb')
BEGIN
    CREATE DATABASE HexagonalArchitectureDb;
END
GO

USE HexagonalArchitectureDb;
GO

-- Crear tabla Users
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(150) NOT NULL UNIQUE,
        Age INT NOT NULL,
        Phone NVARCHAR(20) NOT NULL,
        City NVARCHAR(100) NOT NULL,
        PhotoUrl NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL,
        UpdatedAt DATETIME2 NULL
    );
END
GO 