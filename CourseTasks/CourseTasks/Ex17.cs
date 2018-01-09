using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Threading;
using System.Collections.ObjectModel;

namespace CourseTasks
{
    [TestClass]
    public class Ex17
    {

        private const string URL = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";

        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static ChromeDriver driver;
        private WebDriverWait wait;
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";

        public string RubberDucks = $".//a[text()='Rubber Ducks']";
        public string AllProducts = $".//table[contains(@class,'data-table')]//td[last()-2]/a[contains(@href,'product')]";
        public string CancelBtn = $".//button[@value='Cancel']";
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
        public void Ex_17()
        {

            IWebElement login = driver.FindElement(By.XPath(".//input[@name='username']"));
            IWebElement password = driver.FindElement(By.XPath(".//input[@name='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

            login.SendKeys(ADMIN_LOGIN);
            password.SendKeys(ADMIN_PASSWORD);
            loginButton.Click();
            Thread.Sleep(10000);
            
            ReadOnlyCollection<IWebElement> allProducts;
            int j = 0;
            do
            {
                allProducts = driver.FindElements(By.XPath(AllProducts));
                allProducts[j].Click();
               
                foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                {
                    string message = l.Message;
                    throw new Exception(string.Format("The message was in Log: {0}", message));
                }
                j++;
                driver.FindElement(By.XPath(CancelBtn)).Click();
            }
            while (j < allProducts.Count);
        }

        [ClassCleanup]
        public static void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
