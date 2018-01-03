using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace CourseTasks
{
    [TestClass]
    public class Ex12
    {

        private const string URL = "http://localhost/litecart/public_html/admin/?app=catalog&doc=catalog";
       
        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static ChromeDriver driver;
        private WebDriverWait wait;
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";

        #region repoElement
        //status tab
        public string AddNewProduct = $".//a[contains(@href,'edit_product')]";
        public string Name = $".//label[text()='Name']/..//input";
        public string Code = $".//label[text()='Code']/..//input";
        #endregion

        public static string ProductCodeGenerator()
        {
            return "c" + new Random().Next(1000, 9999);
        }
        [TestInitialize]
        public void TestSetup()

        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Navigate().GoToUrl(URL);
            driver.Manage().Window.Maximize();
            Thread.Sleep(4000);
        }

        [TestMethod]
        public void Ex_12()
        {

            IWebElement login = driver.FindElement(By.XPath(".//input[@name='username']"));
            IWebElement password = driver.FindElement(By.XPath(".//input[@name='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

            login.SendKeys(ADMIN_LOGIN);
            password.SendKeys(ADMIN_PASSWORD);
            loginButton.Click();
            Thread.Sleep(10000);

            var count = driver.FindElements(By.CssSelector(".dataTable .row")).Count;
            driver.FindElement(By.XPath(AddNewProduct)).Click();

            Thread.Sleep(10000);

            driver.FindElement(By.XPath(Name)).SendKeys("ProductName");

            string CodeValue = ProductCodeGenerator();
            driver.FindElement(By.XPath(Code)).SendKeys(CodeValue);

        }
    }
}
