using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using System.Reflection;

namespace Integration.Utilities
{
    public class Endpoints
    {
        private static ISettingsFile _endpoints => new JsonSettingsFile($"Resources.Api.Endpoints.json", Assembly.GetCallingAssembly());

        public static string WallPost => _endpoints.GetValue<string>("wallPost");
        public static string WallEdit => _endpoints.GetValue<string>("wallEdit");
        public static string GetPhotoServer => _endpoints.GetValue<string>("getPhotoServer");
        public static string SaveWallPhoto => _endpoints.GetValue<string>("saveWallPhoto");
        public static string WallCreateComment => _endpoints.GetValue<string>("wallCreateComment");
        public static string WallGetLikes => _endpoints.GetValue<string>("wallGetLikes"); 
        public static string WallDelete => _endpoints.GetValue<string>("wallDelete");
    }
}
