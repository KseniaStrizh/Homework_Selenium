using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;


namespace CourseTasks.Ex19
{
   public class MainPage: BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "#box-most-popular .product:first-child")]
        private IWebElement firstPopularProduct;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        public MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public BasePage OpenStorePage()
        {
            driver.Navigate().GoToUrl(siteUrl);

            return this;
        }

        public CartPage OpenCartPage()
        {
            checkoutButton.Click();

            return new CartPage(driver);
        }
    }
}