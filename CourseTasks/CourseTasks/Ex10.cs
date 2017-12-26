using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;


namespace CourseTasks
{
    [TestClass]
    public class Ex10
    {
        //public static ChromeDriver driver;
        private const string URL = "http://localhost:8084/litecart";
        private const string m_expectedTitle = "My Store | Online Store";
        private const int timeout = 10;
        private const string BROWSERNAME = "Firefox";
        private IWebDriver driver;
        private WebDriverWait wait;

        #region repoElement
        public string repoProduct => $".//div[contains(@class,'products')]/div";
        public string repoProductSticker => $".//div[contains(@class,'sticker')]";

        #endregion
        public class GoodsPrice
        {
           
            public int Size { get; set; }
            public string Style { get; set; }
        }

        public class Goods
        {
            public string URL { get; set; }
            public string Name { get; set; }
        }

        public void TestInit()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
        }
        public void TestSetup(string browerName)

        {
            if (browerName == "Chrome")
            {
                driver = new ChromeDriver();
                TestInit();
                return;
            }
            if (browerName == "Firefox")
            {
                driver = new FirefoxDriver();
                TestInit();
                return;
            }
            if (browerName == "IE")
            {
                driver = new InternetExplorerDriver();
                TestInit();
                return;
            }

            Assert.Fail("Browser does not supported");
        }
        //а) на главной странице и на странице товара совпадает текст названия товара

        [TestMethod]
        public void ValidateAllProfuctParametr()

        {
          TestSetup(BROWSERNAME);
          TestInit();

            var listofProducts = new List<Goods>();
            var productList = driver.FindElements(By.XPath(".//li[contains(@class,'product column')]"));

            foreach (var product in productList)
            {
                var url = product.FindElement(By.XPath(".//a"));
                var name = product.FindElement(By.XPath(".//div[@class='name']"));
                var manufacturer = product.FindElement(By.XPath(".//div[@class='manufacturer']"));
              //различия  var price = product.FindElements(By.XPath(".//span[contains(@class,'price')]"));
                var saleprice = product.FindElements(By.XPath(".//strong[contains(@class,'price')]"));

            }

            }

    }
}
