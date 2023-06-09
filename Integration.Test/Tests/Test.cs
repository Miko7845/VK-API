using Integration.Forms;
using Integration.Forms.Pages;
using Integration.Test.Extensions;
using Integration.Utilities;
using NUnit.Framework;

namespace Integration.Test.Tests
{
    public class Tests : BaseWebTest
    {
        [Test]
        public void Test()
        {
            var mainPage = new MainPage();
            mainPage.EnterEmailAndLogIn(TestData.Login);

            var authPage = new AuthPage();
            authPage.EnterPasswordAndLogIn(TestData.Password);

            var sideBar = new SideBarForm();
            sideBar.GoToMyProfile();

            var myProfile = new MyProfilePage();
            myProfile.AssertIsPresent();

            string rndText = RandomData.GetRandomString();
            string postId = RequestUtil.PostWall(rndText);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ApiConfig.UserId, myProfile.IsPostExistFromCurrentUser(postId), "Wall post does not exist!");
                Assert.AreEqual(rndText, myProfile.GetPostText(ApiConfig.UserId, postId), "Actual post's text and expected does not match");
            });
            
            rndText = RandomData.GetRandomString();
            string photoId = RequestUtil.EditWall(postId, rndText, TestData.PhotoPath);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(rndText, myProfile.GetPostText(ApiConfig.UserId, postId), "Actual post's text and expected does not match");
                Assert.IsTrue(myProfile.IsCurrentPhotoExist(photoId), "Wall post's photo does not exist!");
            });

            rndText = RandomData.GetRandomString();
            RequestUtil.CreateComment(postId, rndText);
            Assert.AreEqual(rndText, myProfile.GetComment(ApiConfig.UserId, postId), "Actual comment's text and expected does not match");

            myProfile.SendLike(ApiConfig.UserId, postId);
            Assert.AreEqual(ApiConfig.UserId, RequestUtil.GetWaitLike(postId), "The current user's like was not found.");

            RequestUtil.DeleteWall(postId);
            Assert.IsFalse(myProfile.PostIsNotExist(), "The post has not been deleted!");
        }
    }
}