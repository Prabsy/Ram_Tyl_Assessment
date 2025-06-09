using Microsoft.Playwright;
using NUnit.Framework;
using AutomationAssessment.Utils;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using Allure.Commons;

namespace AutomationAssessment.Fixtures
{
    public class ProjectFixture
    {
        protected IPlaywright playwright;
        protected IBrowser browser;
        protected IBrowserContext context;
        protected IPage page;

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            Console.WriteLine("Starting Playwright...");
            playwright = await Playwright.CreateAsync();
            
            browser = ConfigManager.Browser.ToLower() switch
            {
                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless }),
                "webkit" => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless }),
                _ => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless } ),
            };
        }

        [SetUp]
        public async Task TestSetup()
        {
            

            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
            page.SetDefaultTimeout(60000); // Set default timeout for all actions
            page.SetDefaultNavigationTimeout(60000); // Set default navigation timeout
        }

        [TearDown]
        public async Task TestTeardown()
        {
            Console.WriteLine("Starting TestTeardown...");
            //if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            var testName = TestContext.CurrentContext.Test.Name;                
            var screenshotPath = testName+"_screenshot.png";
            page.ScreenshotAsync(new() { Path = screenshotPath }).Wait();
            AllureLifecycle.Instance.AddAttachment("Screenshot", "image/png", screenshotPath);
                
            await context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTeardown()
        {
            Console.WriteLine("Starting GlobalTeardown...");
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
