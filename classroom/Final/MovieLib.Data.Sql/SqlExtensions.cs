/*
 * ITSE1430
 */
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace MovieLib.Data.Sql
{
    /// <summary>Provides extensions for SQL code.</summary>
    public static class SqlExtensions
    {
        /// <summary>Creates a stored procedure command.</summary>
        /// <param name="source">The source.</param>
        /// <param name="procedureName">The procedure name.</param>
        /// <returns>The command.</returns>
        public static SqlCommand CreateStoredProcedureCommand ( this SqlConnection source, string procedureName )
        {
            var cmd = source.CreateCommand();
            cmd.CommandText = procedureName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return cmd;
        }

        /// <summary>Executes a reader and returns the list of results using the provided conversion function.</summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="source">The command.</param>
        /// <param name="conversionFunction">The conversion function.</param>
        /// <returns>The list of results.</returns>
        public static IEnumerable<T> ExecuteReaderWithResults<T> ( this DbCommand source, Func<DbDataReader, T> conversionFunction )
        {
            var items = new List<T>();

            using (var reader = source.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = conversionFunction(reader);
                    if (item != null)
                        items.Add(item);
                };
            };

            return items;
        }

        /// <summary>Executes a reader and returns the first result using the provided conversion function.</summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="source">The command.</param>
        /// <param name="conversionFunction">The conversion function.</param>
        /// <returns>The result, if any.</returns>
        public static T ExecuteReaderWithSingleResult<T> ( this DbCommand source, Func<DbDataReader, T> conversionFunction )
        {
            var items = new List<T>();

            using (var reader = source.ExecuteReader())
            {
                if (reader.Read())
                    return conversionFunction(reader);
            };

            return default(T);
        }

        /// <summary>Executes a command and returns back the first result as the given type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="source">The command.</param>
        /// <returns>The result.</returns>
        public static T ExecuteScalar<T> ( this DbCommand source )
        {
            var result = source.ExecuteScalar();
            return (result != null) ? (T)Convert.ChangeType(result, typeof(T)) : default(T);
        }
    }
}
