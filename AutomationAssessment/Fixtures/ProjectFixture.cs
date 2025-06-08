using Microsoft.Playwright;
using NUnit.Framework;
using AutomationAssessment.Utils;
using System.Threading.Tasks;

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
            playwright = await Playwright.CreateAsync();

            browser = ConfigManager.Browser.ToLower() switch
            {
                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless }),
                "webkit" => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless }),
                _ => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless }),
            };
        }

        [SetUp]
        public async Task TestSetup()
        {
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
        }

        [TearDown]
        public async Task TestTeardown()
        {
            await context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTeardown()
        {
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
