using SauceDemoTests.Pages;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;

namespace SauceDemoTests.Tests
{
    [Binding]
    [Scope(Tag = "SuccessfulLogin")]
    [TestFixture]
    [Parallelizable]
    public class TestLoginWithValidCredentialsSteps
    {
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;

        public TestLoginWithValidCredentialsSteps(ScenarioContext scenarioContext)
        {
            var driver = scenarioContext.Get<IWebDriver>("WebDriver");
            _loginPage = new LoginPage(driver);
            _dashboardPage = new DashboardPage(driver);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _loginPage.NavigateToLoginPage();
        }

        [When(@"I enter valid username and password")]
        public void WhenIEnterValidUsernameAndPassword()
        {
            _loginPage.EnterUsername("standard_user");
            _loginPage.EnterPassword("secret_sauce");
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLoginButton();
        }

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            Assert.That(_dashboardPage.GetPageTitle(), Is.EqualTo("Swag Labs"));
        }
    }
}