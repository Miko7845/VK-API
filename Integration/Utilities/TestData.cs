using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using System.Reflection;

namespace Integration.Utilities
{
    public class TestData
    {
        private static ISettingsFile _testingData => new JsonSettingsFile($"Resources.TestData.json", Assembly.GetCallingAssembly());

        public static string Login => _testingData.GetValue<string>("login");
        public static string Password => _testingData.GetValue<string>("password");
        public static string PhotoPath => Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Files\pik.jpg");
    }
}
