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

    INSERT INTO [dbo].[Records] ([CreatedAt], [Description], [Total], [Comment])
    VALUES
          ('2012-12-21T15:14:13', 'Some important operation', 123.44, NULL)
        , ('2012-12-21T16:17:18', 'Some important operation x 10 Some important operation Some important operation Some important operation Some important operation Some important operation Some important operation Some important operation Some important operation', 0, 'Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment Comment ')
        , ('2015-01-09T11:05:00', 'operation Some important operation operation Some important operation Some important operation', 123456789.1234, 'Comment Comment Comment Comment Comment Comment ')
END
GO

SELECT [Id], [CreatedAt], [Description], [Total], [Comment]
FROM [dbo].[Records]