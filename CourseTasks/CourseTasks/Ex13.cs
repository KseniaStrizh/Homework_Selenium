using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System.Linq;

namespace CourseTasks
{
    [TestClass]
    public class Ex13
    {
        private const string URL = "http://localhost/litecart/en/";

        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static IWebDriver driver;
        public static WebDriverWait wait;

        #region repoElement
        public string repoDuck => $".//div[contains(@class,'product column')]";
        public string repoDuckPage => $".//div[@id='box-product']";
        public string QtyinCart => $".//span[@class='quantity']";
        public string Size => $".//select[@name='options[Size]']";
        public string Title => $".//h1[@class='title']";
        public string AddToCartBtn => $".//button[contains(@class,'btn-success')]";
        public string closeBtn => $".//button[contains(@class,'close-icon')]";
        public string CheckOutBtn => $".//div[@class='title']";
        public string RemoveBtn => $".//button[@title='Remove']";
        public string EmptyCartLabel => $".//p/em";
        #endregion

        [FindsBy(How = How.CssSelector, Using = "td.item")]
        [CacheLookup]
        private IList<IWebElement> _ordersList;

        [TestInitialize]
        public void TestSetup()

        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Manage().Window.Maximize();
        }

        #region Waiting
        public static bool IsElementPresent(string xpath)
        {
            return driver.FindElements(By.XPath(xpath)).Count > 0;
        }
        public static void WaitTextPresent(IWebElement element, string text)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
        }
        #endregion
        public void RemoveAllProducts()
        {
            while (IsElementPresent(RemoveBtn))
            {
                driver.FindElement(By.CssSelector("[name=remove_cart_item]")).Click();

            }
        }

        public bool CheckEmptyCart()
        {
            return IsElementPresent(EmptyCartLabel);
        }

        [TestMethod]
        public void Ex_13()
        {

            driver.Navigate().GoToUrl(URL);
            var count = Convert.ToInt32(driver.FindElement(By.XPath(QtyinCart)).Text);
            while (count < 3)
            {
                driver.FindElement(By.CssSelector(".product")).Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath(Title)));
                if (IsElementPresent(Size))
                {
                    var select = new SelectElement(driver.FindElement(By.XPath(Size)));
                    select.SelectByIndex(1);
                }

                driver.FindElement(By.XPath(AddToCartBtn)).Click();
                driver.FindElement(By.XPath(closeBtn)).Click();
                WaitTextPresent(driver.FindElement(By.XPath(QtyinCart)), (count + 1).ToString());
                 count++;
            }

            driver.FindElement(By.XPath(CheckOutBtn)).Click();
            RemoveAllProducts();
            CheckEmptyCart();

        }

        [ClassCleanup]
        public static void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}