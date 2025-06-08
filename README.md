# Ram Simha Prabhanjan's C# Initial Automation Assessment 

## Demo Website used 
UI: https://demoqa.com/automation-practice-form 
API: https://reqres.in 




### Whats in the project?
Api folder-> Refit inerface class for GET and POST implementation 
AutomationTest-> 4 Tests that inherits Fixture class to use setup and teardown methods
				 Tags for differenciating tests
				 2 UI using Playwright and 2API tests using Refit pkg
Data-> csv file: Test data for user form 
Fixture-> ProjectFixture class to setup browser, launch url , teardown
Pages-> RegisterPage class that is used for POM- Page object model , all locators respective to page and action methods
TestDataModel-> Getters and Setters class for test data access
Utils-> baseurl, static address and values , 
        Data read util class, which reads data from csv which serves for test
appsettings.json -> urls
AutomationAssessment.csproj-> dependencies and rules 



## Requirements
i have used .NET 9.0
Powershell for installing Playwright browsers and other components



## Setup and test exection

```
dotnet build
pwsh bin/Debug/net9.0/playwright.ps1 install
dotnet test

```

##Pending work
Parameterize tests
add more negative tests
Allure reporting for tests
PUT and DELETE methods demo using Refit


