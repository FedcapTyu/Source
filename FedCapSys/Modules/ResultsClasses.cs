using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FedCapSys.Modules
{
    class MyBillingResult
    {

        public string Client { get; set; }

        public string SSN { get; set; }

        public string CaseNumber { get; set; }

        public string Site { get; set; }

        public string Milestone { get; set; }

        public string Track { get; set; }

        public DateTime CompletionDate { get; set; }

        public string CompletedBy { get; set; }

        public DateTime? Weekending { get; set; }

        public string Voucher { get; set; }

        public DateTime? PaidDate { get; set; }

        public DateTime? DenialDate { get; set; }

        public string DenialReason { get; set; }

        public string DenialComments { get; set; }

        public DateTime? BilledOfflineDate { get; set; }

        public string BilledOfflineReason { get; set; }
    }

    class MyBPSBillingResult
    {

        public string Client { get; set; }

        public string SSN { get; set; }

        public string CaseNumber { get; set; }

        public string Site { get; set; }

        public string Milestone { get; set; }

        public string Track { get; set; }

        public DateTime CompletionDate { get; set; }

        public string CompletedBy { get; set; }

        public string ActionCodeStatus { get; set; }

        public DateTime? ActionCodeDate { get; set; }

        public string BPSSummaryImaged { get; set; }

        public string LabImaged { get; set; }

        public DateTime? Weekending { get; set; }

        public string Voucher { get; set; }

        public DateTime? PaidDate { get; set; }

        public DateTime? DenialDate { get; set; }

        public string DenialReason { get; set; }

        public string DenialComments { get; set; }

        public DateTime? BilledOfflineDate { get; set; }

        public string BilledOfflineReason { get; set; }

        public string BillingStatus { get; set; }
    }

    class MyBPS2BillingResult
    {

        public string Client { get; set; }

        public string SSN { get; set; }

        public string CaseNumber { get; set; }

        public string Site { get; set; }

        public string Milestone { get; set; }

        public string Track { get; set; }

        public DateTime CompletionDate { get; set; }

        public string CompletedBy { get; set; }

        public string ActionCodeStatus { get; set; }

        public DateTime? ActionCodeDate { get; set; }

        public string Imaging { get; set; }

        public DateTime? Weekending { get; set; }

        public string Voucher { get; set; }

        public DateTime? PaidDate { get; set; }

        public DateTime? DenialDate { get; set; }

        public string DenialReason { get; set; }

        public string DenialComments { get; set; }

        public DateTime? BilledOfflineDate { get; set; }

        public string BilledOfflineReason { get; set; }

        public string BillingStatus { get; set; }
    }

    class MyWellnessBillingResult
    {


        public string Client { get; set; }

        public string SSN { get; set; }

        public string CaseNumber { get; set; }

        public string Site { get; set; }

        public string Track { get; set; }

        public string InitiationCode { get; set; }

        public DateTime InitiationDate { get; set; }

        public string CompletionCode { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime FadDate { get; set; }

        public string Initiation_ExtensionFormSavedBy { get; set; }

        public DateTime? Initiation_ExtensionFormSaveDate { get; set; }

        public string Initiation_ExtensionImaging { get; set; }

        public DateTime? LatestTPWPR { get; set; }

        public string CompletionFormSavedBy { get; set; }

        public DateTime? CompletionFormSaveDate { get; set; }

        public int? DaysBetweenTPWPRAndCompletion { get; set; }

        public string CompletionImaging { get; set; }

        public string Outcome { get; set; }

        public DateTime? Weekending { get; set; }

        public string Voucher { get; set; }

        public DateTime? PaidDate { get; set; }

        public DateTime? DenialDate { get; set; }

        public string DenialReason { get; set; }

        public string DenialComments { get; set; }

        public DateTime? BilledOfflineDate { get; set; }

        public string BilledOfflineReason { get; set; }

        public string BillingStatus { get; set; }
    }


}
