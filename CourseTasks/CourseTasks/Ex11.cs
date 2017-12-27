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
    public class Ex11
    {
        private const string URL = "http://localhost/litecart/public_html/en/create_account";
        private const string m_expectedTitle = "Create Account | My Store";
        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static ChromeDriver driver;
        private WebDriverWait wait;

        #region repoElement
        public string Firstname => $".//input[@name='firstname']";
        public string Lastname => $".//input[@name='lastname']";
        public string Country => $".//select[@name='country_code']";
        public string Email => $".//input[@name='email']";
        public string DesiredPassword => $".//input[@name='password']";
        public string ConfirmPassword => $".//input[@name='confirmed_password']";
        #endregion

        [TestInitialize]
        public void TestSetup()

        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Navigate().GoToUrl(URL);
            driver.Manage().Window.Maximize();
            wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
        }
        private static string GenerateUnicName()
        {
            string uniqueName = "KungFuPanda" + DateTime.Now.ToBinary() + "@gmail.com";
            return uniqueName;
        }

        [TestMethod]
        public void Reg()

        {
           GenerateUnicName();
        }

        [ClassCleanup]
        public static void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
