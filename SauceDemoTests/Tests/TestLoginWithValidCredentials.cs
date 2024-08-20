using NUnit.Framework;
using SauceDemoTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoTests.Tests
{
    [TestFixture]
    [Parallelizable]
    public class TestLoginWithValidCredentials : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]
        public void UC3_TestLoginWithValidCredentials(String browserName)
        {
            SetUp(browserName);
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClickLoginButton();

            var dashboardPage = new DashboardPage(driver);
            string title = dashboardPage.GetPageTitle();
            Assert.That(title, Is.EqualTo("Swag Labs"));
        }
    }
}
