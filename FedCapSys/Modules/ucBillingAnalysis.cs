using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using System.Diagnostics;
using FedCapSys.Classes;
using System.Globalization;
using System.Transactions;
using System.Text.RegularExpressions;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using FedCapSys.Modules;

namespace FedCapSys
{
    public partial class ucBillingAnalysis : BaseModule
    {
        public XtraForm detailform = new XtraForm();

        public ucBillingAnalysis()
        {
            InitializeComponent();
           ViewType[] Types = new ViewType[] { ViewType.Bar, ViewType.StackedBar, ViewType.FullStackedBar, ViewType.Point,
				ViewType.Line, ViewType.StackedLine , ViewType.FullStackedLine, ViewType.Spline, ViewType.Area, ViewType.StepArea,
                ViewType.SplineArea , ViewType.StackedArea, ViewType.StackedSplineArea , ViewType.RadarPoint , ViewType.RadarArea, ViewType.RadarLine 
           };
           foreach (ViewType type in Types)
           {
               
               cmbChartType.Properties.Items.Add(type);
           }
           cmbChartType.Properties.Sorted = true;
           cmbChartType.SelectedItem = ViewType.StackedBar;
        }
       
        private List<int> GetIDListInt(CheckedListBoxControl c)
        {
            List<int> l = new List<int>();
            foreach (object x in c.CheckedItems)
            {
                DevExpress.XtraEditors.Controls.ListBoxItem i = (DevExpress.XtraEditors.Controls.ListBoxItem)x;
                NameIdMapping b = (NameIdMapping)i.Value;
                l.Add((int)b.ID);
            }
            return l;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dxValidationProvider1.Validate())
                {
                    this.Cursor = Cursors.WaitCursor;
                    splashScreenManager1.ShowWaitForm();
                   
                    DataTable tbl = new DataTable();
                    using (Data.dbDataContext dc = new Data.dbDataContext())
                    {
                        dc.CommandTimeout = 1000;
                        List<MyBPSBillingResult> bps1 = new List<MyBPSBillingResult>();
                        List<MyBPS2BillingResult> bps2 = new List<MyBPS2BillingResult>();
                        List<MyWellnessBillingResult> wellness = new List<MyWellnessBillingResult>();

                        //using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                        //{
                        //    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                        //}))
                        {
                            if ((string)cmbMilestones.SelectedItem == "BPS I")
                            {
                                //BillingProcessing.runBPSPaid();
                                //BillingProcessing.runWellnessPaid();

                                var BPSres = dc.GetBPSPaymentDetailsResults(DateTime.Parse("1/1/2013"), DateTime.Today);
                                bps1 = BPSres.GetResult<MyBPSBillingResult>().ToList();
                                //// var result = bps1.Distinct();


                                //List<string> ssn_moreThanOneBPSI = bps1.GroupBy(aa => aa.SSN).Where(aa => aa.Count() > 1).Select(aa => aa.Key).ToList();
                                //List<int> BPS1paymentIds = new List<int>();

                                //foreach (var res in bps1.OrderByDescending(aa => aa.CompletionDate))
                                //{

                                //    if (res.ActionCodeDate == null) res.ActionCodeDate = DateTime.Parse("1/1/1901");
                                //    var payment = dc.Milestones_Paids.Where(aa => aa.SSN_no_Dashes == res.SSN
                                //                 && (aa.MilestCode == "W05B" || aa.MilestCode == "W05A")
                                //                 && !BPS1paymentIds.Contains(aa.ID)
                                //                 && aa.SSN_no_Dashes != "" &&
                                //                 (
                                //                      (res.ActionCodeDate == DateTime.Parse("1/1/1901") ? res.CompletionDate <= aa.PaidDate
                                //                     :// res.ActionCodeDate != null &&
                                //                     res.ActionCodeDate <= aa.PaidDate)
                                //                 )
                                //                 ).FirstOrDefault();
                                //    if (payment != null)
                                //    {
                                //        res.Voucher = payment.Voucher;
                                //        res.PaidDate = payment.PaidDate;
                                //        res.BillingStatus = "Paid";
                                //        if (ssn_moreThanOneBPSI.Contains(res.SSN))
                                //            BPS1paymentIds.Add(payment.ID);
                                //    }
                                //    else
                                //    {

                                //        var denial = dc.Payments_Denieds.Where(aa => aa.SSN_no_Dashes == res.SSN
                                //                 && (aa.MilestCode == "W05B" || aa.MilestCode == "W05A")
                                //                 && aa.SSN_no_Dashes != "" &&
                                //                 (
                                //                      (res.ActionCodeDate == DateTime.Parse("1/1/1901") ? aa.Action_dt >= res.CompletionDate.AddDays(-30) && aa.Action_dt <= res.CompletionDate.AddDays(30)
                                //                      : aa.Action_dt >= ((DateTime)res.ActionCodeDate).AddDays(-30) && aa.Action_dt <= ((DateTime)res.ActionCodeDate).AddDays(30))
                                //                 )
                                //                 ).FirstOrDefault();
                                //        if (denial != null)
                                //        {
                                //            res.DenialDate = denial.RejectDate;
                                //            res.DenialReason = denial.DenialDesc;
                                //            res.DenialComments = denial.comments;
                                //            res.BillingStatus = denial.StatusDesc;
                                //        }

                                //        var billedOffline = dc.Milestones_Offline_Subms.Where(aa => aa.SSN == res.SSN
                                //                 && (aa.MilestCode == "W05B" || aa.MilestCode == "W05A")
                                //                 && aa.SSN != "" &&
                                //                 (
                                //                     (res.ActionCodeDate == DateTime.Parse("1/1/1901") ? aa.ComplDate >= res.CompletionDate.AddDays(-30) && aa.ComplDate <= res.CompletionDate.AddDays(30)
                                //                     : aa.ComplDate >= ((DateTime)res.ActionCodeDate).AddDays(-15) && aa.ComplDate <= ((DateTime)res.ActionCodeDate).AddDays(20)
                                //                     )
                                //                 )
                                //                 ).FirstOrDefault();
                                //        if (billedOffline != null && (denial == null || (denial.RejectDate.HasValue && denial.RejectDate <= billedOffline.OfflineSubmDate)))
                                //        {
                                //            res.DenialDate = null;
                                //            res.DenialReason = "";
                                //            res.DenialComments = "";
                                //            res.BilledOfflineDate = billedOffline.OfflineSubmDate;
                                //            res.BilledOfflineReason = billedOffline.ManualLoadReason;
                                //            res.BillingStatus = "Billed Offline";
                                //        }

                                //    }
                                //    if (res.SSN == null || res.SSN == "" || res.SSN == "         ")
                                //        res.BillingStatus = "Invalid SSN";
                                //    else if (res.BillingStatus == null) res.BillingStatus = "Pending";

                                    //if (res.ActionCodeDate == DateTime.Parse("1/1/1901"))
                                    //    res.ActionCodeDate = null;


                               // }
                            }

                            if ((string)cmbMilestones.SelectedItem == "BPS II")
                            {
                                var BPSIIres = dc.GetCompletedBPS2Results(DateTime.Parse("1/1/2013"), DateTime.Today);
                                bps2 = BPSIIres.GetResult<MyBPS2BillingResult>().ToList();

                                //List<string> ssn_moreThanOneBPSII = bps2.GroupBy(aa => aa.SSN).Where(aa => aa.Count() > 1).Select(aa => aa.Key).ToList();
                                //List<int> paymentIds = new List<int>();

    //                            foreach (var res in bps2.OrderByDescending(aa=> aa.CompletionDate))
    //                            {

    //                                if (res.ActionCodeDate == null) res.ActionCodeDate = DateTime.Parse("1/1/1901");
    //                                var payment = dc.Milestones_Paids.Where(aa => aa.SSN_no_Dashes == res.SSN
    //                                             && (aa.MilestCode == "W06B" || aa.MilestCode == "W06A")
    //                                             &&!paymentIds.Contains(aa.ID)
    //                                             && aa.SSN_no_Dashes != "" &&
    //                                             (
    //                                                 (res.ActionCodeDate == DateTime.Parse("1/1/1901") ? res.CompletionDate <= aa.PaidDate
    //                                                 :// res.ActionCodeDate != null &&
    //                                                 res.ActionCodeDate <= aa.PaidDate)
    //                                             )
    //                                             ).FirstOrDefault();
    //                                if (payment != null)
    //                                {
    //                                    res.Voucher = payment.Voucher;
    //                                    res.PaidDate = payment.PaidDate;
    //                                    res.BillingStatus = "Paid";
    //                                    if (ssn_moreThanOneBPSII.Contains(res.SSN))
    //                                        paymentIds.Add(payment.ID);
    //                                }
    //                                else
    //                                {
                                       
    //                                    var denial = dc.Payments_Denieds.Where(aa => aa.SSN_no_Dashes == res.SSN
    //                                             && (aa.MilestCode == "W06B" || aa.MilestCode == "W06A")
    //                                             && aa.SSN_no_Dashes != "" &&
    //                                             (
    //                                                  (res.ActionCodeDate == DateTime.Parse("1/1/1901") ? aa.Action_dt >= res.CompletionDate.AddDays(-30) && aa.Action_dt <= res.CompletionDate.AddDays(30)
    //                                                  : aa.Action_dt >= ((DateTime)res.ActionCodeDate).AddDays(-30) && aa.Action_dt <= ((DateTime)res.ActionCodeDate).AddDays(30)
    //)                                                     
    //                                             )
    //                                             ).FirstOrDefault();
    //                                    if (denial != null)
    //                                    {
    //                                        res.DenialDate  = denial.RejectDate;
    //                                        res.DenialReason= denial.DenialDesc;
    //                                        res.DenialComments = denial.comments;
    //                                        res.BillingStatus = denial.StatusDesc;
    //                                    }

    //                                    var billedOffline = dc.Milestones_Offline_Subms.Where(aa => aa.SSN == res.SSN
    //                                             && (aa.MilestCode == "W06B" || aa.MilestCode == "W06A")
    //                                             && aa.SSN != "" &&
    //                                             (
    //                                                 (res.ActionCodeDate == DateTime.Parse("1/1/1901") ? aa.ComplDate >= res.CompletionDate.AddDays(-30) && aa.ComplDate <= res.CompletionDate.AddDays(30)
    //                                                 : aa.ComplDate >= ((DateTime)res.ActionCodeDate).AddDays(-15) && aa.ComplDate <= ((DateTime)res.ActionCodeDate).AddDays(20)
                                                     
    //                                                 )
    //                                             )
    //                                             ).FirstOrDefault();
    //                                    if (billedOffline != null && (denial == null || (denial.RejectDate.HasValue && denial.RejectDate <= billedOffline.OfflineSubmDate)))
    //                                    {
    //                                        res.DenialDate = null ;
    //                                        res.DenialReason = "";
    //                                        res.DenialComments = "";
    //                                        res.BilledOfflineDate = billedOffline.OfflineSubmDate;
    //                                        res.BilledOfflineReason = billedOffline.ManualLoadReason;
    //                                        res.BillingStatus = "Billed Offline";
    //                                    }

    //                                }
    //                                if (res.SSN == null || res.SSN == "" || res.SSN == "         ")
    //                                    res.BillingStatus = "Invalid SSN";
    //                                else if (res.BillingStatus == null ) res.BillingStatus = "Pending";

    //                                if (res.ActionCodeDate == DateTime.Parse("1/1/1901"))
    //                                    res.ActionCodeDate =null; 


    //                            }
                               
                            }

                            if ((string)cmbMilestones.SelectedItem == "Wellness")
                            {
                                 var WellnessRes = dc.GetWellnessCompletedtDetailsResults(DateTime.Parse("1/1/2013"), DateTime.Today);
                                wellness = WellnessRes.GetResult<MyWellnessBillingResult>().ToList();

                                //List<string> ssn_moreThanOneWellness= wellness.Where(aa=> aa.CompletionCode == "16PV" || aa.CompletionCode =="969V" || aa.CompletionCode =="16PV" ).GroupBy(aa => aa.SSN).Where(aa => aa.Count() > 1).Select(aa => aa.Key).ToList();
                                //List<int> paymentIds = new List<int>();

                                //foreach (var res in wellness.OrderByDescending(aa => aa.CompletionDate))
                                //{
                                //    var payment = dc.Milestones_Paids.Where(aa => aa.SSN_no_Dashes == res.SSN
                                //                 && (aa.MilestCode == "W10A" || aa.MilestCode == "W10B")
                                //                 && !paymentIds.Contains(aa.ID)
                                //                 && aa.SSN_no_Dashes != "" &&
                                //                 (
                                //                      res.CompletionDate  <= aa.PaidDate
                                //                 )
                                //                 ).FirstOrDefault();
                                //    if (payment != null)
                                //    {
                                //        res.Voucher = payment.Voucher;
                                //        res.PaidDate = payment.PaidDate;
                                //        res.BillingStatus = "Paid";
                                //        if (ssn_moreThanOneWellness.Contains(res.SSN))
                                //            paymentIds.Add(payment.ID);
                                //    }
                                //    else
                                //    {

                                //        var denial = dc.Payments_Denieds.Where(aa => aa.SSN_no_Dashes == res.SSN
                                //                 && (aa.MilestCode == "W10A" || aa.MilestCode == "W10B")
                                //                 && aa.SSN_no_Dashes != "" &&
                                //                 (
                                //                      aa.Action_dt >= ((DateTime)res.CompletionDate).AddDays(-15) && aa.Action_dt <= ((DateTime)res.CompletionDate).AddDays(20)
                                //                 )
                                //                 ).FirstOrDefault();
                                //        if (denial != null)
                                //        {
                                //            res.DenialDate = denial.RejectDate;
                                //            res.DenialReason = denial.DenialDesc;
                                //            res.DenialComments = denial.comments;
                                //            res.BillingStatus = denial.StatusDesc;
                                //        }

                                //        var billedOffline = dc.Milestones_Offline_Subms.Where(aa => aa.SSN == res.SSN
                                //                 && (aa.MilestCode == "W10A" || aa.MilestCode == "W10B")
                                //                 && aa.SSN != "" &&
                                //                 (
                                //                      aa.ComplDate >= ((DateTime)res.CompletionDate).AddDays(-15) && aa.ComplDate <= ((DateTime)res.CompletionDate).AddDays(20)
                                                     
                                //                 )
                                //                 ).FirstOrDefault();
                                //        if (billedOffline != null && (denial == null || (denial.RejectDate.HasValue && denial.RejectDate <= billedOffline.OfflineSubmDate)))
                                //        {
                                //            res.DenialDate = null;
                                //            res.DenialReason = "";
                                //            res.DenialComments = "";
                                //            res.BilledOfflineDate = billedOffline.OfflineSubmDate;
                                //            res.BilledOfflineReason = billedOffline.ManualLoadReason;
                                //            res.BillingStatus = "Billed Offline";
                                //        }

                                //    }
                                //    if (res.SSN == null || res.SSN == "" || res.SSN == "         ")
                                //        res.BillingStatus = "Invalid SSN";
                                //    else if (res.BillingStatus == null) res.BillingStatus = "Pending";

                               // }
 
                            }





                            var result = bps1.Where(aa => aa.CompletionDate.Date  >= datFrom.DateTime && aa.CompletionDate.Date <= datTo.DateTime).Distinct();//.Union(bps2).Union(Day1Placements).Union(Day30Placements).Union(Day90Placements).Union(Day180Placements).Union(CRTFCODeterminations).Union(wellness).Union(DVEIPE).OrderBy(aa => aa.Milestone).ThenBy(a => a.Client);

                            foreach (var b in result)
                            {
                                b.Weekending = Utils.GetWeekEnding(b.CompletionDate.Date);
                            }
                            tbl = Utils.LINQToDataTable(result);
                            grdClients.DataSource = tbl;
                            grdClientsView.PopulateColumns();
                            if (result.Count() == 0)
                            {
                                splashScreenManager1.CloseWaitForm();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            grdClientsView.ColumnPanelRowHeight = 50;
                            CreatePivotTable(tbl);

                            foreach (GridColumn gc in grdClientsView.Columns)
                            {
                                if (gc.AbsoluteIndex != 0)
                                {
                                    gc.Width = 100;
                                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gc.AppearanceCell.Options.UseTextOptions = true;
                                }
                                else gc.Width = 300;
                            }
                            grdClientsView.ClearSorting();

                           
                            splashScreenManager1.CloseWaitForm();
                            this.Cursor = Cursors.Default;
                            cmdExport.Enabled = true;
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                this.Cursor = Cursors.Default;
                ErrorHandling.HandleError(ex);
            }
        }

        private void CreatePivotTable(DataTable data)
        {

            pivotGridControl1.DataSource = data;

            this.pivotGridControl1.ForceInitialize();
            this.pivotGridControl1.RetrieveFields();
            this.pivotGridControl1.Fields["Client"].SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            pivotGridControl1.Fields["Client"].Area = PivotArea.DataArea;
            pivotGridControl1.Fields["BillingStatus"].Area = PivotArea.ColumnArea;
            pivotGridControl1.Fields["Site"].Area = PivotArea.ColumnArea;
            PivotGridField CompletionYear = new PivotGridField("CompletionDate", PivotArea.RowArea);
            PivotGridField CompletionMonth = new PivotGridField("CompletionDate", PivotArea.RowArea);
            // Add the fields to the field collection.
            pivotGridControl1.Fields.Add(CompletionYear);
            pivotGridControl1.Fields.Add(CompletionMonth);
            // Set the caption and group mode of the fields.
            CompletionYear.GroupInterval = PivotGroupInterval.DateYear;
            CompletionYear.Caption = "CompletionYear";
            CompletionMonth.GroupInterval = PivotGroupInterval.DateMonth;
            CompletionMonth.Caption = "CompletionMonth";
            pivotGridControl1.CollapseAll();
            pivotGridControl1.Fields[21].ExpandAll();
            pivotGridControl1.Cells.Selection = new Rectangle(0, 0, pivotGridControl1.Cells.ColumnCount, pivotGridControl1.Cells.RowCount);
            pivotGridControl1.OptionsCustomization.CustomizationFormLayout = DevExpress.XtraPivotGrid.Customization.CustomizationFormLayout.BottomPanelOnly2by2;
            pivotGridControl1.FieldsCustomization(splitContainerControl1.Panel1);
          
       
        }     

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog d = new SaveFileDialog();
                d.CheckPathExists = true;
                if (xtraTabControl.SelectedTabPage == tabData)
                {
                    d.Filter = "Excel File (*.xls)|*.xls";
                    if (d.ShowDialog() != DialogResult.OK)
                        return;

                    grdClientsView.OptionsPrint.PrintDetails = false;
                    grdClientsView.OptionsPrint.ExpandAllDetails = false;

                    grdClientsView.OptionsPrint.SplitCellPreviewAcrossPages = true;
                    grdClientsView.OptionsPrint.AutoWidth = false;

                    grdClientsView.ExportToXls(d.FileName);
                }
                else if (xtraTabControl.SelectedTabPage == tabPivot)
                {
                    d.Filter = "Excel File (*.xls)|*.xls";
                    if (d.ShowDialog() != DialogResult.OK)
                        return;

                    pivotGridControl1.ExportToXls(d.FileName);

                }
              
               

                using (Process pr = new Process())
                {
                    pr.StartInfo = new ProcessStartInfo(d.FileName);
                    pr.Start();
                }

                Classes.Utils.ShowInfo("Export successful");


            }
            catch (Exception)
            {

                Classes.Utils.ShowError("Please Make sure the file is not open or being used by another application");

            }
        }     
      
