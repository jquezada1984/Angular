USE HexagonalArchitectureDb;
GO

-- Stored Procedure: Obtener usuario por ID
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUserById')
    DROP PROCEDURE sp_GetUserById;
GO

CREATE PROCEDURE sp_GetUserById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Age, Phone, City, PhotoUrl, CreatedAt, UpdatedAt
    FROM Users
    WHERE Id = @Id;
END
GO

-- Stored Procedure: Obtener todos los usuarios
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllUsers')
    DROP PROCEDURE sp_GetAllUsers;
GO

CREATE PROCEDURE sp_GetAllUsers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Age, Phone, City, PhotoUrl, CreatedAt, UpdatedAt
    FROM Users
    ORDER BY CreatedAt DESC;
END
GO

-- Stored Procedure: Obtener usuario por email
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUserByEmail')
    DROP PROCEDURE sp_GetUserByEmail;
GO

CREATE PROCEDURE sp_GetUserByEmail
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Age, Phone, City, PhotoUrl, CreatedAt, UpdatedAt
    FROM Users
    WHERE Email = @Email;
END
GO

-- Stored Procedure: Obtener usuarios por ciudad
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUsersByCity')
    DROP PROCEDURE sp_GetUsersByCity;
GO

CREATE PROCEDURE sp_GetUsersByCity
    @City NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Age, Phone, City, PhotoUrl, CreatedAt, UpdatedAt
    FROM Users
    WHERE City LIKE '%' + @City + '%'
    ORDER BY Name;
END
GO

-- Stored Procedure: Crear usuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateUser')
    DROP PROCEDURE sp_CreateUser;
GO

CREATE PROCEDURE sp_CreateUser
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Email NVARCHAR(150),
    @Age INT,
    @Phone NVARCHAR(20),
    @City NVARCHAR(100),
    @PhotoUrl NVARCHAR(500) = NULL,
    @CreatedAt DATETIME2,
    @UpdatedAt DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Users (Id, Name, Email, Age, Phone, City, PhotoUrl, CreatedAt, UpdatedAt)
    VALUES (@Id, @Name, @Email, @Age, @Phone, @City, @PhotoUrl, @CreatedAt, @UpdatedAt);
END
GO

-- Stored Procedure: Actualizar usuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateUser')
    DROP PROCEDURE sp_UpdateUser;
GO

CREATE PROCEDURE sp_UpdateUser
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Email NVARCHAR(150),
    @Age INT,
    @Phone NVARCHAR(20),
    @City NVARCHAR(100),
    @PhotoUrl NVARCHAR(500) = NULL,
    @CreatedAt DATETIME2,
    @UpdatedAt DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Users
    SET Name = @Name,
        Email = @Email,
        Age = @Age,
        Phone = @Phone,
        City = @City,
        PhotoUrl = @PhotoUrl,
        UpdatedAt = @UpdatedAt
    WHERE Id = @Id;
END
GO

-- Stored Procedure: Eliminar usuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteUser')
    DROP PROCEDURE sp_DeleteUser;
GO

CREATE PROCEDURE sp_DeleteUser
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Users WHERE Id = @Id;
END
GO

-- Stored Procedure: Verificar si existe usuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UserExists')
    DROP PROCEDURE sp_UserExists;
GO

CREATE PROCEDURE sp_UserExists
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM Users WHERE Id = @Id;
END
GO

-- Stored Procedure: Verificar si existe email
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_EmailExists')
    DROP PROCEDURE sp_EmailExists;
GO

CREATE PROCEDURE sp_EmailExists
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM Users WHERE Email = @Email;
END
GO

-- Stored Procedure: Verificar si existe email para otro usuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_EmailExistsForOtherUser')
    DROP PROCEDURE sp_EmailExistsForOtherUser;
GO

CREATE PROCEDURE sp_EmailExistsForOtherUser
    @Email NVARCHAR(150),
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM Users WHERE Email = @Email AND Id != @UserId;
END
GO 