using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lesson4_Ex8
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    namespace SeleniumTests
    {
        [TestClass]
        public class OpenPageAndCloseIt
        {
            public static ChromeDriver driver;
            private string m_baseURL = "http://localhost:8080/litecart/en/";
            private const string m_expectedTitle = "My Store";
            private const int timeout = 10;
            private WebDriverWait wait;


            [TestInitialize]
            public void TestSetup()
            {
                driver = new ChromeDriver();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            }

            [TestMethod]
            public void CheckStick()
            {
                driver.Navigate().GoToUrl(m_baseURL);
                wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));

                try
                {
                    var ListItemsWithoutStickers = new List<String>();
                    var ProductItems = driver.FindElements(By.XPath(".//li[contains(@class,'product column')]"));
                    foreach (var Item in ProductItems)
                    {
                        if (Item.FindElements(By.XPath(".//div[contains(@class,'sticker')]")).Count == 0)
                        {
                            ListItemsWithoutStickers.Add(Item.FindElement(By.XPath(".//a")).GetAttribute("href"));
                        }
                    }
                    if (ListItemsWithoutStickers.Count > 0)
                    {
                        Assert.Fail("There is no stickers on:\n" + ListItemsWithoutStickers.Aggregate((i, j) => i + "\n" + j));
                    }

                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }
            }

            [TestCleanup]
            public void QuitBrowser()
            {
                driver.Quit();
                driver = null;
            }

        }
    }
}
