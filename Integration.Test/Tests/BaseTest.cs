using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using Humanizer;
using NUnit.Framework;

namespace Integration.Test.Tests
{
    public abstract class BaseTest
    {
        protected static string ScenarioName
                    => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
                    ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty).Humanize();

        private static Logger Logger => Logger.Instance;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.Info("Setup startup config");
        }

        [SetUp]
        public void Setup()
        {
            Logger.Info($"Start scenario [{ScenarioName}]");
        }
    }
}
