using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QCommon.Components;
using QCommon.Logging;
using IRC.Task.Repositories;
using IRC.Task.Core.TaskDb;

namespace IRC.Task.Svc.Tests.Repositorie
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var rsy = ObjectContainer.Resolve<ITaskInfoRepository>();
            var list = rsy.GetAll();
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rsy = ObjectContainer.Resolve<ITaskInfoRepository>();
            var info = rsy.GetById(1);
            Assert.IsTrue(info != null);
        }


        [TestMethod]
        public void TestMethod3()
        {
            var rsy = ObjectContainer.Resolve<ITaskSettingRepository>();
            var list = rsy.GetAll();
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var rsy = ObjectContainer.Resolve<ITaskSettingRepository>();
            var info = rsy.GetById(1);
            Assert.IsTrue(info != null);
        }
    }
}
