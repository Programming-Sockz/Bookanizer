CREATE TABLE [Test]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId(),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL, 
    [YIppe] INT NOT NULL
)
