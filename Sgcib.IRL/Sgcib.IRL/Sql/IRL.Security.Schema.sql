CREATE SCHEMA Secu
GO

CREATE TABLE [Secu].[Role] (
    [Id] uniqueidentifier NOT NULL ,
    [Name] nvarchar(250) NOT NULL ,
    PRIMARY KEY ([Id])
)
GO
 
ALTER TABLE [Secu].[Role] ADD CONSTRAINT [UQ_Role_Name] UNIQUE ([Name])
GO
 
CREATE TABLE [Secu].[User] (
    [Id] uniqueidentifier NOT NULL ,
    [ADName] nvarchar(250) NOT NULL ,
    [DisplayName] nvarchar(250) NULL ,
    PRIMARY KEY ([Id])
)
GO
 
ALTER TABLE [Secu].[User]
    ADD CONSTRAINT [UQ_User_ADName] UNIQUE ([ADName])
GO
 
CREATE TABLE [Secu].[UserRole] (
	[Id] INT NOT NULL IDENTITY(1,1),
    [UserId] uniqueidentifier NOT NULL ,
    [RoleId] uniqueidentifier NOT NULL ,
    PRIMARY KEY ([Id])
)
GO
 
ALTER TABLE [Secu].[UserRole]
ADD CONSTRAINT [FK_UserRole_User_Id] FOREIGN KEY ([UserId])
    REFERENCES [Secu].[User] ([Id])
GO
 
ALTER TABLE [Secu].[UserRole]
    ADD CONSTRAINT [FK_UserRole_Role_Id] FOREIGN KEY ([RoleId])
    REFERENCES [Secu].[Role] ([Id])
GO

CREATE TABLE [Secu].[WebSiteAccessPermission] (
    [Id] INT NOT NULL IDENTITY(1,1),
	[RoleId] uniqueidentifier NOT NULL,
    [Controller] NVARCHAR (128) NOT NULL,
	[Action] NVARCHAR (128) NULL,
    [Enabled] BIT NOT NULL,
    [Comments] NTEXT NULL,
	PRIMARY KEY ([Id])
)
GO

ALTER TABLE [Secu].[WebSiteAccessPermission] 
	ADD CONSTRAINT [UQ_WebSiteAccessPermission] 
	UNIQUE ([RoleId], [Controller], [Action])
GO

ALTER TABLE [Secu].[WebSiteAccessPermission] 
	ADD CONSTRAINT [FK_WebSiteAccessPermission_Role_Id] FOREIGN KEY ([RoleId])
    REFERENCES [Secu].[Role] ([Id])
GO