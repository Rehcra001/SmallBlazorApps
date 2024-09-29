CREATE PROCEDURE [dbo].[usp_GetTaskPercentageCompleteById]
(
	@Id INT
)AS
BEGIN
	DECLARE @PercentageComplete INT

	SELECT @PercentageComplete = PercentageComplete
	FROM dbo.Task
	WHERE Id = @Id;

	RETURN @PercentageComplete;
END;
