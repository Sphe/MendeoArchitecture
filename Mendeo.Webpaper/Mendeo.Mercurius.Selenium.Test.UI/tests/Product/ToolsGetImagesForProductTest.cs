using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using Mendeo.Mercurius.Service.Test.Utils;
using System.Diagnostics;
using Mendeo.Mercurius.Dto;
using OpenQA.Selenium;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Mendeo.Mercurius.Selenium.Test.UI.Tests.Product
{
    [TestClass]
    public class ToolsGetImagesForProductTest
   { 
        private FirefoxDriver driver;
        private GenerateRandomProductUtil gen;
        private int NumberOfImageToGet = 1;
        private static string url = "https://images.google.com/?gws_rd=ssl";

        [TestInitialize]
        public void Setup()
        {
            Debug.WriteLine("Intiating tests...");
            //FirefoxProfile fxProfile = new FirefoxProfile();
            //fxProfile.SetPreference("browser.download.folderList", 2);
            //fxProfile.SetPreference("browser.download.manager.showWhenStarting", false);
            //fxProfile.SetPreference("browser.download.dir", @"D:\Mercurius.Images\UITestImages\test automation\test");
            //fxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "image/jpeg");
            //driver = new FirefoxDriver(fxProfile);
            driver = new FirefoxDriver();
            gen = new GenerateRandomProductUtil(true, false, false, GenerateRandomProductUtil.uiTestCall);
        }

        [TestMethod]
        public void FillProductForm()
        {
            Debug.WriteLine(string.Concat("launching Firefox and go to url:", url));
            driver.Navigate().GoToUrl(url);

            if (NumberOfImageToGet > 0)
            {
                Debug.WriteLine(string.Concat("Test automation will get ", NumberOfImageToGet, " per product..."));
                for (int i = 1; i < NumberOfImageToGet + 1; i++)
                {
                    Debug.WriteLine("Generating a product...");
                    ProductDto aProduct = new ProductDto();
                    aProduct = gen.GenerateProduct(i, NumberOfImageToGet);

                    Debug.WriteLine(string.Concat("Get image ", i.ToString(), "/", NumberOfImageToGet.ToString(), " for product : ", aProduct.Name));

                    driver.FindElementById("lst-ib").SendKeys(aProduct.Name);
                    driver.Keyboard.SendKeys(Keys.Return);

                    ///XPath   //*[@id=\"rg_s\"]/div[1]/a/img
                    driver.FindElementByXPath("//*[@id=\"rg_s\"]/div[1]/a/img").Click();

                    //display image
                    //driver.FindElementByXPath("//*[@id=\"irc_cc\"]/div[3]/div[3]/div/div[2]/table[1]/tbody/tr/td[2]/a/span").Click();
                    //string imageData = driver.FindElementByXPath("/html/body/img").GetAttribute("src");
                    //Debug.WriteLine(string.Concat("-------------->", imageData));

                    //get source image from display image
                    string imageData = driver.FindElementByXPath("//*[@id=\"irc_cc\"]/div[3]/div[3]/div/div[2]/table[1]/tbody/tr/td[2]/a").GetAttribute("data-href");
                    Debug.WriteLine(string.Concat("-------------->", imageData));
                }

            }
            else
            {
                Debug.WriteLine(string.Concat("product = ", NumberOfImageToGet, " no images get"));
            }

        }

        private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Png);
        }

        [TestCleanup]
        public void TearDown()
        {
            Debug.WriteLine("Exiting...");
            driver.Quit();
            gen.CloseExcelApi();
        }

    }
}
