using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SeleniumTestProject
{
	[TestClass]
	public class Ex9
	{
		public static ChromeDriver driver;
		private const string m_URLa = "http://localhost/litecart/public_html/admin/?app=countries&doc=countries";//"http://localhost/litecart/admin/?app=countries&doc=countries";
		private const string m_URLb = "http://localhost/litecart/public_html/admin/?app=geo_zones&doc=geo_zones";//"http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

		private const string m_expectedTitle = "Countries | My Store";
		private const int timeout = 10;
		private WebDriverWait wait;
		private const string ADMIN_LOGIN = "admin";
		private const string ADMIN_PASSWORD = "admin";

		[TestInitialize]
		public void TestSetup()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
			driver.Navigate().GoToUrl(m_URLa);
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
		}


		[TestMethod]
		public void Ex9a()
		{

			if (driver.Url.Contains("login"))
			{
				ILoginAsAdmin();
			}
			try
			{
				var AllCountry = driver.FindElements(By.XPath(".//td/a[not (contains(@title,'Edit'))]"));
				List<string> AllCountryText = new List<string>();
				List<string> AllCountrySortedList = new List<string>();

				var AllZones = driver.FindElements(By.XPath(".//td[last()-1]"));
				List<string> NotZeroZones = new List<string>();


				for (int i = 0; i < AllCountry.Count; i++)
				{
					AllCountryText.Add(AllCountry[i].Text);
					if (AllZones[i].Text!="0")
					{
						NotZeroZones.Add(AllCountry[i].Text);
					}
				}
				AllCountrySortedList = AllCountryText;
				AllCountrySortedList.Sort();
				Assert.AreEqual(AllCountrySortedList, AllCountryText);
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
