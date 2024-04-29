--ich werde das password jetzt einfach mal im klartext speichern weil kein bock auf security lol
CREATE TABLE [User]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId(), 
    [UserName] NVARCHAR(30) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [LastLogin] DATETIME2 NOT NULL, 
    [Active] BIT NOT NULL,
);