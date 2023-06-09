using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Integration.Forms.Pages
{
    public class AuthPage : Form
    {
        private ITextBox _passwordField => ElementFactory.GetTextBox(By.Name("password"), "Password text box");
        private IButton _signInButton => ElementFactory.GetButton(By.XPath("//button[contains(@type, 'submit')]//span[contains(@class, 'in')]"), "Sign in button");

        public AuthPage() : base(By.Name("password"), nameof(AuthPage)) { }

        public void SetPassword(string pass) => _passwordField.SendKeys(pass);
        public void SignIn() => _signInButton.Click();
        public void EnterPasswordAndLogIn(string pass)
        {
            SetPassword(pass);
            SignIn();
        }
    }
}
