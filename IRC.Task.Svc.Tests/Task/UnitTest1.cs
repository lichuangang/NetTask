using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskDemo1;
using TagBookBeginTask;

namespace IRC.Task.Svc.Tests.Task
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            new TaskOne().Process();
        }

        [TestMethod]
        public void TestMethod2()
        {
            new RunTask().Process();
        }

    }
}
