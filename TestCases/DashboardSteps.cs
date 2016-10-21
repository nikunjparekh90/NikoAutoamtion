using Framework.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace Niko.TestCases
{
    [Binding]
    public class DashboardSteps
    {
       public static IWebDriver _driver;

        public static Int32 intLoginPassCnt = 0;
        public static Int32 intLoginFailCnt = 0;
        public static Int32 intLoginWarningCnt = 0;
        public static bool IsTcAdded = false;
        public static Int32 intFailcnt = 0;
        public static bool bulbStatus = false;

        static string strProjectUrl = Convert.ToString(ConfigurationSettings.AppSettings.Get("ProjectUrl"));
        static string strUseBrowser = Convert.ToString(ConfigurationSettings.AppSettings.Get("UseBrowser"));
        static string strCloseBrowser = Convert.ToString(ConfigurationSettings.AppSettings.Get("CloseBrowser"));

        //Additional Prerequisite for all scenarios - Initialize Browser
        [BeforeScenario]
        public void OpenBrowser()
        {
            if (strUseBrowser == "1")
            {
                _driver = new FirefoxDriver();
            }
            else if (strUseBrowser == "2")
            {
                _driver = new ChromeDriver("c:\\");
            }
            else
            {
                _driver = new InternetExplorerDriver("c:\\");
            }

            _driver.Navigate().GoToUrl(strProjectUrl);
            _driver.Manage().Window.Maximize();

        }

        //Additional Prerequisite for all scenarios - Add testcase details to report
        [Given(@"Enter Testcase Name ""(.*)"" in Report")]
        public void GivenEnterTestcaseNameInReport(string TC)
        {
            Report.AddToHtmlReport(TC, true, false, true);
        }

        [Given(@"Navigate to niko login page")]
        public void GivenNavigateToNikoLoginPage()
        {
            Report.AddToHtmlReport("STEP 1: Insert Url in Browser Addressbar.", false, true);

            if (strUseBrowser == "1")
            {
                Report.AddToHtmlReportPassed("Firefox Browser Open for " + strProjectUrl + " .");
            }
            else if (strUseBrowser == "2")
            {   
                Report.AddToHtmlReportPassed("Chrome Browser Open for " + strProjectUrl + " .");
            }
            else
            {   
                Report.AddToHtmlReportPassed("IE Browser Open for " + strProjectUrl + " .");
            }

             new Common(_driver).FindElement(By.XPath(ElementLocators.LogIn_logo_Niko), "'NIKO | Connected Switch' logo on LogIn page.");
            
        }

        [Given(@"User has entered username ""(.*)"" and passoword ""(.*)"" on niko login page")]
        public void GivenIHaveEnteredUsernameAndPassowordOnNikoLoginPage(string strUsername, string strPassword)
        {
            Report.AddToHtmlReport("STEP 2: Enter valid username, password on LogIn page.", false, true);
            
            new Common(_driver).FindElement(By.XPath(ElementLocators.LogIn_lbl_Email), "'Email' label text verification on LogIn page.");
            IWebElement LogIn_txt_Username = new Common(_driver).FindElement(By.XPath(ElementLocators.LogIn_txt_Username), "'Username' textbox on LogIn page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.LogIn_lbl_Password), "'Password' label text verification on LogIn page.");
            IWebElement LogIn_txt_Password = new Common(_driver).FindElement(By.XPath(ElementLocators.LogIn_txt_Password), "'Password' textbox on LogIn page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.LogIn_btn_LogIn), "'LOGIN' button on LogIn page.");

            Common.enterText(LogIn_txt_Username, strUsername, true);
            Common.enterText(LogIn_txt_Password, strPassword, true);


            Report.AddToHtmlReport("<br>Data Entered: ", false, true, true);
            Report.AddToHtmlReport("Username: " + strUsername, false);
            Report.AddToHtmlReport("Password: " + strPassword + "<br>", false);

        }

        [When(@"User click on LOGIN button")]
        public void WhenIClickOnLOGINButton()
        {
            Report.AddToHtmlReport("STEP 3: Click on LOGIN button on LogIn page.", false, true);

            try
            {
                IWebElement LogIn_btn_LogIn = _driver.FindElement(By.XPath(ElementLocators.LogIn_btn_LogIn));
                LogIn_btn_LogIn.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'LOGIN' button on LogIn page.");
                intFailcnt++;
            }
        }

        [Then(@"User redirected to Dashboard page and Dashboard menu highlighted with yellow color in Left side pane")]
        public void ThenUserRedirectedToDahboardPageAndDashbordMenuHighlightedWithYellowColorInLeftSidePane()
        {
            //Verified Dashboard menu in Yellow color using xPath as it's class attribute changes to "active"
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_menu_Dashboard), "'Dashboard' menu with Yellow color in left side pane on Dashboard page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_lbl_Dashboard), "'Dashboard' header text verification on Dashboard page.");

            //Dynamic xPath created for all these dashboard buttons using "easywavedevice" attribute and not dependant on button name associated 
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_Bulb), "'Bulb' button on Dashboard page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_Dimmer), "'Dimmer bulb' button on Dashboard page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_Blinds), "'Blinds' button on Dashboard page.");           
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_Socket), "'Socket' button on Dashboard page.");

        }

        [When(@"User click on bulb button")]
        public void WhenIClickOnFirstBulbButton()
        {
            Report.AddToHtmlReport("STEP 4: Click on Bulb button on Dashboard page.", false, true);
           
            new Common(_driver).pause(10000); 

            try
            {
                IWebElement Dashboard_btn_Bulb = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_Bulb));
                Dashboard_btn_Bulb.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "Bulb button on Dashboard page.");
                intFailcnt++;
            }

            new Common(_driver).pause(10000);

        }

        [Then(@"Bulb's status gets changed")]
        public void ThenBulbSColorStatusGetsChanged()
        {
            try
            {
                //Verified bulb's status changed to OFF using class attribute that changes to "list-row-icon list-row-icon-status"
                IWebElement Dashboard_btn_FirstBulbOff = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_BulbOff));
                Report.AddToHtmlReportPassed("Bulb turn off successfully.");

            }
            catch (Exception)
            {
                try
                {
                    //Verified bulb's status changed to ON using class attribute that changes to "list-row-icon list-row-icon-status item-switched-on"
                    IWebElement Dashboard_btn_FirstBulbOn = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_BulbOn));
                    Report.AddToHtmlReportPassed("Bulb turn on successfully.");
                }
                catch (Exception ex)
                {
                    Report.AddToHtmlReportFailed(_driver, ex, "Bulb turn on successfully.");
                    intFailcnt++;
                }
            }
        }


        [When(@"User click on socket button")]
        public void WhenIClickOnSocketButton()
        {

            Report.AddToHtmlReport("STEP 4: Click on Socket button on Dashboard page.", false, true);

            new Common(_driver).pause(10000);

            try
            {
                IWebElement Dashboard_btn_Socket = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_Socket));
                Dashboard_btn_Socket.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "Socket button on Dashboard page.");
                intFailcnt++;
            }

            new Common(_driver).pause(10000);
        }

        [Then(@"Socket's status gets changed")]
        public void ThenSocketSStatusGetsChanged()
        {
            try
            {
                //Verified Socket's status changed to OFF using class attribute that changes to "list-row-icon list-row-icon-status"    
                IWebElement Dashboard_btn_SocketOff = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_SocketOff));
                Report.AddToHtmlReportPassed("Socket turn off successfully.");

            }
            catch (Exception)
            {
                try
                {
                    //Verified Socket's status changed to ON using class attribute that changes to "list-row-icon list-row-icon-status item-switched-on"
                    IWebElement Dashboard_btn_SocketOn = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_SocketOn));
                    Report.AddToHtmlReportPassed("Socket turn on successfully.");
                }
                catch (Exception ex)
                {
                    Report.AddToHtmlReportFailed(_driver,ex, "Socket turn on successfully.");
                    intFailcnt++;
                }
            }
        }

        [When(@"User click on dimmer bulb button")]
        public void WhenUserClickOnDimmerBulbButton()
        {
            Report.AddToHtmlReport("STEP 4: Click on Dimmer bulb button on Dashboard page.", false, true);

            new Common(_driver).pause(10000);

            try
            {
                IWebElement Dashboard_btn_Dimmer = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_Dimmer));
                new Common(_driver).ScrolltoView(_driver, Dashboard_btn_Dimmer);
                Dashboard_btn_Dimmer.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Dimmer' bulb button on Dashboard page.");
                intFailcnt++;
            }

            new Common(_driver).pause(10000);

        }

        [Then(@"Dimmer Bulb's status gets changed and increase\(\+\) / decrease\(-\) light intensity button is visible")]
        public void ThenDimmerBulbSStatusGetsChangedAndIncreaseDecrease_LightIntensityButtonIsVisible()
        {
            try
            {
                //Verified Dimmer's status changed to OFF using class attribute that changes to "list-row-icon list-row-icon-status"                  
                IWebElement Dashboard_btn_DimmerOff = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_DimmerOff));
                Report.AddToHtmlReportPassed("Dimmer buld turn off successfully.");

            }
            catch (Exception)
            {
                try
                {
                    //Verified Dimmer's status changed to ON using class attribute that changes to "list-row-icon list-row-icon-status item-switched-on"                  
                    IWebElement Dashboard_btn_DimmerOn = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_DimmerOn));
                    Report.AddToHtmlReportPassed("Dimmer bulb turn on successfully.");
                }
                catch (Exception ex)
                {
                    Report.AddToHtmlReportFailed(_driver, ex, "Dimmer bulb turn on successfully.");
                    intFailcnt++;
                }
            }

            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_DecreaseDimmerIntensity), "'Decrease Dimmer Intensity' button on Dashboard page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_IncreaseDimmerIntensity), "'Increase Dimmer Intensity' button on Dashboard page.");
            
        }

        [When(@"User click on increase\(\+\) light intensity button")]
        public void WhenUserClickOnIncreaseLightIntensityButton()
        {
            Report.AddToHtmlReport("STEP 5: Click and Hold (+) button to increase Dimmer bulb light intensity  on Dashboard page.", false, true);

            try
            {
                IWebElement Dashboard_btn_DimmerOn = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_DimmerOn));              
                IWebElement Dashboard_btn_IncreaseDimmerIntensity = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_IncreaseDimmerIntensity));
                Actions myaction = new Actions(_driver);
                myaction.ClickAndHold(Dashboard_btn_IncreaseDimmerIntensity).Build().Perform();
                new Common(_driver).pause(15000);
                myaction.Release(Dashboard_btn_IncreaseDimmerIntensity).Build().Perform();
                new Common(_driver).pause(10000);

            }
            catch (Exception ex)
            {
                try
                {

                    IWebElement Dashboard_btn_IncreaseDimmerIntensity = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_IncreaseDimmerIntensity));
                   //Additional Prerequisite to turn ON the dimmer before long press (+) if it's turn OFF by other browsers 
                    Dashboard_btn_IncreaseDimmerIntensity.Click();

                    Report.AddToHtmlReportPassed("Dimmer bulb turn on successfully.");
               
                    new Common(_driver).pause(15000);

                    Actions myaction = new Actions(_driver);
                    myaction.ClickAndHold(Dashboard_btn_IncreaseDimmerIntensity).Build().Perform();
                    new Common(_driver).pause(15000);
                    myaction.Release(Dashboard_btn_IncreaseDimmerIntensity).Build().Perform();
                    new Common(_driver).pause(10000);

                }
                catch (Exception)
                {
                    Report.AddToHtmlReportFailed(_driver, ex, "'Increase Dimmer Intensity' button on Dashboard page.");
                    intFailcnt++;
           
                }

            }
        }

        [Then(@"Dimmer Bulb's light intensity increases")]
        public void ThenDimmerBulbSLightIntensityIncreases()
        {
            
            //NOTE: Can't find any change in DOM for increase in light intensity for Dimmer after long press
            //so need to discuss with dev team what to verify in GUI

        }

        [When(@"User click on decrease\(-\) light intensity button")]
        public void WhenUserClickOnDecrease_LightIntensityButton()
        {
            Report.AddToHtmlReport("STEP 6: Click and Hold (-) button to decrease Dimmer bulb light intensity  on Dashboard page.", false, true);

            try
            {
                IWebElement Dashboard_btn_DecreaseDimmerIntensity = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_DecreaseDimmerIntensity));
                Actions myaction = new Actions(_driver);
                myaction.ClickAndHold(Dashboard_btn_DecreaseDimmerIntensity).Build().Perform();
                new Common(_driver).pause(15000);
                myaction.Release(Dashboard_btn_DecreaseDimmerIntensity).Build().Perform(); 
                new Common(_driver).pause(10000);

            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Decrease Dimmer Intensity' button on Dashboard page.");
                intFailcnt++;
            }
        
        }

        [Then(@"Dimmer Bulb's light intensity decreases")]
        public void ThenDimmerBulbSLightIntensityDecreases()
        {

            //NOTE: Can't find any change in DOM for decrease in light intensity for Dimmer after long press
            //so need to discuss with dev team what to verify in GUI

        
        }

        [When(@"User click on blinds button")]
        public void WhenUserClickOnBlindsButton()
        {
            Report.AddToHtmlReport("STEP 4: Click on Blinds button on Dashboard page.", false, true);

            new Common(_driver).pause(10000);

            try
            {
                IWebElement Dashboard_btn_Blinds = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_Blinds));
                new Common(_driver).ScrolltoView(_driver, Dashboard_btn_Blinds);
                Dashboard_btn_Blinds.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Blinds' button on Dashboard page.");
                intFailcnt++;
            }

            new Common(_driver).pause(10000);

        }

        [Then(@"Blinds's status gets changed and down / up buttons are visible")]
        public void ThenBlindsSStatusGetsChangedAndDownUpButtonsAreVisible()
        {

            //NOTE: Can't find any change in Blind's status after pressing it
            //so need to discuss with dev team what to verify as there is no change in status

            //Verified visible down / up buttons
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_DownBlinds), "'Down Blinds' button on Dashboard page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.Dashboard_btn_UpBlinds), "'Up Blinds' button on Dashboard page.");
        }

        [When(@"User click on down button")]
        public void WhenUserClickOnDownButton()
        {
            Report.AddToHtmlReport("STEP 5: Click on Down button to down the blinds on Dashboard page.", false, true);

            try
            {
                IWebElement Dashboard_btn_DownBlinds = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_DownBlinds));
                new Common(_driver).pause(3000);
                Dashboard_btn_DownBlinds.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Down blinds' button on Dashboard page.");
                intFailcnt++;
            }

        }

        [Then(@"Blinds become down and pause button appears for while on dashboard screen")]
        public void ThenBlindsSBecomeDown()
        {
            //Verified blinds down using 5 mins of dynamic waiting and change in class attribute to "list-row-icon list-row-icon-status"
            new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath(ElementLocators.Dashboard_btn_BlindsOff), "Blinds down verification on Dashboard page.");
            
        }
       

        [When(@"User click on up button")]
        public void WhenUserClickOnUpButton()
        {
            Report.AddToHtmlReport("STEP 6: Click on Up button to up blinds on Dashboard page.", false, true);

            try
            {
                IWebElement Dashboard_btn_UpBlinds = _driver.FindElement(By.XPath(ElementLocators.Dashboard_btn_UpBlinds));
                new Common(_driver).pause(3000);
                Dashboard_btn_UpBlinds.Click();

            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Up Blinds' button on Dashboard page.");
                intFailcnt++;
            }
        
        }

        [Then(@"Blinds become up and pause button appears for while on dashboard screen")]
        public void ThenBlindsSBecomeUp()
        {

            //Verified blind up using 5 mins of dynamic waiting and change in class attribute to "list-row-icon list-row-icon-status item-switched-on"
            new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath(ElementLocators.Dashboard_btn_BlindsOn), "Blinds up verification on Dashboard page.");
       
        }

       //clsoing browser after test run
        [AfterScenario]
        public void ClosingBrowser()
        {
            if (strCloseBrowser == "1")
            {
                _driver.Quit();
                Report.AddToHtmlReportPassed("Browser Close.");
            }

            Report.AddToHtmlReportFeatureFinish();

            Report.GenerateHtmlReport();

            if (intFailcnt == 0)
            {
                intLoginPassCnt++;
            }
            else
            {
                intLoginFailCnt++;
                intFailcnt = 0;
            }

        }

        
        //Build report
        [AfterFeature("Dashboard")]
        public static void BuildReport()
        {
            Report.AddToHtmlSummaryReport("NIKO - Test Cases", intLoginPassCnt, intLoginFailCnt);
            Report.AddToHtmlSummaryReportTotal();
            Report.GenerateHtmlSummaryReport();

        }

    }
}
