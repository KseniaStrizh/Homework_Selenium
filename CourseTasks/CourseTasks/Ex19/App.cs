using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CourseTasks.Ex19
{
    public class Application
    {
        private IWebDriver driver;

        private CheckoutPage checkOutPage;
        private ProductListPage productListingPage;
        private ProductDetailsPage productDetailsPage;

        private string siteUrl = "http://localhost/litecart/public_html/";

        public Application()
        {
            driver = new ChromeDriver();
            checkOutPage = new CheckoutPage(driver);
            productListingPage = new ProductListPage(driver);
            productDetailsPage = new ProductDetailsPage(driver);
        }
        #region siteOperations

        public void Quit()
        {
            driver.Quit();
        }

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(siteUrl);
        }
        #endregion

        public void BuyProduct(Product product)
        {
            productListingPage.OpenProduct(product.Name);
            productDetailsPage.SelectSize(product.Size);
            productDetailsPage.AddToCart();
        }

        public void BuyProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                this.OpenSite();
                this.BuyProduct(product);
            }
        }

        public void DeleteAllProducts()
        {
            productListingPage.OpenCart();
            checkOutPage.DeleteAllItems();
        }
    }

}