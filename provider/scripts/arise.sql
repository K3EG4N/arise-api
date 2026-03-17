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

BEGIN TRANSACTION;
ALTER TABLE [usr].[users] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [usr].[users] ADD [DeletedAt] datetime2 NULL;

ALTER TABLE [usr].[users] ADD [UpdatedAt] datetime2 NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260131140715_arise-v2', N'10.0.2');

COMMIT;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'emp') IS NULL EXEC(N'CREATE SCHEMA [emp];');

CREATE TABLE [emp].[employees] (
    [EmployeeId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [MiddleName] nvarchar(max) NULL,
    [PaternalLastName] nvarchar(max) NOT NULL,
    [MaternalLastName] nvarchar(max) NULL,
    [HireDate] datetime2 NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [Photo] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_employees] PRIMARY KEY ([EmployeeId]),
    CONSTRAINT [FK_employees_users_UserId] FOREIGN KEY ([UserId]) REFERENCES [usr].[users] ([UserId]) ON DELETE CASCADE
);

CREATE INDEX [IX_employees_UserId] ON [emp].[employees] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260205034833_arise-v3', N'10.0.2');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [emp].[employees] DROP CONSTRAINT [FK_employees_users_UserId];

DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[emp].[employees]') AND [c].[name] = N'UserId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [emp].[employees] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [emp].[employees] ALTER COLUMN [UserId] uniqueidentifier NULL;

ALTER TABLE [emp].[employees] ADD [Code] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [emp].[employees] ADD [Dni] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [emp].[employees] ADD [Gender] int NOT NULL DEFAULT 0;

ALTER TABLE [emp].[employees] ADD [Phone] nvarchar(max) NULL;

ALTER TABLE [emp].[employees] ADD [StatusId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE TABLE [emp].[employee_statuses] (
    [EmployeeStatusId] uniqueidentifier NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Color] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_employee_statuses] PRIMARY KEY ([EmployeeStatusId])
);

CREATE INDEX [IX_employees_StatusId] ON [emp].[employees] ([StatusId]);

ALTER TABLE [emp].[employees] ADD CONSTRAINT [FK_employees_employee_statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [emp].[employee_statuses] ([EmployeeStatusId]) ON DELETE CASCADE;

ALTER TABLE [emp].[employees] ADD CONSTRAINT [FK_employees_users_UserId] FOREIGN KEY ([UserId]) REFERENCES [usr].[users] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260317032115_arise-v4', N'10.0.2');

COMMIT;
GO

