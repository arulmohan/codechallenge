using FluentAssertions;
using TFL.Specs.PageObjects;
using System.Runtime.Intrinsics.X86;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace TFL.Specs.Steps
{
    [Binding]
    public sealed class PlanYourJourneyStepDefinitions
    {
        private readonly PlanYourJourneyPage _planYourJourneyPage;

        public PlanYourJourneyStepDefinitions(PlanYourJourneyPage planYourJourneyPage)
        {
            _planYourJourneyPage = planYourJourneyPage;
        }

        [Given("the user enter plan journey from (.*)")]
        [When(@"the user enter plan journey from (.*)")]
        public void GivenTheUserEnterPlanJourneyFrom(string journeyFrom)
        {
            _planYourJourneyPage.EnterPlanJourneyFrom(journeyFrom);
        }

        [Given("the user enter plan journey to (.*)")]
        [When("the user enter plan journey to (.*)")]
        public void GivenTheUserEnterPlanJourneyTo(string journeyTo)
        {
            _planYourJourneyPage.EnterPlanJourneyTo(journeyTo);
        }
        [When("the user clicks the plan my journey button")]
        public void GivenTheUserClicksThePlanMyJourneyButton()
        {
            _planYourJourneyPage.SubmitPlanMyJourney();
        }

        [When(@"the user clicks the accept all cookies button")]
        public void WhenTheUserClicksTheAcceptAllCookiesButton()
        {
            _planYourJourneyPage.CheckJourneyResultExists();
        }

        [Then(@"the user should verify the results")]
        public void ThenTheUserShouldVerifyTheResults()
        {
            //_planYourJourneyPage.CheckJourneyResultExists().Should().BeTrue();
            _planYourJourneyPage.VerifyJourneyResults();
        }
        [When(@"the user clicks the edit preferences")]
        public void WhenTheUserClicksTheEditPreferences()
        {
            _planYourJourneyPage.EditPreferncesLink();
        }
        [Then(@"the user should verify the journey time displayed")]
        public void ThenTheUserShouldVerifyTheJourneyTimeDisplayed()
        {
            _planYourJourneyPage.ValidateJourneyTime();
        }


        [When(@"the user clicks the deselect all")]
        public void WhenTheUserClicksTheDeselectAll()
        {
            _planYourJourneyPage.DeselectAllLink();
        }

        [When(@"the user selects the routes with least walking radio button")]
        public void WhenTheUserSelectsTheRoutesWithLeastWalkingRadioButton()
        {
            _planYourJourneyPage.LeastWalkingRadioButton();
        }

        [Then(@"the user clicks the update journey button")]
        public void ThenTheUserClicksTheUpdateJourneyButton()
        {
            _planYourJourneyPage.ClickUpdateJourney();
        }

        [Then(@"the user should verify the walking time displayed")]
        public void ThenTheUserShouldVerifyTheWalkingTimeDisplayed()
        {
            _planYourJourneyPage.ValidateWalkingResults();
        }

        [Then(@"the user should verify the cycling time displayed")]
        public void ThenTheUserShouldVerifyTheCyclingTimeDisplayed()
        {
            _planYourJourneyPage.ValidateCyclingResults();
        }

        [Then(@"the user clicks the view details button")]
        public void ThenTheUserClicksTheViewDetailsButton()
        {
            _planYourJourneyPage.ViewDetails();
        }

        [Then(@"the user should see the complete access information at Covent Garden Underground Station")]
        public void ThenTheUserShouldSeeTheCompleteAccessInformationAtCoventGardenUndergroundStation()
        {
            _planYourJourneyPage.ValidateAccessInformation();
        }


        [Then(@"the user should not see the results")]
        public void ThenTheUserShouldNotSeeTheResults()
        {
            _planYourJourneyPage.CheckJourneyResultExists().Should().BeFalse();           
        }

        [Then(@"the user should see the error as ""([^""]*)""")]
        public void ThenTheUserShouldSeeTheErrorAs(string expectedError)
        {
            _planYourJourneyPage.ValidateErrorMessage(expectedError);
        }

       
        [Given("the user clicks the change time link")]
        public void GivenTheUserClicksTheChangeTimeLink()
        {
            _planYourJourneyPage.ChangeTimeLink();
        }

        [When(@"the user selects the arriving option")]
        public void WhenTheUserSelectsTheArrivingOption()
        {
            _planYourJourneyPage.ArrivingOption();
        }

        [Then(@"the user should see the results as ""([^""]*)""")]
        public void ThenTheUserShouldSeeTheResultsAs(string expectedMessage)
        {
            _planYourJourneyPage.ValidateResults(expectedMessage);
        }



        [Then(@"the user should see the journey results with ""([^""]*)"" as ""([^""]*)""")]
        public void ThenTheUserShouldSeeTheJourneyResultsWithAs(string label, string value)
        {
            _planYourJourneyPage.ValidateJourneyResult(label,value);
        }

        [Then(@"the user should see the result for both walking and cycling time")]
        public void ThenTheUserShouldSeeTheResultForBothWalkingAndCyclingTime()
        {
            _planYourJourneyPage.ValidateWalkingAndCyclingResult();
        }


        [When(@"the user clicks edit journey")]
        public void WhenTheUserClicksEditJourney()
        {
            _planYourJourneyPage.EditJourneyLink();
        }

        /*[When(@"the user clicks the update journey button")]
        public void WhenTheUserClicksTheUpdateJourneyButton()
        {
            _planYourJourneyPage.UpdateJourneyButton();
        }
        */

        [When(@"the user clicks the home")]
        public void WhenTheUserClicksTheHome()
        {
            _planYourJourneyPage.HomeLink();
        }

        [When(@"the user clicks the recents tab")]
        public void WhenTheUserClicksTheRecentsTab()
        {
            _planYourJourneyPage.RecentsTab();
        }


    }
}
