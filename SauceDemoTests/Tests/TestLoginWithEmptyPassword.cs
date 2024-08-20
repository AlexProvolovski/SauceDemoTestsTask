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
    public class TestLoginWithEmptyPassword : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]
        public void UC2_TestLoginWithEmptyPassword(String browserName)
        {
            SetUp(browserName);
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("someusername");
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();

            string errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Password is required"));
        }
    }
}
