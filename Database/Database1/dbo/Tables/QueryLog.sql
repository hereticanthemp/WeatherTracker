CREATE TABLE [dbo].[QueryLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Result] INT NOT NULL, 
    [Timestamp] DATETIME NOT NULL
)
