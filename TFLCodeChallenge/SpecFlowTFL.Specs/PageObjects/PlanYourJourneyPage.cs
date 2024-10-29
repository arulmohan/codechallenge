using FluentAssertions;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Actions.Selenium;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace TFL.Specs.PageObjects
{
    public class PlanYourJourneyPage
    {

        private const string PageUrl = "http://www.tfl.gov.uk";

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly IBrowserInteractions _browserInteractions;
        
        public PlanYourJourneyPage()
        {
            _driver = new ChromeDriver();
            //_wait = new WebDriverWait(this._driver, new TimeSpan(0, 0, 20));
            _wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(20));

        }

        private IWebElement ButtonAcceptCookies => _driver.FindElement(By.XPath("//*[text()='Accept all cookies']/parent::button"));
        private IWebElement PlanJourneyFrom => _driver.FindElement(By.Id("InputFrom"));
        private IWebElement PlanJourneyTo => _driver.FindElement(By.Id("InputTo"));
        private IWebElement PlanJourneyButton => _driver.FindElement(By.Id("plan-journey-button"));
        private int JourneyOptionResultsCount => getElementCount("option-1-content");
        private IWebElement FromError => _driver.FindElement(By.Id("InputFrom-error"));
        private IWebElement ToError => _driver.FindElement(By.Id("InputTo-error"));
        private IWebElement ChangeTime => _driver.FindElement(By.ClassName("change-departure-time"));
        private IWebElement Arriving => _driver.FindElement(By.Id("arriving"));
        private IWebElement JourneyResultSummary => _driver.FindElement(By.ClassName("journey-result-summary"));
        private IWebElement WalkingAndCycling => _driver.FindElement(By.XPath("//*[text()='Walking and cycling']"));
        private IWebElement EditJourney => _driver.FindElement(By.ClassName("edit-journey"));
        private IWebElement UpdateJourney => _driver.FindElement(By.XPath("//*[@value='Cancel']/following-sibling::input[@id='plan-journey-button']"));
        private IWebElement Home => _driver.FindElement(By.ClassName("home"));
        private IWebElement Recents => _driver.FindElement(By.XPath("//*[@id='recent-journeys']//a[text()='Recents']"));
        private IWebElement EditPreferences => _driver.FindElement(By.XPath("//button[text()='Edit preferences']"));
        private IWebElement DeselectAll => _driver.FindElement(By.XPath("//button[text()='deselect all']"));
        private IWebElement LeastWalking => _driver.FindElement(By.XPath("//input[@type='radio' and @value='leastwalking']"));
        private IWebElement LeastWalkingLabel => _driver.FindElement(By.XPath("//label[text()='Routes with least walking']"));
        private IWebElement UpdateJourneyButton => _driver.FindElement(By.XPath("//*[@id='more-journey-options']//*[@value='Update journey' ]"));
        private IWebElement ViewDetailsButton => _driver.FindElement(By.XPath("//*[@id='option-1-content']/div[1]//button[text()='View details']"));
        private IWebElement UpStairs => _driver.FindElement(By.XPath("//*[contains(text(),'Covent Garden Underground')]/ancestor::div[@class='journey-detail-step footpath']//div[@class='access-information']/a[1]"));
        private IWebElement UpLift => _driver.FindElement(By.XPath("//*[contains(text(),'Covent Garden Underground')]/ancestor::div[@class='journey-detail-step footpath']//div[@class='access-information']/a[2]"));
        private IWebElement LevelWalkway => _driver.FindElement(By.XPath("//*[contains(text(),'Covent Garden Underground')]/ancestor::div[@class='journey-detail-step footpath']//div[@class='access-information']/a[3]"));


        public void Goto()
        {
            _driver.Navigate().GoToUrl(PageUrl);
            _driver.Manage().Window.Maximize();

        }
        public void CloseBrowser()
        {
            _driver.Close();
        }

        public void AcceptAllCookies()
        {
            //_driver.Manage().Cookies.DeleteAllCookies();
            ButtonAcceptCookies.Click();
            Thread.Sleep(2000);
        }

        public void EnterPlanJourneyFrom(string location)
        {
            PlanJourneyFrom.Click();
            PlanJourneyFrom.SendKeys(Keys.Control + "a");
            PlanJourneyFrom.SendKeys(Keys.Delete);
            PlanJourneyFrom.SendKeys(location);
            Thread.Sleep(2000);
            PlanJourneyFrom.SendKeys(Keys.ArrowDown);
            PlanJourneyFrom.SendKeys(Keys.Tab);
        }

        public void EnterPlanJourneyTo(string location)
        {
            PlanJourneyTo.Click();
            PlanJourneyTo.SendKeys(Keys.Control + "a");
            PlanJourneyTo.SendKeys(Keys.Delete);
            PlanJourneyTo.SendKeys(location);
            Thread.Sleep(2000);
            PlanJourneyTo.SendKeys(Keys.ArrowDown);
            PlanJourneyTo.SendKeys(Keys.Tab);
        }

        public void SubmitPlanMyJourney()
        {
            PlanJourneyButton.Click();
        }

        public bool CheckJourneyResultExists()
        {
            if (JourneyOptionResultsCount > 0)
            {
                return true;
            }

            return false;
        }
        public bool CheckJourneyResultNotExists()
        {
            bool t = _driver.FindElement(By.Id("option-1-content")).Displayed;
            if (t)
            {
                return false;
            }
            return true;
        }
        public bool ValidateErrorMessage(string expectedError)
        {
            var fromActualError = FromError.GetAttribute("value");
            var toActualError = ToError.GetAttribute("value");
            if (expectedError.Equals(fromActualError) || expectedError.Equals(toActualError))
            {
                return true;
            }

            return false;
        }
        public void ChangeTimeLink()
        {
            ChangeTime.Click();
        }
        public void ArrivingOption()
        {
            Arriving.Click();
        }

        private int getElementCount(string elementId)
        {
            By elementbyId = By.Id(elementId);
            try
            {
                Thread.Sleep(10000);
                _wait.Until(drv => drv.FindElement(elementbyId));
                return _driver.FindElements(By.Id(elementId)).Count();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public void VerifyJourneyResults()
        {
            IWebElement visibleElement = _wait.Until(driver =>
            {
                var element = _driver.FindElement(By.ClassName("journey-result-summary"));
                return element.Displayed ? element : null;
            });
        }
        public void EditJourneyLink()
        {
            EditJourney.Click();
        }
        public void EditPreferncesLink()
        {
            EditPreferences.Click();
        }
        public void DeselectAllLink()
        {
           // Thread.Sleep(3000);
            _wait.Until(driver =>
            {
                return DeselectAll.Displayed && DeselectAll.Enabled ? DeselectAll : null;
            });

            DeselectAll.Click();
        }
        public void LeastWalkingRadioButton()
        {
            _wait.Until(driver =>
            {
                return LeastWalkingLabel.Displayed ? LeastWalkingLabel : null;
            });

            if (!LeastWalking.Selected) 
            { try 
                {
                    LeastWalking.Click(); 
                } 
              catch (ElementClickInterceptedException) 
                { 
                    IJavaScriptExecutor js = (IJavaScriptExecutor)_driver; 
                   js.ExecuteScript("arguments[0].click();", LeastWalking); 
                } 
            }
        }
        public void ClickUpdateJourney()
        {
            UpdateJourneyButton.Click();
        }
        public void ViewDetails()
        {
            _wait.Until(driver =>
            {
                return ViewDetailsButton.Displayed ? ViewDetailsButton : null;
            });
            ViewDetailsButton.Click();
        }
        public void ValidateAccessInformation()
        {
            //Thread.Sleep(3000);
            _wait.Until(driver =>
            {
                return UpStairs.Displayed && UpStairs.Enabled ? UpStairs : null;
            });
            UpStairs.Click();
            //Thread.Sleep(1000);
            var upstairsValue = UpStairs.Text;
            Assert.AreEqual("Up stairs", upstairsValue);

            UpLift.Click();
            //Thread.Sleep(1000);
            var upliftValue = UpLift.Text;
            Assert.AreEqual("Up lift", upliftValue);

            LevelWalkway.Click();
            //Thread.Sleep(1000);
            var levelWalkwayValue = LevelWalkway.Text;
            Assert.AreEqual("Level walkway", levelWalkwayValue);

        }
        public void HomeLink()
        {
            Home.Click();
        }
        public void RecentsTab()
        {
            Recents.Click();
        }
        public bool ValidateResults(string expectedResult)
        {
            var actualResult = _driver.FindElement(By.XPath("//*[contains(text(),'" + expectedResult + "')]")).GetAttribute("value");

            if (expectedResult.Equals(actualResult))
            {
                return true;
            }
            return false;
        }
        public void ValidateJourneyResult(string label, string expectedValue)
        {
            var actualResult = _driver.FindElement(By.XPath("//*[contains(text(),'" + label + "')]/following-sibling::span/strong")).Text;
            Assert.AreEqual(expectedValue, actualResult);
        }
        public void ValidateWalkingAndCyclingResult()
        {
            var cyclingResult = _driver.FindElement(By.XPath("//*[text()='Walking and cycling']/parent::div//a[1]//*[@class='col2 journey-info']")).Text;
            //Assert.AreEqual("2mins", cyclingResult);

            var walkingResult = _driver.FindElement(By.XPath("//*[text()='Walking and cycling']/parent::div//a[2]//*[@class='col2 journey-info']")).Text;
            //Assert.AreEqual("9mins", walkingResult);
        }
        public void ValidateCyclingResults()
        {
            var actualValue = _driver.FindElement(By.XPath("//a[@class='journey-box cycling']//div[@class='col2 journey-info']")).Text;
            actualValue = actualValue.Replace("\r\n", "");
            Regex regex = new Regex("[0-9]+mins");
            Assert.That(regex.IsMatch(actualValue), Is.True, "The string does not contain both letters and digits.");

            //Assert.AreEqual(expectedValue, actualResult);

        }
        public void ValidateWalkingResults()
        {
            var actualValue = _driver.FindElement(By.XPath("//a[@class='journey-box walking']//div[@class='col2 journey-info']")).Text;
            actualValue = actualValue.Replace("\r\n", "");
            Regex regex = new Regex("[0-9]+mins");
            Assert.That(regex.IsMatch(actualValue), Is.True, "The string does not contain both letters and digits.");

           //Assert.AreEqual(expectedValue, actualResult);
        }
        public void ValidateJourneyTime()
        {
            Thread.Sleep(3000);
            var actualValue = _driver.FindElement(By.XPath("//*[@id='option-1-heading']//div[contains(@class,'journey-time')]")).Text;
            
            actualValue = actualValue.Replace("\r\n", "");
            Regex regex = new Regex("Total time:[0-9]+mins");
            Assert.IsTrue(actualValue.Contains("mins"), actualValue + " doesn't contains 'mins'");
            Assert.That(regex.IsMatch(actualValue), Is.True, "The string does not contain both letters and digits.");

         }
    }
    
}
