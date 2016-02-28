using Castle.Windsor;
using Mendeo.Common.Core;
using Mendeo.Mercurius.Bootstrap.Init;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Selenium.Test.UI.utils;
using Mendeo.Mercurius.Service.Contract;
using Mendeo.Mercurius.Service.Test.Utils;
using Mendeo.Mercurius.Service.Test.Utils.GenerateProductFromSheet;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Test
{
    [TestFixture]
    public class ProductServiceDataSampleFill : BaseWindsorCastleTest
    {
        /// <summary>
        /// Global Vars & Constants
        /// </summary>
        private static string debugPrefix = "[MercuriusServiceTests]: ";

        #region not used anymore (externalized) Global Vars & Constants
        //private Application excelApp = null;
        //private Workbook wb = null;
        //private static string excelFileName = "ProductTestDataFillService-values.xlsx";
        //private static string longDescriptionPrefix = "Cela ne veut strictement rien dire mais ce produit ";
        #endregion

        #region not used anymore (externalized) Category vars & constants
        /// <summary>
        /// Category vars & constants
        /// </summary>
        //private static int categoryRowStart = 1;
        //private static int categoryRowEnd = 76;
        //private static HashSet<int> categoryRowExclude = new HashSet<int>() { 1, 2, 3, 17, 23, 33, 41, 48, 59, 69 }; /// These are the main categories.
        //private static int categoryRangeStart = 0;
        //private static int categoryRangeEnd = categoryRowEnd - categoryRowExclude.Count;
        //private IEnumerable<int> categoryRowsRange;
        //private static int categoryNameCol = 4;
        //private static int categoryIDCol = 1;
        //private int categoryAndProductSheetMaxRow;
        //private int categoryAndProductSheetMaxCol;
        //private static Worksheet categoryAndProductWorkSheet;
        //private static int categoryCurrentRow;
        //private string categoryCurrentName;
        //private string categoryCurrentID;
        //private Random categoryRdm;
        #endregion

        #region not used anymore (externalized) Product vars & constants
        /// <summary>
        /// Product vars & constants
        /// </summary>
        //private static Worksheet productAddressWorkSheet;
        //private static int productNameColStart = 1;
        //private static int productNameColEnd = 14;
        //private static HashSet<int> productNameColExclude = new HashSet<int>() { 1, 2, 3, 4 };
        //private static int productNameRangeStart = 0;
        //private static int productNameRangeEnd = productNameColEnd - productNameColExclude.Count;
        //private IEnumerable<int> productNameColRange;
        //private Random productNameRdm;
        //private static int productNameCurrentCol;
        //private string productNameCurrentName;
        #endregion

        #region not used anymore (externalized) Product Address vars & Constants
        /// <summary>
        /// Product Address vars & Constants
        /// </summary>
        //private int productAddressListSheetMaxRow;
        //private int productAddressListSheetMaxCol;
        //private static int productAddressRowSart = 1;
        //private static int productAddressRowEnd = 24;
        //private static HashSet<int> productAddressRowExclude = new HashSet<int>() { 1 };
        //private static int productAddressRangeStart = 0;
        //private static int productAddressRangeEnd = productAddressRowEnd - productAddressRowExclude.Count;
        //private IEnumerable<int> productAddressRowRange;
        //private Random productAddressRdm;
        //private int productAddressCurrentRow;
        //private static int productAddressCol = 2;
        //private static int productAddressLongitudeCol = 3;
        //private static int productAddressLatitudeCol = 4;
        //private static int productAddressCountryCodeCol = 5;
        //private string productAddressCurrentAddress;
        //private string productAddressCurrentLongitude;
        //private string productAddressCurrentLatitude;
        //private string productAddressCurrentCountryCode;
        #endregion

        #region not used anymore (externalized) Sentences construction, vers, adjectives, etc Vars & Constants
        /// <summary>
        /// Sentences construction, vers, adjectives, etc Vars & Constants
        /// </summary>
        //private static Worksheet sentenceSheet;
        //private Random adjectivesRdm;
        //private Random verbsRdm;
        //private Random codRdm;
        //private Random shortQualRdm;
        //private int sentenceMaxRow;
        //private int sentenceMaxCol;
        //private static int adjectivesNameCol = 2;
        //private static int sentenceRowStart = 1;
        //private static int adjectivesRowEnd = 23;
        //private static HashSet<int> sentenceRowRangeExclude = new HashSet<int>() { 1 };
        //private static int sentenceRangeStart = 0;
        //private static int adjectivesRangeEnd = adjectivesRowEnd - sentenceRowRangeExclude.Count;
        //private int adjectivesCurrentRow;
        //private string adjectivesCurrentName;
        //private IEnumerable<int> adjectivesRowRange;
        //private static int verbsNameCol = 5;
        //private static int verbsRowEnd = 10;
        //private static int verbsRangeEnd = verbsRowEnd - sentenceRowRangeExclude.Count;
        //private int verbsCurrentRow;
        //private string verbsCurrentName;
        //private IEnumerable<int> verbsRowRange;
        //private static int codNameCol = 8;
        //private static int codNameRowEnd = 9;
        //private static int codNameRangeEnd = codNameRowEnd - sentenceRowRangeExclude.Count;
        //private int codNameCurrentRow;
        //private string codNameCurrentName;
        //private IEnumerable<int> codRowRange;
        //private static int shortQualNameCol = 14;
        //private static int shortQualRowEnd = 6;
        //private static int shortQualRangeEnd = shortQualRowEnd - sentenceRowRangeExclude.Count;
        //private int shortQualCurrentRow;
        //private string shortQualCurrentName;
        //private IEnumerable<int> shortQualRowRange;
        #endregion

        #region not used anymore (externalized) Category / caracteristics mapping vars & cosntants
        /// <summary>
        /// Category / caracteristics mapping vars & cosntants
        /// </summary>
        //private static Worksheet categoryCaracSheet;
        //private static int categoryCaracMaxRows;
        //private static int categoryCaracMaxCols;
        //private Random attItemRdm;
        //private static int attTypeNameCol = 5;
        //private static int attItemNameColStart = 1;
        //private static int attItemNameColEnd = 15;
        //private static HashSet<int> attItemNameColRangeExclude = new HashSet<int>() { 1, 2, 3, 4, 5 };
        //private IEnumerable<int> attItemNameColRange;
        //private static int attItemNameColRangeStart = 0;
        //private static int attItemNameColRangeEnd = attItemNameColEnd - attItemNameColRangeExclude.Count;
        //private int attItemCurrentCol;
        //private string attTypeCurrentValue;
        //private string attItemCurrentValue;
        //private static Dictionary<int, int> categoryIDToRowStartNumber = new Dictionary<int, int> { { 6, 3 }, { 7, 13 }, { 8, 23 }, { 9, 33 }, { 10, 43 }, { 11, 53 }, { 12, 63 } };
        //private static int numberOfAttributeItemNamesPerCategory = 10;
        //private static int firstCategoryStartRowNumber = 3;
        //private static int firstCategoryID = 6;
        //private Random priceRdm;
        #endregion

        /// <summary>
        /// Parallel
        /// </summary>
        private ConcurrentQueue<Func<bool>> _queueToExecute = new ConcurrentQueue<Func<bool>>();
        private ConcurrentBag<Task> _executingTasks = new ConcurrentBag<Task>();

        #region not used anymore (externalized)  Excel Constants
        /// <summary>
        ///  Excel Constants
        /// </summary>
        //private static string categoryProductSheetName = "category-product-mapping";
        //private static string productAdressWorkSheetName = "random-address";
        //private static string categoryCaracteristicSheetName = "catagory-caracteristics";
        //private static string sentenceListSheetName = "random-sentence";
        #endregion 

        /// <summary>
        /// Test setup parameters
        /// </summary>
        private static Boolean isLoggingDebug = true;
        private static Boolean isLoggingVerbose = false;
        private static Boolean isLoggingProductFullDetailsOnCreation = false;
        private static int numberOfProductToGenerate = 100;
        private static int _concurrentExecution = 10;
        private static Boolean isMultithreaded = false;


        IProductService service;
        IIndexingProductService indexService;
        //IProductImageService _productImageService;
        GenerateRandomProductUtil gen;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            //LoadSheets();
            gen = new GenerateRandomProductUtil(isLoggingDebug, isLoggingVerbose, isLoggingProductFullDetailsOnCreation, GenerateRandomProductUtil.serviceCall);
            _container = new WindsorContainer();
            ComponentRegistrar.AddServicesTo(_container);
            ComponentRegistrar.AddNestConnection(_container);
            ComponentRegistrar.AddGenericRepositoriesTo(_container);
            ComponentRegistrar.AddCustomRepositoriesTo(_container);
            ComponentRegistrar.AddUnitOfWorkTo(_container);
            ComponentRegistrar.AddDatabaseFactoryPerThreadTo(_container);
            service = _container.Resolve<IProductService>();
            indexService = _container.Resolve<IIndexingProductService>();
            //_productImageService = _container.Resolve<IProductImageService>();
        }

        [Test]
        public void FillProduct()
        {
            if (isLoggingDebug)
                Debug.WriteLine(string.Concat(debugPrefix, "MultiThreading:", isMultithreaded.ToString(), "Starting Creating ", numberOfProductToGenerate, " Random Products from data list..."));
            if (isMultithreaded == false)
            {
                for(int num = 1; num <numberOfProductToGenerate+1;  num++)
                {
                    ProductCreation(num);
                }
            }
            else if (isMultithreaded)
            {
                #region multithreading
                var threadNum = new ProductNumThreadSafe();

                var enQueueTask = Task.Run(() =>
                {
                    var numTest = 0;
                    while (numTest < numberOfProductToGenerate)
                    {
                        _queueToExecute.Enqueue(() =>
                        {
                            var retValue = ProductCreation(threadNum.GetNextNumber());
                            return retValue;
                        });

                        numTest++;
                    }
                });

                var deQueueTask = Task.Run(() =>
                {
                    while (_queueToExecute.Count > 0)
                    {
                        Func<bool> toProcess;
                        if (_executingTasks.Count < _concurrentExecution && _queueToExecute.TryDequeue(out toProcess))
                        {
                            var runTask = Task.Run(toProcess);
                            runTask.ContinueWith(x =>
                            {
                                if (x.IsFaulted)
                                {
                                    Debug.WriteLine(string.Format("ERROR Thread: {0}", x.Exception));
                                }
                            });
                            _executingTasks.Add(runTask);
                        }

                        var completedTask = _executingTasks.Where(t => t.Status == TaskStatus.RanToCompletion
                            || t.Status == TaskStatus.Faulted);
                        if (completedTask.Any())
                        {
                            _executingTasks = new ConcurrentBag<Task>(_executingTasks.Except(completedTask));
                        }
                    }
                });

                Task.WaitAll(new[] { enQueueTask, deQueueTask });

                #endregion
            }
                
        }

        [TestFixtureTearDown]
        public void TestTearDown()
        {
            _container.Dispose();
            //excelApp.Quit();
            gen.CloseExcelApi();
        }

        private bool ProductCreation(long num)
        {
            ProductDto aProduct = new ProductDto();

            aProduct = gen.GenerateProduct(num, numberOfProductToGenerate);

            var s = _container.Resolve<IProductService>();
            var p = s.AddProduct(aProduct, null);
            
            ProcessProductImages(p.Id);
                        
            var i = _container.Resolve<IIndexingProductService>();
            i.IndexSingleProduct(p.Id);

            return true;
        }

        private void ProcessProductImages(int pProductId)
        {
            string path = @"D:\Mercurius.Images\UITestImages\loft.jpeg";
            ManageImageStreamUtil imgUtil = new ManageImageStreamUtil();
            var g = imgUtil.LoadImage(pProductId, 0, path);
        }
        
        #region old code

        //public override void SetuppingTest()
        //{
        //    LoadSheets();
        //    base.SetuppingTest();
        //}

        //private bool ProductCreation(long num)
        //{
        //    ProductDto aProduct = new ProductDto();
        //    IList<ProductAttributeDto> aProductAttributeList = new List<ProductAttributeDto>(); ;
        //    ProductAttributeDto aProductAttributes = null;
        //    StringBuilder longdescription = new StringBuilder(); ;
        //    StringBuilder debugLogAttributesList = new StringBuilder();
        //    StringBuilder debutLogStringBuilder = new StringBuilder();

        //    if (isLoggingDebug)
        //        Debug.WriteLine(String.Format("{0} Creating Random category, and random corresponding product... {1}/{2} - {3}", debugPrefix, num, numberOfProductToGenerate, DateTime.Now));

        //    /// Generate randomly a category
        //    RandomizeCategory();
        //    RandomizeProductName();
        //    aProduct.Name = string.Concat(productNameCurrentName, "{#", num, "}");

        //    if (isLoggingDebug && isLoggingVerbose)
        //        Debug.WriteLine(String.Format("{0} Initializing a random price between 1 and 10 000", debugPrefix));
        //    aProduct.Price = priceRdm.Next(1, 10000);

        //    if (isLoggingDebug && isLoggingVerbose)
        //        Debug.WriteLine(String.Format("{0} Creating some random short description...", debugPrefix));
        //    RandomizeAdjectives();
        //    RandomizeShortQual();
        //    aProduct.ShortDescription = string.Concat(adjectivesCurrentName, " ", shortQualCurrentName);

        //    #region DescriptionLongue

        //    if (isLoggingDebug && isLoggingVerbose)
        //        Debug.WriteLine(string.Concat(debugPrefix, "Creating some random long description..."));
        //    RandomizeVerbs();
        //    RandomizeAdjectives();
        //    RandomizeCOD();
        //    longdescription.Append("<b>Ceci est une description vraiment extrêmement longue...!</b ><br/>");
        //    for (int j = 0; j < 10; j++)
        //    {
        //        longdescription.AppendLine(string.Format("{0} {1} {2} {3} {4} <br/>", longDescriptionPrefix, productNameCurrentName,
        //            verbsCurrentName, adjectivesCurrentName, codNameCurrentName));
        //    }
        //    aProduct.Description = longdescription.ToString();

        //    #endregion

        //    if (isLoggingDebug && isLoggingVerbose)
        //        Debug.WriteLine(string.Concat(debugPrefix, "Choosing some random address..."));
        //    RandomizeProductAddress();
        //    aProduct.AdressOneLine = productAddressCurrentAddress;
        //    aProduct.Country = productAddressCurrentCountryCode;
        //    aProduct.Latitude = Decimal.Parse(productAddressCurrentLatitude, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.Float, CultureInfo.InvariantCulture);
        //    aProduct.Longitude = Decimal.Parse(productAddressCurrentLongitude, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.Float, CultureInfo.InvariantCulture);

        //    if (isLoggingDebug && isLoggingVerbose)
        //        Debug.WriteLine(string.Concat(debugPrefix, "Assigning category..."));
        //    aProduct.CategoryId = Int32.Parse(categoryCurrentID) + 1; // offset in database

        //    if (isLoggingDebug && isLoggingVerbose)
        //        Debug.WriteLine(string.Concat(debugPrefix, "Generating product's ", numberOfAttributeItemNamesPerCategory, " characteristics: 2-tupe Attribute-type / Attribute-item..."));

        //    debugLogAttributesList = new StringBuilder();
        //    for (int k = 0; k < numberOfAttributeItemNamesPerCategory; k++)
        //    {
        //        aProductAttributes = new ProductAttributeDto();

        //        if (isLoggingDebug && isLoggingVerbose)
        //            Debug.WriteLine(string.Concat(debugPrefix, "Generating product's characteristics...", k + 1, "/", numberOfAttributeItemNamesPerCategory));
        //        GetAttributeTypeNameForCategory(Int32.Parse(categoryCurrentID), k);
        //        RandomizeAttributeItem(Int32.Parse(categoryCurrentID), k);
        //        aProductAttributes.Type = attTypeCurrentValue;
        //        aProductAttributes.Item = attItemCurrentValue;

        //        if (isLoggingDebug && isLoggingVerbose)
        //            debugLogAttributesList.Append(string.Concat("[AttributeType=", attTypeCurrentValue, "]|[AttributeItem=", attItemCurrentValue, "]"));
        //        if (isLoggingDebug && isLoggingVerbose)
        //            Debug.WriteLine(string.Concat(debugPrefix, "Addoing product's characteristics...", k + 1, "/", numberOfAttributeItemNamesPerCategory));

        //        aProductAttributeList.Add(aProductAttributes);
        //    }
        //    if (isLoggingDebug && isLoggingProductFullDetailsOnCreation)
        //        Debug.WriteLine(String.Format("{0} This is a random Category attribute type |ID={1}|CategoryName={2}|Offset={3}|AttributeTypeName={4}|AttributeItemName({6})={7}|", debugPrefix, categoryCurrentID, categoryCurrentName, num, attTypeCurrentValue, attItemCurrentCol, attItemCurrentValue));
        //    //Debug.WriteLine(debugPrefix + string.Concat("This is a random Category attribute type [ID=", categoryCurrentID, "|CategoryName=", categoryCurrentName, "|Offset=", v, "|AttributeTypeName=", attTypeCurrentValue, "|AttributeItemName(", attItemCurrentCol, ")=", attItemCurrentValue, "]"));
        //    aProduct.ProductAttributes = aProductAttributeList;

        //    var s = _container.Resolve<IProductService>();
        //    var p = s.AddProduct(aProduct);

        //    var i = _container.Resolve<IIndexingProductService>();
        //    i.IndexSingleProduct(p.Id);

        //    return true;
        //}


        //private void CategoryRandomReinit()
        //{
        //    categoryRdm = new Random();
        //}


        //private void LoadSheets()
        //{
        //    // instantiate
        //    Debug.WriteLine(debugPrefix + "Starting Excel application");
        //    excelApp = new Microsoft.Office.Interop.Excel.Application();
        //    excelApp.Visible = true;

        //    string path = AppDomain.CurrentDomain.BaseDirectory;
        //    Debug.WriteLine(debugPrefix + "Openning Excel file");
        //    wb = excelApp.Workbooks.Open(Path.Combine(path, @"data\ProductDataSampleFill\", excelFileName));
        //    LoadCategoryAndProductSheet();
        //    LoadAddressList();
        //    LoadSentenceSheet();
        //    LoadcategoryCaracteristicSheet();
        //    priceRdm = new Random();
        //}

        //private void LoadcategoryCaracteristicSheet()
        //{
        //    categoryCaracSheet = wb.Sheets[categoryCaracteristicSheetName];
        //    categoryCaracMaxRows = categoryCaracSheet.UsedRange.Rows.Count;
        //    categoryCaracMaxCols = categoryCaracSheet.UsedRange.Columns.Count;
        //    attItemNameColRange = Enumerable.Range(attItemNameColStart, attItemNameColEnd).Where(i => !attItemNameColRangeExclude.Contains(i));
        //    attItemRdm = new Random();
        //}

        //private void GetAttributeTypeNameForCategory(int aCategoryID, int anAttTypeOffset)
        //{
        //    attTypeCurrentValue = GetcategoryCaracSheetCellValue(GetCaracteristicStartRowForCategory(aCategoryID, anAttTypeOffset), attTypeNameCol);
        //}

        //private int GetCaracteristicStartRowForCategory(int aCategoryID, int anAttTypeOffset)
        //{
        //    return firstCategoryStartRowNumber + numberOfAttributeItemNamesPerCategory * (aCategoryID - firstCategoryID) + anAttTypeOffset;
        //}

        //private void LoadCategoryAndProductSheet()
        //{
        //    categoryAndProductWorkSheet = wb.Sheets[categoryProductSheetName];
        //    categoryAndProductSheetMaxRow = categoryAndProductWorkSheet.UsedRange.Rows.Count;
        //    categoryAndProductSheetMaxCol = categoryAndProductWorkSheet.UsedRange.Columns.Count;
        //    categoryRowsRange = Enumerable.Range(categoryRowStart, categoryRowEnd).Where(i => !categoryRowExclude.Contains(i));
        //    productNameColRange = Enumerable.Range(productNameColStart, productNameColEnd).Where(i => !productNameColExclude.Contains(i));
        //    categoryRdm = new Random();
        //    productNameRdm = new Random();
        //}

        //private void LoadAddressList()
        //{
        //    productAddressWorkSheet = wb.Sheets[productAdressWorkSheetName];
        //    productAddressListSheetMaxRow = productAddressWorkSheet.UsedRange.Rows.Count;
        //    productAddressListSheetMaxCol = productAddressWorkSheet.UsedRange.Columns.Count;
        //    productAddressRowRange = Enumerable.Range(productAddressRowSart, productAddressRowEnd).Where(i => !productAddressRowExclude.Contains(i));
        //    productAddressRowRange = Enumerable.Range(productAddressRowSart, productAddressRowEnd).Where(i => !productAddressRowExclude.Contains(i));
        //    productAddressRdm = new Random();
        //}

        //private void LoadSentenceSheet()
        //{
        //    sentenceSheet = wb.Sheets[sentenceListSheetName];
        //    sentenceMaxRow = sentenceSheet.UsedRange.Rows.Count;
        //    sentenceMaxCol = sentenceSheet.UsedRange.Columns.Count;
        //    adjectivesRowRange = Enumerable.Range(sentenceRowStart, adjectivesRowEnd).Where(i => !sentenceRowRangeExclude.Contains(i));
        //    verbsRowRange = Enumerable.Range(sentenceRowStart, verbsRowEnd).Where(i => !sentenceRowRangeExclude.Contains(i));
        //    codRowRange = Enumerable.Range(sentenceRowStart, codNameRowEnd).Where(i => !sentenceRowRangeExclude.Contains(i));
        //    shortQualRowRange = Enumerable.Range(sentenceRowStart, shortQualRowEnd).Where(i => !sentenceRowRangeExclude.Contains(i));
        //    adjectivesRdm = new Random();
        //    verbsRdm = new Random();
        //    codRdm = new Random();
        //    shortQualRdm = new Random();
        //}

        //private void RandomizeAttributeItem(int aCategoryID, int anAttTypeOffset)
        //{
        //    int index = attItemRdm.Next(attItemNameColRangeStart, attItemNameColRangeEnd);
        //    attItemCurrentCol = attItemNameColRange.ElementAt(index);
        //    attItemCurrentValue = GetcategoryCaracSheetCellValue(GetCaracteristicStartRowForCategory(aCategoryID, anAttTypeOffset), attItemCurrentCol);
        //}

        //private void RandomizeAdjectives()
        //{
        //    int index = adjectivesRdm.Next(sentenceRangeStart, adjectivesRangeEnd);
        //    adjectivesCurrentRow = adjectivesRowRange.ElementAt(index);
        //    adjectivesCurrentName = GetSentenceWorkSheetCellValue(adjectivesCurrentRow, adjectivesNameCol);
        //}

        //private void RandomizeVerbs()
        //{
        //    int index = verbsRdm.Next(sentenceRangeStart, verbsRangeEnd);
        //    verbsCurrentRow = verbsRowRange.ElementAt(index);
        //    verbsCurrentName = GetSentenceWorkSheetCellValue(verbsCurrentRow, verbsNameCol);
        //}

        //private void RandomizeCOD()
        //{
        //    int index = codRdm.Next(sentenceRangeStart, codNameRangeEnd);
        //    codNameCurrentRow = codRowRange.ElementAt(index);
        //    codNameCurrentName = GetSentenceWorkSheetCellValue(codNameCurrentRow, codNameCol);
        //}

        //private void RandomizeShortQual()
        //{
        //    int index = shortQualRdm.Next(sentenceRangeStart, shortQualRangeEnd);
        //    shortQualCurrentRow = shortQualRowRange.ElementAt(index);
        //    shortQualCurrentName = GetSentenceWorkSheetCellValue(shortQualCurrentRow, shortQualNameCol);
        //}

        //private void RandomizeCategory()
        //{
        //    int index = categoryRdm.Next(categoryRangeStart, categoryRangeEnd);
        //    categoryCurrentRow = categoryRowsRange.ElementAt(index);
        //    categoryCurrentName = GetCategoryAndProductWorkSheetCellValue(categoryCurrentRow, categoryNameCol);
        //    categoryCurrentID = GetCategoryAndProductWorkSheetCellValue(categoryCurrentRow, categoryIDCol);
        //}


        //private void RandomizeProductName()
        //{
        //    int index = productNameRdm.Next(productNameRangeStart, productNameRangeEnd);
        //    productNameCurrentCol = productNameColRange.ElementAt(index);
        //    productNameCurrentName = GetCategoryAndProductWorkSheetCellValue(categoryCurrentRow, productNameCurrentCol);
        //}



        //private void RandomizeProductAddress()
        //{
        //    int index = productAddressRdm.Next(productAddressRangeStart, productAddressRangeEnd);
        //    productAddressCurrentRow = productAddressRowRange.ElementAt(index);
        //    productAddressCurrentAddress = GetProductAddressWorkSheetCellValue(productAddressCurrentRow, productAddressCol);
        //    productAddressCurrentLongitude = GetProductAddressWorkSheetCellValue(productAddressCurrentRow, productAddressLongitudeCol);
        //    productAddressCurrentLatitude = GetProductAddressWorkSheetCellValue(productAddressCurrentRow, productAddressLatitudeCol);
        //    productAddressCurrentCountryCode = GetProductAddressWorkSheetCellValue(productAddressCurrentRow, productAddressCountryCodeCol);

        //}



        //private string GetProductAddressWorkSheetCellValue(int aRow, int aCol)
        //{
        //    return productAddressWorkSheet.Cells[aRow, aCol].Value.ToString();
        //}

        //private string GetProductAddressWorkSheetCellDecimalValue(int aRow, int aCol)
        //{
        //    return productAddressWorkSheet.Cells[aRow, aCol].Value.ToString();
        //}

        //private string GetCategoryAndProductWorkSheetCellValue(int aRow, int aCol)
        //{
        //    return categoryAndProductWorkSheet.Cells[aRow, aCol].Value.ToString();
        //}

        //private string GetSentenceWorkSheetCellValue(int aRow, int aCol)
        //{
        //    return sentenceSheet.Cells[aRow, aCol].Value.ToString();
        //}

        //private string GetcategoryCaracSheetCellValue(int aRow, int aCol)
        //{
        //    return categoryCaracSheet.Cells[aRow, aCol].Value.ToString();
        //}

        //private void AdjectivesRandomReinit()
        //{
        //    adjectivesRdm = new Random();
        //}

        //private void VerbsRandomReinit()
        //{
        //    verbsRdm = new Random();
        //}

        //private void CODRandomReinit()
        //{
        //    codRdm = new Random();
        //}

        //private void ShortQualRandomReinit()
        //{
        //    shortQualRdm = new Random();
        //}

        //private void ProductAddressRandomReinit()
        //{
        //    productAddressRdm = new Random();
        //}

        //private void ProductNameRandomReinit()
        //{
        //    productNameRdm = new Random();
        //}


        //private void testGenerationProduct()
        //{
        //    //test adjectives
        //    for (int i = 0; i < 50; i++)
        //    {
        //        RandomizeAdjectives();
        //        Debug.WriteLine(debugPrefix + string.Concat("This is a random adjective [adjective=", adjectivesCurrentName, "]"));
        //    }
        //    // test verbs
        //    for (int i = 0; i < 50; i++)
        //    {
        //        RandomizeVerbs();
        //        Debug.WriteLine(debugPrefix + string.Concat("This is a random verb [verb=", verbsCurrentName, "]"));
        //    }
        //    // test cod
        //    for (int i = 0; i < 50; i++)
        //    {
        //        RandomizeCOD();
        //        Debug.WriteLine(debugPrefix + string.Concat("This is a random cod [cod=", codNameCurrentName, "]"));
        //    }
        //    // test short qual
        //    for (int i = 0; i < 50; i++)
        //    {
        //        RandomizeShortQual();
        //        Debug.WriteLine(debugPrefix + string.Concat("This is a random short qual [short qual=", shortQualCurrentName, "]"));
        //    }

        //    /// test address
        //    for (int k = 0; k < 50; k++)
        //    {
        //        RandomizeProductAddress();
        //        Debug.WriteLine(debugPrefix + string.Concat("This is a random Address [Address=", productAddressCurrentAddress, "|Longitude=", productAddressCurrentLongitude, "|Latitude=", productAddressCurrentLatitude, "|CountryCode=", productAddressCurrentCountryCode, "]"));
        //    }



        //    //number of 
        //    for (int i = 0; i < 65; i++)
        //    {
        //        RandomizeCategory();
        //        Debug.WriteLine(debugPrefix + string.Concat("This is a random Category [ID=", categoryCurrentID, "|CategoryName=", categoryCurrentName, "]"));


        //        for (int v = 0; v < numberOfAttributeItemNamesPerCategory; v++)
        //        {
        //            GetAttributeTypeNameForCategory(Int32.Parse(categoryCurrentID), v);
        //            RandomizeAttributeItem(Int32.Parse(categoryCurrentID), v);
        //            Debug.WriteLine(debugPrefix + string.Concat("This is a random Category attribute type [ID=", categoryCurrentID, "|CategoryName=", categoryCurrentName, "|Offset=", v, "|AttributeTypeName=", attTypeCurrentValue, "|AttributeItemName(", attItemCurrentCol, ")=", attItemCurrentValue, "]"));

        //        }

        //        for (int j = 0; j < 50; j++)
        //        {
        //            RandomizeProductName();
        //            Debug.WriteLine(debugPrefix + string.Concat("This is a random Product [CategoryName=", categoryCurrentName, "|ProductName=", productNameCurrentName, "]"));
        //        }
        //    }
        //    Debug.WriteLine(debugPrefix + "End Randomize Category...");


        //}
        #endregion
    }


}
