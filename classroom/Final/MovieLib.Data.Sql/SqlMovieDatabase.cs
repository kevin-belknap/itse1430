/*
 * ITSE1439
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace MovieLib.Data.Sql
{
    /// <summary>Provides an implementation of <see cref="IMovieDatabase"/> using SQL Server.</summary>
    public class SqlMovieDatabase : MovieDatabase
    {
        #region Construction

        public SqlMovieDatabase ( string connectionStringOrName )
        {
            //Is this a connection string or name ?

            var connString = ConfigurationManager.ConnectionStrings[connectionStringOrName];
            _connectionString = connString?.ConnectionString ?? connectionStringOrName;
        }
        #endregion

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected Movie AddCore ( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("AddMovie");
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@rating", movie.Rating);
                cmd.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);

                conn.Open();

                movie.Id = cmd.ExecuteScalar<int>();
            };

            return movie;
        }

        /// <summary>Finds a movie by its title.</summary>
        /// <param name="title">The title to find.</param>
        /// <returns>The movie, if any.</returns>
        protected override Movie FindByTitleCore ( string title )
        {
            //Not supported directly
            var movies = GetAllCore();

            return movies.FirstOrDefault(m => String.Compare(m.Title, title, true) == 0);
        }

        /// <summary>Gets a specific movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie, if found.</returns>
        protected override Movie GetCore ( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("GetMovie");
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                return cmd.ExecuteReaderWithSingleResult(ReadMovie);
            };
        }

        /// <summary>Gets all the movies.</summary>
        /// <returns>The list of movies.</returns>
        protected override IEnumerable<Movie> GetAllCore ()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("GetAllMovies");

                conn.Open();

                return cmd.ExecuteReaderWithResults(ReadMovie);
            };
        }

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        protected override void RemoveCore ( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("RemoveMovie");
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            };
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore ( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("UpdateMovie");
                cmd.Parameters.AddWithValue("@id", movie.Id);
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@rating", movie.Rating);
                cmd.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);

                conn.Open();
                cmd.ExecuteNonQuery();                
            };

            return movie;
        }

        #region Private Members

        private Movie ReadMovie ( DbDataReader reader )
        {
            return new Movie()
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Length = reader.GetInt32(3),
                IsOwned = reader.GetBoolean(4),
                Rating = (Rating)reader.GetInt32(5),
                ReleaseYear = (int)reader.GetInt16(6)
            };
        }

        private readonly string _connectionString;
        #endregion
    }
}
