/* ITSE1430
 * Kevin Belknap
 * November 17, 2017
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLib;

namespace MovieLib.Data.Memory {
    /// <summary>Provides an implementation of <see cref="IMovieDatabase"/> using a memory collection.</summary>
    public class MemoryMovieDatabase : MovieDatabase {
        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected override Movie AddCore(Movie movie)
        {
            var newMovie = CopyMovie(movie);
            _movies.Add(newMovie);

            if (newMovie.Id <= 0)
                newMovie.Id = _nextId++;
            else if (newMovie.Id >= _nextId)
                _nextId = newMovie.Id + 1;

            return CopyMovie(newMovie);
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        protected override Movie GetCore(int id)
        {
            var movie = FindMovie(id);

            return (movie != null) ? CopyMovie(movie) : throw new Exception("Movie not in memory.");
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The movies.</returns>
        protected override IEnumerable<Movie> GetAllCore()
        {
            return from item in _movies
                   select CopyMovie(item);
            
            //Note - keep following lines for future reference
            //foreach (var movie in _movies)
            //    yield return CopyMovie(movie);
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="movie">The movie to remove.</param>
        protected override void RemoveCore(int id)
        {
            var movie = FindMovie(id);
            if (movie != null)
                _movies.Remove(movie);
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore(Movie existing, Movie movie)
        {
            //Find and remove existing movie
            existing = FindMovie(movie.Id);
            _movies.Remove(existing);

            //Add a copy of the new movie
            var newMovie = CopyMovie(movie);
            _movies.Add(newMovie);

            return CopyMovie(newMovie);
        }
        //Copies one movie to another
        private Movie CopyMovie(Movie movie)
        {
            if (movie == null)
                return null;

            var newMovie = new Movie() 
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Length = movie.Length,
                Owned = movie.Owned
            };
            
            return newMovie;
        }

        //Find a movie by its ID
        private Movie FindMovie(int id)
        {
            return (from movie in _movies
                    where movie.Id == id
                    select movie).FirstOrDefault();
        }

        private List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;

    }
}