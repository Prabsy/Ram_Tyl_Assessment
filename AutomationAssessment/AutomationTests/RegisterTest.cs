using NUnit.Framework;
using System.Threading.Tasks;
using AutomationAssessment.Fixtures;
using AutomationAssessment.Pages;
using Microsoft.Playwright;
using AutomationAssessment.Utils;
using Refit;
using NUnit.Allure.Attributes;
using Allure.Commons;
using NUnit.Allure.Core;


namespace AutomationAssessment.AutomationTests
{
    [AllureNUnit]
    [AllureSuite("Register Form tests")]
    [AllureEpic("User Registration Test class")]
    public class ExampleTest : ProjectFixture
    {
        private RegisterPage _registerPage;
        private IUserApi _userApi;
        //calling the ReadTestData class to read the test data

        [SetUp]
        public async Task InitPage()
        {
            _registerPage = new RegisterPage(page);

        }

        [Test]
        [Category("Demo")]
        public async Task DemoQaTitleTest()
        {
            await _registerPage.NavigateBaseUrl();
            // Wait for the page to load
            await page.WaitForLoadStateAsync(LoadState.Load, options: new PageWaitForLoadStateOptions
            {
                Timeout = 60000 // Set a timeout for the load state
            });
            // await page.WaitForTimeoutAsync(2000);
            var title = await _registerPage.GetPageTitle();
            Assert.That(title, Does.Contain("DEMOQA"));

            RegisterUserModel user = new RegisterUserModel();
            user.FirstName = "John";
            user.LastName = "Doe";
            Console.WriteLine($"User First Name: {user.FirstName}");
            Console.WriteLine($"User Last Name: {user.LastName}");
        }

        [Test, TestCaseSource(typeof(ReadTestData), nameof(ReadTestData.ParseTestData))]
        [AllureTag("DemoTests")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Positive case Filling the Form")]
        public async Task TestRegisterUser(RegisterUserModel testUser)
        {
            await _registerPage.NavigateBaseUrl();
            // Wait for the page to load
            await page.WaitForLoadStateAsync(LoadState.Load, options: new PageWaitForLoadStateOptions
            {
                Timeout = 60000 // Set a timeout for the load state
            });

            // Reading Test data from CSV file
            //var testUsers = ReadTestData.ParseTestData("Data/test_data.csv");

            // Fill the registration form with test data
            await _registerPage.EnterFirstName(testUser.FirstName);
            await _registerPage.EnterLastName(testUser.LastName);
            await _registerPage.EnterUserEmail(testUser.Email);
            await _registerPage.SelectGender(testUser.Gender);
            await _registerPage.EnterMobileNumber(testUser.MobileNumber);
            // await _registerPage.SelectDateOfBirth(testUsers[0].DateOfBirth);
            foreach (var sub in testUser.Subjects.ToString().Split(';'))
            {

                await _registerPage.EnterSubjects(sub);
                await _registerPage.PressTab();
            }
            foreach (var hobby in testUser.Hobbies.ToString().Split(';'))
            {
                await _registerPage.SelectHobbies(hobby);
            }
            // Upload a file if the path is provided
            if (!string.IsNullOrEmpty(testUser.PicturePath))
            {
                await _registerPage.UploadFileAsync(testUser.PicturePath);
            }
            await _registerPage.EnterCurrentAddress(testUser.CurrentAddress);
            await _registerPage.SelectState(testUser.State);
            await _registerPage.SelectCity(testUser.City);
            // Submit the form
            await _registerPage.SubmitForm();
            // Wait for the form submission to complete
            await page.WaitForLoadStateAsync(LoadState.Load);
            // Verify the success message
            var successMessage = await _registerPage.GetSuccessMessage();
            Assert.That(successMessage, Does.Contain("Thanks for submitting the form"));

        }

        [Test]
        [Category("ApiDemo")]
        public async Task TestApiUserCreation()
        {
            //This test will create a user using the API and verify the response

            var testUsers = ReadTestData.ParseTestData();
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigManager.ApiBaseUrl) // Use the API base URL from the config`
            };

            httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");

            _userApi = RestService.For<IUserApi>(httpClient);

            var createdUser = await _userApi.CreateUserAsync(testUsers[0]);

            var statusCode = createdUser.StatusCode;
            Assert.That(statusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), "User creation failed with status code: " + statusCode);
            Assert.That(createdUser.IsSuccessStatusCode, Is.True, "User creation failed");


            if (createdUser.IsSuccessStatusCode)
            {
                var userDetails = createdUser.Content;
                if (userDetails != null)
                {
                    Console.WriteLine($"Created user: {userDetails.FirstName} {userDetails.LastName}");
                }
                else
                {
                    Console.WriteLine("User details are null.");
                }
                // Print the raw JSON response for debugging
                var rawJson = Newtonsoft.Json.JsonConvert.SerializeObject(createdUser.Content);
                Console.WriteLine("Raw JSON Response:\n" + rawJson);
            }

            await Task.CompletedTask;
        }


        [Test]
        [Category("ApiDemo")]
        // This test will fetch a user by ID using the API and verify the response
        public async Task TestApiGetUserById()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigManager.ApiBaseUrl) // Use the API base URL from the config`
            };

            httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");

            _userApi = RestService.For<IUserApi>(httpClient);
            try
            {
                var user = await _userApi.GetUserByIdAsync(1);
                // Use user
            }
            catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("User not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            await Task.CompletedTask;

        }


    }  
}
