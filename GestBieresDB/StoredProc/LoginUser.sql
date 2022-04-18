CREATE PROCEDURE [dbo].[LoginUser]
	@email VARCHAR(50),
	@passwd VARCHAR(50)
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SELECT @salt = Salt FROM AppUser WHERE Email LIKE @email

	DECLARE @secretKey VARCHAR(250)
	SET @secretKey = dbo.GetSecret()

	DECLARE @pwd_hash VARBINARY(64)
	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @passwd, @secretKey, @salt))

	DECLARE @currentId INT
	SET @currentId = (SELECT Id FROM AppUser WHERE Email = @email AND [Password] = @pwd_hash)

	SELECT * FROM V_AppUser WHERE Id = @currentId
END