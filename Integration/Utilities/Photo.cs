using Integration.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Integration.Utilities
{
    internal static class Photo
    {
        public static string GetPhotoId(string photoPath)
        {
            var result = UploadPhotoOnTheWall(GetWallPhotoUploadServer(), photoPath);
            return SaveUploadWallPhoto(result);
        }

        private static string GetWallPhotoUploadServer()
        {
            RestResponse response = RequestUtil.Client.Get(new RestRequest(Endpoints.GetPhotoServer)
                .AddParameter("user_id", ApiConfig.UserId)
                .AddParameter(RequestUtil.version, ApiConfig.Version));
            return (string)JObject.Parse(response.Content).SelectToken("response.upload_url");
        }

        private static SavePhotoModel UploadPhotoOnTheWall(string url, string filePath)
        {
            RestResponse response = RequestUtil.Client.Post(new RestRequest(url).AddFile("photo", filePath, "multipart/form-data"));

            var model = new SavePhotoModel();
            model.Server = (string)JObject.Parse(response.Content).SelectToken("server");
            model.Photo = (string)JObject.Parse(response.Content).SelectToken("photo");
            model.Hash = (string)JObject.Parse(response.Content).SelectToken("hash");

            return model;
        }

        private static string SaveUploadWallPhoto(SavePhotoModel model)
        {
            RestResponse response = RequestUtil.Client.Post(new RestRequest(Endpoints.SaveWallPhoto)
                .AddParameter("user_id", ApiConfig.UserId)
                .AddParameter("photo", model.Photo)
                .AddParameter("server", model.Server)
                .AddParameter("hash", model.Hash)
                .AddParameter(RequestUtil.version, ApiConfig.Version));
            return (string)JObject.Parse(response.Content).SelectToken("response[0].id");
        } 
    }
}
