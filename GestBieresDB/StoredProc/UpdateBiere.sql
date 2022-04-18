CREATE PROCEDURE [dbo].[UpdateBiere]
	@nom VARCHAR(80),
	@degre DECIMAL(18,2),
	@origine VARCHAR(50),
	@id INT
AS
BEGIN
	UPDATE Bieres SET [Nom] = @nom, Degre = @degre, Origine = @origine
	WHERE Id = @id
END