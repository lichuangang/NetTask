using IRC.Task.Core.TaskDb;
using QCommon.Components;
using QCommon.ThirdParty.Dapper;
using QCommon.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Repositories.Impl
{
    /* ==============================================================================
     * 描述：TaskSettingRepository
     * 创建人：李传刚 2017/7/26 9:43:03
     * ==============================================================================
     */
    [Component]
    public class TaskSettingRepository : TaskDbRepository<TaskSetting>, ITaskSettingRepository
    {
        /// <summary>
        /// 0 停止 1 启动
        /// </summary>
        public void UpdateStatus(int setId, int status)
        {
            var sql = status == 1 ? "UPDATE TaskSetting SET Status=@Status, RunLastTime=getdate(),RunningTimes=RunningTimes+1 WHERE Id=@Id"
                : "UPDATE TaskSetting SET Status=@Status WHERE Id=@Id";
            Execute(sql, new { Status = status, Id = setId });
        }

        public void Insert(TaskSetting setting)
        {
            var sql = @"INSERT INTO TaskSetting (TaskInfoId,ScheduleName,ServerIP,CronExpression,TaskType,BusinessParameter) 
                                         VALUES (@TaskInfoId,@ScheduleName,@ServerIP,@CronExpression,@TaskType,@BusinessParameter)";
            Execute(sql, setting);
        }

        public void Update(TaskSetting setting)
        {
            var sql = @"UPDATE TaskSetting SET 
TaskInfoId=@TaskInfoId,
ScheduleName=@ScheduleName,
ServerIP=@ServerIP,
CronExpression=@CronExpression,
TaskType=@TaskType,
BusinessParameter=@BusinessParameter WHERE Id=@Id ";
            Execute(sql, setting);
        }

        public int CountByTaskInfoId(int id)
        {
            var sql = "SELECT COUNT(1) FROM TaskSetting WHERE TaskInfoId=@Id";
            return ExecuteQueryScalar<int>(sql, new { Id = id });
        }

        public List<TaskSetting> GetListByStatus(params int[] status)
        {
            var sql = "SELECT * FROM TaskSetting WHERE Status in @Status ";
            return QueryList<TaskSetting>(sql, new { Status = status }).ToList();
        }
    }
}
