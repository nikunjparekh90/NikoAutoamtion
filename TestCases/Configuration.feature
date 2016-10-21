Feature: Configuration
	

Scenario: Test case 1
	Given Enter Testcase Name "Configuration screen: Test case 1:" in Report 
	Given Navigate to niko login page 
	Given User has entered username "nikotest3@fptl.be" and passoword "Niko123" on niko login page
	When User click on LOGIN button
	Then User redirected to Dashboard page and Dashboard menu highlighted with yellow color in Left side pane
	When User click on Configuration menu in left side pane 
	Then User redirected to Configuration page and Configuration menu highlighted with yellow color in Left side pane
	When User Edit configurations for each functions
	Then Edited configurations are visible on configuration page  
	When User click on Manage my switches button
	Then User redirected to Manage my switches page 

