using IRC.Task.Core.TaskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IRC.TaskManager.Svc.Controllers
{
    public class TaskController : ApiController
    {
        [HttpGet]
        public string Test()
        {
            return "helloword";
        }

        public List<TaskInfo> GetTask()
        {
            return null;
        }
    }
}
