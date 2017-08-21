using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace TagBooksTask.DAOS
{
    public class BaseDapperDao
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["GoodBooksDb"].ConnectionString);
        }


        public static IEnumerable<T> QueryList<T>(string sql, object condition)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<T>(sql, condition);
            }

        }

        public static int Execute(string sql, object condition)
        {
            using (var connection = GetConnection())
            {
                return connection.Execute(sql, condition);
            }

        }

        public static T ExecuteScalar<T>(string sql, object condition)
        {
            using (var connection = GetConnection())
            {
                return connection.ExecuteScalar<T>(sql, condition);
            }
        }
    }
}
