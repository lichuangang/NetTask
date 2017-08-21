using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Task.Common.Extensions;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Dao
{
    public static class BaseDao
    {
        public static IDbConnection GetGoodBooksConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["GoodBooksConnection"].ConnectionString);
        }

        public static IDbConnection GetTaskDbConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString);
        }

        public static IDbConnection GetUserLogConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["userLogConnection"].ConnectionString);
        }

        public static IDbConnection GetStatisticalConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["statisticalConnection"].ConnectionString);
        }

        public static IDbConnection GetStatConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["StatConnection"].ConnectionString);
        }


        public static IDbConnection GetHistoryConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnection"].ConnectionString);
        }

        //public static IDbConnection GetMySqlConnection()
        //{
        //    var connectionStr = ConfigurationManager.ConnectionStrings["goodbook_mysql"].ConnectionString;

        //    return new MySqlConnection(connectionStr);
        //}

        //public static IDbConnection GetAdsSqlConnection()
        //{
        //    var connectionStr = ConfigurationManager.ConnectionStrings["AdsGoodBooks"].ConnectionString;

        //    return new MySqlConnection(connectionStr);
        //}

        public static void Insert(IDbConnection connection, string tableName, DataTable dataTable, IDbTransaction transaction = null)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            using (var sqlBulkCopy = new SqlBulkCopy(connection as SqlConnection, SqlBulkCopyOptions.CheckConstraints, transaction as SqlTransaction))
            {
                for (var i = 0; i < dataTable.Columns.Count; i++)
                {
                    var column = dataTable.Columns[i];
                    sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                sqlBulkCopy.BulkCopyTimeout = 120000;
                sqlBulkCopy.DestinationTableName = tableName;
                sqlBulkCopy.WriteToServer(dataTable);
            }
        }

        public static List<T> SelectList<T>(IDbConnection connection, List<string> ids, Func<IDbConnection, string, List<T>> func)
        {
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var randomStr = Guid.NewGuid().ToString().Replace("-", string.Empty);
                var tableName = "#Select_Temp_" + randomStr;
                try
                {
                    connection.Execute("create table " + tableName + "(id varchar(50))");
                    Insert(connection, tableName, ids.Select(o => new { id = o }).ToList().ToDataTable());
                    return func(connection, tableName);
                }
                finally
                {
                    connection.Execute("drop table " + tableName);
                }
            }
        }
    }
}
