@Dashboard
Feature: Dashboard Screen

Scenario: bulbTest case 1
	
	Given Enter Testcase Name "Dashboardscreen: bulbTest case 1" in Report 
	Given Navigate to niko login page 
	Given User has entered username "nikotest3@fptl.be" and passoword "Niko123" on niko login page
	When User click on LOGIN button
	Then User redirected to Dashboard page and Dashboard menu highlighted with yellow color in Left side pane
	When User click on bulb button
	Then Bulb's status gets changed 
	

	
Scenario: dimmerTest case 2
	
	Given Enter Testcase Name "Dashboard screen: dimmerTest case 2" in Report 
	Given Navigate to niko login page 
	Given User has entered username "nikotest3@fptl.be" and passoword "Niko123" on niko login page
	When User click on LOGIN button
	Then User redirected to Dashboard page and Dashboard menu highlighted with yellow color in Left side pane
	When User click on dimmer bulb button
	Then Dimmer Bulb's status gets changed and increase(+) / decrease(-) light intensity button is visible
	When User click on increase(+) light intensity button
	Then Dimmer Bulb's light intensity increases 
	When User click on decrease(-) light intensity button
	Then Dimmer Bulb's light intensity decreases 
	
	
Scenario: blindsTest case 3
	
	Given Enter Testcase Name "Dashboard screen: blindsTest case 3" in Report 
	Given Navigate to niko login page 
	Given User has entered username "nikotest3@fptl.be" and passoword "Niko123" on niko login page
	When User click on LOGIN button
	Then User redirected to Dashboard page and Dashboard menu highlighted with yellow color in Left side pane
	When User click on blinds button
	Then Blinds's status gets changed and down / up buttons are visible
	When User click on down button
	Then Blinds become down and pause button appears for while on dashboard screen
	When User click on up button
	Then Blinds become up and pause button appears for while on dashboard screen 
		
Scenario: socketTest case 4
	
	Given Enter Testcase Name "Dashboard screen: socketTest case 4" in Report 
	Given Navigate to niko login page 
	Given User has entered username "nikotest3@fptl.be" and passoword "Niko123" on niko login page
	When User click on LOGIN button
	Then User redirected to Dashboard page and Dashboard menu highlighted with yellow color in Left side pane
	When User click on socket button
	Then Socket's status gets changed 

