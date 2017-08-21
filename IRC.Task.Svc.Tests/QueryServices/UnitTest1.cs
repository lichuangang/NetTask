using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QCommon.Components;
using IRC.Task.QueryServices;

namespace IRC.Task.Svc.Tests.QueryServices
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var qs = ObjectContainer.Resolve<TaskQueryService>();
            var data = qs.GetTaskPage(1, 10,"");
            Assert.IsTrue(data.TotalCount > 0);
        }


        [TestMethod]
        public void TestMethod2()
        {
            var qs = ObjectContainer.Resolve<TaskQueryService>();
            var data = qs.GetTaskInfo(1);
            Assert.IsTrue(data != null);
        }


        [TestMethod]
        public void TestMethod3()
        {
            var qs = ObjectContainer.Resolve<TaskQueryService>();
            var data = qs.GetSetting(1);
            Assert.IsTrue(data != null);
        }
    }
}
