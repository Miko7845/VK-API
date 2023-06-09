using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Integration.Forms.Pages
{
    public class MyProfilePage : Form
    {
        private IList<ILabel> _posts => ElementFactory.FindElements<ILabel>(By.XPath("//div[contains(@id, 'page_wall_posts')]/div[contains(@id, 'post')]"), "All posts");
        private IList<ILabel> _postsText => ElementFactory.FindElements<ILabel>(By.XPath("//div[contains(@class, 'wall_post_cont')]"), "All posts texts");
        private IList<ILabel> _postsPhoto => ElementFactory.FindElements<ILabel>(By.XPath("//div[contains(@class, 'page_post_sized_thumbs')]//a"), "New posts photo");
        private IList<ILabel> _likeButtons => ElementFactory.FindElements<ILabel>(By.XPath("//div[contains(@data-reaction-set-id, 'reactions')]"), "All posts likes");
        private By _comments => By.XPath("//div[contains(@class, 'wall_reply_text')]");
        private By _showComment => By.XPath("//span[contains(@class, 'js-replies_next_label')]");

        public MyProfilePage() : base(By.Id("owner_page_name"), nameof(MyProfilePage)) { }

        public string IsPostExistFromCurrentUser(string postId) => _posts.FirstOrDefault(x => x.GetAttribute("data-post-id").Split('_').Last() == postId).GetAttribute("data-post-id").Split('_').First();
        public bool PostIsNotExist() => _postsText.Any(x => x.State.IsExist != true);
        public string GetPostText(string userId, string postId) => _postsText.FirstOrDefault(x => x.GetAttribute("id") == $"wpt{userId}_{postId}").Text;
        public bool IsCurrentPhotoExist(string photoId) => _postsPhoto.Any(x => x.GetAttribute("href").Split('_').Last() == photoId);
        public void SendLike(string userId, string postId) => _likeButtons.FirstOrDefault(x => x.GetAttribute("data-reaction-target-object") == $"wall{userId}_{postId}").ClickAndWait();

        public string GetComment(string userId, string postId)
        {
            foreach (var item in _posts)
                if (item.GetAttribute("data-post-id") == $"{userId}_{postId}")
                {
                    item.FindChildElement<IButton>(_showComment, "Show comments button").Click();
                    return item.FindChildElement<ILabel>(_comments, "All comments").Text;
                }       
            return "";
        }  
    }
}
