using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Threading;
using OpenQA.Selenium.Interactions;

namespace CourseTasks
{
    [TestClass]
    public class Ex12
    {

        private const string URL = "http://localhost/litecart/public_html/admin/?app=catalog&doc=catalog";
       
        private const int timeout = 10;
        private const string BROWSERNAME = "Chrome";
        public static ChromeDriver driver;
        private WebDriverWait wait;
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";

        #region repoElement
        //status tab
        public string AddNewProduct = $".//a[contains(@href,'edit_product')]";
        public string Name = $".//label[text()='Name']/..//input";
        public string Code = $".//label[text()='Code']/..//input";
        public string StatusEnable = $".//input[@name='status' and @value = '1']";
        public string Quantity = $".//label[text()='Quantity']/..//div/input[@type='number']";
        public string DateValidFrom = $".//label[text()='Date Valid From']/..//input";
        public string DateValidTo = $".//label[text()='Date Valid To']/..//input";
        public string NewImages = $".//input[contains(@name,'new_images')]";
        //Information tab
        public string InformationBtn = $".//a[text()='Information']";
        public string Keywords = $".//input[@name='keywords']";
        public string ShortDescription = $".//input[@name='short_description[en]']";
        public string Description = $".//textarea[contains(@name,'description')]";
        public string Attributes = $".//textarea[contains(@name,'attributes')]";
        //Price Tab
        public string PriceBtn = $".//a[text()='Prices']";



        #endregion

        public static string ProductNameGenerator()
        {
            return "Name" + new Random().Next(1000, 9999);
        }
        public static string ProductCodeGenerator()
        {
            return "code" + new Random().Next(1000, 9999);
        }

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
        public void Ex_12()
        {

            IWebElement login = driver.FindElement(By.XPath(".//input[@name='username']"));
            IWebElement password = driver.FindElement(By.XPath(".//input[@name='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

            login.SendKeys(ADMIN_LOGIN);
            password.SendKeys(ADMIN_PASSWORD);
            loginButton.Click();
            Thread.Sleep(10000);

            int count = driver.FindElements(By.CssSelector(".dataTable .row")).Count;
            driver.FindElement(By.XPath(AddNewProduct)).Click();

            Thread.Sleep(10000);
            string NameValue = ProductNameGenerator();
            driver.FindElement(By.XPath(Name)).SendKeys(NameValue);

            string CodeValue = ProductCodeGenerator();
            driver.FindElement(By.XPath(Code)).SendKeys(CodeValue);

            driver.FindElement(By.XPath(Quantity)).SendKeys("10");
            driver.FindElement(By.XPath(DateValidFrom)).SendKeys("01.01.2018");
            driver.FindElement(By.XPath(DateValidTo)).SendKeys("31.12.2018");
            var path = Directory.GetCurrentDirectory() + @"\input\gift.jpg";
            driver.FindElement(By.XPath(NewImages)).SendKeys(path);

            //Information Tab
            driver.FindElement(By.XPath(InformationBtn)).Click();
            Thread.Sleep(10000);
            var select = new SelectElement(driver.FindElement(By.Name("manufacturer_id")));
            select.SelectByIndex(1);
            driver.FindElement(By.XPath(Keywords)).SendKeys("Keywords");
            driver.FindElement(By.XPath(ShortDescription)).SendKeys("Short Description");
            driver.FindElement(By.XPath(Description)).SendKeys("Description");
            driver.FindElement(By.XPath(Attributes)).SendKeys("Attributes");

            //Prices Tab
            driver.FindElement(By.XPath(PriceBtn)).Click();
            Thread.Sleep(10000);

            var price = driver.FindElement(By.Name("purchase_price"));
            price.Clear();
            price.SendKeys("100");

            var currency = new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code")));
            currency.SelectByIndex(2);

            driver.FindElement(By.Name("save")).Click();
            var updatedTableRows = driver.FindElements(By.ClassName("row")).Count;
            NUnit.Framework.Assert.True((count + 1) == updatedTableRows);


        }
        [ClassCleanup]
        public static void QuitBrowser()
        {
            driver.Quit();
            driver = null;
        }

    }
}
