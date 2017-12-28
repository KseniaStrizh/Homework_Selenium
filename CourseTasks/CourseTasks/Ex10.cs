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

        private const string URL = "http://localhost/litecart/en/";
        
        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static IWebDriver driver;
        private WebDriverWait wait;

        #region repoElement
        public string repoDuck => $".//div[contains(@class,'product column')]";
        public string repoDuckPage => $".//div[@id='box-product']";
        #endregion


        public void TestInit()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
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
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void CheckIfGrey(string color)
        {
            color = color.Trim(')').Split('(')[1]; IList<string> rgba = color.Substring(0, color.Length - 1).Split(',');
            rgba[1] = rgba[1].Trim();
            rgba[2] = rgba[2].Trim();
            Assert.IsTrue((rgba[0] == rgba[1]) && (rgba[1] == rgba[2]) && (rgba[2] == rgba[0]));
            Console.WriteLine("Price color is grey - all components are equale");
        }

        public void CheckIfRed(string color)
        {
            color = color.Trim(')').Split('(')[1]; IList<string> rgba = color.Substring(0, color.Length - 1).Split(',');
            rgba[1] = rgba[1].Trim();
            rgba[2] = rgba[2].Trim();
            Assert.IsTrue((rgba[1] == "0") && (rgba[2] == "0"), "Campaign price is  red.");
        }

        public string GetTextDecorationValue(IWebElement duckPrice)
        {
            if (duckPrice.GetCssValue("text-decoration-line") != "")
            {
                return duckPrice.GetCssValue("text-decoration-line");
            }
            else
            {
                return duckPrice.GetCssValue("text-decoration");
            }
        }

        [TestMethod]
        public void Ex_10()

        {
            TestSetup(BROWSERNAME);
            TestInit();

            var AllDucks = driver.FindElements(By.XPath(repoDuck));
            foreach (var duck in AllDucks)
            {
                #region Duck parametr on CAMPAIGN PAGE
                if (!IsElementPresent(By.XPath(".//span[@class='price']")))
                {
                    var DuckName = duck.FindElement(By.XPath(".//div[@class='name']"));
                    //we validate text and standart price only  var
                    Console.WriteLine("We can not validate all parametrs so than I validate only name");
                    Thread.Sleep(timeout);
                    duck.Click();
                    Thread.Sleep(timeout);
                    IWebElement DuckOnProductPage = driver.FindElement(By.XPath(repoDuckPage));
                    var DuckNameOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//h1"));

                    Assert.AreEqual(DuckName.Text, DuckNameOnProductPage.Text);
                    Console.WriteLine($"Name on campaign page{DuckName.Text} name on product page {DuckNameOnProductPage.Text}");

                }
                else
                {
                    var DuckName = duck.FindElement(By.XPath(".//div[@class='name']"));
                    IWebElement ItemRegularPrice = driver.FindElement(By.XPath(".//s[@class='regular-price']"));
                    IWebElement ItemCampaignPrice = driver.FindElement(By.XPath(".//strong[@class='campaign-price']"));

                   
                    var RegularPriceValue = ItemRegularPrice.Text;
                    var CampaignPriceValue = ItemCampaignPrice.Text;

                    var ColorRegularPriceCampaignPage = ItemRegularPrice.GetCssValue("color");
                    var ColorCampagnPriceCampaignPage = ItemCampaignPrice.GetCssValue("color");

                    var DecorationRegularPriceCampaignPage = GetTextDecorationValue(ItemRegularPrice);
                   
                    var FontRegularPriceCampaignPage = ItemRegularPrice.GetCssValue("font-weight");
                    var FontCampaignPriceCampaignPage = ItemCampaignPrice.GetCssValue("font-weight");


                    #endregion

                    Thread.Sleep(timeout);
                    duck.Click();
                    Thread.Sleep(timeout);

                    #region Duck parametr on product page

                    IWebElement DuckOnProductPage = driver.FindElement(By.XPath(repoDuckPage));

                    IWebElement ItemRegularDuckPagePrice = driver.FindElement(By.XPath(".//s[@class='regular-price']"));
                    IWebElement ItemDuckPageCampaignPrice = driver.FindElement(By.XPath(".//strong[@class='campaign-price']"));

                    var DuckNameOnProductPage = DuckOnProductPage.FindElement(By.XPath(".//h1"));

                    var RegularPriceValueOnProdictPage = ItemRegularDuckPagePrice.Text;
                    var CampaignPriceValueOnProductPage = ItemDuckPageCampaignPrice.Text;

                    var ColorRegularPriceOnProductPage = ItemRegularDuckPagePrice.GetCssValue("color");
                    var ColorCampagnPriceOnProductPage = ItemDuckPageCampaignPrice.GetCssValue("color");

                    var DecorationRegularPriceOnProductPage = GetTextDecorationValue(ItemRegularDuckPagePrice);
                    
                    var FontRegularPriceOnProductPage = ItemRegularDuckPagePrice.GetCssValue("font-weight");
                    var FontCampaignPriceOnProductPage = ItemDuckPageCampaignPrice.GetCssValue("font-weight");
                    #endregion

                    //А
                    #region a. I validate product name
                    Assert.AreEqual(DuckName.Text, DuckNameOnProductPage.Text);
                    Console.WriteLine($"Name on campaign page{DuckName.Text} name on product page {DuckNameOnProductPage.Text}");
                    #endregion

                    //Б
                    #region I validate  standart price value and campaign price on both page
                    Assert.AreEqual(RegularPriceValue, RegularPriceValueOnProdictPage);
                    Assert.AreEqual(CampaignPriceValue, CampaignPriceValueOnProductPage);
                    #endregion

                    //B
                    #region I validate regular price are grey and 
                    //on campaign page
                    CheckIfGrey(ColorRegularPriceCampaignPage);
                    Assert.IsTrue(DecorationRegularPriceCampaignPage == "line-through");

                    //on product page
                    CheckIfGrey(ColorRegularPriceOnProductPage);
                    Assert.IsTrue(DecorationRegularPriceOnProductPage == "line-through");
                    #endregion
                    //Г
                    CheckIfRed(ColorCampagnPriceCampaignPage);
                    Assert.IsTrue(FontCampaignPriceCampaignPage == "bold" || FontCampaignPriceCampaignPage == "700");
                    CheckIfRed(ColorCampagnPriceOnProductPage);
                    Assert.IsTrue(FontCampaignPriceCampaignPage == "bold" || FontCampaignPriceCampaignPage == "700");

                    //Д
                    #region I validate sales price bigger than regular price on both pages
                    if ((int.Parse(FontCampaignPriceCampaignPage) > int.Parse(FontRegularPriceCampaignPage)) & (int.Parse(FontCampaignPriceOnProductPage) > int.Parse(FontRegularPriceOnProductPage)))

                        Console.WriteLine("Validation that sales price bigger that regular price on both pages - SUCCEDED");

                    else
                        Assert.Fail("Validation failed");

                    driver.FindElement(By.XPath(".//button[contains(@class,'close')]")).Click();



                    #endregion
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