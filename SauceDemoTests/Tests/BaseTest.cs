using log4net;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using NUnit.Framework.Interfaces;

namespace SauceDemoTests.Tests
{
    public abstract class BaseTest
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseTest));
        protected IWebDriver driver;

        public void SetUp(String browserName)
        {
            log4net.Config.XmlConfigurator.Configure();
            logger.Info("Test started.");

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
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    var screenshotPath = Path.Combine("Screenshots", $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
                    screenshot.SaveAsFile(screenshotPath);
                    logger.Error($"Test failed. Screenshot saved to {screenshotPath}.");
                }
                else
                {
                    logger.Info("Test passed.");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error during TearDown", ex);
            }
            finally
            {
                driver?.Quit();
                logger.Info("Browser closed.");
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