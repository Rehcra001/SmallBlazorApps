CREATE PROCEDURE [dbo].[usp_GetTasks] AS
BEGIN
	SELECT Id, PersonId, Task, PercentageComplete, DateToBeCompleted, DateCompleted
	FROM dbo.Task;
END;
