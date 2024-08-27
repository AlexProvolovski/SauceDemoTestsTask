using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository;
using OpenQA.Selenium;

namespace SauceDemoTests.Utils
{
    public class TestHelper
    {
        private readonly ILog _log;
        private readonly IWebDriver _driver;

        public TestHelper(IWebDriver driver, string testName)
        {
            _driver = driver;
            _log = ConfigureLog4Net(testName);
        }

        public void LogTestStart(string testName, string browserName)
        {
            _log.Info($"Test '{testName}' started on browser '{browserName}' at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        public void LogTestEnd(string testName, string status)
        {
            _log.Info($"Test '{testName}' finished with status '{status}' at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        public void LogTestAction(string actionDescription)
        {
            _log.Info($"Action: {actionDescription} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        public void LogError(string errorMessage)
        {
            _log.Error($"Error: {errorMessage} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        public void CaptureScreenshot(string screenshotDirectory)
        {
            try
            {
                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                string screenshotPath = Path.Combine(screenshotDirectory, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotPath);

                _log.Info($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception ex)
            {
                LogError($"Failed to capture screenshot: {ex.Message}");
            }
        }

        private static ILog ConfigureLog4Net(string testName)
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string logDirectory = Path.Combine(projectDirectory, "Logs");
            string logFilePath = Path.Combine(logDirectory, $"{testName}_TestResults.log");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var fileAppender = new RollingFileAppender
            {
                File = logFilePath,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1MB",
                StaticLogFileName = true,
                Layout = new PatternLayout("%date [%thread] %-5level %logger - %message%newline"),
                LockingModel = new FileAppender.MinimalLock(),
                ImmediateFlush = true
            };
            fileAppender.ActivateOptions();

            ILoggerRepository repository = LogManager.CreateRepository(Guid.NewGuid().ToString());
            BasicConfigurator.Configure(repository, fileAppender);

            return LogManager.GetLogger(repository.Name, testName);
        }
    }
}