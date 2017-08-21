using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using QCommon.Configurations;
using QCommon.Logging;
using QCommon.Components;
using IRC.Task.Core.Common;
using QCommon.ThirdParty.Autofac;
using Autofac;
using IRC.Task.Repositories;

namespace IRC.Task.Svc.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected ILogger _logger;

        public BaseTest()
        {
            Startup.InitConfiguration();
            //注册TaskDbRepository 
            var container = ((AutofacObjectContainer)ObjectContainer.Current).Container;
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(TaskDbRepository<>));
            builder.Update(container);
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(this.GetType().FullName);
        }

        
    }
}
