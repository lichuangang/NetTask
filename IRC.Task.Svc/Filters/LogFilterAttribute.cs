using QCommon.Components;
using QCommon.Logging;
using QCommon.Utilities;
using QCommon.Extentions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IRC.Task.Svc.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(LogFilterAttribute).FullName);
        private Stopwatch _watch;
        private string _id;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _watch = new Stopwatch();
            _id = GuidUtility.GetGuid();
            _logger.InfoFormat(
                "id:[{3}] 请求Contorller:[{0}] Action:[{1}] 参数信息:[{2}] ",
                actionContext.ControllerContext.RouteData.Values["controller"],
                actionContext.ControllerContext.RouteData.Values["action"], actionContext.ActionArguments.Serialize(),
                _id);
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            _watch.Stop();
            _logger.InfoFormat("id:[{1}] 总共处理时间为:{0},返回结果：[{2}]",
                _watch.ElapsedMilliseconds,
                _id,
                 actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
        }
    }
}