using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework.Interfaces;

namespace SauceDemoTests.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver driver;

        public void SetUp(String browserName)
        {

            if (browserName == "Chrome")
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
            }
            else if (browserName == "Edge")
            {
                new DriverManager().SetUpDriver(new EdgeConfig());
                driver = new EdgeDriver();
            }

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        [TearDown]
        public void TearDown()
        {
            string screenshotPath = string.Empty;

            try
            {
                if (driver != null)
                {
                    if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
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
        public static IEnumerable<String> BrowserToRunWith()
        {
            String[] browsers = { "Chrome", "Edge" };

            foreach (String b in browsers)
            {
                yield return b;
            }
        }
        
    }
}