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
    public class Ex11
    {
        private const string URL = "http://localhost/litecart/en/create_account";
        private const string m_expectedTitle = "Create Account | My Store";
        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static ChromeDriver driver;
        private WebDriverWait wait;

        #region repoElement
        public string Firstname => $".//input[@name='firstname']";
        public string Lastname => $".//input[@name='lastname']";
        public string Company => $".//input[@name='company']";
        public string TaxId => $".//input[@name='tax_id']";
        public string Address1 => $".//input[@name='address1']";
        public string PostCode => $".//input[@name='postcode']";
        public string City => $".//input[@name='city']";
        public string Email => $".//label[text()='Email']/..//input";
        public string DesiredPassword => $".//label[text()='Desired Password']/..//input";
        public string ConfirmPassword => $".//input[@name='confirmed_password']";

        public string SingInEmail = $".//div[@class='input-group']//input[@name='email']";
        public string SingInPassword = $".//div[@class='input-group']//input[@name='password']";
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
            string uniqueName = "Test" + DateTime.Now.ToBinary() + "@gmail.com";
            return uniqueName;
        }

        [TestMethod]
        public void Ex_11()

        {
            string email = GenerateUnicName();
            driver.FindElement(By.XPath(TaxId)).SendKeys("TaxId");
            driver.FindElement(By.XPath(Company)).SendKeys("Company");
            driver.FindElement(By.XPath(Firstname)).SendKeys("FirstName");
            driver.FindElement(By.XPath(Lastname)).SendKeys("LastName");
            driver.FindElement(By.XPath(Address1)).SendKeys("850 Ave");
            driver.FindElement(By.XPath(PostCode)).SendKeys("60654");

            new SelectElement(driver.FindElement(By.CssSelector("select[name='country_code']"))).SelectByValue("US");
            Thread.Sleep(4000);

            driver.FindElement(By.XPath(Email)).SendKeys(email);
            
            driver.FindElement(By.XPath(DesiredPassword)).SendKeys("Password");
            driver.FindElement(By.XPath(ConfirmPassword)).SendKeys("Password");

            driver.FindElement(By.XPath(".//button[contains(@name,'create_account')]")).Click();
            Thread.Sleep(10000);
            //Logout from the newly created account
            driver.FindElement(By.CssSelector("a[href *= 'logout']")).Click();
            Thread.Sleep(10000);
            //Login again
            driver.FindElement(By.XPath(SingInEmail)).SendKeys(email);
            driver.FindElement(By.XPath(SingInPassword)).SendKeys("Password");
            driver.FindElement(By.CssSelector("button[name='login']")).Click();
            Thread.Sleep(10000);
            //Logout again
            driver.FindElement(By.CssSelector("a[href *= 'logout']")).Click();


        }

        [ClassCleanup]
        public static void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
