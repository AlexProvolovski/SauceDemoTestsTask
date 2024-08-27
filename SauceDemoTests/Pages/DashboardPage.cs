using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoTests.Pages
{
    public class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver driver) : base(driver) { }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public void VerifyDashboardPageTitle(string expectedTitle)
        {
            string actualTitle = GetPageTitle();
            if (actualTitle != expectedTitle)
            {
                throw new AssertionException($"Expected page title to be '{expectedTitle}', but was '{actualTitle}'.");
            }
        }
    }
}