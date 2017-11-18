using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MovieLib.Data.Sql
{
    public class SqlMovieDatabase : MovieDatabase 
    {
        private readonly string _connectionString;
        public SqlMovieDatabase( string connectionString )
        {
            _connectionString = connectionString;
        }

        protected override Movie AddCore(Movie movie)
        {
            var id = 0;

            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("AddProduct", conn) 
                  { CommandType = CommandType.StoredProcedure };
                
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@owned", movie.Owned);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCore(id);
        }

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

        protected override Movie GetCore(int id)
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("GetMovie", conn)
                  { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", id);

                /////////////////////////////
                //Using a dataset instead of a reader
            //    var ds = new DataSet();
            //    var da = new SqlDataAdapter() {
            //        SelectCommand = cmd,
            //        //DeleteCommand,
            //        //UpdateCommand,
            //        //InsertCommand,

            //    };

            //    da.Fill(ds);

            //    var table = ds.Tables.OfType<DataTable>().FirstOrDefault();
            //    if (table != null)
            //    {
            //        var row = table.AsEnumerable().FirstOrDefault();
            //        if (row != null)
            //        {
            //            return new Product() {
            //                Id = Convert.ToInt32(row["Id"]),
            //                Name = row.Field<string>("Name"),
            //                Description = row.Field<string>("Description"),
            //                Price = row.Field<decimal>("price"),
            //                IsDiscontinued = row.Field<bool>("isdiscontinued")
            //            };
            //        };
            //    };
            //};

            //return null;
            ////////////////////////////

            using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
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

        protected override void RemoveCore(int id)
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("RemoveMovie", conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

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
                cmd.Parameters.AddWithValue("@owned", newItem.Owned);

                cmd.ExecuteNonQuery();
            };

            return GetCore(existing.Id);
        }

        private SqlConnection OpenDatabase()
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}
