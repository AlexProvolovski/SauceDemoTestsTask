using SauceDemoTests.Pages;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Tests
{
    [Binding]
    [Scope(Tag = "SuccessfulLogin")]
    public class TestLoginWithValidCredentialsSteps
    {
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;
        private readonly TestHelper _testHelper;
        private readonly IWebDriver _driver;

        public TestLoginWithValidCredentialsSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("WebDriver");
            _loginPage = new LoginPage(_driver);
            _dashboardPage = new DashboardPage(_driver);

            var testName = scenarioContext.ScenarioInfo.Title;

            _testHelper = new TestHelper(_driver, testName);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _testHelper.LogTestAction("Navigating to the login page.");
            _loginPage.NavigateToLoginPage();
        }

        [When(@"I enter valid username and password")]
        public void WhenIEnterValidUsernameAndPassword()
        {
            _testHelper.LogTestAction("Entering valid username and password.");
            _loginPage.EnterUsername("standard_user");
            _loginPage.EnterPassword("secret_sauce");
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _testHelper.LogTestAction("Clicking the login button.");
            _loginPage.ClickLoginButton();
        }

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            _testHelper.LogTestAction("Verifying redirection to the dashboard.");
            _dashboardPage.VerifyDashboardPageTitle("Swag Labs");
        }
    }
}