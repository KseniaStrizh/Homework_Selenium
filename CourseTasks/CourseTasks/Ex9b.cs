using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Threading;

namespace CourseTasks
{

    [TestClass]
    public class Ex9b
    {
        public static ChromeDriver driver;
        private const string m_URLb = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

        private const string m_expectedTitle = "Geo Zones | My Store";
        private const int timeout = 10;
        private WebDriverWait wait;
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";

        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Navigate().GoToUrl(m_URLb);
            driver.Manage().Window.Maximize();
        }

        public void ILoginAsAdmin()
        {
            IWebElement login = driver.FindElement(By.XPath(".//input[@name='username']"));
            IWebElement password = driver.FindElement(By.XPath(".//input[@name='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

            login.SendKeys(ADMIN_LOGIN);
            password.SendKeys(ADMIN_PASSWORD);
            loginButton.Click();
            wait.Until(ExpectedConditions.TitleIs(m_expectedTitle));
            Thread.Sleep(timeout);
        }


        [TestMethod]
        public void Ex9_partb()
        {

            if (driver.Url.Contains("login"))
            {
                ILoginAsAdmin();
            }
            try
            {
                var zones = driver.FindElements(By.XPath("//tbody//a[not (contains(@title,'Edit'))]"));
                List<string> zonesList = new List<string>();
                List<string> listSubZones = new List<string>();

                for (var i = 0; i < zones.Count; i++)
                {
                    zonesList.Add(zones[i].Text);
                    zones[i].Click();
                    Thread.Sleep(TimeSpan.FromSeconds(10));

                    var subzones = driver.FindElements(By.XPath(".//tbody/tr/td[last()-1]"));

                    for (var j = 0; j < subzones.Count; j++)
                    {
                      listSubZones.Add(subzones[j].Text);
                    }
                    
                    var inputList = listSubZones;
                    listSubZones.Sort();
                    Assert.AreEqual(listSubZones, inputList);

                    driver.FindElement(By.XPath(".//button[contains(@name,'cancel')]")).Click();
                    Thread.Sleep(TimeSpan.FromSeconds(10));

                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message + "\n" + e.InnerException.Message);
            }
        }
    }
}