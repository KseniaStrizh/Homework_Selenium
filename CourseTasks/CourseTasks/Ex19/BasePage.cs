using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CourseTasks.Ex19
{
    public class BasePage
    {
        protected string siteUrl;
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            siteUrl = $"http://localhost/litecart/public_html/en";
        }
    }
}
