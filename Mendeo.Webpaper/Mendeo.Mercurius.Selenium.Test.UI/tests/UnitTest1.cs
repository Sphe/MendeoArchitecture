using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace Mendeo.Mercurius.Selenium.Test.UI
{
    [TestClass]
    public class UnitTest1
    {
        FirefoxDriver driver;
        string debugPrefix = "[MercuriusTests]: ";

        /// <summary>
        /// This initialize the web driver firefox and open ups the page to add product
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            Debug.WriteLine(debugPrefix+ "launching Firefox...");
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:15670/addProduct");

            Excel.Worksheet seet = ThisWorkbook.Sheets["Sheet1"] as Excel.Worksheet;
        }

        // This is the test to be carried out.
        [TestMethod]
        public void FillProductForm()
        {
            
            driver.FindElement(By.Id("product-name")).SendKeys("My Super Product");
            driver.FindElement(By.Id("product-one_line_description")).SendKeys("This is a magnificent one line product description.");
            driver.FindElement(By.Id("product-price")).SendKeys("999");
            driver.FindElement(By.Id("product-one_line_address")).SendKeys("12 Rue Saint-Bernard, 75011 Paris, France");

            //driver.FindElement(By.Id("gbqfq")).SendKeys("Google");
            //driver.FindElement(By.Id("gbqfq")).SendKeys(Keys.Enter);
        }

        // This closes the driver down after the test has finished.
        [TestCleanup]
        public void TearDown()
        {
            //driver.Quit();
        }
    }
}
