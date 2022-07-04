using HBSpecFlow.Drivers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow.Assist;
using AventStack.ExtentReports;

namespace HBSpecFlow.StepDefinitions
{
    [Binding]
    public sealed class AddToCartStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        string phoneName = "";
        public AddToCartStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //private readonly IWebElement btnLoginUser = driver.FindElement(By.Id("btnLogin"));


        [Given(@"I have navigated to the application on Browser")]
        public void GivenIHaveNavigatedToTheApplicationOnBrowser(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            driver = _scenarioContext.Get<SeleniumDriver>("SeleniumDriver").Setup((string)data.Browser);

            driver.Url = "https://giris.hepsiburada.com/?ReturnUrl=https%3A%2F%2Foauth.hepsiburada.com%2Fconnect%2Fauthorize%2Fcallback%3Fclient_id%3DSPA%26redirect_uri%3Dhttps%253A%252F%252Fwww.hepsiburada.com%252Fuyelik%252Fcallback%26response_type%3Dcode%26scope%3Dopenid%2520profile%26state%3D66c7eb6054ea48f7ad14ebaa47c922bd%26code_challenge%3Dr-3Uaff70Sd8AED0NATYzi6GcS2vLF6Rrx4ySUp1y5A%26code_challenge_method%3DS256%26response_mode%3Dquery%26ActivePage%3DPURE_LOGIN%26oidcReturnUrl%3Dhttps%253A%252F%252Fwww.hepsiburada.com%252F";
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(5000);

        }

        [Given(@"I see the application opened")]
        public void GivenISeeTheApplicationOpened()
        {
            IWebElement btnLoginUser = driver.FindElement(By.Id("btnLogin"));
        }

        [When(@"I enter UserName")]
        public void WhenIEnterUserName(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            IWebElement txtUserName = driver.FindElement(By.Id("txtUserName"));
            txtUserName.SendKeys(data.UserName);
        }

        [Then(@"I click Namelogin button")]
        public void ThenIClickNameloginButton()
        {
            IWebElement btnLoginUser = driver.FindElement(By.Id("btnLogin"));
            btnLoginUser.Click();
            System.Threading.Thread.Sleep(5000);
        }

        [When(@"I enter Password")]
        public void WhenIEnterPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            IWebElement txtUserPass = driver.FindElement(By.Id("txtPassword"));
            txtUserPass.SendKeys(data.Password);
            System.Threading.Thread.Sleep(5000);
        }

        [Then(@"I click Passwordlogin button")]
        public void ThenIClickPasswordloginButton()
        {
            IWebElement btnLoginPass = driver.FindElement(By.Id("btnEmailSelect"));
            btnLoginPass.Click();
            System.Threading.Thread.Sleep(5000);
        }

        [Then(@"I should see username")]
        public void ThenIShouldSeeUsername()
        {
            IWebElement userAcc = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[3]/div[6]/div/div/div/div/div[2]/div[3]/div[2]/span/a/span[2]"));
            try
            {
                if (userAcc.Text.Contains("Selim"))
                {
                    Console.WriteLine("Login Successful");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw new ElementNotVisibleException("Login Failed!"); 
            }
        }

        [When(@"I search for an Item")]
        public void WhenISearchForAnItem(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            IWebElement searchBar = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[3]/div[6]/div/div/div/div/div[2]/div[2]/div/div/div/div/div/div/div[1]/div[2]/input"));
            searchBar.SendKeys(data.Item);
            IWebElement btnSearch = driver.FindElement(By.XPath("/ html / body / div[1] / div / div / div[3] / div[6] / div / div / div / div / div[2] / div[2] / div / div / div / div / div / div / div[2]"));
            btnSearch.Click();
        }

        [Then(@"I see results")]
        public void ThenISeeResults()
        {
            
            IWebElement priceSearchLow = driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[6]/div[1]/div/div/div/div/div/div/div/div/div/div/div/div/div/div[3]/div/div/div/div[1]/div/div[1]/input"));
            IWebElement priceSearchHigh = driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[6]/div[1]/div/div/div/div/div/div/div/div/div/div/div/div/div/div[3]/div/div/div/div[1]/div/div[2]/input"));
        }

        [Then(@"I sort by option")]
        public void ThenISortByOption()
        {
            IWebElement priceSearchLow = driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[6]/div[1]/div/div/div/div/div/div/div/div/div/div/div/div/div/div[3]/div/div/div/div[1]/div/div[1]/input"));
            IWebElement priceSearchHigh = driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[6]/div[1]/div/div/div/div/div/div/div/div/div/div/div/div/div/div[3]/div/div/div/div[1]/div/div[2]/input"));
            priceSearchLow.SendKeys("3000");
            priceSearchHigh.SendKeys("5000");
        }

        [Then(@"I select the bottom item")]
        public void ThenISelectTheBottomItem()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,1500", "");
            jse.ExecuteScript("window.scrollBy(0,1500", "");
            jse.ExecuteScript("window.scrollBy(0,1500", "");
            jse.ExecuteScript("window.scrollBy(0,1500", "");
            IWebElement elementPhone = driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[6]/div[2]/div[2]/div[4]/div/div/div/div/div/div/div/ul[4]/li[16]/div/a/div[3]"));
            phoneName = elementPhone.Text;
            elementPhone.Click();
        }

        [Then(@"I select the lowest score")]
        public void ThenISelectTheLowestScore()
        {
            IWebElement fullList = driver.FindElement(By.XPath("/html/body/div[2]/main/div[3]/section[1]/div[4]/div/div[4]/div[2]/div[3]/div/div[1]/a"));
            fullList.Click();
            System.Threading.Thread.Sleep(2000);
            IWebElement sortItems = driver.FindElement(By.XPath("/html/body/div[2]/main/div[3]/section[3]/div/div/div[7]/table/tbody/tr[1]/td[2]"));
            sortItems.Click();
            sortItems.Click();
            IWebElement addToCart = driver.FindElement(By.XPath("//*[@id=\"merchant - list\"]/tbody/tr[3]/td[4]/form/button"));
            addToCart.Click();
            IWebElement repairKitDeny = driver.FindElement(By.XPath("/html/body/div[13]/div/div/a"));
            repairKitDeny.Click();
            IWebElement goToCart = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div/div/div/div[1]/div/div[1]/div/div[2]/button[1]"));
            goToCart.Click();
        }

        [Then(@"I should see the item in cart")]
        public void ThenIShouldSeeTheItemInCart()
        {
            IWebElement cartItem = driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div/div[2]/section/section/div[3]/div/ul/li/div/div/div[1]/div[2]/div[2]/a"));
            try
            {
                if(cartItem.Text == phoneName)
                {
                    Console.WriteLine("Operation Completed");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw new ElementNotVisibleException("Operation Failed!");
            }
        }

    }
}