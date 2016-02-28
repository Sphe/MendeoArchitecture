using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using Mendeo.Mercurius.Selenium.Test.UI.utils;
using System.Threading;
using Mendeo.Mercurius.Service.Test.Utils;
using Mendeo.Mercurius.Dto;

namespace Mendeo.Mercurius.Selenium.Test.UI
{
    /// <summary>
    /// This class is used in order to fill database with many products !
    /// There is no assert to test correct insertion !
    /// </summary>
    [TestClass]
    public class AddRandomProductUITest
    {
        private FirefoxDriver driver;
        private string debugPrefix = "[MercuriusTests]: ";


        private string address = null;
        private string[] splittedAddressWord = null;
        private char[] splittedAddressChar = null;
        private string productLongDesc = null;
        private string[] splittedProductLongDescWord = null;
        private char[] splittedProductLongDescChar = null;

        /// These below are the setup parameters values
        private static string pTargetEnvIIS = "IIS";
        private static string pTargetEnvVS = "VS";
        private static string pTargetEnvPROD = "PROD";

        private static string pAddressFillModeChar = "charMode";
        private static string pAddressFillModeWord = "wordMode";
        private static string pAddressFillModeAtOnce = "AtOnce";

        private static string pLongDescFillModeChar = "charMode";
        private static string pLongDescFillModeWord = "wordMode";
        private static string pLongDescFillModeAtOnce = "AtOnce";
        private static string pLongDescGenerateShortOne = "GenerateAnotherOne";

        private static string pDataModeFull = "full";
        private static string pDataModeSample = "sample";


        GenerateRandomProductUtil gen = null;

        /// <summary>
        /// These setup parameters variables below setup the test case
        /// </summary>
        private static int pLongFillThreadSleepDuration = 2;
        private string setTargetEnv = pTargetEnvPROD;
        private string setAddressFillMode = pAddressFillModeChar;
        private string setLongDescFillMode = pLongDescGenerateShortOne;


        private static Boolean isLoggingDebug = true;
        private static Boolean isLoggingVerbose = false;
        private static Boolean isLoggingProductFullDetailsOnCreation = false;
        
        
        private static int numberOfProductToGenerate = 1;
 

        /// This initialize the web driver firefox and open ups the page to add product
        [TestInitialize]
        public void Setup()
        {
            LaunchFirefox();
            //driver.FindElement(By.LinkText("Add Product")).Click();
            gen = new GenerateRandomProductUtil(isLoggingDebug, isLoggingVerbose, isLoggingProductFullDetailsOnCreation, GenerateRandomProductUtil.uiTestCall);
            Wait(500);
        }

