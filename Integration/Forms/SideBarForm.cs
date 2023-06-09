using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Integration.Forms
{
    public class SideBarForm : Form
    {
        private IButton _myPrifileButton => ElementFactory.GetButton(By.Id("l_pr"), "My profile button");
        private IButton _newsButton => ElementFactory.GetButton(By.Id("l_nwsf"), "News button");

        public SideBarForm() : base(By.Id("side_bar"), nameof(SideBarForm)) { }

        public void GoToMyProfile() => _myPrifileButton.Click();
    }
}
