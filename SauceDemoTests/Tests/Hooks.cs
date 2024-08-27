using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Tests
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var browserName = _scenarioContext.ScenarioInfo.Tags.Contains("edge") ? "edge" : "chrome";
            var testName = _scenarioContext.ScenarioInfo.Title;

            var driver = new BrowserManager().InitializeBrowser(browserName);
            _scenarioContext.Set(driver, "WebDriver");

            var testHelper = new TestHelper(driver, testName);
            _scenarioContext.Set(testHelper, "TestHelper");

            testHelper.LogTestStart(testName, browserName);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.ContainsKey("WebDriver"))
            {
                var driver = _scenarioContext.Get<IWebDriver>("WebDriver");
                var testHelper = _scenarioContext.Get<TestHelper>("TestHelper");

                var testName = _scenarioContext.ScenarioInfo.Title;

                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                    string screenshotDirectory = Path.Combine(projectDirectory, "Screenshots");
                    testHelper.CaptureScreenshot(screenshotDirectory);

                    string errorMessage = TestContext.CurrentContext.Result.Message;
                    testHelper.LogError($"Test '{testName}' failed with error: {errorMessage}");
                }

                string status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
                testHelper.LogTestEnd(testName, status);

                new BrowserManager().CleanupBrowser(driver);
            }
        }
    }
}