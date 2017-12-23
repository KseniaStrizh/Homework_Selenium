using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lesson4_ex8

{
    [TestClass]
    public class SeleniumTestProject
    {
        public static ChromeDriver driver;
        private const string m_baseURL = "http://localhost:8084/litecart";
        private const string m_expectedTitle = "My Store | Online Store";
        private const int timeout = 10;
        private WebDriverWait wait;

        #region repoElement
        public string repoProduct => $".//div[contains(@class,'products')]/div";
        public string repoProductSticker => $".//div[contains(@class,'sticker')]";

        #endregion
        [TestInitialize]
        public void TestSetup()

        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Navigate().GoToUrl(m_baseURL);
            driver.Manage().Window.Maximize();
            wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
        }
        [TestMethod]
        public void CheckAllProductsStikers()

        {
            var ListItemsWithoutStickers = new List<String>();
            var ProductItems = driver.FindElements(By.XPath(repoProduct));

            foreach (var item in ProductItems)

            {
                if (item.FindElements(By.XPath(repoProductSticker)).Count != 1)
                {
                    var productName = item.FindElement(By.XPath(repoProductSticker)).Text;
                    Assert.Fail($"Stickers validation failed for product {productName}");
                }
            }
        }
        [ClassCleanup]
        public static void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}