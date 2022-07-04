using HBSpecFlow.Drivers;
using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using AventStack.ExtentReports;
using OpenQA.Selenium.Remote;
using AventStack.ExtentReports.Gherkin.Model;

[assembly:Parallelizable(ParallelScope.Fixtures)]


namespace HBSpecFlow.Hooks
{
    [Binding]
    public sealed class HookInitialization
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private readonly ScenarioContext _scenarioContext;
        private ExtentTest _currentScenarioName;
        public HookInitialization(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;
        
        public RemoteWebDriver Driver { get; set; }
        public MediaEntityModelProvider CaptureScreenshot(string Name)
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, Name).Build();
        }

        [AfterStep]
        public void AfterEachStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError != null)
            {
                var mediaEntity = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title.Trim());
                if (stepType == "Given")
                    _currentScenarioName.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "When")
                    _currentScenarioName.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "Then")
                    _currentScenarioName.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
            }
            

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            SeleniumDriver seleniumDriver = new SeleniumDriver(_scenarioContext);
            _scenarioContext.Set(seleniumDriver, "SeleniumDriver");
        }


        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Selenium webdriver quit");
            _scenarioContext.Get<IWebDriver>("WebDriver").Quit();
        }
    }
}