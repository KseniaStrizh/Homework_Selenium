using OpenQA.Selenium;

namespace CourseTasks.Ex19
{
    public class LiteCartApp<TDriver> where TDriver : IWebDriver, new()
    {
        private MainPage storePage;
        private ProductPage productPage;
        private CartPage cartPage;

        private IWebDriver driver;

        public LiteCartApp()
        {
            driver = new TDriver();
            driver.Manage().Window.Maximize();
            storePage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }

        public void OpenCart() => storePage.OpenCartPage();

        public void ClearCart() => cartPage.RemoveAllItems();

        public void Quit()
        {
            driver.Quit();
        }
    }
}