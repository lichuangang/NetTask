using IRC.Task.Core;
using QCommon.ThirdParty.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.QueryServices
{
    public class BaseQueryService : BaseDapperQueryService
    {
        protected BaseQueryService()
            : base(ConfigSettings.TaskDbConnectionString)
        {
        }
    }
}
