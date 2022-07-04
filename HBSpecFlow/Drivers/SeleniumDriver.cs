using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBSpecFlow.Drivers
{
    public class SeleniumDriver
    {
        private IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public SeleniumDriver(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

        public IWebDriver Setup(string browserName)
        {
            dynamic capability = GetBrowserOptions(browserName);

            // Change the string if NOT gonna execute on local
            driver = new RemoteWebDriver(new Uri("http://192.168.56.1:4444/"), capability.ToCapabilities());

            //driver = new RemoteWebDriver(new Uri("http://192.168.56.1:4444/"), chromeOptions.ToCapabilities());

            _scenarioContext.Set(driver, "WebDriver");

            driver.Manage().Window.Maximize();

            return driver;
        }

        private dynamic GetBrowserOptions(string browserName)
        {
            if (browserName.ToLower() == "chrome")
                return new ChromeOptions();
            if (browserName.ToLower() == "firefox")
                return new FirefoxOptions();
            if (browserName == "MicrosoftEdge")
                return new EdgeOptions();
            if (browserName == "Safari")
                return new SafariOptions();

            return new ChromeOptions();
        }

    }
}
