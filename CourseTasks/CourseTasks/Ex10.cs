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

        private const string URL = "http://localhost/litecart/public_html/en/";//"http://localhost:8084/litecart/public_html";
        private const string m_expectedTitle = "My Store | Online Store";
        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        private IWebDriver driver;
        private WebDriverWait wait;

        #region repoElement
        public string repoProductSticker => $".//div[contains(@class,'sticker')]";
        public string repoDuck => $".//div[contains(@id,'campaign')]//div[contains(@class,'product column')]";
        public string repoDuckPrice => $"{repoDuck}//div[@class='price-wrapper']";
        public string repoDuckPage => $".//div[@id='box-product']";
        public string repoDuckPriceOnProductPage => $"{repoDuckPage}//div[contains(@class,'price-wrapper')]";
        #endregion


      
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
            //I get duck in campaign and get its parametrs
            IWebElement FirstGoodsInCampaign = driver.FindElement(By.XPath(repoDuck));
            IWebElement FirstGoodsInCampaignPrice = driver.FindElement(By.XPath(repoDuckPrice));

            //Duck parametr on campaign page 
            var url = FirstGoodsInCampaign.FindElement(By.XPath(".//a"));
            var name = FirstGoodsInCampaign.FindElement(By.XPath(".//div[@class='name']"));
            var manufacturer = FirstGoodsInCampaign.FindElement(By.XPath(".//div[@class='manufacturer']"));
            var price = FirstGoodsInCampaignPrice.FindElement(By.XPath(".//s"));
            var saleprice = FirstGoodsInCampaignPrice.FindElement(By.XPath(".//strong"));

            //I go to product page
            FirstGoodsInCampaign.Click();

            IWebElement DuckOnProductPage = driver.FindElement(By.XPath(repoDuckPage));
            var nameOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//h1"));
            var manufacturerOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//div[@class='manufacturer']//img"));
            var priceOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//del"));
            

            #region I validate product name
            Assert.AreEqual(name.Text, nameOnProductPage.Text);
            Console.WriteLine($"Name on campaign page{name.Text} name on product page {nameOnProductPage.Text}");
            #endregion

            #region I validate price
            Assert.AreEqual(price.Text, priceOnProductPage.Text);
            Console.WriteLine($"Name on campaign page{name.Text} name on product page {nameOnProductPage.Text}");
            #endregion


        }
    }
}