        private void pivotGridControl_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            try
            {
                ShowDrilldown(e.CreateDrillDownDataSource());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ShowDrilldown(PivotDrillDownDataSource ds)
        {
            detailform.Controls.Clear();
                detailform.Text = "Detail Form";
                detailform.StartPosition = FormStartPosition.CenterParent;
                DevExpress.XtraGrid.GridControl detailsGrid = new DevExpress.XtraGrid.GridControl();
                DevExpress.XtraGrid.Views.Grid.GridView detailsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();

                detailsGridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                detailsGrid.Parent = detailform;
                detailsGrid.MainView = detailsGridView;
                detailsGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);

                detailsGrid.DataSource = ds;


                SimpleButton btn = new SimpleButton();
                btn.Text = "Export";
                
                btn.Click += new System.EventHandler(this.btn_Click);

                detailform.Controls.Add(btn);
                detailform.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.detailform_CloseForm);
                detailform.Width = 800;
                detailform.Height = 600;
                detailsGrid.Size = new Size(detailform.Width - 50, detailform.Height - 100);
                btn.Location = new Point(detailsGrid.Size.Width - 80, detailsGrid.Size.Height + 10);
                btn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                detailsGridView.OptionsBehavior.ReadOnly = true;
                detailsGridView.OptionsView.ColumnAutoWidth = false;
                detailsGridView.BestFitColumns();

