using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
        [Scope(Tag = "chrome")]
        public void SetupChrome()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            var driver = new ChromeDriver(options);
            InitializeWebDriver(driver);
        }

        [BeforeScenario]
        [Scope(Tag = "edge")]
        public void SetupEdge()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            var driver = new EdgeDriver(options);
            InitializeWebDriver(driver);
        }

        private void InitializeWebDriver(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _scenarioContext.Set(driver, "WebDriver");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _scenarioContext.Get<IWebDriver>("WebDriver");
            string screenshotPath = string.Empty;

            try
            {
                if (driver != null && TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                    string screenshotDirectory = Path.Combine(projectDirectory, "Screenshots");

                    if (!Directory.Exists(screenshotDirectory))
                    {
                        Directory.CreateDirectory(screenshotDirectory);
                    }

                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    screenshotPath = Path.Combine(screenshotDirectory, $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
                    screenshot.SaveAsFile(screenshotPath);
                }

                LogTestResult(TestContext.CurrentContext.Result.Outcome.Status.ToString(), screenshotPath);
            }
            catch (Exception ex)
            {
                LogTestResult("TearDown Error: " + ex.Message, screenshotPath);
            }
            finally
            {
                driver?.Quit();
            }
        }

        private void LogTestResult(string status, string screenshotPath = "")
        {
            try
            {
                string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                string logDirectory = Path.Combine(projectDirectory, "Logs");
                string logFilePath = Path.Combine(logDirectory, "TestResults.log");

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                if (!File.Exists(logFilePath))
                {
                    using (File.Create(logFilePath)) { }
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: Test {TestContext.CurrentContext.Test.Name} {status}. Screenshot: {screenshotPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LogTestResult Error: {ex.Message}");
            }
        }
    }
}