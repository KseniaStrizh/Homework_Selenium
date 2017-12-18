using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SeleniumTestProject
{
    [TestClass]
    public class OpenPageAndCloseIt
    {


        public static ChromeDriver driver;
        private string m_baseURL = "http://en.wikipedia.org/wiki/Giant_panda";
        private const int timeout = 10;
        private const string m_expectedTitle = "Giant panda - Wikipedia";
        private WebDriverWait wait;

        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        }

        [TestMethod]
        public void GoToURL()
        {
            driver.Navigate().GoToUrl(m_baseURL);
            wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
        }

        [TestCleanup]
        public void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
