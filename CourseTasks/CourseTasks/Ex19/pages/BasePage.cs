using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CourseTasks.Ex19
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;


        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        protected bool IsElementPresented(string xPath)
        {
            System.Threading.Thread.Sleep(1000);
            return driver.FindElements(By.XPath(xPath)).Count > 0;
        }
        protected void WaitTextPresented(IWebElement element, string text)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
        }
    }
}