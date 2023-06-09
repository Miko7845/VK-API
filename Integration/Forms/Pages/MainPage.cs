using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Integration.Forms.Pages
{
    public class MainPage : Form
    {
        private ITextBox _emailField => ElementFactory.GetTextBox(By.Id("index_email"), "Email text box");
        private IButton _signInButton => ElementFactory.GetButton(By.XPath("//button[contains(@type, 'submit')]//span[contains(@class, 'in')]"), "Sign in button");

        public MainPage() : base(By.Id("index_login"), nameof(MainPage)) { }

        public void SetEmail(string email) => _emailField.SendKeys(email);
        public void SignIn() => _signInButton.Click();
        public void EnterEmailAndLogIn(string email)
        {
            SetEmail(email);
            SignIn();
        }
    }
}
