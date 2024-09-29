CREATE PROCEDURE [dbo].[usp_GetPeople] AS
BEGIN
	SELECT Id, [Name]
	FROM dbo.Person
	ORDER BY [Name];
END;
