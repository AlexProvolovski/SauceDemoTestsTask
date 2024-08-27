using SauceDemoTests.Pages;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Tests
{
    [Binding]
    [Scope(Tag = "EmptyPassword")]
    public class TestLoginWithEmptyPasswordSteps
    {
        private readonly LoginPage _loginPage;
        private readonly TestHelper _testHelper;
        private readonly IWebDriver _driver;

        public TestLoginWithEmptyPasswordSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("WebDriver");
            _loginPage = new LoginPage(_driver);

            var testName = scenarioContext.ScenarioInfo.Title;

            _testHelper = new TestHelper(_driver, testName);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _testHelper.LogTestAction("Navigating to the login page.");
            _loginPage.NavigateToLoginPage();
        }

        [When(@"I enter a valid username")]
        public void WhenIEnterAValidUsername()
        {
            _testHelper.LogTestAction("Entering a valid username.");
            _loginPage.EnterUsername("someusername");
        }

        [When(@"I clear the password field")]
        public void WhenIClearThePasswordField()
        {
            _testHelper.LogTestAction("Clearing the password field.");
            _loginPage.ClearPassword();
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _testHelper.LogTestAction("Clicking the login button.");
            _loginPage.ClickLoginButton();
        }

        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIShouldSeeTheErrorMessage(string expectedErrorMessage)
        {
            _testHelper.LogTestAction($"Verifying the error message: {expectedErrorMessage}.");
            Assert.That(_loginPage.GetErrorMessage(), Is.EqualTo(expectedErrorMessage));
        }
    }
}