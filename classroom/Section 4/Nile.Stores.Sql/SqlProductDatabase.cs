using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores.Sql
{
    public class SqlProductDatabase : ProductDatabase {

        public SqlProductDatabase( string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        protected override Product AddCore( Product product )
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Product> GetAllCore()
        {
            var products = new List<Product>();

            using (var connection = OpenDatabase())
            {
                var cmd = new SqlCommand("GetAllProducts", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //reader.GetName(0);
                        //reader.GetFieldType(1);
                        //reader["Id"] - problem is, reader does not know underlying type, so you have to do the conversion

                        var product = new Product() {
                            //if Id were a nullable column, use the following line:
                            //Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Id = reader.GetInt32(0),
                            Name = reader.GetFieldValue<string>(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.GetString(3),
                            IsDiscontinued = reader.GetBoolean(4)
                        };
                        products.Add(product);
                    }
                };

                return products;
            };
        }

        protected override Product GetCore( int id )
        {
            throw new NotImplementedException();
        }

        protected override void RemoveCore( int id )
        {
            throw new NotImplementedException();
        }

        protected override Product UpdateCore( Product existing, Product newItem )
        {
            throw new NotImplementedException();
        }

        private SqlConnection OpenDatabase()
        {
            var connection = new SqlConnection(_connectionString);

                //This is where it will fail if connection string is incorrrect
                connection.Open();

                return connection;
        }
    }
}
