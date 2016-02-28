using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using Mendeo.Mercurius.Selenium.Test.UI.utils;
using System.Threading;

namespace Mendeo.Mercurius.Selenium.Test.UI
{
    [TestClass]
    public class AddProductUITest
    {
        private FirefoxDriver driver;
        private string debugPrefix = "[MercuriusTests]: ";
        private ExcelApi excelApi;

        private string address = null;
        private string[] splittedAddressWord = null;
        private char[] splittedAddressChar = null;
        private string productLongDesc = null;
        private string[] splittedProductLongDesc = null;


        /// Excel columns parameters
        private static int rowStart = 2;
        private static int colProductName = 2;
        private static int colProductShortDesc = 3;
        private static int colProductPrice = 4;
        private static int colProductAddress = 5;
        private static int colProductLongDesc = 6;
        private static int colProductCategoryName = 7;
        private static int colProductAttributeName1 = 9;
        private static int colProductAttributeValue1 = 10;
        private static int nbProductAttributes = 10;


        /// These below are the setup parameters values
        private static string pTargetEnvIIS = "IIS";
        private static string pTargetEnvVS = "VS";
        private static string pAddressFillModeChar = "charMode";
        private static string pAddressFillModeWord = "wordMode";
        private static string pAddressFillModeAtOnce = "AtOnce";
        private static string pDataModeFull = "full";
        private static string pDataModeSample = "sample";
        

        /// <summary>
        /// These setup parameters variables below setup the test case
        /// </summary>
        private static int pLongFillThreadSleepDuration = 2;
        private static int pDataModeSampleRowsNumber = 7;
        private string setTargetEnv = pTargetEnvIIS;
        private string setAddressFillMode = pAddressFillModeChar;
        private string setDataMode = pDataModeFull;
        private static int pProcessOneSpecificLine = 0; ///if 0 then process whole file, if not process the given row.

        /// This initialize the web driver firefox and open ups the page to add product
        [TestInitialize]
        public void Setup()
        {
            excelApi = new ExcelApi();
            excelApi.LoadSheet();
            LaunchFirefox();
            //driver.FindElement(By.LinkText("Add Product")).Click();
            Wait(500);
        }

        // This is the test to be carried out.
        [TestMethod]
        public void FillProductForm()
        {
            /// Get the data size
            int nRows = excelApi.GetRowsNumber();
            int nCols = excelApi.GetColsNumber();

            /// choose to process full file or part of the file
            int rowsToProcess = 0;
            if (setDataMode.Equals(pDataModeSample))
                rowsToProcess = pDataModeSampleRowsNumber;
            else if (setDataMode.Equals(pDataModeFull))
                rowsToProcess = nRows;

            /// allows to process one specific line for debugging purpose.
            if (pProcessOneSpecificLine != 0)
                rowsToProcess = pProcessOneSpecificLine+1;

            for (int aRow = rowStart; aRow < rowsToProcess; aRow++)
            {
                /// allows to process one specific line for debugging purpose.
                if (pProcessOneSpecificLine != 0)
                    aRow = pProcessOneSpecificLine;

                /// product name
                driver.FindElement(By.Id("product-name")).Click();
                driver.FindElement(By.Id("product-name")).SendKeys(excelApi.GetCellValue(aRow, colProductName));

                /// product one line description
                driver.FindElement(By.Id("product-one_line_description")).SendKeys(excelApi.GetCellValue(aRow, colProductShortDesc));

                /// product price
                driver.FindElement(By.Id("product-price")).SendKeys(excelApi.GetCellValue(aRow, colProductPrice));

                /// Need to split the address in order to be sure that the correct one will be entered (instead of sendkeys at once is too fast: //driver.FindElement(By.Id("product-one_line_address")).SendKeys("12 Rue Saint-Bernard, 75011 Paris, France");)
                address = excelApi.GetCellValue(aRow, colProductAddress);
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
                driver.FindElement(By.Id("product-category")).SendKeys(excelApi.GetCellValue(aRow, colProductCategoryName));
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Tab);
                driver.Keyboard.SendKeys(Keys.Space);             /// Select the corresponding category

                /// Need to send a TAB key as we don't select the generated input, from the tree to the caracteristics field
                /// //driver.FindElement(By.Id("product-caracteristics")).Click(); //8
                driver.Keyboard.SendKeys(Keys.Tab);
                int i = colProductAttributeName1;
                int j = colProductAttributeValue1;
                for (int attIndex = 0; attIndex < nbProductAttributes; attIndex++)
                {
                    driver.Keyboard.SendKeys(excelApi.GetCellValue(aRow, i));      /// Attribute-type (charactestic name)
                    driver.Keyboard.SendKeys(Keys.Tab);                                                        /// Validate attribute-type
                                                                                                               ///TODO: remove the replace
                    driver.Keyboard.SendKeys(excelApi.GetCellValue(aRow, j).Replace(',', '.'));     /// Attribute-item (characteristic value)
                    driver.Keyboard.SendKeys(Keys.Return);                                                        /// Validate attribute-item
                    i = i + 2;
                    j = j + 2;
                }

                // manage images
                driver.FindElement(By.Id("uploadImageButton")).SendKeys(@"D:\Mercurius.Images\UITestImages\voiture-1.jpg");
                //driver.FindElement(By.XPath("//*[@id=\"uploadImageButtonSpan\"]/input")).SendKeys(@"D:\Mercurius.Images\UITestImages\voiture-1.jpg");

                //*[@id="uploadImageButtonSpan"]/input

                //driver.Keyboard.SendKeys(@"D:\Mercurius.Images\UITestImages");
                //driver.Keyboard.SendKeys(Keys.Return);
                //driver.Keyboard.SendKeys("\"voiture-1.jpg\" \"voiture-2.jpg\" ");
                //driver.Keyboard.SendKeys(Keys.Return);



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
                productLongDesc = excelApi.GetCellValue(aRow, colProductLongDesc);
                Thread.Sleep(100);
                driver.Keyboard.SendKeys("longDesc! " + excelApi.GetCellValue(aRow, colProductName)); // We don't use the generated long description in the product because Firefox crashes... even if we split like the address...
                driver.Keyboard.SendKeys(Keys.Return);
                driver.Keyboard.SendKeys("Done!");

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
            excelApi.CloseSheet();
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
            //driver.Navigate().GoToUrl("http://localhost:15670/addProduct");
        }



