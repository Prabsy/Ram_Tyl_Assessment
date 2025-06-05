using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

public class TestBase : IAsyncLifetime
{
    protected IPlaywright _playwright;
    protected IBrowser _browser;
    protected IPage _page;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        _page = await _browser.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await _page.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
