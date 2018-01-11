using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;


namespace CourseTasks.Ex19
{
        public class CartPage : BasePage
        {
            [FindsBy(How = How.CssSelector, Using = "ul.items")]
            private IWebElement productsSlider;

            [FindsBy(How = How.CssSelector, Using = "ul.items li.item:first-child button[name='remove_cart_item']")]
            private IWebElement removeButton;

            public CartPage(IWebDriver driver) : base(driver)
            {
                PageFactory.InitElements(driver, this);
            }

            public CartPage RemoveAllItems()
            {
               for (int i = 0; i < 3; i++)
                {
                    var orderSummary = driver.FindElement(By.CssSelector("#order_confirmation-wrapper"));
                    removeButton.Click();
                    wait.Until(ExpectedConditions.StalenessOf(orderSummary));
                }

                return this;
            }  
        }
    }