using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Dao
{
    /* ==============================================================================
     * 描述：TaskDao
     * 创建人：李传刚 2017/8/1 13:46:46
     * ==============================================================================
     */
    public class TaskDao
    {
        public static void SetTaskStatus(int settingId, int status, string desc = "")
        {
            var sql = "UPDATE TaskSetting SET Status=@Status, RunLastTime=getdate(),[Desc]=@Desc  WHERE Id=@Id";
            using (var connection = BaseDao.GetTaskDbConnection())
            {
                connection.Execute(sql, new { Status = status, Desc = desc, Id = settingId });
            }
        }
    }
}
