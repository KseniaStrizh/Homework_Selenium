using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lesson2_Ex3
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
            private string m_baseURL = "localhost:80/litecart/admin";
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
            public void GoToURL()
            {
                driver.Navigate().GoToUrl(m_baseURL);
                wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
                IWebElement Login = driver.FindElement(By.XPath(".//input[@name='username']"));
                IWebElement Password = driver.FindElement(By.XPath(".//input[@name='password']"));
                IWebElement LoginButton = driver.FindElement(By.XPath(".//button[@name='login']"));
                Login.SendKeys("admin");
                Password.SendKeys("admin");
                LoginButton.Click();
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
