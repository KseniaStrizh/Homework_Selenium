﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

//Go through all the elements of the admin area
namespace Lesson4_Ex7
{
    [TestClass]
    public class FindItemsCheckHeaders
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
        }
        public void ILoginAsAdminaAndIWaitAdminPanelLoad()
        {
           try
            {
                driver.Navigate().GoToUrl(m_baseURL);
                wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
               
                IWebElement Login = driver.FindElement(By.XPath(".//input[@name='username']"));
                IWebElement Password = driver.FindElement(By.XPath(".//input[@name='password']"));
                IWebElement LoginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

                Login.SendKeys(ADMIN_LOGIN);
                Password.SendKeys(ADMIN_PASSWORD);
                LoginButton.Click();
                //we wait that the administrator menu has been loaded
                wait.Until(ExpectedConditions.ElementExist(By.XPath(".//div[@id='sidebar']")));
                
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
                var listFirstLevelElements = new List<string>();
                var listSecondLevelElements = new List<string>();

                var allFirstLevelElements = driver.FindElements(By.XPath("ul//contains[@id='menu']/li"));
                foreach (var item in allFirstLevelElements)
                {
                    listFirstLevelElements.Add(item.FindElement(By.XPath(".//a")).GetAttribute("href"));// or .value weshould get link
                }
                
                //now we shoul added second level elements
                foreach (var secondLeveItem in listFirstLevelElements)
                {
                    driver.Navigate().GoToUrl(secondLeveItem);
                    var element = driver.FindElements(By.XPath(".//h1"));
                    Assert.IsTrue(element.Count > 0);

                    var secondLevelElement = driver.FindElements(By.XPath(".//li[@id='app-']/ul//a"));
                    if (secondLevelElement.Count > 0)
                    {
                        foreach (var item in secondLevelElement)
                        {
                            listSecondLevelElements.Add(item.GetAttribute("href"));
                        }
                    }
                }

                foreach (var secondLevelElement in listSecondLevelElements)
                {
                    driver.Navigate().GoToUrl(secondLevelElement);
                    var element = driver.FindElements(By.XPath(".//h1"));
                    Assert.IsTrue(element.Count > 0);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message + "\n" + e.InnerException.Message);
            }
        }

        [ClassCleanup]
        public void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}