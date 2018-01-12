using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseTasks.Ex19
{
    public class TestBase
    {
        public Application app;

        [TestInitialize]
        public void Setup()
        {
            app = new Application();
        }

        [TestCleanup]
        public void TearDown()
        {
            app.Quit();
            app = null;
        }
    }
}