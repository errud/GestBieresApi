﻿CREATE PROCEDURE [dbo].[DeleteBiere]
	@id INT
AS
BEGIN
	DELETE FROM Bieres WHERE Id = @id
END