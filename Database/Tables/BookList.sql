﻿CREATE TABLE BookList
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId() NOT NULL,
    [Name] varchar(200) NOT NULL,
    [CreatedOn] DATETIME2(7) NOT NULL,
    [CreatedById] UNIQUEIDENTIFIER NOt NULL,
    [ListType] INT NOT NULL,
    CONSTRAINT [FK_BookList_User] FOREIGN KEY ([CreatedById]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);