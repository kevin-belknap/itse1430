CREATE PROCEDURE [dbo].[GetAllMovies]	
AS BEGIN
	SET NOCOUNT ON;

	SELECT Id, Title, Description, Length, IsOwned, Rating, ReleaseYear
	FROM Movies
END
