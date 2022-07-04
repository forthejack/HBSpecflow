Feature: AddToCartChrome

@login
Scenario Outline: Add an item to cart
	Given I have navigated to the application on Browser
	| Browser |
	| <Browser> |
	And I see the application opened
	When I enter UserName
	| UserName                |
	| mail comes here |
	Then I click Namelogin button
	When I enter Password
	| Password |
	| password comes here |
	Then I click Passwordlogin button
	Then I should see username
	When I search for an Item
	| Item         |
	| cep telefonu |
	Then I see results
	Then I sort by option
	Then I select the bottom item
	Then I select the lowest score
	Then I should see the item in cart

	Examples: 
		| Browser |
		| chrome  |