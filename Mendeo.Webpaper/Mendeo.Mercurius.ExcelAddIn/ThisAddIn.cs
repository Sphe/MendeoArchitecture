using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Linq;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Mendeo.Mercurius.Bootstrap.Init;
using Castle.Windsor;
using Mendeo.Mercurius.ExcelBusiness;
using Castle.MicroKernel.Registration;

namespace Mendeo.Mercurius.ExcelAddIn
{
    public partial class ThisAddIn
    {
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        private DataSet _ds;
        private ListObject _lo;
        private IWindsorContainer _container;
        private IProductModule _productModule;

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        private void RegisterBusiness()
        {
            _container.Register(
                Component.For(typeof(IProductModule))
                    .ImplementedBy<ProductModule>()
                    .LifestyleTransient()
                    .Named("excelBusiness"));
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _container = new WindsorContainer();
            ComponentRegistrar.AddExcelComponentsTo(_container);
            RegisterBusiness();
            MappingRegistrar.AddMapping();

            _productModule = _container.Resolve<IProductModule>();

            Globals.Ribbons.RiaRibbon.btnSync.Click += btnSync_Click;
            Globals.Ribbons.RiaRibbon.btnSaveUpdate.Click += btnSaveUpdate_Click;
            Globals.Ribbons.RiaRibbon.btnImage.Click += btnImage_Click;

            RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            item.Label = "Test";
            Globals.Ribbons.RiaRibbon.ddlMainView.Items.Add(item);
        }

        void btnImage_Click(object sender, RibbonControlEventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnSync_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.DisplayStatusBar = true;
            Globals.ThisAddIn.Application.StatusBar = "Loading...";
            Task.Factory.StartNew(LoadRiaProducts).ContinueWith(x =>
            {
                Globals.ThisAddIn.Application.DisplayStatusBar = false;
            });
        }

        void btnSaveUpdate_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            if (_ds.HasChanges())
            {
                var c = _ds.GetChanges();
                var t = c.Tables[0];

                //var list = t.ToList<AdminProductViewProductDto>();

                for (var i = 0; i < t.Rows.Count; i++)
                {
                    var row = t.Rows[i];
                    switch (row.RowState)
                    {
                        case DataRowState.Added:
                            //_adminProductService.SaveProduct(list[i]);
                            continue;

                        case DataRowState.Deleted:
                            //_adminProductService.DeleteProduct(list[i].Id);
                            continue;

                        case DataRowState.Modified:
                            //_productModule.UpdateFromProductFlatten(t, row);
                            continue;

                        default:
                            throw new Exception("not implemented");
                    }
                }

                _ds.AcceptChanges();
            }
        }

        internal void LoadRiaProducts()
        {
            Globals.ThisAddIn.Dispatcher.Invoke(() =>
            {
                var worksheet = this.Application.ActiveWorkbook.ActiveSheet;

                _lo = FindListObject("mendeo");
                if (_lo == null)
                {
                    // Create a workhseet host item for .NET Framework 4 projects.
                    var extendedWorksheet = (Worksheet)Globals.Factory.GetVstoObject(worksheet);
                    var cell = extendedWorksheet.Range["A1:G5"];
                    _lo = extendedWorksheet.Controls.AddListObject(cell, "mendeo");
                }

                _lo.AutoSetDataBoundColumnHeaders = true;
                _lo.SelectionChange += _lo_SelectionChange;

                var dt = _productModule.GetAllProductFlatten();
                var ds = new DataSet();
                ds.Tables.Add(dt);
                _ds = ds;
                _ds.AcceptChanges();

                _lo.SetDataBinding(dt);
                _lo.ShowTotals = true;

                AddSelectionChangeEventHandlers();
            });
        }

        void _lo_SelectionChange(Excel.Range target)
        {
        }

        private ListObject FindListObject(string name)
        {
            var sheet = this.Application.ActiveWorkbook.ActiveSheet;
            foreach (Excel.ListObject listObject in sheet.ListObjects)
            {
                if (listObject.Name == name)
                {
                    var lo = Globals.Factory.GetVstoObject(listObject);
                    return lo;
                }
            }
            return null;
        }

        private void AddSelectionChangeEventHandlers()
        {
            var listObject = FindListObject("mendeo");
            listObject.Selected += listObject_Selected;
            listObject.Deselected += listObject_Deselected;
        }

        private void listObject_Deselected(Excel.Range tRange)
        {
            SetRibbonControlsState(false);
        }

        private void listObject_Selected(Excel.Range tRange)
        {
            SetRibbonControlsState(true);
        }

        private void SetRibbonControlsState(bool state)
        {
            var ribbon = Globals.Ribbons.RiaRibbon;
            foreach (var ribbonControl in ribbon.grpMain.Items)
            {
                ribbonControl.Enabled = state;
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
