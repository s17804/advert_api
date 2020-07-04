IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Buildings] (
    [IdBuilding] int NOT NULL IDENTITY,
    [Street] nvarchar(100) NOT NULL,
    [StreetNumber] int NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Height] float NOT NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY ([IdBuilding])
);

GO

CREATE TABLE [Clients] (
    [IdClient] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Phone] nvarchar(100) NOT NULL,
    [Login] nvarchar(100) NULL,
    [Password] nvarchar(255) NOT NULL,
    [RefreshToken] nvarchar(32) NULL,
    [Salt] varbinary(32) NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([IdClient])
);

GO

CREATE TABLE [Campaigns] (
    [IdCampaign] int NOT NULL IDENTITY,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [PricePerSquareMeter] float NOT NULL,
    [IdClient] int NULL,
    [FromIdBuilding] int NULL,
    [ToIdBuilding] int NULL,
    CONSTRAINT [PK_Campaigns] PRIMARY KEY ([IdCampaign]),
    CONSTRAINT [FK_Campaigns_Buildings_FromIdBuilding] FOREIGN KEY ([FromIdBuilding]) REFERENCES [Buildings] ([IdBuilding]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Campaigns_Buildings_ToIdBuilding] FOREIGN KEY ([ToIdBuilding]) REFERENCES [Buildings] ([IdBuilding]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Campaigns_Clients_IdClient] FOREIGN KEY ([IdClient]) REFERENCES [Clients] ([IdClient]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Banners] (
    [IdAdvertisement] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Price] float NOT NULL,
    [Area] float NOT NULL,
    [IdCampaign] int NULL,
    CONSTRAINT [PK_Banners] PRIMARY KEY ([IdAdvertisement]),
    CONSTRAINT [FK_Banners_Campaigns_IdCampaign] FOREIGN KEY ([IdCampaign]) REFERENCES [Campaigns] ([IdCampaign]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Banners_IdCampaign] ON [Banners] ([IdCampaign]);

GO

CREATE INDEX [IX_Campaigns_FromIdBuilding] ON [Campaigns] ([FromIdBuilding]);

GO

CREATE INDEX [IX_Campaigns_IdClient] ON [Campaigns] ([IdClient]);

GO

CREATE INDEX [IX_Campaigns_ToIdBuilding] ON [Campaigns] ([ToIdBuilding]);

GO

CREATE UNIQUE INDEX [IX_Clients_Login] ON [Clients] ([Login]) WHERE [Login] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200704140036_InitialCreate', N'5.0.0-preview.5.20278.2');

GO

