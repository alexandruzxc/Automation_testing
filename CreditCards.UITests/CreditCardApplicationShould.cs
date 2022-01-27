using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        private const string homeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string AboutUrl = "http://localhost:44108/Home/About";

        private readonly ITestOutputHelper output; 

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]

        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }

        [Fact]

        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement applyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();


                //IWebElement appllyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                //appllyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
        [Fact]

        public void BeInitietedFromHomePage_EasyApplication_Prebuild_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));
                IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }


        [Fact]

        public void BeInitialtedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Settings implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to '{homeUrl}'");
                driver.Navigate().GoToUrl(homeUrl);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Findind Element");
                IWebElement applyLink = driver.FindElement(By.ClassName("customer-service-apply-now"));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed ={applyLink} Enabled={applyLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking Element");
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }

        }

        [Fact]      

        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]

        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
    }
}
