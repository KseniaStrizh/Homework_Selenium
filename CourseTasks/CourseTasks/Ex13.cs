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

namespace CourseTasks
{
    [TestClass]
    public class Ex13
    {
        private const string URL = "http://localhost/public_html/litecart/en/";

        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static IWebDriver driver;
        private WebDriverWait wait;

        #region repoElement
        public string repoDuck => $".//div[contains(@class,'product column')]";
        public string repoDuckPage => $".//div[@id='box-product']";
        #endregion
        [TestInitialize]
        public void TestSetup()

        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Manage().Window.Maximize();
        }
        [TestMethod]
        public void Ex_13()
        {
            for (int i = 1; i <= 3; i++)
            {
                driver.Navigate().GoToUrl(URL);
                driver.FindElement(By.CssSelector(".product")).Click();
                if (driver.Url.Contains("yellow-duck-p-1"))
                {
                    var select = new SelectElement(driver.FindElement(By.Name("options[Size]")));
                    select.SelectByIndex(1);
                }
                driver.FindElement(By.Name("add_cart_product")).Click();
                var cartCounter = driver.FindElement(By.ClassName("quantity"));
                wait.Until(ExpectedConditions.TextToBePresentInElement(cartCounter, i.ToString()));
            }
        }
    }
}