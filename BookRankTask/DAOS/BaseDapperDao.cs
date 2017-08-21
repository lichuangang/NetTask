using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BookRankTask.DAOS
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
        public static IEnumerable<T> QueryList<T>(string sql)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<T>(sql);
            }
        }
        public static T QueryFirst<T>(string sql, object condition)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryFirst<T>(sql, condition);
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
