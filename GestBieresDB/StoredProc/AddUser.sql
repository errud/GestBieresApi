CREATE PROCEDURE [dbo].[AddUser]
	@email VARCHAR(50),
	@nickname VARCHAR(50),
	@passwd VARCHAR(50),
	@isadmin bit
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = CONCAT(NEWID(), NEWID(), NEWID())

	DECLARE @secretKey VARCHAR(250)
	SET @secretKey = dbo.GetSecret()

	DECLARE @pwd_hash VARBINARY(64)
	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @passwd, @secretKey, @salt))

	INSERT INTO [AppUser] (Email, Nickname, [Password], Salt, isAdmin) VALUES (@email, @nickname, @pwd_hash, @salt, @isadmin)
END