                detailform.ShowDialog();
           
        }

        private void detailform_CloseForm(object sender, FormClosingEventArgs e)
        {
            this.detailform.Hide();
            e.Cancel = true;
            
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog d = new SaveFileDialog();
                d.CheckPathExists = true;

                d.Filter = "Excel File (*.xls)|*.xls";
                if (d.ShowDialog() != DialogResult.OK)
                    return;
                detailform.Controls.OfType<DevExpress.XtraGrid.GridControl>().FirstOrDefault().Views[0].ExportToXls(d.FileName);


                using (Process pr = new Process())
                {
                    pr.StartInfo = new ProcessStartInfo(d.FileName);
                    pr.Start();
                }

                Classes.Utils.ShowInfo("Export successful");


            }
            catch (Exception)
            {

                Classes.Utils.ShowError("Please Make sure the file is not open or being used by another application");

            }
        }

        void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartCtrl.SeriesTemplate.ChangeView((ViewType)cmbChartType.SelectedItem);

            if ((chartCtrl.SeriesTemplate.View as SimpleDiagramSeriesViewBase) == null)
                chartCtrl.Legend.Visible = true;
            if (chartCtrl.Diagram is Diagram3D)
            {
                Diagram3D diagram = (Diagram3D)chartCtrl.Diagram;
                diagram.RuntimeRotation = true;
                diagram.RuntimeZooming = true;
                diagram.RuntimeScrolling = true;
            }
            foreach (Series series in chartCtrl.Series)
            {
                ISupportTransparency supportTransparency = series.View as ISupportTransparency;
                if (supportTransparency != null)
                {
                    if ((series.View is AreaSeriesView) || (series.View is Area3DSeriesView)
                        || (series.View is RadarAreaSeriesView) || (series.View is Bar3DSeriesView))
                        supportTransparency.Transparency = 135;
                    else
                        supportTransparency.Transparency = 0;
                }
            }
        }
    }

   
}


