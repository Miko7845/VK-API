using Aquality.Selenium.Browsers;
using Integration.Configurations;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace Integration.Utilities
{
    public static class RequestUtil
    {
        public const string version = "v";

        internal static readonly RestClient Client = new RestClient(Configuration.ApiUrl)
        {
            Authenticator = new JwtAuthenticator(ApiConfig.Token),
        };

        public static string PostWall(string message)
        {
            RestResponse response = Client.Post(new RestRequest(Endpoints.WallPost)
                .AddParameter("close_comments", ApiConfig.Open_comments)
                .AddParameter("message", message)
                .AddParameter(version, ApiConfig.Version));

            return (string)JObject.Parse(response.Content).SelectToken("response.post_id");
        }

        public static string EditWall(string postId, string message, string photoPath)
        {
            var photoId = Photo.GetPhotoId(photoPath);

            Client.Post(new RestRequest(Endpoints.WallEdit)
                .AddParameter("post_id", postId)
                .AddParameter("message", message)
                .AddParameter("attachments", $"photo{ApiConfig.UserId}_{photoId}")
                .AddParameter(version, ApiConfig.Version));

            return photoId;
        }

        public static void CreateComment(string postId, string message)
        {
            Client.Post(new RestRequest(Endpoints.WallCreateComment)
                .AddParameter("owner_id", ApiConfig.UserId)
                .AddParameter("post_id", postId)
                .AddParameter("message", message)
                .AddParameter(version, ApiConfig.Version));
        }

        public static string GetLikes(string postId)
        {
            RestResponse response = Client.Get(new RestRequest(Endpoints.WallGetLikes)
                .AddParameter("owner_id", ApiConfig.UserId)
                .AddParameter("post_id", postId)
                .AddParameter(version, ApiConfig.Version));

            return (string)JObject.Parse(response.Content).SelectToken("response.users[0].uid");
        }

        public static string GetWaitLike(string postId)
        {
            AqualityServices.ConditionalWait.WaitFor(_ => GetLikes(postId) != null, pollingInterval: TimeSpan.FromSeconds(5));
            return GetLikes(postId);
        }

        public static void DeleteWall(string postId)
        {
            Client.Post(new RestRequest(Endpoints.WallDelete)
                .AddParameter("owner_id", ApiConfig.UserId)
                .AddParameter("post_id", postId)
                .AddParameter(version, ApiConfig.Version));
        }
    }
}
