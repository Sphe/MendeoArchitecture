namespace Mendeo.Mercurius.ExcelAddIn
{
    partial class RiaRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RiaRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.grpMain = this.Factory.CreateRibbonGroup();
            this.ddlMainView = this.Factory.CreateRibbonDropDown();
            this.btnSync = this.Factory.CreateRibbonButton();
            this.btnSaveUpdate = this.Factory.CreateRibbonButton();
            this.btnImage = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.grpMain.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.grpMain);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // grpMain
            // 
            this.grpMain.Items.Add(this.ddlMainView);
            this.grpMain.Items.Add(this.btnSync);
            this.grpMain.Items.Add(this.btnSaveUpdate);
            this.grpMain.Items.Add(this.btnImage);
            this.grpMain.Label = "Mendeo";
            this.grpMain.Name = "grpMain";
            // 
            // ddlMainView
            // 
            this.ddlMainView.Label = "View";
            this.ddlMainView.Name = "ddlMainView";
            this.ddlMainView.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ddlMainView_SelectionChanged);
            // 
            // btnSync
            // 
            this.btnSync.Label = "Synchronize";
            this.btnSync.Name = "btnSync";
            this.btnSync.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSync_Click);
            // 
            // btnSaveUpdate
            // 
            this.btnSaveUpdate.Label = "Save / Update";
            this.btnSaveUpdate.Name = "btnSaveUpdate";
            this.btnSaveUpdate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // btnImage
            // 
            this.btnImage.Label = "Image";
            this.btnImage.Name = "btnImage";
            this.btnImage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnImage_Click);
            // 
            // RiaRibbon
            // 
            this.Name = "RiaRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RiaRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpMain;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown ddlMainView;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSync;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveUpdate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnImage;
    }

    partial class ThisRibbonCollection
    {
        internal RiaRibbon RiaRibbon
        {
            get { return this.GetRibbon<RiaRibbon>(); }
        }
    }
}
