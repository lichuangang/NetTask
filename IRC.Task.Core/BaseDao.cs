using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using QCommon.Extentions;

namespace IRC.Task.Core
{
    public static class BaseDao
    {

        public static void Insert(SqlConnection connection, string tableName, DataTable dataTable, IDbTransaction transaction = null)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, transaction as SqlTransaction))
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

        public static List<T> SelectList<T>(SqlConnection connection, List<string> ids, Func<IDbConnection, string, List<T>> func)
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
