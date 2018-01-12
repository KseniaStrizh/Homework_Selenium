using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CourseTasks.Ex19
{
    [TestClass]
    public class Ex19Tests : TestBase
    {
        [TestMethod]
        public void Ex_19()
        {
            #region GenerateTestData

            List<Product> products = new List<Product>();

            for (int i = 0; i < 3; i++)
            {
                products.Add(new Product());
            }

            #endregion

            app.OpenSite();
            app.BuyProducts(products);
            app.DeleteAllProducts();
            app.Quit();
        }
    }
}