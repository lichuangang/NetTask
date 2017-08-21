using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using QCommon.Logging;
using IRC.Task.Core.Common;
using System.Reflection;
using QCommon.ThirdParty.Autofac;
using Autofac;
using IRC.Task.Repositories;
using QCommon.Components;
using Autofac.Integration.WebApi;

namespace IRC.TaskManager.Svc
{
    public class Global : HttpApplication
    {
        private ILogger _logger;

        void Application_Start(object sender, EventArgs e)
        {
            Startup.InitConfiguration(Assembly.Load("IRC.TaskManager.Svc"));
            RegisterBaseRepositorys();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterApiControllers(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);

            _logger.Info("初始化完成...");          
        }

        private void RegisterApiControllers(HttpConfiguration config)
        {
            var container = ((AutofacObjectContainer)ObjectContainer.Current).Container;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.Update(container);
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
    }
}