#  C# Automation Assessment - Ram Simha Prabhanjan

##  Demo Websites Used
- **UI**: [https://demoqa.com/automation-practice-form](https://demoqa.com/automation-practice-form)  
- **API**: [https://reqres.in](https://reqres.in)

---

##  What's in the Project?

###  Folder Structure

- **Api/**  
  Contains Refit interface definitions for `GET` and `POST` API operations.

- **AutomationTest/**  
  Contains 4 test cases (2 UI and 2 API tests), all inheriting from a fixture base class.  
  - Uses NUnit test tags for test categorization.

- **Data/**  
  CSV file with test data for the registration form.

- **Fixture/**  
  `ProjectFixture` class handles browser setup, navigation, and teardown using Playwright.

- **Pages/**  
  `RegisterPage` implements the Page Object Model (POM) pattern with locators and methods for UI interaction.

- **TestDataModel/**  
  Contains POCO classes like `RegisterUserModel` for binding test data with getter/setter properties.

- **Utils/**  
  - Base URLs and constants  
  - CSV parsing helper (`ReadTestData`) using CsvHelper

- **appsettings.json**  
  Configuration file for URLs and other static values.

- **AutomationAssessment.csproj**  
  Defines dependencies including Playwright, Refit, CsvHelper, and NUnit.

---

##  Requirements

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download)
- PowerShell (to run Playwright installation script)

---

##  Setup & Test Execution

```bash
dotnet build
pwsh bin/Debug/net9.0/playwright.ps1 install
dotnet test
