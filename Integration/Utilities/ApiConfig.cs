using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using System.Reflection;

namespace Integration.Utilities
{
    public static class ApiConfig
    {
        private static ISettingsFile _config => new JsonSettingsFile($"Resources.Api.config.json", Assembly.GetCallingAssembly());
        private static ISettingsFile _options => new JsonSettingsFile($"Resources.Api.options.json", Assembly.GetCallingAssembly());

        public static string UserId => _config.GetValue<string>("userId");
        public static string Token => _config.GetValue<string>("token");
        public static string Version => _config.GetValue<string>("version");

        public static string Open_comments => _options.GetValue<string>("open_comments");
    }
}