        // This is the test to be carried out.
        [TestMethod]
        public void FillProductForm()
        {

            for (int productNb = 1; productNb < numberOfProductToGenerate + 1; productNb++)
            {
                ProductDto aProduct = new ProductDto();
                aProduct = gen.GenerateProduct(productNb, numberOfProductToGenerate);

                
                /// product name
                driver.FindElement(By.Id("product-name")).Click();
                driver.FindElement(By.Id("product-name")).SendKeys(aProduct.Name);

                /// product one line description
                driver.FindElement(By.Id("product-one_line_description")).SendKeys(aProduct.ShortDescription);

                /// product price
                driver.FindElement(By.Id("product-price")).SendKeys(aProduct.Price.ToString());

                /// Need to split the address in order to be sure that the correct one will be entered (instead of sendkeys at once is too fast: //driver.FindElement(By.Id("product-one_line_address")).SendKeys("12 Rue Saint-Bernard, 75011 Paris, France");)
                address = aProduct.AdressOneLine;
                if (setAddressFillMode.Equals(pAddressFillModeAtOnce))
                {
                    driver.FindElement(By.Id("product-one_line_address")).SendKeys(address);
                }
                else if (setAddressFillMode.Equals(pAddressFillModeWord))
                {
                    splittedAddressWord = address.Split(' ');
                    foreach (var item in splittedAddressWord)
                    {
                        driver.FindElement(By.Id("product-one_line_address")).SendKeys(item + " ");
                        if (pLongFillThreadSleepDuration > 0)
                            Thread.Sleep(pLongFillThreadSleepDuration);
                    }
                }
                else if (setAddressFillMode.Equals(pAddressFillModeChar))
                {
                    splittedAddressChar = address.ToCharArray();
                    for (int z = 0; z < splittedAddressChar.Length; z++)
                    {
                        driver.FindElement(By.Id("product-one_line_address")).SendKeys(splittedAddressChar.GetValue(z).ToString());
                        if (pLongFillThreadSleepDuration > 0)
                            Thread.Sleep(pLongFillThreadSleepDuration);
                    }
                }


                /// validate selection of address
                //driver.Keyboard.SendKeys(Keys.ArrowDown);
                //driver.Keyboard.SendKeys(Keys.Return);
                driver.Keyboard.SendKeys(Keys.Tab);

                /// Select the category after searching the correct category in the tree deph, need to tab 3 times to be in the corresponding checkbox
                driver.FindElement(By.Id("product-category")).SendKeys(gen.GetCurrentGeneratedCategoryName());
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Space);             /// Select the corresponding category

                /// Need to send a TAB key as we don't select the generated input, from the tree to the caracteristics field
                /// //driver.FindElement(By.Id("product-caracteristics")).Click(); //8
                driver.Keyboard.SendKeys(Keys.Tab);
                foreach(ProductAttributeDto att in aProduct.ProductAttributes)
                {
                    driver.Keyboard.SendKeys(att.Type);      /// Attribute-type (charactestic name)
                    driver.Keyboard.SendKeys(Keys.Tab);                                                        /// Validate attribute-type
                    ///TODO: remove the replace
                    driver.Keyboard.SendKeys(att.Item);     /// Attribute-item (characteristic value)
                    driver.Keyboard.SendKeys(Keys.Return);                                                        /// Validate attribute-item
                }

                /// Send two TABS from the empty attributes type > attributes item > textarea
                /// /// Need to split the product long description in order to avoid crash of Firefox (instead of sendkeys at once is too fast: //driver.Keyboard.SendKeys(productLongDesc);;)
                //driver.Keyboard.SendKeys(Keys.Tab);
                //driver.Keyboard.SendKeys(Keys.Tab);
                driver.FindElement(By.Id("recaptcha_response_field")).Click(); /// goes back from captcha
                //Thread.Sleep(3);
                driver.Keyboard.PressKey(Keys.LeftShift);
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.ReleaseKey(Keys.LeftShift);
                
                Thread.Sleep(100);
                
                // manage the long description
                productLongDesc = aProduct.Description;
                if (setLongDescFillMode.Equals(pLongDescGenerateShortOne))
                {
                    driver.Keyboard.SendKeys("longDesc! " + aProduct.Name + " " + productNb); // We don't use the generated long description in the product because Firefox crashes... even if we split like the address...
                    driver.Keyboard.SendKeys(Keys.Return);
                    driver.Keyboard.SendKeys("Done!");
                }
                else if (setLongDescFillMode.Equals(pLongDescFillModeWord))
                {
                    splittedProductLongDescWord = productLongDesc.Split(' ');
                    foreach (var item in splittedProductLongDescWord)
                    {
                        driver.Keyboard.SendKeys(item + " ");
                        if (pLongFillThreadSleepDuration > 0)
                            Thread.Sleep(pLongFillThreadSleepDuration);
                    }
                }
                else if (setLongDescFillMode.Equals(pLongDescFillModeChar))
                {
                    splittedProductLongDescChar = productLongDesc.ToCharArray();
                    for (int z = 0; z < splittedProductLongDescChar.Length; z++)
                    {
                        driver.Keyboard.SendKeys(splittedProductLongDescChar.GetValue(z).ToString());
                        if (pLongFillThreadSleepDuration > 0)
                            Thread.Sleep(pLongFillThreadSleepDuration);
                    }
                }
                else if (setLongDescFillMode.Equals(pLongDescGenerateShortOne))
                {
                    driver.Keyboard.SendKeys(productLongDesc);
                }

                // manage images
                driver.FindElement(By.Id("uploadImageButton")).SendKeys(@"D:\Mercurius.Images\UITestImages\voiture-1.jpg");
                driver.FindElement(By.Id("uploadImageButton")).SendKeys(@"D:\Mercurius.Images\UITestImages\voiture-2.jpg");

                /// validation
                driver.FindElement(By.Id("product-publish")).Click();


                /// reinitialize the form by force refresh the page
                Thread.Sleep(1500);
                driver.Keyboard.PressKey(Keys.LeftControl);
                driver.Keyboard.SendKeys(Keys.F5);
                driver.Keyboard.ReleaseKey(Keys.LeftControl);
            }
        }

        // This closes the driver down after the test has finished.
        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
            gen.CloseExcelApi();
        }

        public void Wait(int milliseconds)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(milliseconds));
        }

        public void LaunchFirefox()
        {
            DebugLog("launching Firefox...");
            driver = new FirefoxDriver();
            if (setTargetEnv.Equals(pTargetEnvIIS))
            {
                driver.Navigate().GoToUrl("http://localhost:8080/addProduct");   /// IISS local
            }
            else if (setTargetEnv.Equals(pTargetEnvVS))
            {
                driver.Navigate().GoToUrl("http://localhost:15670/addProduct");   /// Visual Studio run/debug
            }
            else if (setTargetEnv.Equals(pTargetEnvPROD))
            {
                driver.Navigate().GoToUrl("http://mercurius.mendeo.com/addProduct");   /// PROD environment
            }
            //driver.Navigate().GoToUrl("http://localhost:15670/addProduct");
        }

        public void DebugLog(string textAction)
        {
            Debug.WriteLine(debugPrefix + textAction);
        }

    }

}
