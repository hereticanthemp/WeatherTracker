CREATE TABLE [dbo].[WeatherData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QueryLogId] INT NOT NULL, 
    [LocationName] NVARCHAR(50) NOT NULL, 
    [Weather] NVARCHAR(50) NULL, 
    [MinTemperature] INT NULL, 
    [MaxTemperature] INT NULL, 
    [ChanceOfRain] INT NULL, 
    [Comfort] NVARCHAR(50) NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [Timestamp] DATETIME NOT NULL
)
