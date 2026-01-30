IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'usr') IS NULL EXEC(N'CREATE SCHEMA [usr];');

CREATE TABLE [usr].[users] (
    [UserId] uniqueidentifier NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_users] PRIMARY KEY ([UserId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260130041550_arise-v1', N'10.0.2');

COMMIT;
GO

