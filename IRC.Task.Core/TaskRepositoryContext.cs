using QCommon.Components;
using QCommon.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core
{
    /* ==============================================================================
     * 描述：TaskRepositoryContext
     * 创建人：李传刚 2017/7/25 13:40:28
     * ==============================================================================
     */
    [Component]
    public class TaskRepositoryContext : AbstractRepositoryContext
    {
        public TaskRepositoryContext() : base(ConfigSettings.TaskDbConnectionString) { }

        public override IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
