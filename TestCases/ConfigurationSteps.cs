using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Framework.Init;
using OpenQA.Selenium;

namespace Niko.TestCases
{
    [Binding]
    public class ConfigurationSteps 
    {

        IWebDriver _driver = DashboardSteps._driver;

        //Array to edit function name with postfix "automate"
        string[] strEditFunctions = {"A. Bulb operated by 05-318 automate","A. Bulb operated by 3 channel automate","A. Bulb- Kitchen operated by Remote automate",
                                    "B. Dimlamp -operated by eightbutton automate","C. Blinds/Shutter operated by Remote automate",
                                    "D. Blinds/Shutter  operated by eight automate","E. Socket operated by Remote automate"
                                    };
      
  

        [When(@"User click on Configuration menu in left side pane")]
        public void WhenIClickOnConfigurationMenuInLeftSidePane()
        {
            new Common(_driver).pause(10000);
            new Common(_driver).FindElementClick(By.XPath(ElementLocators.Configuration_menu_Configuration),"'Configuration' menu in left side pane on Dashboard page." );
            new Common(_driver).pause(10000);
           
            Report.AddToHtmlReport("STEP 4: Click on Configuration menu in left side pane on Dashboard page.", false, true);
           
        }

        [Then(@"User redirected to Configuration page and Configuration menu highlighted with yellow color in Left side pane")]
        public void ThenUserRedirectedToConfigurationPageAndConfigurationMenuHighlightedWithYellowColorInLeftSidePane()
        {
            //Verified Configuration menu in Yellow color using xPath as it's class attribute changes to "active"
            new Common(_driver).FindElement(By.XPath(ElementLocators.Configuration_menu_ActiveConfiguration), "'Configuration' menu with Yellow color in left side pane on Configuration page.");
            new Common(_driver).FindElement(By.XPath(ElementLocators.Configuration_lbl_Configuration), "'Configuration' header text verification on Configuration page.");
            new Common(_driver).FindElement(By.XPath("//div[@class='ng-scope'][contains(@ng-repeat,'item')][1]//div[contains(@ng-click,'editDevice')]"), "'Edit functions' button on Configurations page.");
         
        }

        [When(@"User Edit configurations for each functions")]
        public void WhenIEditConfigurationsForEachFunctions()
        {
            Report.AddToHtmlReport("STEP 5: Click on Edit functions button on Configuration page.", false, true);
           
            try
            {

                //Taken count for all the available functions to edit them dynamically.
                //For ex: if there are 7 or 5 functions then script will edit 7 or 5 functions based on their availability checked by script dynamically using count
                IList<IWebElement> Configuration_btn_EditConfigurations = _driver.FindElements(By.XPath("//div[@class='ng-scope'][contains(@ng-repeat,'item')]//div[contains(@ng-click,'editDevice')]"));

                for (int i = 1; i <= Configuration_btn_EditConfigurations.Count; i++)
                {
                    IWebElement Configuration_btn_EditConfiguration = _driver.FindElement(By.XPath("//div[@class='ng-scope'][contains(@ng-repeat,'item')][" + i + "]//div[contains(@ng-click,'editDevice')]"));

                    new Common(_driver).ScrolltoView(_driver, Configuration_btn_EditConfiguration);

                    Configuration_btn_EditConfiguration.Click();

                    new Common(_driver).pause(5000);

                    if (i == 1)
                    {
                        new Common(_driver).FindElement(By.XPath(ElementLocators.Configuration_lbl_Editfunction), "'Edit function' header text verificaion on Edit function page.");
                    }

                    try
                    {
                        IWebElement EditConfiguration_txt_Editname = _driver.FindElement(By.XPath(ElementLocators.Configuration_txt_Editname));

                        Common.enterText(EditConfiguration_txt_Editname, strEditFunctions[i - 1], true);

                        if (i == 1)
                        {
                            Report.AddToHtmlReportPassed("'Function Name' textbox on Edit function page.");
                        }

                        new Common(_driver).pause(5000);

                    }
                    catch (Exception ex)
                    {
                        Report.AddToHtmlReportFailed(_driver, ex, "'Function Name' textbox on Edit function page.");
                        DashboardSteps.intFailcnt++;

                    }

                    try
                    {
                        IWebElement Configuration_btn_Close = _driver.FindElement(By.XPath(ElementLocators.Configuration_btn_Close));

                        Configuration_btn_Close.Click();

                        if (i == 1)
                        {
                            Report.AddToHtmlReportPassed("'Close' button on Edit function page.");
                        }

                        new Common(_driver).pause(5000);

                    }
                    catch (Exception ex)
                    {
                        Report.AddToHtmlReportFailed(_driver, ex, "'Close' button on Edit function page.");
                        DashboardSteps.intFailcnt++;

                    }

                    if (i == 1)
                    {
                        Report.AddToHtmlReport("<br>Data Entered: ", false, true, true);
                    }

                    Report.AddToHtmlReport("Function Name: " + strEditFunctions[i - 1], false);

                    if (i == Configuration_btn_EditConfigurations.Count)
                    {
                        Report.AddToHtmlReport("<br>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Edit functions' button on Configurations page.");
                DashboardSteps.intFailcnt++;

            }

        }

        [Then(@"Edited configurations are visible on configuration page")]
        public void ThenEditedConfigurationsAreVisibleOnConfigurationPage()
        {
            try
            {

                //Taken count for all the available functions to verify after edit them dynamically.
                //For ex: if there are 7 or 5 functions edited then script will verify 7 or 5 functions edited based on their availability checked by script dynamically using count
                IList<IWebElement> Configuration_lbl_EditConfigurations = _driver.FindElements(By.XPath("//div[@class='ng-scope'][contains(@ng-repeat,'item')]//p[@class='ng-binding']"));

                for (int i = 1; i <= Configuration_lbl_EditConfigurations.Count; i++)
                {
                    new Common(_driver).FindElement(By.XPath("//div[@class='ng-scope'][contains(@ng-repeat,'item')][" + i + "]//p[@class='ng-binding'][contains(.,'" + strEditFunctions[i - 1] + "')]"), "Function name '" + strEditFunctions[i - 1] + "' text verification on Configuration page.");
                }
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'Function name' text verification on Configurations page.");
                DashboardSteps.intFailcnt++;

            }
        }

        [When(@"User click on Manage my switches button")]
        public void WhenIClickOnManageMySwitchesButton()
        {
            new Common(_driver).FindElementClick(By.XPath(ElementLocators.Configuration_btn_ManageMySwitches),"'Manage my switches' button on Configuration page.");

            Report.AddToHtmlReport("STEP 6: Click on Manage my switches button on Configuration page.", false, true);
           
        }

        [Then(@"User redirected to Manage my switches page")]
        public void ThenUserRedirectedToManageMySwitchesPage()
        {
            new Common(_driver).FindElement(By.XPath(ElementLocators.Configuration_lbl_ManageMySwitches), "'Manage my switches' header text verification on Manage my switches page.");

        }

    }
}
