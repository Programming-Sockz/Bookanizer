CREATE TABLE BookGenre
(
	BookId UNIQUEIDENTIFIER NOT NULL,
	GenreId UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_BookGenre] PRIMARY KEY ([BookId], [GenreId]),
	CONSTRAINT [FK_BookGenre_Book] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_BookGenre_Genre] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE
);