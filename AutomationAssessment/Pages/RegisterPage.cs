using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationAssessment.Utils;
using Microsoft.VisualBasic;

namespace AutomationAssessment.Pages
{
    public class RegisterPage
    {
        private IPage _page;
        private readonly ILocator firstNameInput;
        private readonly ILocator lastNameInput;
        private readonly ILocator userEmailInput;
        private readonly ILocator maleRadioButton;
        private readonly ILocator femaleRadioButton;
        private readonly ILocator otherRadioButton;
        private readonly ILocator mobileInput;
        private readonly ILocator dobInput;
        private readonly ILocator subjectsContainer;
        private readonly ILocator sportsCheckbox;
        private readonly ILocator readingCheckbox;
        private readonly ILocator musicCheckbox;

        private readonly ILocator fileInput;
        private readonly ILocator currentAddressInput;
        private readonly ILocator stateDropdown;
        private readonly ILocator cityDropdown;
        private readonly ILocator submitButton;

        private readonly ILocator submitSuccessMessage;
        public RegisterPage(IPage page)
        {
            _page = page;
            firstNameInput = _page.Locator("//input[@id='firstName']");
            lastNameInput = _page.Locator("//input[@id='lastName']");
            userEmailInput = _page.Locator("//input[@id='userEmail']");
            maleRadioButton = _page.Locator("//label[@for='gender-radio-1']");
            femaleRadioButton = _page.Locator("//label[@for='gender-radio-2']");
            otherRadioButton = _page.Locator("//label[@for='gender-radio-3']");
            mobileInput = _page.Locator("//input[@id='userNumber']");
            dobInput = _page.Locator("//input[@id='dateOfBirthInput']");
            subjectsContainer = _page.Locator("//input[@id='subjectsInput']");
            sportsCheckbox = _page.Locator("//label[@for='hobbies-checkbox-1']");
            readingCheckbox = _page.Locator("//label[@for='hobbies-checkbox-2']");
            musicCheckbox = _page.Locator("//label[@for='hobbies-checkbox-3']");
            fileInput =  _page.Locator("//input[@id='uploadPicture']");
            currentAddressInput = _page.Locator("//textarea[@id='currentAddress']");
            stateDropdown = _page.Locator("//div[@id='state']");
            cityDropdown = _page.Locator("//div[@id='city']");
            submitButton = _page.Locator("//button[@id='submit']");            
            submitSuccessMessage = _page.Locator("//div[contains(text(), 'Thanks for submitting the form')]");

        }

        public async Task NavigateBaseUrl()
        {
            await _page.GotoAsync(ConfigManager.BaseUrl);
        }

        public async Task<string> GetPageTitle()
        {
            return await _page.TitleAsync();
        }

        //enter first name
        public async Task EnterFirstName(string firstName)
        {
            await firstNameInput.FillAsync(firstName);
        }

        //enter last name
        public async Task EnterLastName(string lastName)
        {
            await lastNameInput.FillAsync(lastName);
        }

        //enter user email
        public async Task EnterUserEmail(string email)
        {
            await userEmailInput.FillAsync(email);
        }
        //select radio button
        public async Task SelectGender(string Gender)
        {
            if (Gender.Equals("male", StringComparison.CurrentCultureIgnoreCase))
                await maleRadioButton.ClickAsync();

            else if (Gender.Equals("female", StringComparison.CurrentCultureIgnoreCase))
                await femaleRadioButton.ClickAsync();

            else
                await otherRadioButton.ClickAsync();

        }
        //enter mobile number
        public async Task EnterMobileNumber(string mobileNumber)
        {
            await mobileInput.FillAsync(mobileNumber);
        }
        //enter date of birth
        public async Task EnterDateOfBirth(string date)
        {
            await dobInput.FillAsync(date);
        }
        //enter subjects
        public async Task EnterSubjects(string subject)
        {
            await subjectsContainer.ClickAsync();
            await _page.WaitForTimeoutAsync(600);
            await subjectsContainer.FillAsync(subject);
            await _page.WaitForTimeoutAsync(600);
            await _page.Keyboard.PressAsync("Tab");
        }

        public Task PressTab()
        {
            return _page.Keyboard.PressAsync("Tab");
        }

        //select hobbies
        public async Task SelectHobbies(string hobby)
        {
            switch (hobby.ToLower())
            {
                case "sports":
                    await sportsCheckbox.CheckAsync();
                    break;
                case "reading":
                    await readingCheckbox.CheckAsync();
                    break;
                case "music":
                    await musicCheckbox.CheckAsync();
                    break;
                default:
                    throw new ArgumentException("Invalid hobby specified.");
            }
        }

        public async Task UploadFileAsync(string filePath)
        {
            
            await fileInput.SetInputFilesAsync(filePath);
        }

        //enter current address
        public async Task EnterCurrentAddress(string address)
        {
            await currentAddressInput.FillAsync(address);
        }
        //select state
        public async Task SelectState(string state)
        {
            await stateDropdown.ClickAsync();
            var stateOption = _page.Locator($"//div[text()='{state}']");
            await stateOption.ClickAsync();
        }
        //select city
        public async Task SelectCity(string city)
        {
            await cityDropdown.ClickAsync();
            var cityOption = _page.Locator($"//div[text()='{city}']");
            await cityOption.ClickAsync();
        }
        //submit the form
        public async Task SubmitForm()
        {
            await submitButton.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.Load);
        }

        //get success message
        public async Task<string> GetSuccessMessage()
        {
            await submitSuccessMessage.WaitForAsync(new LocatorWaitForOptions { Timeout = 5000 });
            return await submitSuccessMessage.InnerTextAsync();
        }
    }
}
