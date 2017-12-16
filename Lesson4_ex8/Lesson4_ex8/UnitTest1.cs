using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lesson4_ex8
{
    [TestClass]
    public class CheckStickers
    {
        public static ChromeDriver driver;
        // 1 private string m_baseURL = "http://localhost:8080/litecart/en/";
        
        private string m_baseURL = "http://localhost/litecart/public_html/en/";
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
           try
            {
                var ListItemsWithoutStickers = new List <String>();
                var ProductItems = driver.FindElements(By.XPath(repoProduct));
                foreach (var Item in ProductItems)
                {
                    if (Item.FindElements(By.XPath(repoProductSticker)).Count == 0)
                    {
                        ListItemsWithoutStickers.Add(Item.FindElement(By.XPath(".//a")).GetAttribute("href"));
                        Assert.Fail("Product has not sticker");
                    }
                }
                if (ListItemsWithoutStickers.Count > 1)
                {
                    Assert.Fail("There is more tan one stickers");
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
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
