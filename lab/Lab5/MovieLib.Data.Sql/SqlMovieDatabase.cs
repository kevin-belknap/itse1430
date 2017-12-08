/*
 *ITSE1430
 * Kevin Belknap
 * November 17, 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MovieLib.Data.Sql
{
    /// <summary>Provides an implementation of <see cref="IMovieDatabase"/> using a database.</summary>
    public class SqlMovieDatabase : MovieDatabase 
    {
        private readonly string _connectionString;
        
        public SqlMovieDatabase( string connectionString )
        {
            _connectionString = connectionString;
        }

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected override Movie AddCore(Movie movie)
        {
            var id = 0;

            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("AddMovie", conn) 
                  { CommandType = CommandType.StoredProcedure };
                
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@isowned", movie.Owned);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCore(id);
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The movies.</returns>
        protected override IEnumerable<Movie> GetAllCore()
        {
            var movies = new List<Movie>();
            using (var connection = OpenDatabase())
            {
                var cmd = new SqlCommand("GetAllMovies", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var movie = new Movie() {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Length = reader.GetInt32(3),
                            Owned = reader.GetBoolean(4)
                        };
                        movies.Add(movie);
                    };
                };

                return movies;
            };
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        protected override Movie GetCore(int id)
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("GetMovie", conn)
                  { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", id);
                
            using (var reader = cmd.ExecuteReader())
                {
                    //if (reader.HasRows)
                    while (reader.Read())
                    {
                        return new Movie {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Length = reader.GetInt32(3),
                            Owned = reader.GetBoolean(4)
                        };
                    };
                };
            }
            return null;
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="movie">The movie to remove.</param>
        protected override void RemoveCore(int id)
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("RemoveMovie", conn)
                  { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore(Movie existing, Movie newItem)
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("UpdateMovie", conn) 
                  { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", existing.Id);
                cmd.Parameters.AddWithValue("@title", newItem.Title);
                cmd.Parameters.AddWithValue("@description", newItem.Description);
                cmd.Parameters.AddWithValue("@length", newItem.Length);
                cmd.Parameters.AddWithValue("@isowned", newItem.Owned);

                cmd.ExecuteNonQuery();
            };

            return GetCore(existing.Id);
        }

        //Method to open connection to the database
        private SqlConnection OpenDatabase()
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}
