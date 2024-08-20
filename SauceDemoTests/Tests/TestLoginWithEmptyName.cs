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
    public class TestLoginWithEmptyName : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]
        public void UC1_TestLoginWithEmptyName(String browserName)
        {
            SetUp(browserName);
            var loginPage = new LoginPage(driver);
            loginPage.ClearUsername();
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();

            string errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Username is required"));
        }
    }
}
