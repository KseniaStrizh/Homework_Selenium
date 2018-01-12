using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CourseTasks.Ex19
{
    public class ProductDetailsPage : BasePage
    {
        public ProductDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        public void SelectSize(string productSize)
        {
            if (IsElementPresented(".//select[@name='options[Size]']"))
            {
                var selectElement = new SelectElement(driver.FindElement(By.XPath(".//select[@name='options[Size]']")));
                selectElement.SelectByText(productSize);
            }
        }

        public void AddToCart()
        {
            var counter = Convert.ToInt16(driver.FindElement(By.XPath(".//span[@class='quantity']")).Text);
            driver.FindElement(By.XPath(".//button[@name='add_cart_product']")).Click();
            WaitTextPresented(driver.FindElement(By.XPath(".//span[@class='quantity']")), (counter + 1).ToString());
        }
    }
}