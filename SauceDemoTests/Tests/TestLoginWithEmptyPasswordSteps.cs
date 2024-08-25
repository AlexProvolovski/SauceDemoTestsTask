using SauceDemoTests.Pages;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;

namespace SauceDemoTests.Tests
{
    [Binding]
    [Scope(Tag = "EmptyPassword")]
    public class TestLoginWithEmptyPasswordSteps
    {
        private readonly LoginPage _loginPage;
        public TestLoginWithEmptyPasswordSteps(ScenarioContext scenarioContext)
        {
            var driver = scenarioContext.Get<IWebDriver>("WebDriver");
            _loginPage = new LoginPage(driver);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _loginPage.NavigateToLoginPage();
        }

        [When(@"I enter a valid username")]
        public void WhenIEnterAValidUsername()
        {
            _loginPage.EnterUsername("someusername");
        }

        [When(@"I clear the password field")]
        public void WhenIClearThePasswordField()
        {
            _loginPage.ClearPassword();
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLoginButton();
        }

        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIShouldSeeTheErrorMessage(string expectedErrorMessage)
        {
            Assert.That(_loginPage.GetErrorMessage(), Is.EqualTo(expectedErrorMessage));
        }
    }
}