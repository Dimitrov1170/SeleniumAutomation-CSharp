using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumAutomation.Pages;

namespace SeleniumAutomation.Tests
{
    public class LoginTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void ValidLoginTest()
        {
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClickLogin();

            Assert.That(driver.Url, Does.Contain("inventory"));
        }

        [Test]
        public void InvalidLoginTest()
        {
            loginPage.EnterUsername("locked_out_user");
            loginPage.EnterPassword("wrong_password");
            loginPage.ClickLogin();

            Assert.That(loginPage.GetErrorMessage(), Does.Contain("Epic sadface: Username and password do not match"));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
