using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FedCapSys.Modules;

namespace FedCapSys.Classes
{
    class BillingProcessing
    {
        static public void runBPSPaid()
        {
            using (Data.dbDataContext dc = new Data.dbDataContext())
            {
                dc.CommandTimeout = 1000;
                var payments = from p in dc.Milestones_Paids
                               where (p.MilestCode == "W05B" || p.MilestCode == "W05A")
                               && p.trackNumber == null
                               select p;
                foreach (var p in payments)
                {
                    int? tn = dc.cmcaseforms.Where(aa => aa.HRACase.SSN == p.SSN_no_Dashes  && aa.FormID == 1009
                        && aa.LastSavedWhen >= p.Action_dt.AddDays(-90) && aa.LastSavedWhen <= p.Action_dt.AddDays(90) && aa.LastSavedBy != "SysAdmin"
                        && p.SSN_no_Dashes != null && p.SSN_no_Dashes != " " && p.SSN_no_Dashes != "" && p.SSN_no_Dashes != "         ").Select(aa => new { aa.TrackNumber, days = Math.Abs(((DateTime)aa.LastSavedWhen - p.Action_dt).TotalDays) }).OrderBy(aa => aa.days).Select(aa => aa.TrackNumber).FirstOrDefault();
                    if (tn == 0) //In cases where there is no SSN match by CaseNumber
                    {
                        tn = dc.cmcaseforms.Where(aa => aa.HRACase.HRACaseNumber == p.CaseN && aa.HRACase.Suffix == p.Suffix && aa.HRACase.LineNumber == p.Line   && aa.FormID == 1009
                        && aa.LastSavedWhen >= p.Action_dt.AddDays(-90) && aa.LastSavedWhen <= p.Action_dt.AddDays(90) && aa.LastSavedBy != "SysAdmin"
                      ).Select(aa => new { aa.TrackNumber, days = Math.Abs(((DateTime)aa.LastSavedWhen - p.Action_dt).TotalDays) }).OrderBy(aa => aa.days).Select(aa => aa.TrackNumber).FirstOrDefault();
                    }

                    var tn_exist = (from check in dc.Milestones_Paids 
                                   where check.trackNumber == tn
                                   select check).FirstOrDefault();
                    if (tn_exist == null)
                        p.trackNumber = tn;
                    dc.SubmitChanges();
                }


            }
        }

        static public void runWellnessPaid()
        {
            using (Data.dbDataContext dc = new Data.dbDataContext())
            {
                dc.CommandTimeout = 1000;
                var payments = from p in dc.Milestones_Paids
                               where (p.MilestCode == "W10A" || p.MilestCode == "W10B")
                               && p.trackNumber == null
                               select p;
               

                var wellness = (from cmf in dc.cmcaseforms
                            from formData in dc.CMDatas
                            let TrackingFormId = dc.cmcaseforms.Where(aa => aa.CaseID == cmf.CaseID && aa.EpisodeNumber == cmf.EpisodeNumber && (aa.FormID == 563 || aa.FormID == 563)).OrderByDescending(aa => aa.LastSavedWhen).FirstOrDefault().FormID
                            where cmf.TrackNumber == formData.RecordID &&
                                //  cmf.SequenceNum == 2 &&
                          (cmf.FormID == 397 || cmf.FormID == 379) &&
                          formData.FieldName == "DropEmpDisposition" &&
                          (formData.Value == "1" || formData.Value == "4" || formData.Value == "5") &&
                           cmf.LastSavedWhen >= DateTime.Parse("1/1/2013") && cmf.LastSavedWhen < DateTime.Today.AddDays(1) &&
                         (cmf.HRACase.LineNumber != "50" && !cmf.HRACase.CaseSurname.Contains("Test") && !cmf.HRACase.CaseFirstName.Contains("Test"))      //Exclude Test Cases
                            select new
                            {
                                Client = cmf.HRACase.CaseSurname.Trim() + ", " + cmf.HRACase.CaseFirstName.Trim(),
                                SSN = cmf.HRACase.SSN,
                                CaseNumber = cmf.HRACase.HRACaseNumber + "-" + cmf.HRACase.Suffix + "-" + cmf.HRACase.LineNumber,
                                Site = dc.Schedule_FindLocationOfAppointment(cmf.HRACase.SSN, cmf.HRACase.HRACaseID, 10, (DateTime)cmf.LastSavedWhen).ToString(),
                                Milestone = TrackingFormId == 563 ? "Wellness" : "Wellness Plus",
                                Track = formData.Value == "1" ? "No Functional Limitation" : formData.Value == "4" ? "VRS" : formData.Value == "5" ? "Unable To Work" : "Unknown",
                                CompletionDate = (DateTime)cmf.LastSavedWhen.Value.Date,
                                CompletedBy = cmf.logintable.FirstName + " " + cmf.logintable.LastName,
                                cmf.TrackNumber 
                            }).Distinct().ToList();

                foreach (var p in payments)
                {                   
                    int? tn = (from completion in wellness 
                               where
                               completion.CompletionDate >= p.Action_dt.AddDays(-180) && completion.CompletionDate <= p.Action_dt.AddDays(180)
                               && p.SSN_no_Dashes == completion.SSN 
                               && p.SSN_no_Dashes != null && p.SSN_no_Dashes != " " && p.SSN_no_Dashes != ""  && p.SSN_no_Dashes != "         "
                               select new
                               {
                                   completion.TrackNumber,
                                   days = Math.Abs(((DateTime)completion.CompletionDate - p.Action_dt).TotalDays)
                               }).OrderBy(aa => aa.days).Select(aa => aa.TrackNumber).FirstOrDefault();

                    var tn_exist = (from check in dc.Milestones_Paids
                                    where check.trackNumber == tn
                                    select check).FirstOrDefault();
                    if (tn_exist == null)
                        p.trackNumber = tn;
                    dc.SubmitChanges();
                }


            }
        }
    }
}
