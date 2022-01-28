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
    public class JavaScriptExamples
    {
        [Fact]

        public void ClickOverlayedLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("Http://localhost:44108/jsoverlay.html");
             

                DemoHelper.Pause();

                string script = "document.getElementById('HiddenLink').click();";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);

                //driver.FindElement(By.Id("HiddenLink")).Click();
                DemoHelper.Pause();

                Assert.Equal("https://www.pluralsight.com/", driver.Url);


            }
        }
    }
}
