using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Standprof.QA.Tart.Tests.GUI
{
    public class DashboardSession
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub";
        private const string DashboardAppId = @"C:\_repo\_Personal\_Standprof\Tools\Tart.Dashboard\TestResultsDashboard.exe";

        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context)
        {
            // Launch a new instance of Notepad application
            if (session == null)
            {
                // Create a new session to launch Dashboard application
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", DashboardAppId);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);

                // Verify that Notepad is started with untitled new file
                Assert.AreEqual("Standprof - Automated Test Results Dashboard", session.Title);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                var searchButton = session.FindElementByName("Search");
                Assert.IsNotNull(searchButton);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Close();

                try
                {
                    // Dismiss Save dialog if it is blocking the exit
                    session.FindElementByName("OK").Click();
                }
                catch { }

                session.Quit();
                session = null;
            }
        }

        protected static string SanitizeBackslashes(string input) => input.Replace("\\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt);
    }
}
