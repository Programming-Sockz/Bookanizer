--Die Eckigen klammern [] kann man weglassen.
CREATE TABLE [Books]
(
-- UNIQUEIDENTIFIER Ist eine GUID. DEFAULT newId() erstellt eine neue Guid automatisch.
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId() NOT NULL, 
    [Title] NVARCHAR(255) NOT NULL, 
    [ReleaseDate] DATETIME2 NOT NULL, 
    [AuthorId] UNIQUEIDENTIFIER NULL, 
    --[SeriesId] UNIQUEIDENTIFIER NULL, 
    [PageCount] INT NOT NULL, 
    [Description] nvarchar(MAX) NULL,
    [ImageLink] nvarchar(MAX) NULL,
    CONSTRAINT [FK_AuthorId_Author] FOREIGN KEY ([AuthorId]) REFERENCES [Author]([Id]) ON DELETE SET NULL, 
    --CONSTRAINT [FK_SeriesId_Series] FOREIGN KEY ([SeriesId]) REFERENCES [Series]([Id]) ON DELETE SET NULL,
);