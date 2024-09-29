CREATE TABLE [dbo].[Task]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
	PersonId INT NOT NULL,
	Task NVARCHAR(MAX) NOT NULL,
	PercentageComplete INT DEFAULT(0) NOT NULL,
	DateToBeCompleted DATETIME2 NOT NULL,
	DateCompleted DATETIME2 NULL,
	CONSTRAINT FK_Task_Person_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person(Id),
	CONSTRAINT CK_Task_PercentageComplete CHECK (PercentageComplete >= 0 AND PercentageComplete <=100)
)
