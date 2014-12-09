namespace FedCapSys
{
    partial class ucBillingAnalysis
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBillingAnalysis));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.cmdExport = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabData = new DevExpress.XtraTab.XtraTabPage();
            this.grdClients = new DevExpress.XtraGrid.GridControl();
            this.grdClientsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabPivot = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.chartCtrl = new DevExpress.XtraCharts.ChartControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.datTo = new DevExpress.XtraEditors.DateEdit();
            this.datFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMilestones = new DevExpress.XtraEditors.ComboBoxEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FedCapSys.Modules.WaitForm1), true, true, DevExpress.XtraSplashScreen.ParentType.UserControl);
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cmbChartType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClientsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tabPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMilestones.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChartType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExport.Appearance.Options.UseFont = true;
            this.cmdExport.Enabled = false;
            this.cmdExport.Image = ((System.Drawing.Image)(resources.GetObject("cmdExport.Image")));
            this.cmdExport.Location = new System.Drawing.Point(473, 40);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(106, 42);
            this.cmdExport.TabIndex = 93;
            this.cmdExport.Text = "Export";
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl.Location = new System.Drawing.Point(33, 110);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.tabData;
            this.xtraTabControl.Size = new System.Drawing.Size(827, 446);
            this.xtraTabControl.TabIndex = 104;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabData,
            this.tabPivot});
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.grdClients);
            this.tabData.Name = "tabData";
            this.tabData.Size = new System.Drawing.Size(821, 418);
            this.tabData.Text = "Data";
            // 
            // grdClients
            // 
            this.grdClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClients.Location = new System.Drawing.Point(0, 0);
            this.grdClients.MainView = this.grdClientsView;
            this.grdClients.Name = "grdClients";
            this.grdClients.Size = new System.Drawing.Size(821, 418);
            this.grdClients.TabIndex = 19;
            this.grdClients.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdClientsView,
            this.gridView2});
            // 
            // grdClientsView
            // 
            this.grdClientsView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdClientsView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(119)))), ((int)(((byte)(99)))));
            this.grdClientsView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdClientsView.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grdClientsView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdClientsView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdClientsView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grdClientsView.GridControl = this.grdClients;
            this.grdClientsView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Client", null, "(Count={0})")});
            this.grdClientsView.Name = "grdClientsView";
            this.grdClientsView.OptionsBehavior.Editable = false;
            this.grdClientsView.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            this.grdClientsView.OptionsFind.AlwaysVisible = true;
            this.grdClientsView.OptionsView.ColumnAutoWidth = false;
            this.grdClientsView.OptionsView.ShowFooter = true;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdClients;
            this.gridView2.Name = "gridView2";
            // 
            // tabPivot
            // 
            this.tabPivot.Controls.Add(this.splitContainerControl1);
            this.tabPivot.Name = "tabPivot";
            this.tabPivot.Size = new System.Drawing.Size(821, 418);
            this.tabPivot.Text = "Pivots";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(821, 418);
            this.splitContainerControl1.SplitterPosition = 275;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.pivotGridControl1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.chartCtrl);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(541, 418);
            this.splitContainerControl2.SplitterPosition = 203;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsCustomization.AllowFilterInCustomizationForm = true;
            this.pivotGridControl1.OptionsCustomization.AllowSortInCustomizationForm = true;
            this.pivotGridControl1.OptionsCustomization.CustomizationFormStyle = DevExpress.XtraPivotGrid.Customization.CustomizationFormStyle.Excel2007;
            this.pivotGridControl1.OptionsView.ShowColumnHeaders = false;
            this.pivotGridControl1.OptionsView.ShowDataHeaders = false;
            this.pivotGridControl1.OptionsView.ShowFilterHeaders = false;
            this.pivotGridControl1.Size = new System.Drawing.Size(541, 203);
            this.pivotGridControl1.TabIndex = 0;
            this.pivotGridControl1.CellDoubleClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.pivotGridControl_CellDoubleClick);
            // 
            // chartCtrl
            // 
            this.chartCtrl.DataSource = this.pivotGridControl1;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartCtrl.Diagram = xyDiagram1;
            this.chartCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCtrl.Legend.MaxHorizontalPercentage = 30D;
            this.chartCtrl.Location = new System.Drawing.Point(0, 0);
            this.chartCtrl.Name = "chartCtrl";
            this.chartCtrl.SeriesDataMember = "Series";
            this.chartCtrl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartCtrl.SeriesTemplate.ArgumentDataMember = "Arguments";
            this.chartCtrl.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.chartCtrl.SeriesTemplate.ValueDataMembersSerializable = "Values";
            this.chartCtrl.SeriesTemplate.View = stackedBarSeriesView1;
            this.chartCtrl.Size = new System.Drawing.Size(541, 210);
            this.chartCtrl.TabIndex = 0;
            // 
            // datTo
            // 
            this.datTo.EditValue = null;
            this.datTo.Location = new System.Drawing.Point(229, 58);
            this.datTo.Name = "datTo";
            this.datTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTo.Properties.MinValue = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            this.datTo.Size = new System.Drawing.Size(100, 20);
            this.datTo.TabIndex = 100;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Please enter end-date";
            this.dxValidationProvider1.SetValidationRule(this.datTo, conditionValidationRule1);
            // 
            // datFrom
            // 
            this.datFrom.EditValue = null;
            this.datFrom.Location = new System.Drawing.Point(63, 58);
            this.datFrom.Name = "datFrom";
            this.datFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datFrom.Properties.MinValue = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            this.datFrom.Size = new System.Drawing.Size(100, 20);
            this.datFrom.TabIndex = 99;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Please enter start-date";
            this.dxValidationProvider1.SetValidationRule(this.datFrom, conditionValidationRule2);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(207, 61);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(16, 13);
            this.labelControl8.TabIndex = 102;
            this.labelControl8.Text = "To:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 61);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(52, 13);
            this.labelControl7.TabIndex = 101;
            this.labelControl7.Text = "Date from:";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Appearance.Options.UseFont = true;
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(361, 40);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(106, 42);
            this.cmdSearch.TabIndex = 92;
            this.cmdSearch.Text = "Generate";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(27, 36);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 66;
            this.labelControl3.Text = " Sites:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(169, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 64;
            this.labelControl2.Text = "Milestones:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cmdExport);
            this.groupControl1.Controls.Add(this.comboBoxEdit1);
            this.groupControl1.Controls.Add(this.cmbMilestones);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.datTo);
            this.groupControl1.Controls.Add(this.datFrom);
            this.groupControl1.Controls.Add(this.cmdSearch);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(33, 9);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(584, 95);
            this.groupControl1.TabIndex = 105;
            this.groupControl1.Text = "Search Parameters";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(63, 33);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(100, 20);
            this.comboBoxEdit1.TabIndex = 105;
            // 
            // cmbMilestones
            // 
            this.cmbMilestones.Location = new System.Drawing.Point(229, 33);
            this.cmbMilestones.Name = "cmbMilestones";
            this.cmbMilestones.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMilestones.Properties.Items.AddRange(new object[] {
            "BPS I",
            "BPS II",
            "CRT",
            "Wellness",
            "IPE Completion",
            "Day 1 Placements",
            "30 Day Retention",
            "90 Day Retention",
            "180 Day Retention"});
            this.cmbMilestones.Size = new System.Drawing.Size(100, 20);
            this.cmbMilestones.TabIndex = 104;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.cmbChartType);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Location = new System.Drawing.Point(623, 9);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(200, 95);
            this.groupControl2.TabIndex = 106;
            this.groupControl2.Text = "Chart Options:";
            // 
            // cmbChartType
            // 
            this.cmbChartType.EditValue = "";
            this.cmbChartType.Location = new System.Drawing.Point(79, 48);
            this.cmbChartType.Name = "cmbChartType";
            this.cmbChartType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbChartType.Size = new System.Drawing.Size(100, 20);
            this.cmbChartType.TabIndex = 107;
            this.cmbChartType.SelectedIndexChanged += new System.EventHandler(this.cmbChartType_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 106;
            this.labelControl1.Text = "Chart Type:";
            // 
            // ucBillingAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.groupControl1);
            this.Name = "ucBillingAnalysis";
            this.Size = new System.Drawing.Size(892, 565);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClientsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tabPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMilestones.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChartType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.SimpleButton cmdExport;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage tabData;
        private DevExpress.XtraGrid.GridControl grdClients;
        private DevExpress.XtraGrid.Views.Grid.GridView grdClientsView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.DateEdit datTo;
        private DevExpress.XtraEditors.DateEdit datFrom;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTab.XtraTabPage tabPivot;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraCharts.ChartControl chartCtrl;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMilestones;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbChartType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}
