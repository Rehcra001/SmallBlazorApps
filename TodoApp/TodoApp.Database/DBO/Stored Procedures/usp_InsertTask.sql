CREATE PROCEDURE [dbo].[usp_InsertTask]
(
	@PersonId INT,
	@Task NVARCHAR(100),
	@PercentageComplete INT,
	@DateToBeCompleted DATETIME2
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			INSERT INTO dbo.Task 
			(
				PersonId, 
				Task, 
				PercentageComplete, 
				DateToBeCompleted
			)
			VALUES
			(
				@PersonId,
				@Task,
				@PercentageComplete,
				@DateToBeCompleted
			);

			SELECT SCOPE_IDENTITY() AS Id;
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
		BEGIN
			ROLLBACK TRAN;
		END;
	END CATCH;
END;
