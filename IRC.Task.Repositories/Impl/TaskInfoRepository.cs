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
     * 描述：TaskInfoRepository
     * 创建人：李传刚 2017/7/25 13:46:08
     * ==============================================================================
     */
    [Component]
    public class TaskInfoRepository : TaskDbRepository<TaskInfo>, ITaskInfoRepository
    {
        public void Insert(TaskInfo task)
        {
            var sql = @"INSERT INTO TaskInfo 
                        (TaskName,BusinessName,TaskAssemblyName,RunningTimes,[Desc],FileName) VALUES 
                        (@TaskName,@BusinessName,@TaskAssemblyName,@RunningTimes,@Desc,@FileName) ";
            Execute(sql, task);
        }

        public void Update(TaskInfo task)
        {
            var sql = @"UPDATE TaskInfo SET
                        TaskName=@TaskName,BusinessName=@BusinessName,TaskAssemblyName=@TaskAssemblyName,[Desc]=@Desc,FileName=@FileName
                        WHERE Id=@Id";
            Execute(sql, task);
        }

        public int CountByName(string name, int id = 0)
        {

            var sql = "SELECT COUNT(1) FROM TaskInfo WHERE TaskName=@TaskName ";
            if (id != 0)
            {
                sql += "AND Id <> @Id";
            }
            return ExecuteQueryScalar<int>(sql, new { TaskName = name, Id = id });
        }
    }
}
