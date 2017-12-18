using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SeleniumTestProject
{
    [TestClass]
    public class Ex7
    {
        public static ChromeDriver driver;
        private string m_baseURL = "http://localhost:8080/litecart/admin";
        private const string m_expectedTitle = "My Store";
        private const int timeout = 10;
        private WebDriverWait wait;
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";

        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Navigate().GoToUrl(m_baseURL);
            driver.Manage().Window.Maximize();
            wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
        }

        public void ILoginAsAdminaAndIWaitAdminPanelLoad()
        {
            try
            {
                IWebElement login = driver.FindElement(By.XPath(".//input[@name='username']"));
                IWebElement password = driver.FindElement(By.XPath(".//input[@name='password']"));
                IWebElement loginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

                login.SendKeys(ADMIN_LOGIN);
                password.SendKeys(ADMIN_PASSWORD);
                loginButton.Click();
                //we wait that the sitebar has been loaded
                wait.Until(ExpectedConditions.ElementExists(By.XPath(".//div[@id='sidebar']")));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message + "\n" + e.InnerException.Message);
            }
        }

        [TestMethod]
        public void CheckAllMenuTabs()
        {
            if (driver.Url.Contains("login"))
            {
                ILoginAsAdminaAndIWaitAdminPanelLoad();
            }
            try
            {
                ReadOnlyCollection<IWebElement> listFirstLevelElements = driver.FindElements(By.XPath(".//li[contains(@id,'app-')]/a"));
                ReadOnlyCollection<IWebElement> listSecondLevelElements;

                for (int i = 0; i < listFirstLevelElements.Count; i++)
                {
                    listFirstLevelElements = driver.FindElements(By.XPath(".//li[contains(@id,'app-')]/a"));
                    listFirstLevelElements[i].Click();

                    wait.Until(ExpectedConditions.ElementExists(By.XPath(".//h1")));
                    listSecondLevelElements = driver.FindElements(By.XPath(".//li[contains(@id,'doc-')]/a"));
                    for (int j = 0; j < listSecondLevelElements.Count; j++)
                    {
                        listSecondLevelElements = driver.FindElements(By.XPath(".//li[contains(@id,'doc-')]/a"));
                        listSecondLevelElements[j].Click();
                        wait.Until(ExpectedConditions.ElementExists(By.XPath(".//h1")));
                    }
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message + "\n" + e.InnerException.Message);
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
