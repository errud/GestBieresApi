CREATE PROCEDURE [dbo].[AddBiere]
	@nom VARCHAR(80),
	@degre DECIMAL(4,2),
	@origine VARCHAR(50)
AS
	BEGIN
		INSERT INTO Bieres (Nom, Degre, Origine) VALUES (@nom, @degre, @origine)
	END