        public void DebugLog(string textAction)
        {
            Debug.WriteLine(debugPrefix + textAction);
        }

        private void fillFormDemo()
        {
            /// product name
            driver.FindElement(By.Id("product-name")).Click();
            driver.FindElement(By.Id("product-name")).SendKeys("My Super Product 1");

            /// product one line description
            driver.FindElement(By.Id("product-one_line_description")).SendKeys("This is a magnificent one line product description.");

            /// product price
            driver.FindElement(By.Id("product-price")).SendKeys("999");

            /// Need to split the address in order to be sure that the correct one will be entered (sendkeys at once is too fast).
            address = "12 Rue Saint-Bernard, 75011 Paris, France";
            //splittedAddress = address.Split(' ');
            foreach (var item in splittedAddressChar)
            {
                driver.FindElement(By.Id("product-one_line_address")).SendKeys(item + " ");
            }
            //driver.FindElement(By.Id("product-one_line_address")).SendKeys("12 Rue Saint-Bernard, 75011 Paris, France");
            driver.Keyboard.SendKeys(Keys.ArrowDown);
            driver.Keyboard.SendKeys(Keys.Return);
            driver.Keyboard.SendKeys(Keys.Tab);

            /// Select the category after searching the correct category in the tree deph, need to tab 3 times to be in the corresponding checkbox
            driver.FindElement(By.Id("product-category")).SendKeys("informatique");
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys(Keys.Space);             /// Select the corresponding category


            /// Need to send a TAB key as we don't select the generated input, from the tree to the caracteristics field
            /// //driver.FindElement(By.Id("product-caracteristics")).Click();
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys("NomAttribut");
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys("ValeurAttribut");
            driver.Keyboard.SendKeys(Keys.Return);

            /// Send two TABS from the empty attributes type > attributes item > textarea
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys(Keys.Tab);
            driver.Keyboard.SendKeys("ceci est une longue description !");
            driver.Keyboard.SendKeys(Keys.Return);
            driver.Keyboard.SendKeys("ceci est une longue description !");

            /// validation
            Wait(500);
            //driver.FindElement(By.Id("product-publish")).Click();


            /// reinitialize the form by force refresh the page
            Wait(750);
            driver.Keyboard.PressKey(Keys.LeftControl);
            driver.Keyboard.SendKeys(Keys.F5);
            driver.Keyboard.ReleaseKey(Keys.LeftControl);
            Wait(250);
        }
    }

}
