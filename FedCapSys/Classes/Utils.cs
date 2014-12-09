using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace FedCapSys.Classes
{
    class Utils
    {
        public const string PHONEMASK = "(000) 000-0000";
        public const string PHONEEXTENSIONMASK = "(000) 000-0000 X99999";

        public static string ToNullString(string str)
        {
            if (str == null || str.Trim() == "")
                return null;
            else
                return str;
        }

        public static string ToNullString(object str)
        {
            if (str == null)
                return null;
            string s = str.ToString();
            if (s.Trim() == "")
                return null;
            else
                return s;
        }

        public static string ToNullString(string str, bool trim)
        {
            if (str == null || str.Trim() == "")
                return null;
            else
            {
                if (trim)
                    return str.Trim();
                else
                    return str;
            }
        }

        public static string ToEmptyString(object o)
        {
            if (o == null)
                return "";
            else
                return o.ToString();
        }

        public static string ToEmptyString(string str)
        {
            if (str == null || str.Trim() == "")
                return "";
            else
                return str;
        }

        public static bool IsStringNull(string str)
        {
            if (str == null || str == "")
                return true;
            else
                return false;
        }

        public static DateTime ParseDateTime(DateTime date, string time)
        {
            try
            {
                return DateTime.Parse(date.ToString("MM/dd/yyyy") + " " + time);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static string ExecutablePath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others  will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static void HideFirstNcolumns(DevExpress.XtraGrid.Views.Grid.GridView v, int c)
        {
            if (v == null || c < 1)
                return;
            if (v.Columns.Count == 0)
                return;
            v.PopulateColumns();

            for (int i = 0; i < c; i++)
            {
                if (v.Columns.Count - 1 >= i)
                    v.Columns[i].Visible = false;
            }
        }

        public static string ExtractSSNNumbers(string str)
        {
            if (str == null)
                return null;

            StringBuilder ret = new StringBuilder(9);

            foreach (char c in str.ToCharArray())
            {
                if (c >= '0' && c <= '9')
                    ret.Append(c);
            }
            return ret.ToString();
        }

        public static string GetEncryptedSSN(string ssn)
        {
            long i = long.Parse(ssn);
            i = i * 2;
            i = i + 519732168;
            i = i * 3;

            return i.ToString();
        }

        public static string GetDecryptedSSN(string enssn)
        {
            long tmpssn = long.Parse(enssn);
            tmpssn = ((tmpssn / 3) - 519732168) / 2;
            if (tmpssn <= 0)
                return "";
            return tmpssn.ToString().PadLeft(9, '0');
        }

        public static DataRow GetFirstSelectedGridDataRow(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            if (gv == null)
                return null;

            if (gv.GetSelectedRows().Count() == 0)
                return null;

            return gv.GetDataRow(gv.GetSelectedRows()[0]);
        }

        public static void ShowError(string msg)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(msg, "FedCap", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static void ShowInfo(string msg)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(msg, "FedCap", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public static void ShowErrorInMSWindow(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, "FedCap", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static string GetTimeString(int minutes)
        {
            if (minutes <= 0)
                return "";
            TimeSpan x = new TimeSpan(0, minutes, 0);
            return DateTime.Parse(x.ToString()).ToString("hh:mm tt");
        }

        public static string GetKeyCodeChar(System.Windows.Forms.Keys k, int kv)
        {
            if (kv >= 48 && kv <= 57)
            {
                return (kv - 48).ToString();
            }
            if (kv >= 65 && kv <= 90)
            {
                return k.ToString();
            }
            return "";
        }

        public static bool IsWeekDay(DateTime d)
        {
            return d.DayOfWeek >= DayOfWeek.Monday && d.DayOfWeek <= DayOfWeek.Friday;
        }

        public static void WeekEndingEditorDateTimeChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.DateEdit d = (DevExpress.XtraEditors.DateEdit)sender;
            if (d.EditValue == null)
                return;
            if (d.DateTime.DayOfWeek != DayOfWeek.Friday)
            {
                d.DateTime = d.DateTime.AddDays(DayOfWeek.Friday - d.DateTime.DayOfWeek);
            }
        }
      

        public static void SetPhone(TextEdit t, CheckEdit e, string p)
        {
            if (p != null && p.Contains("X"))
            {
                t.Properties.Mask.EditMask = Utils.PHONEEXTENSIONMASK;
                e.Checked = true;
            }
            else
            {
                t.Properties.Mask.EditMask = Utils.PHONEMASK;
                e.Checked = false;
            }
            t.Text = p;
        }

        public static void SetPhoneMask(TextEdit t, CheckEdit c)
        {
            if (c.Checked)
                t.Properties.Mask.EditMask = PHONEEXTENSIONMASK;
            else
            {
                t.Properties.Mask.EditMask = PHONEMASK;
                if (t.Text != null && t.Text.Contains("X"))
                    t.Text = t.Text.Split('X')[0].Trim();
            }
        }

        public static void PhoneTextBoxCheckedChanged(object sender, EventArgs e)
        {
            CheckEdit c = (CheckEdit)sender;
            TextEdit t = (TextEdit)c.Tag;
            SetPhoneMask(t, c);
        }

        public static DateTime ToDateOnly(DateTime dt)
        {
            return DateTime.Parse(dt.ToString("MM/dd/yyyy"));
        }

        public static DialogResult ShowQuestion(string msg)
        {
            return XtraMessageBox.Show(msg, "FedCap", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
        }

        public static void SetColumnToMemEditColumn(DevExpress.XtraGrid.Columns.GridColumn gc, bool valigcenter)
        {
            gc.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            if (valigcenter)
                gc.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        public static long GetFileSize(string filepath)
        {
            try
            {
                System.IO.FileInfo i = new System.IO.FileInfo(filepath);
                return i.Length;
            }
            catch
            {
                return -1;
            }
        }

        public static string GetExecutingAssemblyPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static double? GetDoubleValue(object x)
        {
            if (x == null)
                return null;

            double y;

            return double.TryParse(x.ToString(), out y) ? (double?)y : null;
        }

        public static int? GetIntValue(object x)
        {
            if (x == null)
                return null;

            int y;

            return int.TryParse(x.ToString(), out y) ? (int?)y : null;
        }

        public static void SetItemCheckStates(CheckedListBoxControl c, CheckState s)
        {
            if (c.Enabled)
                for (int i = 0; i < c.Items.Count; i++)
                    c.SetItemCheckState(i, s);
        }

        public static string AddQuotes(string x)
        {
            if (x == null) return null;
            return "'" + x.Replace("'", "''") + "'";
        }

       


  

     

        public static bool DoRangesOverlap(short r1start, short r1end, short r2start, short r2end)
        {
            if (r1start >= r2start && r1start < r2end)
                return true;
            if (r1end > r2start && r1end <= r2end)
                return true;

            return false;
        }

        public static string FormatSSN(object ssn)
        {
            if (ssn == null)
                return null;

            string ssn2 = ssn.ToString();
            if (ssn2.Length >= 9)
            {
                string formattedSSN = ssn2.Substring(0, 3) + "-" + ssn2.Substring(3, 2) + "-" + ssn2.Substring(5, 4);
                return formattedSSN;
            }
            else
                return "";
        }

        public static DateTime GetWeekEnding(DateTime sourceDateTime)
        {
            var daysAhead = DayOfWeek.Friday - (int)sourceDateTime.DayOfWeek;

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

    }
}
