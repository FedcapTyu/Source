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

namespace FedCapSys
{
   public  partial class ucPerformanceIndicators : BaseModule
    {
        public ucPerformanceIndicators()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                using (Data.dbDataContext dc = new Data.dbDataContext())
                {
                    var locations = from l in dc.WeCARELocationsGroupeds  
                                   
                                    orderby l.Description  
                                    select new NameIdMapping { ID = l.WeCareLocationsGroupedId, Name = l.Description  };
                    foreach (var i in locations)
                        chklstLocation.Items.Add(i);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleError(ex);
            }
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
                    chartControl1.DataSource = null;
                    chartControl1.ClearCache();
                    chartControl1.Series.Clear();
                    DataTable tbl = new DataTable();
                    using (Data.dbDataContext dc = new Data.dbDataContext())
                    {
                        dc.CommandTimeout = 360;
                        List<MyResult> bps1 = new List<MyResult>();
                        List<MyResult> bps2 = new List<MyResult>();
                        List<MyResult> wellness = new List<MyResult>();
                        List<MyResult> CRTFCODeterminations = new List<MyResult>();
                        List<MyResult> DVEIPE = new List<MyResult>();
                        List<MyResult> Day1Placements = new List<MyResult>();
                        List<MyResult> Day30Placements = new List<MyResult>();
                        List<MyResult> Day90Placements = new List<MyResult>();
                        List<MyResult> Day180Placements = new List<MyResult>();

                        using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                        {
                            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                        }))
                        {
                            if (chklstColumns.Items["BPS1"].CheckState == CheckState.Checked)
                            {
                                //Calculate BPSI completed by counting number of BPS page 7 saved

                                bps1= (from formaudit in dc.cmformsaudits  
                                        from cmc in dc.cmcaseforms.DefaultIfEmpty()
                                        let page9 = dc.cmcaseforms.Where(aa=> aa.CaseID == cmc.CaseID && aa.EpisodeNumber == cmc.EpisodeNumber && aa.FormID == 531 ).FirstOrDefault()
                                        let page9Data = dc.CMDatas.Where(aa=> aa.RecordID == page9.TrackNumber && aa.FieldName == "DropEmpDisposition" ).FirstOrDefault()
                                        let track = page9Data.Value == "1" ? "No Functional Limitation" : (
                                                                page9Data.Value == "4" ? "VRS" : (
                                                                page9Data.Value == "2" ? "Wellness" : (
                                                                page9Data.Value == "6" ? "Wellness Plus" : (
                                                                page9Data.Value == "5" ? "Unable to Work" : "N/A"
                                                                ))))
                                     //   let statusAtFCO = dc.activity_tests.Where(aa=> aa.ssn == cmc.HRACase.SSN && (aa.action_code == "169F" || aa.action_code == "969F")
                                    //    && aa.action_date <= formaudit.SaveTime.AddDays(20) ).FirstOrDefault().action_code 
                                        where
                                        cmc.FormID == formaudit.FormID &&
                                        cmc.TrackNumber == formaudit.TrackNumber &&
                                        formaudit.SaveTime >= datFrom.DateTime.Date && formaudit.SaveTime < datTo.DateTime.Date.AddDays(1) &&
                                        formaudit.FormID == 1009 && formaudit.Seq == 2 &&
                                        (cmc.HRACase.LineNumber != "50" && !cmc.HRACase.CaseSurname.Contains("Test") && !cmc.HRACase.CaseFirstName.Contains("Test"))      //Exclude Test Cases

                                      select new MyResult()
                                        {
                                            Client = cmc.HRACase.CaseSurname.Trim() + ", " + cmc.HRACase.CaseFirstName.Trim(),
                                            SSN = cmc.HRACase.SSN,
                                            CaseNumber = cmc.HRACase.HRACaseNumber + "-" + cmc.HRACase.Suffix + "-" + cmc.HRACase.LineNumber,
                                            Site = dc.Schedule_FindLocationOfAppointment(cmc.HRACase.SSN, cmc.HRACase.HRACaseID, 2, formaudit.SaveTime).ToString(),
                                            Milestone = "BPS I",
                                            CompletionDate = formaudit.SaveTime,
                                            CompletedBy = cmc.logintable.FirstName + " " + cmc.logintable.LastName,
                                            Track = track ,
                                          //  StatusAtFCO = statusAtFCO == "169F" ? "Participant" : (statusAtFCO == "969F"? "Applicant" : "" ),
                                            isCRTFastTrack = "N/A",
                                            Weekending = null
                                        }).ToList();
                              
                            }

                            if (chklstColumns.Items["CRTFCODeterminations"].CheckState == CheckState.Checked)
                            {
                                List<string> NoLimitation = new List<string>(){ "1", "8525"};
                                List<string> VRS = new List<string>() { "4", "8523" };
                                List<string> Wellness = new List<string>() { "2", "8521" };
                                List<string> WellnessPlus = new List<string>() { "6", "8550" };
                                List<string> SSI = new List<string>() { "5", "8524" };

                                var FastTrackFCODeterminations  =( from formaudit in dc.cmformsaudits
                                                         from data in dc.CMDatas
                                                         from fastTrackData in dc.CMDatas 
                                                         let outcome = NoLimitation.Contains(data.Value)? "No Functional Limitation" : 
                                                         (VRS.Contains(data.Value)? "VRS" : (
                                                         Wellness.Contains(data.Value)? "Wellness" : (
                                                         WellnessPlus.Contains(data.Value)? "Wellness Plus" :(
                                                         SSI.Contains(data.Value) ? "Unable to Work" : (
                                                         data.Value == "8519" ? "New & Acute Condition" :(
                                                         data.Value == "8520" ? "New BPS Referral" :(
                                                         data.Value == "8526" ? "Return to previous WeCARE activity" :(
                                                         data.Value == "8527" ? "New Phase II Exam Required" : data.Value.Trim().ToString()
                                                         ))))))))
                                                    //     let statusAtFCO = dc.activity_tests.Where(aa => aa.ssn == formaudit.cmcaseform.HRACase.SSN && (aa.action_code == "169F" || aa.action_code == "969F")
                                                 // && aa.action_date <= formaudit.SaveTime.AddDays(20)).FirstOrDefault().action_code 
                                        where
                                        data.RecordID == formaudit.TrackNumber && data.FormID == formaudit.FormID &&
                                        data.FormID == 652 && data.FieldName == "DropReferralSource" &&
                                        fastTrackData.RecordID == data.RecordID && fastTrackData.FormID == data.FormID &&
                                        fastTrackData.FieldName == "txtIsFastTrack" && fastTrackData.Value == "1" &&
                                        formaudit.SaveTime >= datFrom.DateTime.Date && formaudit.SaveTime < datTo.DateTime.Date.AddDays(1) &&
                                        formaudit.Seq == 2 &&
                                        formaudit.cmcaseform.HRACase.LineNumber != "50"   //Exclude Test Cases
                                        select new MyResult()
                                        {
                                            Client = formaudit.cmcaseform.HRACase.CaseSurname.Trim() + ", " + formaudit.cmcaseform.HRACase.CaseFirstName.Trim(),
                                            SSN = formaudit.cmcaseform.HRACase.SSN,
                                            CaseNumber = formaudit.cmcaseform.HRACase.HRACaseNumber + "-" + formaudit.cmcaseform.HRACase.Suffix + "-" + formaudit.cmcaseform.HRACase.LineNumber,
                                            Site = dc.Schedule_FindLocationOfAppointment(formaudit.cmcaseform.HRACase.SSN, formaudit.cmcaseform.HRACase.HRACaseID, 5, formaudit.SaveTime).ToString(),
                                          //  StatusAtFCO = statusAtFCO == "169F" ? "Participant" : (statusAtFCO == "969F" ? "Applicant" : ""),
                                            Milestone = "CRT FCO Determination",
                                            Track = outcome,
                                            isCRTFastTrack = "Yes",
                                            CompletionDate = formaudit.SaveTime,
                                            CompletedBy = formaudit.cmcaseform.logintable.FirstName + " " + formaudit.cmcaseform.logintable.LastName,
                                            Weekending = null
                                        }).ToList();

                                var Pg3FCODeterminations = (from data in dc.CMDatas join
                                                            subOutcomeData in dc.CMDatas on new {data.RecordID, p2 = "DropEmpDisposition"} equals new {subOutcomeData.RecordID, p2 = subOutcomeData.FieldName   }
                                                            into leftjoin
                                                            from subOutcomeData in leftjoin.DefaultIfEmpty()
                                                           let outcome = subOutcomeData.Value == "1" ? "No Functional Limitation" : (
                                                                 subOutcomeData.Value == "4" ? "VRS" : (
                                                                 subOutcomeData.Value == "2" ? "Wellness" : (
                                                                 subOutcomeData.Value == "6" ? "Wellness Plus" : (
                                                                 subOutcomeData.Value == "5" ? "Unable to Work" : (
                                                                 data.Value == "3" ? "New & Acute Condition" : (
                                                                 data.Value == "1" ? "New BPS Referral" : (
                                                                 data.Value == "2" ? "New Phase II Exam Required" : data.Value.Trim().ToString()
                                                                 )))))))
                                                          // let statusAtFCO = dc.activity_tests.Where(aa => aa.ssn == data.cmcaseform.HRACase.SSN && (aa.action_code == "169F" || aa.action_code == "969F")
                                                          //      && aa.action_date <= ((DateTime)data.cmcaseform.LastSavedWhen).AddDays(20)).FirstOrDefault().action_code 
                                                           where
                                                           data.FormID == 1003 && data.FieldName == "selectoutcome" &&
                                                           data.cmcaseform.LastSavedWhen >= datFrom.DateTime.Date && data.cmcaseform.LastSavedWhen < datTo.DateTime.Date.AddDays(1) &&
                                                               // formaudit.Seq == 2 &&
                                                           data.cmcaseform.HRACase.LineNumber != "50"   //Exclude Test Cases
                                                           select new MyResult()
                                                           {
                                                               Client = data.cmcaseform.HRACase.CaseSurname.Trim() + ", " + data.cmcaseform.HRACase.CaseFirstName.Trim(),
                                                               SSN = data.cmcaseform.HRACase.SSN,
                                                               CaseNumber = data.cmcaseform.HRACase.HRACaseNumber + "-" + data.cmcaseform.HRACase.Suffix + "-" + data.cmcaseform.HRACase.LineNumber,
                                                               Site = dc.Schedule_FindLocationOfAppointment(data.cmcaseform.HRACase.SSN, data.cmcaseform.HRACase.HRACaseID, 5, (DateTime)data.cmcaseform.LastSavedWhen).ToString(),
                                                               //StatusAtFCO = statusAtFCO == "169F" ? "Participant" : (statusAtFCO == "969F" ? "Applicant" : ""),
                                                               Milestone = "CRT FCO Determination",
                                                               Track = outcome,
                                                               isCRTFastTrack = "No",
                                                               CompletionDate = (DateTime)data.cmcaseform.LastSavedWhen,
                                                               CompletedBy = data.cmcaseform.logintable.FirstName + " " + data.cmcaseform.logintable.LastName,
                                                               Weekending = null
                                                           }).ToList();



                                var Pg2FCODeterminations = (from data in dc.CMDatas join
                                                            subOutcomeData in dc.CMDatas on new {data.RecordID, p2 = "DropEmpDisposition"} equals new {subOutcomeData.RecordID, p2 = subOutcomeData.FieldName   }
                                                            into leftjoin
                                                            from subOutcomeData in leftjoin.DefaultIfEmpty()
                                                           let outcome = subOutcomeData.Value == "1" ? "No Functional Limitation" : (
                                                                 subOutcomeData.Value == "4" ? "VRS" : (
                                                                 subOutcomeData.Value == "2" ? "Wellness" : (
                                                                 subOutcomeData.Value == "6" ? "Wellness Plus" : (
                                                                 subOutcomeData.Value == "5" ? "Unable to Work" : (
                                                                 data.Value == "3" ? "New & Acute Condition" : (
                                                                 data.Value == "1" ? "New BPS Referral" : (
                                                                 data.Value == "2" ? "New Phase II Exam Required" : data.Value.Trim().ToString()
                                                                 )))))))
                                                        //   let statusAtFCO = dc.activity_tests.Where(aa => aa.ssn == data.cmcaseform.HRACase.SSN && (aa.action_code == "169F" || aa.action_code == "969F")
                                                        //         && aa.action_date <= ((DateTime)data.cmcaseform.LastSavedWhen).AddDays(20)).FirstOrDefault().action_code 
                                                           where                                                          
                                                           data.FormID == 654 && data.FieldName == "selectoutcome" &&
                                                           data.cmcaseform.LastSavedWhen >= datFrom.DateTime.Date && data.cmcaseform.LastSavedWhen < datTo.DateTime.Date.AddDays(1) &&
                                                               // formaudit.Seq == 2 &&
                                                           data.cmcaseform.HRACase.LineNumber != "50"   //Exclude Test Cases
                                                           select new MyResult()
                                                           {
                                                               Client = data.cmcaseform.HRACase.CaseSurname.Trim() + ", " + data.cmcaseform.HRACase.CaseFirstName.Trim(),
                                                               SSN = data.cmcaseform.HRACase.SSN,
                                                               CaseNumber = data.cmcaseform.HRACase.HRACaseNumber + "-" + data.cmcaseform.HRACase.Suffix + "-" + data.cmcaseform.HRACase.LineNumber,
                                                               Site = dc.Schedule_FindLocationOfAppointment(data.cmcaseform.HRACase.SSN, data.cmcaseform.HRACase.HRACaseID, 5, (DateTime)data.cmcaseform.LastSavedWhen).ToString(),
                                                           //    StatusAtFCO = statusAtFCO == "169F" ? "Participant" : (statusAtFCO == "969F" ? "Applicant" : ""),
                                                               Milestone = "CRT FCO Determination",
                                                               Track = outcome,
                                                               isCRTFastTrack = "No",
                                                               CompletionDate = (DateTime)data.cmcaseform.LastSavedWhen,
                                                               CompletedBy = data.cmcaseform.logintable.FirstName + " " + data.cmcaseform.logintable.LastName,
                                                               Weekending = null
                                                           }).ToList();


                                var pg2FCODetermintaionsWithNoPg3 = (from pg2 in Pg2FCODeterminations
                                                                     where
                                                                      !Pg3FCODeterminations.Any(aa => aa.CaseNumber == pg2.CaseNumber)
                                                                     select new MyResult()
                                                           {
                                                               Client = pg2.Client,
                                                               SSN = pg2.SSN,
                                                               CaseNumber = pg2.CaseNumber,
                                                               Site = pg2.Site,
                                                               StatusAtFCO = pg2.StatusAtFCO ,
                                                               Milestone = pg2.Milestone,
                                                               Track = pg2.Track,
                                                               isCRTFastTrack = pg2.isCRTFastTrack,
                                                               CompletionDate = pg2.CompletionDate,
                                                               Weekending = pg2.Weekending,
                                                               CompletedBy = pg2.CompletedBy 

                                                           }).ToList();

                                CRTFCODeterminations = FastTrackFCODeterminations.Union(pg2FCODetermintaionsWithNoPg3).Union(Pg3FCODeterminations).ToList();
                            }

                            if (chklstColumns.Items["BPS2"].CheckState == CheckState.Checked)
                            {
                                List<int> BPS2Forms = new List<int>() { 536, 570, 574, 580, 581, 1002, 1284 };
                                bps2 = (from forms in dc.cmcaseforms
                                        where BPS2Forms.Contains(forms.FormID)
                                        && forms.Comments.Contains("Exam Completed")
                                        && forms.LastSavedWhen >= datFrom.DateTime.Date && forms.LastSavedWhen < datTo.DateTime.Date.AddDays(1)
                                        && (forms.HRACase.LineNumber != "50" && !forms.HRACase.CaseSurname.Contains("Test") && !forms.HRACase.CaseFirstName.Contains("Test"))      //Exclude Test Cases
                                        select new MyResult()
                                        {
                                            Client = forms.HRACase.CaseSurname.Trim() + ", " + forms.HRACase.CaseFirstName.Trim(),
                                            SSN = forms.HRACase.SSN,
                                            CaseNumber = forms.HRACase.HRACaseNumber + "-" + forms.HRACase.Suffix + "-" + forms.HRACase.LineNumber,
                                            Site = dc.Schedule_FindLocationOfAppointment(forms.HRACase.SSN, forms.HRACase.HRACaseID, 2, forms.LastSavedWhen).ToString(),
                                            Milestone = "BPS II",
                                            Track = forms.CMForm.Name,
                                            isCRTFastTrack = "No",
                                            CompletionDate = (DateTime)forms.LastSavedWhen,
                                            CompletedBy = forms.logintable.FirstName + " " + forms.logintable.LastName,
                                            Weekending = null

                                        }).ToList();

                              //  var res = dc.GetCompletedBPS2Results(datFrom.DateTime.Date, datTo.DateTime.Date);
                              //  bps2 = res.GetResult<MyResult>().ToList();

                            }

                            if (chklstColumns.Items["WellnessCompleted"].CheckState == CheckState.Checked)
                            {
                               // var wellnessRoutes = from actions in dc.CMActions
                                                   //  where actions.Description.Contains("Wellness Process Completed")
                                 //                    where actions.Description.Contains("Send to case Manager") 
                                   //                  select actions;
                                //Calculate Wellness completed by "Wellness Completed" routes on Wellness Tracking form 
                                //wellness = (from data in dc.CMData_RouteDates
                                //            from reasons in dc.CMReasons
                                //            where
                                //            reasons.ID.ToString() == data.RouteReason &&
                                //            data.RouteValue == "140" && (data.RouteReason == "320" || data.RouteReason == "321" || data.RouteReason == "322" || data.RouteReason == "323") &&
                                //            data.DateValue >= datFrom.DateTime.Date && data.DateValue < datTo.DateTime.Date.AddDays(1) &&
                                //           (data.FormID == 563 || data.FormID == 5631) &&
                                //            data.cmcaseform.HRACase.LineNumber != "50"   //Exclude Test Cases
                                //            select new MyResult()
                                //            {
                                //                Client = data.cmcaseform.HRACase.CaseSurname.Trim() + ", " + data.cmcaseform.HRACase.CaseFirstName.Trim(),
                                //                SSN = data.cmcaseform.HRACase.SSN,
                                //                CaseNumber = data.cmcaseform.HRACase.HRACaseNumber + "-" + data.cmcaseform.HRACase.Suffix + "-" + data.cmcaseform.HRACase.LineNumber,
                                //                Site = dc.Schedule_FindLocationOfAppointment(data.cmcaseform.HRACase.SSN, data.cmcaseform.HRACase.HRACaseID, 10, (DateTime)data.DateValue).ToString(),
                                //                Milestone = data.FormID == 563 ? "Wellness" : "Wellness Plus",
                                //                Track = reasons.Reason,
                                //                CompletionDate = (DateTime)data.DateValue,
                                //                CompletedBy = data.cmcaseform.logintable.FirstName + " " + data.cmcaseform.logintable.LastName,
                                //                Weekending = null
                                //            }).ToList();

                                //Calculate Wellness completed by counted non-extension completed exam forms

                                wellness = ( from cmf in dc.cmcaseforms
                                             from formData in dc.CMDatas 
                                             let TrackingFormId = dc.cmcaseforms.Where(aa => aa.CaseID == cmf.CaseID && aa.EpisodeNumber == cmf.EpisodeNumber && (aa.FormID == 563 || aa.FormID == 563)).OrderByDescending(aa=> aa.LastSavedWhen).FirstOrDefault().FormID 
                                             where cmf.TrackNumber == formData.RecordID &&
                                         //  cmf.SequenceNum == 2 &&
                                           (cmf.FormID == 397 || cmf.FormID == 379) &&
                                           formData.FieldName == "DropEmpDisposition"  &&
                                           (formData.Value == "1" || formData.Value == "4" ||formData.Value == "5") && 
                                            cmf.LastSavedWhen >= datFrom.DateTime.Date && cmf.LastSavedWhen < datTo.DateTime.Date.AddDays(1) &&
                                          (cmf.HRACase.LineNumber != "50" && !cmf.HRACase.CaseSurname.Contains("Test") && !cmf.HRACase.CaseFirstName.Contains("Test"))      //Exclude Test Cases
                                           select new MyResult()
                                            {
                                                Client = cmf.HRACase.CaseSurname.Trim() + ", " + cmf.HRACase.CaseFirstName.Trim(),
                                                SSN = cmf.HRACase.SSN,
                                                CaseNumber = cmf.HRACase.HRACaseNumber + "-" + cmf.HRACase.Suffix + "-" + cmf.HRACase.LineNumber,
                                                Site = dc.Schedule_FindLocationOfAppointment(cmf.HRACase.SSN, cmf.HRACase.HRACaseID, 10, (DateTime)cmf.LastSavedWhen).ToString(),
                                                Milestone = TrackingFormId == 563 ? "Wellness" : "Wellness Plus",
                                                Track = formData.Value == "1"?"No Functional Limitation":formData.Value == "4"?"VRS":formData.Value == "5"?"Unable To Work":"Unknown",
                                                CompletionDate = (DateTime)cmf.LastSavedWhen.Value.Date ,
                                                CompletedBy = cmf.logintable.FirstName + " " + cmf.logintable.LastName,
                                                Weekending = null
                                            }).Distinct().ToList();
                        
                            }                          

                            if (chklstColumns.Items["DVEIPECompleted"].CheckState == CheckState.Checked)
                            {
                                 DVEIPE = (from cmf in dc.cmcaseforms
                                           let cmd = dc.CMDatas.Where(aa=> aa.RecordID == cmf.TrackNumber && aa.FieldName == "cboReason").FirstOrDefault()
                                           where cmf.LastSavedBy != "SysAdmin" &&
                                                 cmf.FormID == 558 &&
                                                 (cmf.Comments.Contains("Client Signed") || cmf.Comments.Contains("Client refused") || cmf.Comments == "Signed"
                                                 || cmd.Value == "3" || cmd.Value == "4") &&
                                                 cmf.LastSavedWhen >= datFrom.DateTime.Date && cmf.LastSavedWhen < datTo.DateTime.Date.AddDays(1)

                                           select new MyResult()
                                           {
                                               Client = cmf.HRACase.CaseSurname.Trim() + ", " + cmf.HRACase.CaseFirstName.Trim(),
                                               SSN = cmf.HRACase.SSN,
                                               CaseNumber = cmf.HRACase.HRACaseNumber + "-" + cmf.HRACase.Suffix + "-" + cmf.HRACase.LineNumber,
                                               Site = dc.Schedule_FindLocationOfAppointment(cmf.HRACase.SSN, cmf.HRACase.HRACaseID, 10, (DateTime)cmf.LastSavedWhen).ToString(),
                                               Milestone = "IPE",
                                               Track = "VRS",
                                               CompletionDate = (DateTime)cmf.LastSavedWhen.Value.Date,
                                               CompletedBy = cmf.logintable.FirstName + " " + cmf.logintable.LastName,
                                               Weekending = null
                                           }).ToList();


                               // var res = dc.GetSignedIPEResults(datFrom.DateTime.Date, datTo.DateTime.Date);
                               // DVEIPE = res.GetResult<MyResult>().ToList();

                            }

                            if (chklstColumns.Items["Day1Placements"].CheckState == CheckState.Checked)
                            {

                                var Day1PlacementsWeekly = (from actions in dc.activity_tests
                                                  where (actions.action_code == "169j" || actions.action_code == "16PJ")  &&
                                                  (actions.ssn != null && actions.ssn != "" && actions.ssn != "         ") &&
                                                  actions.action_date >= datFrom.DateTime.Date && actions.action_date <= datTo.DateTime.Date
                                                  select new MyResult()
                                                  {
                                                      Client = actions.HRACase.CaseSurname.Trim() + ", " + actions.HRACase.CaseFirstName.Trim(),
                                                      SSN = actions.ssn,
                                                      CaseNumber = actions.case_number + "-" + actions.suffix + "-" + actions.line,
                                                      Site = dc.Schedule_FindLocationOfAppointment(actions.HRACase.SSN, actions.HRACase.HRACaseID, 1000, (DateTime)actions.action_date).ToString(),
                                                      Milestone = "Day 1 Placements",
                                                      CompletionDate = (DateTime)actions.action_date,
                                                     // CompletedBy = actions.User_ID,
                                                      Weekending = null
                                                  }).Distinct();
                                var Day1PlacementsDaily = (from actions in dc.Activity1s
                                                           where (actions.action_code == "169j" || actions.action_code == "16PJ") &&
                                                  (actions.ssn != null && actions.ssn != "" && actions.ssn != "         ") &&
                                                            actions.action_date >= datFrom.DateTime.Date && actions.action_date <= datTo.DateTime.Date
                                                            select new MyResult()
                                                            {
                                                                Client = actions.HRACase.CaseSurname.Trim() + ", " + actions.HRACase.CaseFirstName.Trim(),
                                                                SSN = actions.ssn,
                                                                CaseNumber = actions.case_number + "-" + actions.suffix + "-" + actions.line,
                                                                Site = dc.Schedule_FindLocationOfAppointment(actions.HRACase.SSN, actions.HRACase.HRACaseID, 1000, (DateTime)actions.action_date).ToString(),
                                                                Milestone = "Day 1 Placements",
                                                                CompletionDate = (DateTime)actions.action_date,
                                                               // CompletedBy = actions.User_ID,
                                                                Weekending = null
                                                            }).Distinct();
                                Day1Placements = Day1PlacementsWeekly.Union(Day1PlacementsDaily).ToList();

                            }

                            if (chklstColumns.Items["Day30Retention"].CheckState == CheckState.Checked)
                            {
                              var  Day30 = (from batches in dc.cmconvertlists
                                                   from attachdata in dc.CMDatas
                                                   where
                                                   attachdata.RecordID == batches.RecordID &&
                                                   attachdata.FieldName == "txtScan1" &&
                                                   batches.DocType == 8716 &&
                                                 //  Convert.ToDateTime(attachdata.Value) >= datFrom.DateTime.Date && Convert.ToDateTime(attachdata.Value) < datTo.DateTime.Date.AddDays(1) &&
                                                 //  !dc.CMDatas.Any( aa=> aa.cmcaseform.CaseID == attachdata.cmcaseform.CaseID &&  aa.FieldName == "txtScan1" && Convert.ToDateTime(aa.Value).AddDays(10) < Convert.ToDateTime(attachdata.Value)) &&
                                                    batches.HRACase.LineNumber != "50" 
                                                   //group new {batches,attachdata} by new 
                                                   //{
                                                   //    Client = batches.HRACase.CaseSurname.Trim() + ", " + batches.HRACase.CaseFirstName.Trim(),
                                                   //    SSN = batches.HRACase.SSN,
                                                   //    CaseNumber = batches.HRACase.HRACaseNumber + "-" + batches.HRACase.Suffix + "-" + batches.HRACase.LineNumber,
                                                   //    CaseId = batches.HRACase.HRACaseID 
                                                   //} into g
                                                   select new MyResult()
                                                   {
                                                       Client = batches.HRACase.CaseSurname.Trim() + ", " + batches.HRACase.CaseFirstName.Trim(),
                                                       SSN = batches.HRACase.SSN,
                                                       CaseNumber = batches.HRACase.HRACaseNumber + "-" + batches.HRACase.Suffix + "-" + batches.HRACase.LineNumber,
                                                       Site = dc.Schedule_FindLocationOfAppointment(batches.HRACase.SSN, batches.HRACase.HRACaseID, 1000, Convert.ToDateTime(attachdata.Value)).ToString(),
                                                       Milestone = "30 Day Retention",
                                                       CompletionDate = Convert.ToDateTime(attachdata.Value) ,
                                                       Weekending = null
                                                   }).Distinct().ToList();
                              Day30Placements = (from list in Day30
                                                where !Day30.Any(aa => aa.SSN == list.SSN && list.CompletionDate < aa.CompletionDate.AddDays(20) && list.CompletionDate > aa.CompletionDate  && aa.Milestone == list.Milestone) &&
                                                 list.CompletionDate >= datFrom.DateTime.Date && list.CompletionDate < datTo.DateTime.Date.AddDays(1)
                                                select list).ToList();


                                                  

                                //var Day30PlacementsPaid = (from miestones in dc.Milestones_Paids
                                //                   where miestones.MilestName == "30-Day Ret Unsub" &&
                                //                  miestones.PaidDate >= datFrom.DateTime.Date && miestones.PaidDate <= datTo.DateTime.Date
                                //                   select new MyResult()
                                //                   {
                                //                       Client = miestones.Lastname.Trim() + ", " + miestones.Firstname.Trim(),
                                //                       SSN = miestones.SSN,
                                //                       CaseNumber = miestones.Casenum + "-" + miestones.Suffix + "-" + miestones.Line,
                                //                       Milestone = "30 Day Retention",
                                //                       CompletionDate = (DateTime)miestones.PaidDate,
                                //                       Weekending = null
                                //                   }).ToList();


                            }

                            if (chklstColumns.Items["Day90Retention"].CheckState == CheckState.Checked)
                            {
                                var Day90 = (from batches in dc.cmconvertlists
                                                   from attachdata in dc.CMDatas
                                                   where
                                                   attachdata.RecordID == batches.RecordID &&
                                                   attachdata.FieldName == "txtScan1" &&
                                                   batches.DocType == 8717 &&
                                                  //Convert.ToDateTime(attachdata.Value) >= datFrom.DateTime.Date && Convert.ToDateTime(attachdata.Value) < datTo.DateTime.Date.AddDays(1) &&
                                                    batches.HRACase.LineNumber != "50"
                                                   //group new { batches, attachdata } by new
                                                   //{
                                                   //    Client = batches.HRACase.CaseSurname.Trim() + ", " + batches.HRACase.CaseFirstName.Trim(),
                                                   //    SSN = batches.HRACase.SSN,
                                                   //    CaseNumber = batches.HRACase.HRACaseNumber + "-" + batches.HRACase.Suffix + "-" + batches.HRACase.LineNumber,
                                                   //    CaseId = batches.HRACase.HRACaseID
                                                   //} into g
                                                   select new MyResult()
                                                   {
                                                       Client = batches.HRACase.CaseSurname.Trim() + ", " + batches.HRACase.CaseFirstName.Trim(),
                                                       SSN = batches.HRACase.SSN,
                                                       CaseNumber = batches.HRACase.HRACaseNumber + "-" + batches.HRACase.Suffix + "-" + batches.HRACase.LineNumber,
                                                       Site = dc.Schedule_FindLocationOfAppointment(batches.HRACase.SSN, batches.HRACase.HRACaseID, 1000, Convert.ToDateTime(attachdata.Value)).ToString(),
                                                       Milestone = "90 Day Retention",
                                                       CompletionDate = Convert.ToDateTime(attachdata.Value),
                                                       Weekending = null
                                                   }).Distinct().ToList();

                                Day90Placements = (from list in Day90
                                                   where !Day90.Any(aa => aa.SSN == list.SSN && list.CompletionDate <= aa.CompletionDate.AddDays(60) && list.CompletionDate > aa.CompletionDate && aa.Milestone == list.Milestone) &&
                                                    list.CompletionDate >= datFrom.DateTime.Date && list.CompletionDate < datTo.DateTime.Date.AddDays(1)
                                                   select list).ToList();


                              //var  Day90PlacementsPaid = (from miestones in dc.Milestones_Paids
                              //                     where miestones.MilestName == "90-Day Ret Unsub" &&
                              //                    miestones.PaidDate >= datFrom.DateTime.Date && miestones.PaidDate <= datTo.DateTime.Date
                              //                     select new MyResult()
                              //                     {
                              //                         Client = miestones.Lastname.Trim() + ", " + miestones.Firstname.Trim(),
                              //                         SSN = miestones.SSN,
                              //                         CaseNumber = miestones.Casenum + "-" + miestones.Suffix + "-" + miestones.Line,
                              //                         Milestone = "90 Day Retention",
                              //                         CompletionDate = (DateTime)miestones.PaidDate,
                              //                         Weekending = null
                              //                     }).ToList();


                            }
                            if (chklstColumns.Items["Day180Retention"].CheckState == CheckState.Checked)
                            {

                              var Day180 = (from batches in dc.cmconvertlists
                                                    from attachdata in dc.CMDatas
                                                    where
                                                    attachdata.RecordID == batches.RecordID &&
                                                    attachdata.FieldName == "txtScan1" &&
                                                    batches.DocType == 8718 &&
                                                 //  Convert.ToDateTime(attachdata.Value) >= datFrom.DateTime.Date && Convert.ToDateTime(attachdata.Value) < datTo.DateTime.Date.AddDays(1) &&
                                                     batches.HRACase.LineNumber != "50"
                                                    //group new { batches, attachdata } by new
                                                    //{
                                                    //    Client = batches.HRACase.CaseSurname.Trim() + ", " + batches.HRACase.CaseFirstName.Trim(),
                                                    //    SSN = batches.HRACase.SSN,
                                                    //    CaseNumber = batches.HRACase.HRACaseNumber + "-" + batches.HRACase.Suffix + "-" + batches.HRACase.LineNumber,
                                                    //    CaseId = batches.HRACase.HRACaseID
                                                    //} into g
                                                    select new MyResult()
                                                    {
                                                        Client = batches.HRACase.CaseSurname.Trim() + ", " + batches.HRACase.CaseFirstName.Trim(),
                                                        SSN = batches.HRACase.SSN,
                                                        CaseNumber = batches.HRACase.HRACaseNumber + "-" + batches.HRACase.Suffix + "-" + batches.HRACase.LineNumber,
                                                        Site = dc.Schedule_FindLocationOfAppointment(batches.HRACase.SSN, batches.HRACase.HRACaseID, 1000, Convert.ToDateTime(attachdata.Value)).ToString(),
                                                        Milestone = "180 Day Retention",
                                                        CompletionDate = Convert.ToDateTime(attachdata.Value),
                                                        Weekending = null
                                                    }).Distinct().ToList();


                                Day180Placements = (from list in Day180
                                                    where !Day180.Any(aa => aa.SSN == list.SSN && list.CompletionDate < aa.CompletionDate.AddDays(60) && list.CompletionDate > aa.CompletionDate && aa.Milestone == list.Milestone) &&
                                                    list.CompletionDate >= datFrom.DateTime.Date && list.CompletionDate < datTo.DateTime.Date.AddDays(1)
                                                   select list).ToList();

                              //var  Day180PlacementsPaid = (from miestones in dc.Milestones_Paids
                              //                      where miestones.MilestName == "180-Day Ret Unsub" &&
                              //                     miestones.PaidDate >= datFrom.DateTime.Date && miestones.PaidDate <= datTo.DateTime.Date
                              //                      select new MyResult()
                              //                      {
                              //                           Client = miestones.Lastname.Trim() + ", " + miestones.Firstname.Trim(),
                              //                          SSN = miestones.SSN,
                              //                          CaseNumber = miestones.Casenum + "-" + miestones.Suffix + "-" + miestones.Line,
                              //                          Milestone = "180 Day Retention",
                              //                          CompletionDate = (DateTime)miestones.PaidDate,
                              //                          Weekending = null
                              //                      }).ToList();
                            }



                            var result = bps1.Union(bps2).Union(Day1Placements).Union(Day30Placements).Union(Day90Placements).Union(Day180Placements).Union(CRTFCODeterminations).Union(wellness).Union(DVEIPE).OrderBy(aa => aa.Milestone).ThenBy(a=> a.Client);

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
                            CreateChartSeries(tbl);

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

                            chartControl1.Visible = true;
                            splashScreenManager1.CloseWaitForm();
                            this.Cursor = Cursors.Default;
                            cmdExport.Enabled = true;
                            chartControl1.Focus();
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

        private void CreateChartSeries(DataTable  data)
        {
            DataTable tbl = new DataTable();
            chartControl1.Series.Clear();
            if ((string)radChartInterval.EditValue == "day")
            {
                var results = from b1 in data.AsEnumerable()
                              group b1 by                               
                                  b1.Field<DateTime>("CompletionDate").Date                          
                                  into g
                                  orderby g.Key
                                  select new
                                  {
                                      Key = g.Key.ToShortDateString(),
                                      BPS1 = g.Count(aa => aa.Field<string>("Milestone") == "BPS I"),
                                      BPS2 = g.Count(aa => aa.Field<string>("Milestone") == "BPS II"),
                                      CRTFCODeterminations = g.Count(aa => aa.Field<string>("Milestone") == "CRT FCO Determination"),
                                      WellnessCompleted = g.Count(aa => aa.Field<string>("Milestone") == "Wellness") + g.Count(aa => aa.Field<string>("Milestone") == "Wellness Plus"),
                                      DVEIPECompleted = g.Count(aa => aa.Field<string>("Milestone") == "IPE"),
                                      Day1Placements = g.Count(aa => aa.Field<string>("Milestone") == "Day 1 Placements"),
                                      Day30Retention = g.Count(aa => aa.Field<string>("Milestone") == "30 Day Retention"),
                                      Day90Retention = g.Count(aa => aa.Field<string>("Milestone") == "90 Day Retention"),
                                      Day180Retention = g.Count(aa => aa.Field<string>("Milestone") == "180 Day Retention")
                                  };

                tbl = Utils.LINQToDataTable(results);
            }
            else if ((string)radChartInterval.EditValue == "week")
            {
                var results = from b1 in data.AsEnumerable()
                              group b1 by
                                  b1.Field<DateTime>("Weekending")
                                  into g
                                  orderby g.Key
                                  select new
                                  {
                                      Key = g.Key.ToShortDateString(),
                                      BPS1 = g.Count(aa=> aa.Field<string>("Milestone") == "BPS I" ),
                                      BPS2 = g.Count(aa => aa.Field<string>("Milestone") == "BPS II"),
                                      CRTFCODeterminations = g.Count(aa => aa.Field<string>("Milestone") == "CRT FCO Determination"),
                                      DVEIPECompleted = g.Count(aa => aa.Field<string>("Milestone") == "IPE"),
                                      WellnessCompleted = g.Count(aa => aa.Field<string>("Milestone") == "Wellness") + g.Count(aa => aa.Field<string>("Milestone") == "Wellness Plus"),
                                      Day1Placements = g.Count(aa => aa.Field<string>("Milestone") == "Day 1 Placements"),
                                      Day30Retention = g.Count(aa => aa.Field<string>("Milestone") == "30 Day Retention"),
                                      Day90Retention = g.Count(aa => aa.Field<string>("Milestone") == "90 Day Retention"),
                                      Day180Retention = g.Count(aa => aa.Field<string>("Milestone") == "180 Day Retention")
                                  };

                tbl = Utils.LINQToDataTable(results);
            }
            else if ((string)radChartInterval.EditValue == "month")
            {
                var results = from b1 in data.AsEnumerable()
                              group b1 by new
                              {
                                  Date = b1.Field<DateTime>("CompletionDate").ToString("MMMM, yyyy"),
                                  b1.Field<DateTime>("CompletionDate").Year,
                                  b1.Field<DateTime>("CompletionDate").Month
                              }
                                  into g
                                  orderby g.Key.Year , g.Key.Month 
                                  select new
                                  {
                                      Key = g.Key.Date,
                                      BPS1 = g.Count(aa => aa.Field<string>("Milestone") == "BPS I"),
                                      BPS2 = g.Count(aa => aa.Field<string>("Milestone") == "BPS II"),
                                      CRTFCODeterminations = g.Count(aa => aa.Field<string>("Milestone") == "CRT FCO Determination"),
                                      DVEIPECompleted = g.Count(aa => aa.Field<string>("Milestone") == "IPE"),
                                      WellnessCompleted = g.Count(aa => aa.Field<string>("Milestone") == "Wellness") + g.Count(aa => aa.Field<string>("Milestone") == "Wellness Plus"),
                                      Day1Placements = g.Count(aa => aa.Field<string>("Milestone") == "Day 1 Placements"),
                                      Day30Retention = g.Count(aa => aa.Field<string>("Milestone") == "30 Day Retention"),
                                      Day90Retention = g.Count(aa => aa.Field<string>("Milestone") == "90 Day Retention"),
                                      Day180Retention = g.Count(aa => aa.Field<string>("Milestone") == "180 Day Retention")
                                  };

                tbl = Utils.LINQToDataTable(results);
            }
            
            chartControl1.DataSource = tbl;
            for (int i = 0; i < chklstColumns.Items.Count; i++ )
            {
                if (chklstColumns.Items[i].CheckState == CheckState.Checked )
                {
                    Series series;
                    if ((string)radChartType.EditValue == "line")
                    {
                        series = new Series(chklstColumns.Items[i].Description, ViewType.Line);

                    }
                    else
                    {
                        series = new Series(chklstColumns.Items[i].Description, ViewType.Bar);
                        ((BarSeriesView)series.View).FillStyle.FillMode = FillMode.Gradient;
                        ((RectangleGradientFillOptions)((BarSeriesView)series.View).FillStyle.Options).GradientMode = RectangleGradientMode.BottomLeftToTopRight;
                        ((BarSeriesView)series.View).Shadow.Visible = true;
                        ((BarSeriesView)series.View).Shadow.Size = 4;
                        ((BarSeriesLabel)series.Label).Position = BarSeriesLabelPosition.BottomInside;
                    }
                    chartControl1.Series.Add(series);
                    series.ArgumentScaleType = ScaleType.Qualitative;
                    series.ArgumentDataMember = "Key";
                    series.ValueScaleType = ScaleType.Numerical;
                    series.ValueDataMembers.AddRange(new string[] { chklstColumns.Items[i].Value.ToString() });
                    series.ShowInLegend = true;

                    series.Label.PointOptions.PointView = PointView.Values;
                    series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    series.Label.PointOptions.ValueNumericOptions.Precision = 0;
                    series.Label.PointOptions.Pattern = "{S}: {V}";
                    series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    series.Label.MaxLineCount = 2;
                    series.Label.MaxWidth = 50;
                    series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

                }

            }           
          
            ((XYDiagram)chartControl1.Diagram).EnableAxisXScrolling = true  ;
            ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true ;
            ((XYDiagram)chartControl1.Diagram).AxisX.Range.Auto = true ;
            //((XYDiagram)chartControl1.Diagram).AxisX.Range.MaxValueInternal = 5;
            //((XYDiagram)chartControl1.Diagram).AxisX.Range.MinValueInternal = -0.5;
            //((XYDiagram)chartControl1.Diagram).AxisX.Range.MinValue = 0;
            ((XYDiagram)chartControl1.Diagram).AxisY.Range.Auto = true;               
            
            chartControl1.Refresh();

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
                else if (xtraTabControl.SelectedTabPage == tabCharts)
                {
                    d.Filter = "JPEG File (*.jpg)|*.jpg";
                    if (d.ShowDialog() != DialogResult.OK)
                        return;
                    DialogResult res = Classes.Utils.ShowQuestion("Do you want to export only the visible portion of the chart?");
                    if (res == DialogResult.No)
                    {
                        AxisRange rangeX = ((XYDiagram)chartControl1.Diagram).AxisX.Range;
                        AxisRange rangeY = ((XYDiagram)chartControl1.Diagram).AxisY.Range;
                        AxisRange rangeSecondY = ((XYDiagram)chartControl1.Diagram).SecondaryAxesY[0].Range;

                        double lastXMin = rangeX.MinValueInternal;
                        double lastXMax = rangeX.MaxValueInternal;
                        double lastYMin = rangeY.MinValueInternal;
                        double lastYMax = rangeY.MaxValueInternal;
                        double lastSecYMin = rangeSecondY.MinValueInternal;
                        double lastSecYMax = rangeSecondY.MaxValueInternal;

                        rangeX.Auto = true;
                        rangeY.Auto = true;

                        chartControl1.OptionsPrint.ImageFormat = DevExpress.XtraCharts.Printing.PrintImageFormat.Bitmap;
                        chartControl1.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;

                        chartControl1.ExportToImage(d.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                        rangeX.SetInternalMinMaxValues(lastXMin, lastXMax);
                        rangeY.SetInternalMinMaxValues(lastYMin, lastYMax);
                        rangeSecondY.SetInternalMinMaxValues(lastSecYMin, lastSecYMax);
                    }
                    else
                    {
                        chartControl1.OptionsPrint.ImageFormat = DevExpress.XtraCharts.Printing.PrintImageFormat.Bitmap;
                        chartControl1.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;

                        chartControl1.ExportToImage(d.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
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
       

        private void chartControl1_Paint(object sender, PaintEventArgs e)
        {
            int VisibleSeries = 0;
            foreach (Series s in chartControl1.Series)
            {
                if (s.Visible == true)
                    VisibleSeries++;
            }
        
        }

        private void radChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();           
            tbl = (DataTable)grdClients.DataSource;
            if (tbl != null)
                CreateChartSeries(tbl);
        }

        private void radChartInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = (DataTable)grdClients.DataSource;
            if (tbl != null)
                CreateChartSeries(tbl);
        }

        private void chartControl1_CustomDrawSeries(object sender, CustomDrawSeriesEventArgs e)
        {
            // Replace the default colored (thin) line marker with a 'rectangle' marker
            // Create a rectangle with the same width as the line marker, but a little thicker
            int thickness = 3;
            Rectangle rectMarker = new Rectangle(0, 0, e.LegendMarkerSize.Width, thickness);
            Bitmap image = new Bitmap(rectMarker.Width, rectMarker.Height);
            Graphics graphics = Graphics.FromImage(image);
            Color color = e.SeriesDrawOptions.Color;     // use the same color as the line marker

            graphics.FillRegion(new SolidBrush(color), new Region(rectMarker));
            graphics.DrawImage(image, rectMarker);

            e.LegendMarkerImage = image;     // Now replace the default line marker with our bitmap
        }     
    }

   class MyResult
   {

       public string Client { get; set; }

       public string SSN { get; set; }

       public string CaseNumber { get; set; }

       public string Site { get; set; }

       public string StatusAtFCO { get; set; }

       public string Milestone { get; set; }

       public string Track { get; set; }

       public string isCRTFastTrack { get; set; }

       public DateTime CompletionDate { get; set; }

       public string CompletedBy { get; set; }

       public DateTime? Weekending { get; set; }
   }
}