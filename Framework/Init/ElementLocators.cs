using System;

namespace Framework.Init
{
    public static class ElementLocators
    {

        #region //########### LogIn Page Elements ###########//

        public static String LogIn_logo_Niko = "//div[contains(@class,'login-logo')]";
        public static String LogIn_lbl_Email = "//label[@class='general-input-title ng-binding ng-scope'][contains(.,'EMAIL')]";
        public static String LogIn_txt_Username = "//input[@type='email']";
        public static String LogIn_lbl_Password = "//label[@class='general-input-title ng-binding'][contains(.,'Password')]";
        public static String LogIn_txt_Password = "//input[@type='password']";
        public static String LogIn_btn_LogIn = "//input[@value='LOGIN']";

        #endregion

        
        // All the xPath for dashboard buttons are dynamic and not dependant on name
         
        #region //########### Dashboard Page Elements ###########//

        public static String Dashboard_menu_Dashboard = "//a[@class='active'][contains(.,'Dashboard')]";
        public static String Dashboard_lbl_Dashboard = "//div[contains(@class,'view-title')][contains(.,'Dashboard')]";
        public static String Dashboard_btn_Bulb = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][1]//my-row-icon[@easywavedevice='light']";
        public static String Dashboard_btn_BulbOff = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][1]//div[@class='list-row-icon list-row-icon-status']";
        public static String Dashboard_btn_BulbOn = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][1]//div[@class='list-row-icon list-row-icon-status item-switched-on']";
        public static String Dashboard_btn_Socket = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][7]//my-row-icon[@easywavedevice='light']";
        public static String Dashboard_btn_SocketOff = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][7]//div[@class='list-row-icon list-row-icon-status']";
        public static String Dashboard_btn_SocketOn = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][7]//div[@class='list-row-icon list-row-icon-status item-switched-on']";
        public static String Dashboard_btn_Dimmer = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')]//my-row-icon[@easywavedevice='dimmer']";
        public static String Dashboard_btn_DimmerOff = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')]//my-row-icon[@easywavedevice='dimmer']/..//div[@class='list-row-icon list-row-icon-status']";
        public static String Dashboard_btn_DimmerOn = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')]//my-row-icon[@easywavedevice='dimmer']/..//div[@class='list-row-icon list-row-icon-status item-switched-on']";
        public static String Dashboard_btn_DecreaseDimmerIntensity = "//div[contains(@ng-if,'dimmer')]/div[contains(@ng-include,'minus.svg')]";
        public static String Dashboard_btn_IncreaseDimmerIntensity = "//div[contains(@ng-if,'dimmer')]/div[contains(@ng-include,'plus.svg')]";
        public static String Dashboard_btn_Blinds = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][5]//my-row-icon[@easywavedevice='blind']";
        public static String Dashboard_btn_BlindsOff = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][5]//div[@class='list-row-icon list-row-icon-status']";
        public static String Dashboard_btn_BlindsOn = "//div[@class='ng-scope'][contains(@ng-show,'deviceInGroup')][5]//div[@class='list-row-icon list-row-icon-status item-switched-on']";
        public static String Dashboard_btn_DownBlinds = "//div[contains(@ng-if,'blind')]/div[contains(@ng-include,'down.svg')]";
        public static String Dashboard_btn_UpBlinds = "//div[contains(@ng-if,'blind')]/div[contains(@ng-include,'up.svg')]";

        #endregion

        #region //########### Configuration Page Elements ###########//

        public static String Configuration_menu_Configuration = "//a[contains(.,'Configuration')]";
        public static String Configuration_menu_ActiveConfiguration = "//a[@class='active'][contains(.,'Configuration')]";
        public static String Configuration_lbl_Configuration = "//div[contains(@class,'view-title')][contains(.,'Configuration')]";
        public static String Configuration_lbl_Editfunction = "//div[contains(@class,'view-title')][contains(.,'Edit function')]";
        public static String Configuration_txt_Editname = " //input[@type='text']";
        public static String Configuration_btn_Close = "//button[contains(.,'Close')]";
        public static String Configuration_btn_ManageMySwitches = "//div[contains(@class,'list-row-add')]/div[contains(.,'Manage my switches')]/..//div[contains(@class,'list-row-icon')]";
        public static String Configuration_lbl_ManageMySwitches = "//div[contains(@class,'view-title')][contains(.,'Manage my switches')]";
        
        #endregion



    }
}
