using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string homeUrl = "http://localhost:44108/";
        private const string aboutUrl = "http://localhost:44108/Home/About";
        private const string homeTitle = "Home Page - Credit Cards";
        private const string aboutTitle = "About - Credit Cards";


        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
              

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);

            }
        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePage() 
        {
            using (IWebDriver driver = new ChromeDriver())
            { 
               
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);

            }

        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(aboutUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();
                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);

                string reloadToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initialToken, reloadToken);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePageOnForwardV2()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(aboutUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();
                driver.Navigate().Forward();
                Assert.Equal(aboutTitle, driver.Title);
                Assert.Equal(aboutUrl, driver.Url);


            }
        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePageonForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(aboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(homeUrl);
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                string reloadToken = driver.FindElement(By.Id("GenerationToken")).Text;
                DemoHelper.Pause();

                Assert.NotEqual(initialToken, reloadToken);
                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);

                //ToDo: assert that page was reloaded
            }
        }

        [Fact]

        public void DisplayProdutcsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement firstTableCell = driver.FindElement(By.TagName("td"));
                string firstProduct = firstTableCell.Text;

                Assert.Equal("Easy Credit Card", firstProduct);

                //ToDo: check rest of the product table
            }
        }
    }
}
