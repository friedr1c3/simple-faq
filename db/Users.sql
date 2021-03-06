﻿CREATE TABLE [dbo].[Users]
(
	[ID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(150) NOT NULL,
	[Salt] VARCHAR(10) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
	[IPAddress] VARCHAR(39) NOT NULL,
	[IsAdmin] BIT NOT NULL DEFAULT 0,
	[Registered] DATETIME NOT NULL DEFAULT GETDATE(),
	[Questions]	INT NOT NULL DEFAULT 0
)