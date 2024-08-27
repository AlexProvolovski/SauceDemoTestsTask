using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemoTests.Utils
{
    public class BrowserManager
    {
        public IWebDriver InitializeBrowser(string browserName)
        {
            IWebDriver driver;

            switch (browserName.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized");
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("start-maximized");
                    driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException($"Browser '{browserName}' is not supported.");
            }

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            return driver;
        }

        public void CleanupBrowser(IWebDriver driver)
        {
            driver?.Quit();
        }
    }
}