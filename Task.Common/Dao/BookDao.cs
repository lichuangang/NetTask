using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common.Mapping;

namespace Task.Common.Dao
{
    /* ==============================================================================
     * 描述：BookDao
     * 创建人：李传刚 2017/7/31 14:34:27
     * ==============================================================================
     */
    public class BookDao
    {
        public static List<Book> GetBookByIds(List<string> ids)
        {
            var sql = @"SELECT * FROM Book WHERE BookId IN @ids";
            using (var connection = BaseDao.GetGoodBooksConnection())
            {
                return connection.Query<Book>(sql, new { ids = ids }).ToList();
            }
        }
    }
}
