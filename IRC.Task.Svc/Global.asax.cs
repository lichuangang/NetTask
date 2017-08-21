using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using IRC.Task.Core.Common;
using QCommon.ThirdParty.Autofac;
using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Compilation;
using System.Reflection;
using QCommon.Components;
using QCommon.Logging;
using IRC.Task.Repositories;
using IRC.Task.Applications;

namespace IRC.Task.Svc
{
    public class Global : HttpApplication
    {
        private ILogger _logger;
        void Application_Start(object sender, EventArgs e)
        {
            Startup.InitConfiguration(Assembly.Load("IRC.Task.Svc"));
            RegisterBaseRepositorys();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterApiControllers(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);

            ObjectContainer.Resolve<ITaskRunService>().Restart();

            _logger.Info("初始化完成...");
        }

        /// <summary>
        /// 注册TaskDbRepository 
        /// </summary>
        private void RegisterBaseRepositorys()
        {
            var container = ((AutofacObjectContainer)ObjectContainer.Current).Container;
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(TaskDbRepository<>));
            builder.Update(container);
        }

        private void RegisterApiControllers(HttpConfiguration config)
        {
            var container = ((AutofacObjectContainer)ObjectContainer.Current).Container;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.Update(container);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            if (_logger == null)
            {
                _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
            }
            _logger.Error("未捕获的异常：" + lastError.Message, lastError);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest req = HttpContext.Current.Request;
            if (req.HttpMethod == "OPTIONS")//过滤options请求，用于js跨域
            {
                Response.StatusCode = 200;
                Response.SubStatusCode = 200;
                Response.End();
            }
        }
    }
}