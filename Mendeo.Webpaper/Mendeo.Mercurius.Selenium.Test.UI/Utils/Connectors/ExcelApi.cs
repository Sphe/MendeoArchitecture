using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Mendeo.Mercurius.Selenium.Test.UI.utils
{
    public class ExcelApi
    {

        private Application excelApp = null;
        private Worksheet sheet = null;
        private Workbook wb = null;
        private string debugPrefix = "[MercuriusTests][ExcelApi]: ";
        private string excelFileName = "ProductTestDataSamplePopulate-VALUES.xlsx";
        private int nRows = 0;
        private int nCols = 0;

        public ExcelApi()
        {

        }

        public void LoadSheet()
        {
            // instantiate
            Debug.WriteLine(debugPrefix + "Starting Excel application");
            excelApp = new Excel.Application();
            excelApp.Visible = true;


            string path = AppDomain.CurrentDomain.BaseDirectory;
            Debug.WriteLine(debugPrefix + "Openning Excel file");
            wb = excelApp.Workbooks.Open(Path.Combine(path, @"data\product\", excelFileName));
            sheet = (Worksheet)wb.Sheets["Sheet1"];

            if (sheet != null)
            {
                //test
                //string value = sheet.Cells[1, 1].Value.ToString();
                //Debug.WriteLine(value);

                Range range = sheet.UsedRange;
                nRows = range.Rows.Count;
                nCols = range.Columns.Count;
                Debug.WriteLine(debugPrefix + "Start parsing Excel file" + "\"" + excelFileName + "\" [Rows:" + nRows + ";Cols:" + nCols + "]");

                for (int i = 1; i <= nRows; i++)
                {
                    //get 
                }
            }
        }

        public int GetRowsNumber()
        {
            return nRows;
        }

        public int GetColsNumber()
        {
            return nCols;
        }

        public string GetCellValue(int row, int col)
        {
            return sheet.Cells[row, col].Value.ToString();
        }

        public void CloseSheet()
        {
            excelApp.Quit();
        }
    }
}
