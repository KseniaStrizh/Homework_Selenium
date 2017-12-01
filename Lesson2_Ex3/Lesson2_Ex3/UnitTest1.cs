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
            public static ChromeDriver m_driver;
            private string m_baseURL = "http://localhost:8080/litecart/admin";
            [TestInitialize]
            public void TestSetup()
            {
                m_driver = new ChromeDriver();
            }

            [TestMethod]
            public void GoToURL()
            {
                m_driver.Navigate().GoToUrl(m_baseURL);
                IWebElement Login = m_driver.FindElement(By.XPath(".//input[@name='username']"));
                IWebElement Password = m_driver.FindElement(By.XPath(".//input[@name='password']"));
                IWebElement LoginButton = m_driver.FindElement(By.XPath(".//button[@name='login']"));
                Login.SendKeys("admin");
                Password.SendKeys("admin");
                LoginButton.Click();
            }

            [TestCleanup]
            public void QuitBrowser()
            {
                m_driver.Quit();
            }

        }
    }
}
