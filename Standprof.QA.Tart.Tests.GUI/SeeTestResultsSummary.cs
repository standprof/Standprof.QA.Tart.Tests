using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

//using OpenQA.Selenium.Remote;

//using OpenQA.Selenium.Appium.Windows;
//using OpenQA.Selenium.Remote;

namespace Standprof.QA.Tart.Tests.GUI
{
    [TestClass]
    public class SeeTestResultsSummary:DashboardSession
    {
        [TestMethod]
        public void PassedTestsFound()
        {
            // Search for test results
            session.FindElementByName("Search").Click();
            var dataGridView = session.FindElementsByName("DataGridView")[0];

            // Check that the amount of passed tests is greater than 0

            var row0 = dataGridView.FindElementByName("Row 0");
            Assert.IsNotNull(row0);

            var amountCell = row0.FindElementByName("Amount Row 0");
            Assert.IsTrue(Convert.ToInt16(amountCell.Text) > 0);
        }

        [TestMethod]
        public void FilterByTextFindsAtLeastOneTest()
        {
            // Search for test results
            session.FindElementByName("Search").Click();
            var dataGridView = session.FindElementsByName("DataGridView")[1];

            // Filter the search results by text
            var textForFilter = "email";

            session.FindElementByName("Any text:").SendKeys(textForFilter);
            session.FindElementByName("Filter").Click();


            // Check there is at least one record with the filter text
            var testSummaryInRow0 = dataGridView.FindElementByName("TestSummary Row 0");
            Assert.IsNotNull(testSummaryInRow0);

            var isSubstringFound = testSummaryInRow0.Text.IndexOf(textForFilter, 0, StringComparison.OrdinalIgnoreCase) != -1;
            Assert.IsTrue(isSubstringFound);

        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}
