using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using FedCapSys.Modules;
namespace FedCapSys.Data
{
    partial class dbDataContext
    {
            [FunctionAttribute(Name = "fed.GetCompletedBPS2")]
            [ResultType(typeof(MyBPS2BillingResult))]
        public IMultipleResults GetCompletedBPS2Results([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_From", DbType = "DateTime")] System.Nullable<System.DateTime> date_From, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_To", DbType = "DateTime")] System.Nullable<System.DateTime> date_To)
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), date_From, date_To);
                return (IMultipleResults)(result.ReturnValue);
            }

            [FunctionAttribute(Name = "fed.GetSignedIPE")]
            [ResultType(typeof(MyResult))]
            public IMultipleResults GetSignedIPEResults([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_From", DbType = "DateTime")] System.Nullable<System.DateTime> date_From, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_To", DbType = "DateTime")] System.Nullable<System.DateTime> date_To)
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), date_From, date_To);
                return (IMultipleResults)(result.ReturnValue);
            }

            [FunctionAttribute(Name = "fed.GetBPSPaymentDetails")]
            [ResultType(typeof(MyBPSBillingResult))]
            public IMultipleResults GetBPSPaymentDetailsResults([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_From", DbType = "DateTime")] System.Nullable<System.DateTime> date_From, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_To", DbType = "DateTime")] System.Nullable<System.DateTime> date_To)
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), date_From, date_To);
                return (IMultipleResults)(result.ReturnValue);
            }

            [FunctionAttribute(Name = "fed.GetWellnessCompletedDetails")]
            [ResultType(typeof(MyWellnessBillingResult))]
            public IMultipleResults GetWellnessCompletedtDetailsResults([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_From", DbType = "DateTime")] System.Nullable<System.DateTime> date_From, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date_To", DbType = "DateTime")] System.Nullable<System.DateTime> date_To)
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), date_From, date_To);
                return (IMultipleResults)(result.ReturnValue);
            }
       
    }
}
