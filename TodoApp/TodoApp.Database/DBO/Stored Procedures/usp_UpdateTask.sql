CREATE PROCEDURE [dbo].[usp_UpdateTask]
(
	@Id INT,
	@PersonId INT,
	@Task NVARCHAR(100),
	@PercentageComplete INT,
	@DateToBeCompleted DATETIME2	
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			-- Check if Percent complete = 100 and compare to previous value
			-- if previous was 100 do not update date completed
			-- if previous was 100 and now it is less than 100 remove date completed
			-- if previous was not 100 add today's date to date completed
			DECLARE @PreviousPercentComplete INT;

			SET @PreviousPercentComplete = (SELECT PercentageComplete FROM dbo.Task WHERE Id = @Id);

			--Task already complete
			IF (@PreviousPercentComplete = @PercentageComplete AND @PercentageComplete = 100)
			BEGIN
				UPDATE dbo.Task
				SET PersonId = @PersonId,
					Task = @Task
				WHERE Id = @Id;
			END

			--Task now completed
			ELSE IF (@PreviousPercentComplete != @PercentageComplete AND @PercentageComplete = 100)
			BEGIN
				UPDATE dbo.Task
				SET PersonId = @PersonId,
					Task = @Task,
					PercentageComplete = @PercentageComplete,
					DateToBeCompleted = @DateToBeCompleted,
					DateCompleted = GETDATE()
				WHERE Id = @Id;
			END;

			--Task was complete now incomplete
			ELSE IF (@PreviousPercentComplete != @PercentageComplete AND @PreviousPercentComplete = 100)
			BEGIN
				UPDATE dbo.Task
				SET PersonId = @PersonId,
					Task = @Task,
					PercentageComplete = @PercentageComplete,
					DateToBeCompleted = @DateToBeCompleted,
					DateCompleted = NULL
				WHERE Id = @Id;
			END;

			--Task still not complete but something changed
			ELSE
			BEGIN
				UPDATE dbo.Task
				SET PersonId = @PersonId,
					Task = @Task,
					PercentageComplete = @PercentageComplete,
					DateToBeCompleted = @DateToBeCompleted
				WHERE Id = @Id;
			END;

			SELECT Id, PersonId, Task, PercentageComplete, DateToBeCompleted, DateCompleted
			FROM dbo.Task
			WHERE Id = @Id;
			
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
		BEGIN
			ROLLBACK TRAN;
		END;
	END CATCH;
END;
