using System;
using System.Collections.Generic;
using System.Data;
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
            var id = 0;

            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("AddProduct", conn) { CommandType = System.Data.CommandType.StoredProcedure };

                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = product.Name;
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@isDiscontinued", product.IsDiscontinued);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCore(id);
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
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("GetProduct", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", id);

                var ds = new DataSet();

                var da = new SqlDataAdapter() 
                {
                    SelectCommand = cmd
                };

                da.Fill(ds);

                var table = ds.Tables.OfType<DataTable>().FirstOrDefault();
                if (table != null)
                {
                    var row = table.AsEnumerable().FirstOrDefault();
                    if (row != null)
                    {
                        return new Product() {
                            Id = Convert.ToInt32(row["Id"]),
                            Name = row.Field<string>("Name"),    //preferred approach in generic world
                            Description = row.Field<string>("Description"),
                            Price = row.Field<decimal>("price"),
                            IsDiscontinued = row.Field<bool>("isDiscontinued")
                        };
                    };
                }

                return null;
            };

            
        }

        protected override void RemoveCore( int id )
        {
            using (var conn = OpenDatabase())
            {
                //Alternative approach to creating command
                var cmd = conn.CreateCommand();
                cmd.CommandText = "RemoveProduct";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = new SqlParameter("@id", id);
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
            }
            
        }

        protected override Product UpdateCore( Product existing, Product product )
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("UpdateProduct", conn) { CommandType = System.Data.CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", existing.Id);
                cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = product.Name;
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@isDiscontinued", product.IsDiscontinued);

                cmd.ExecuteNonQuery();
            }

            return GetCore(existing.Id);
            
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
