using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using System.IO;
using ApprovalTests;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string homeUrl = "http://localhost:44108/";
        private const string aboutUrl = "http://localhost:44108/Home/About";
        private const string homeTitle = "Home Page - Credit Cards";
        private const string aboutTitle = "About - Credit Cards";
        private const string contactUrl = "http://localhost:44108/Home/Contact";
        private const string contactTitle = "Contact - Credit Cards";


        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadHomePage()
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

                driver.Manage().Window.Maximize();
                DemoHelper.Pause();

                driver.Manage().Window.Maximize();
                DemoHelper.Pause();

                driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                DemoHelper.Pause();

                driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                DemoHelper.Pause();

                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                DemoHelper.Pause();

                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                DemoHelper.Pause();

                driver.Manage().Window.FullScreen();
                DemoHelper.Pause(5000);


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

                ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);
                
                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);

                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);

                //ToDo: check rest of the product table
            }
        }
        [Fact]
        private void NavigateToContactPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(contactUrl);
                DemoHelper.Pause();

                Assert.Equal(contactUrl, driver.Url);
                Assert.Equal(contactTitle, driver.Title);

            }
        }

        [Fact]

        public void NotDisplayeCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);
                
                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));

                driver.Navigate().Refresh();

                ReadOnlyCollection<IWebElement> message = driver.FindElements(By.Id("CookiesBeingUsed"));

                Assert.Empty(message);

                Cookie cookieValue = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                Assert.Equal("true", cookieValue.Value);

                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();
                Assert.NotNull(driver.FindElement(By.Id("CookiesBeingUsed")));

            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompare4Reporter))]

        public void RenderAboutPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(aboutUrl);

                ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;

                Screenshot screenshot = screenShotDriver.GetScreenshot();

                screenshot.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("aboutpage.bmp");

                Approvals.Verify(file); 

            }
        }



    }
}
