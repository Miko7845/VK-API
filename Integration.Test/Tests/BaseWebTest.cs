using Aquality.Selenium.Browsers;
using Integration.Configurations;
using NUnit.Framework;

namespace Integration.Test.Tests
{
    public abstract class BaseWebTest : BaseTest
    {
        public static void SetScreenExpansionMaximize() => AqualityServices.Browser.Maximize();
        public static void GoToPageStartPage() => AqualityServices.Browser.GoTo(Configuration.StartUrl);

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
                AqualityServices.Browser.Quit();
        }

        [SetUp]
        public new void Setup()
        {
            GoToPageStartPage();
            SetScreenExpansionMaximize();
        }
    }
}
