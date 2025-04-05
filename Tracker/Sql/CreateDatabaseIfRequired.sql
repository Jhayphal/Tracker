IF NOT EXISTS (
    SELECT *
    FROM sys.databases db
    WHERE db.[name] = 'TrackerDataStorage'
) CREATE DATABASE [TrackerDataStorage]
GO

USE [TrackerDataStorage]
GO

DROP TABLE IF EXISTS [dbo].[Records]
GO

IF OBJECT_ID(N'[dbo].[Records]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Records] (
          [Id] BIGINT IDENTITY PRIMARY KEY NOT NULL
        , [CreatedAt] DATETIME NOT NULL
        , [Description] NVARCHAR(256) NOT NULL
        , [Total] DECIMAL(18, 4) NOT NULL
        , [Comment] NVARCHAR(2048) NULL
    )

    INSERT INTO [dbo].[Records] ([CreatedAt], [Description], [Total])
    VALUES (GETDATE(), 'Some important operation', 123.44)
END
GO

SELECT [Id], [CreatedAt], [Description], [Total], [Comment]
FROM [dbo].[Records]