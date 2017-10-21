using System.Collections.Generic;

namespace MovieLib {
    /// <summary>Provides a database of <see cref="Movie"/> items.</summary>
    public interface IMovieDatabase {
        /// <summary>Adds a Movie.</summary>
        /// <param name="movie">The Movie to add.</param>
        /// <returns>The added Movie.</returns>
        Movie Add(Movie movie);

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        Movie Get(int id);

        /// <summary>Gets all movies.</summary>
        /// <returns>The movies.</returns>
        IEnumerable<Movie> GetAll();

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        void Remove(int id);

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        Movie Update(Movie movie);
    }
}
