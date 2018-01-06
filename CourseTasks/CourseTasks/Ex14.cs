using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Threading;

namespace CourseTasks
{
    [TestClass]
    public class Ex14
    {
        private const string URL = "http://localhost/litecart/public_html/admin/?app=countries&doc=countries";

        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static ChromeDriver driver;
        private WebDriverWait wait;
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        #region repoElement
        public string UserName => $".//input[@name='username']";
        public string UserPassword => $".//input[@name='password']";
        public string BtnLogin => $".//button[@name='login']";
        public string CountryPageLabel => $".//main[@id='main']/h1";
        #endregion

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
        public void Ex_14()
        {
            IWebElement login = driver.FindElement(By.XPath(UserName));
            IWebElement password = driver.FindElement(By.XPath(UserPassword));
            IWebElement loginButton = driver.FindElement(By.XPath(BtnLogin));

            login.SendKeys(ADMIN_LOGIN);
            password.SendKeys(ADMIN_PASSWORD);
            loginButton.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(CountryPageLabel)));

            driver.FindElement(By.XPath(".//a[@class='btn btn-default']")).Click();
            var mainWindow = driver.CurrentWindowHandle;

            var links = driver.FindElements(By.XPath(".//i[contains(@class,'fa-external')]"));
            //I should check for all link on page (8)
            foreach (var item in links)
            {
                item.Click();

                wait.Until((d) =>
                {
                    var windows = d.WindowHandles;
                    foreach (var window in windows)
                    {
                        if (window != mainWindow)
                        {
                            return d.SwitchTo().Window(window);
                        }
                    }
                    return null;
                });

                driver.Close();

                driver.SwitchTo().Window(mainWindow);
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