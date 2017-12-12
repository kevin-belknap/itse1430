CREATE PROCEDURE [dbo].[AddMovie]
	@title VARCHAR(100),
	@length INT,
	@isOwned BIT,
	@releaseYear SMALLINT,
	@rating INT = 0,
	@description VARCHAR(MAX) = NULL	
AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO Movies (Title, Description, Length, IsOwned, Rating, ReleaseYear) VALUES (@title, @description, @length, @isOwned, @rating, @releaseYear)

	SELECT SCOPE_IDENTITY()
END
