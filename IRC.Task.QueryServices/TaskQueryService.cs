using IRC.Task.Core.TaskDb;
using QCommon.Entity.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCommon.Extentions;
using QCommon.Components;

namespace IRC.Task.QueryServices
{
    /* ==============================================================================
     * 描述：TaskQueryService
     * 创建人：李传刚 2017/7/27 17:55:05
     * ==============================================================================
     */
    [Component(LifeStyle.Singleton)]
    public class TaskQueryService : BaseQueryService
    {
        public PageResult<TaskInfo> GetTaskPage(int page, int pageSize, string name)
        {
            var sql = "SELECT * FROM TaskInfo";
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql += " WHERE TaskName LIKE @Name OR BusinessName LIKE @Name ";
            }

            var query = QueryList<TaskInfo>(sql, new { Name = "%" + name + "%" });

            return new PageResult<TaskInfo>
            {
                Items = query.GetPage(page, pageSize).ToList(),
                PageParameter = new PageParameter(page, pageSize),
                TotalCount = query.Count()
            };
        }

        public List<TaskInfo> Tasks()
        {
            var sql = "SELECT * FROM TaskInfo";
            return QueryList<TaskInfo>(sql, null).ToList();
        }

        public PageResult<TaskSetting> GetSettingPage(int page, int pageSize,string name)
        {
            var sql = @"SELECT 
T1.Id,T1.TaskInfoId,T1.ScheduleName,
T1.ServerIP,T1.CronExpression,T1.RunLastTime,
T1.RunningTimes,T1.Status,T1.TaskType,T1.BusinessParameter,T1.[Desc],
T2.TaskName,T2.BusinessName 
from TaskSetting T1 LEFT JOIN TaskInfo T2 ON T1.TaskInfoId = T2.Id ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                sql += " WHERE T1.ScheduleName LIKE @Name OR T2.TaskName LIKE @Name OR T2.BusinessName LIKE @Name ";
            }

            var query = QueryList<TaskSetting>(sql, new { Name = "%" + name + "%" });
            return new PageResult<TaskSetting>
            {
                Items = query.GetPage(page, pageSize).ToList(),
                PageParameter = new PageParameter(page, pageSize),
                TotalCount = query.Count()
            };
        }


        public TaskInfo GetTaskInfo(int taskId)
        {
            var sql = "SELECT TOP 1 * FROM TaskInfo WHERE Id=@Id";
            return QueryList<TaskInfo>(sql, new { Id = taskId }).FirstOrDefault();
        }

        public TaskSetting GetSetting(int taskId)
        {
            var sql = "SELECT TOP 1 * FROM TaskSetting WHERE Id=@Id";
            return QueryList<TaskSetting>(sql, new { Id = taskId }).FirstOrDefault();
        }
    }
}
