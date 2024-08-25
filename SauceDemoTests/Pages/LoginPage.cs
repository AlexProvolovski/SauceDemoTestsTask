using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SauceDemoTests.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "user-name")]
        public IWebElement UsernameField { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement PasswordField { get; set; }

        [FindsBy(How = How.Id, Using = "login-button")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "h3[data-test='error']")]
        public IWebElement ErrorMessage { get; set; }

        public void NavigateToLoginPage()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        private WebDriverWait GetWait()
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void EnterUsername(string username)
        {
            var wait = GetWait();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("user-name")));
            UsernameField.Clear();
            UsernameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var wait = GetWait();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }

        public void ClearUsername()
        {
            var wait = GetWait();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("user-name")));
            UsernameField.Clear();
        }

        public void ClearPassword()
        {
            var wait = GetWait();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            PasswordField.Clear();
        }

        public void ClickLoginButton()
        {
            var wait = GetWait();
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton)).Click();
        }

        public string GetErrorMessage()
        {
            var wait = GetWait();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h3[data-test='error']")));
            return ErrorMessage.Text;
        }
    }
}