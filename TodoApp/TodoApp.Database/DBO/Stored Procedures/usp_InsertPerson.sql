﻿CREATE PROCEDURE [dbo].[usp_InsertPerson]
(
	@Name NVARCHAR(100)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			INSERT INTO dbo.Person
			(
				[Name]
			)
			VALUES
			(
				@Name
			);

			SELECT SCOPE_IDENTITY() AS Id;

		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
		BEGIN
			ROLLBACK TRAN;
		END
	END CATCH;
END;