using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Threading;

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

        public void CheckIfGrey(string color)
        {
            color = color.Split('(')[1]; IList<string> rgba = color.Substring(0, color.Length - 1).Split(',');
            rgba[1] = rgba[1].Trim();
            rgba[2] = rgba[2].Trim();
            Assert.IsTrue((rgba[0] == rgba[1]) && (rgba[1] == rgba[2]) && (rgba[2] == rgba[0]));
            Console.WriteLine("Price color is grey - all components are equale");
        }

        public void CheckIfRed(string color)
        {
            color = color.Split('(')[1]; IList<string> rgba = color.Substring(0, color.Length - 1).Split(',');
            rgba[1] = rgba[1].Trim();
            rgba[2] = rgba[2].Trim();
            Assert.IsTrue((rgba[1] == "0") && (rgba[2] == "0"), "Campaign price is  red.");
        }
        [TestMethod]
        public void ValidateAllProfuctParametr()

        {
            TestSetup(BROWSERNAME);
            TestInit();
            #region Duck parametr on CAMPAIGN PAGE
            //I get duck in campaign and get its parametrs
            IWebElement FirstGoodsInCampaign = driver.FindElement(By.XPath(repoDuck));
            IWebElement FirstGoodsInCampaignPrice = driver.FindElement(By.XPath(repoDuckPrice));

            //Duck parametr on campaign page 
            var NameCampaignPage = FirstGoodsInCampaign.FindElement(By.XPath(".//div[@class='name']"));
            var PriceCampaignPage = FirstGoodsInCampaignPrice.FindElement(By.XPath(".//s"));
            var SalesPriceCampaignPage = FirstGoodsInCampaignPrice.FindElement(By.XPath(".//strong"));

            //styles - standart price - on site its called - regulare
            var FontPriceCampaignPage = PriceCampaignPage.GetCssValue("font-weight");
            var ColorPriceCampaignPage = PriceCampaignPage.GetCssValue("color");
            var DecorationPriceCampaignPage = PriceCampaignPage.GetCssValue("text-decoration");

            //styles - sales(discount) price - on site its called - campaign
            string FontSalesPrice = SalesPriceCampaignPage.GetCssValue("font-weight");
            string ColorsalesPrice = SalesPriceCampaignPage.GetCssValue("color");
            string DecorationSalesPrice = SalesPriceCampaignPage.GetCssValue("text-decoration");
            #endregion

            #region Validate Color
            CheckIfGrey(ColorPriceCampaignPage);
            CheckIfRed(ColorsalesPrice);
            #endregion
            //I go to product page
            Thread.Sleep(timeout);
            FirstGoodsInCampaign.Click();

            #region Duck parametr on product page
            //Duck parametr on product page
            IWebElement DuckOnProductPage = driver.FindElement(By.XPath(repoDuckPage));

            var NameOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//h1"));
            var PriceOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//del"));
            var SalePriceOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//strong"));

            //styles - standart price - on product page

            string FontPriceProductPage = PriceOnProductPage.GetCssValue("font-weight");
            string ColorPriceProductPage = PriceOnProductPage.GetCssValue("color");
            string DecorationPriceProductPage = PriceOnProductPage.GetCssValue("text-decoration");
            //styles - sales price - on product page

            string FontSalesPriceProductPage = SalePriceOnProductPage.GetCssValue("font-weight");
            string ColorSalesPriceProductPage = SalePriceOnProductPage.GetCssValue("color");
            string DecorationSalesPriceProductPage = SalePriceOnProductPage.GetCssValue("text-decoration");
            #endregion

            #region  Validate  price color
            CheckIfGrey(ColorPriceProductPage);
            CheckIfRed(ColorSalesPriceProductPage);
            #endregion

            #region I validate product name
            Assert.AreEqual(NameCampaignPage.Text, NameOnProductPage.Text);
            Console.WriteLine($"Name on campaign page{NameCampaignPage.Text} name on product page {NameOnProductPage.Text}");
            #endregion

            #region I validate  standart price
            Assert.AreEqual(PriceCampaignPage.Text, PriceOnProductPage.Text);
            Console.WriteLine($"Price on campaign page {PriceCampaignPage.Text} price on product page {PriceOnProductPage.Text}");
            #endregion

            #region I validate sales price
            Assert.AreEqual(SalesPriceCampaignPage.Text, SalePriceOnProductPage.Text);
            Console.WriteLine($"Sales price on campaign page {SalesPriceCampaignPage.Text} price on product page {SalePriceOnProductPage.Text}");
            #endregion

            #region I validate sales price bigger than regular price on both pages
            if ((int.Parse(FontSalesPrice) > int.Parse(FontPriceCampaignPage)) & (int.Parse(FontSalesPriceProductPage) > int.Parse(FontPriceProductPage)))

                Console.WriteLine("Validation that sales price bigger that regular price on both pages - SUCCEDED");

            else
                Assert.Fail("Validation failed");


            #endregion

        }
    }
}