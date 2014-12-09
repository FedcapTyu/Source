using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FedCapSys.Classes
{
    class ErrorHandling
    {
        private static string mLogFile;

        public static void HandleError(Exception e)
        {
            MessageBox.Show(e.Message, "FedCap", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                HandleError((Exception)e.ExceptionObject);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            finally
            {
                Application.Exit();
            }
        }

        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                HandleError(e.Exception);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            finally
            {
                Application.Exit();
            }
        }

        public static void LogToDB(string logtxt, bool iserror, string src)
        {
            try
            {
                //using (Data.ArborDataClassesDataContext dc = DB.GetDataContext())
                //{
                //    Data.GeneralLog l = new ArborET.B2W.ACELib.Data.GeneralLog();
                //    l.IsError = iserror;
                //    l.LogText = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt : ") + logtxt;
                //    l.Source = src;

                //    dc.GeneralLogs.InsertOnSubmit(l);
                //    dc.SubmitChanges();
                //}
            }
            catch { }
        }

        public static void LogToFile(string txt)
        {
            LogToFileInternal(txt, false);
        }

        public static void LogErrorToFile(Exception ex)
        {
            if (ex != null)
            {

                LogToFileInternal(
                    "Message: " + ex.Message + Environment.NewLine +
                    "Source: " + ex.Source + Environment.NewLine +
                    "Stack:" + Environment.NewLine + ex.StackTrace + Environment.NewLine,
                    true
                    );
            }
        }

        public static void LogErrorToFile(string exstring)
        {
            if (exstring != null)
            {
                LogToFileInternal(
                    "Message: " + exstring + Environment.NewLine,
                    true
                    );
            }
        }

        public static void LogErrorToFile(Exception ex, string comment)
        {
            if (ex != null)
            {
                string com;
                if (string.IsNullOrEmpty(comment))
                    com = "";
                else
                    com = "Comment: " + comment + Environment.NewLine;

                LogToFileInternal(
                    "Message: " + ex.Message + Environment.NewLine +
                    com +
                    "Source: " + ex.Source + Environment.NewLine +
                    "Stack:" + Environment.NewLine + ex.StackTrace + Environment.NewLine,
                    true
                    );
            }
        }

        private static void LogToFileInternal(string txt, bool isError)
        {
            try
            {
                if (mLogFile == null)
                    CreateLogFile();
                if (File.Exists(mLogFile) && string.IsNullOrEmpty(txt) == false)
                {
                    StreamWriter sw = File.AppendText(mLogFile);
                    string type = (isError ? " (ERROR) " : " (LOG) ").PadRight(9);
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyy hh:mm:ss tt") + type + ":");
                    sw.WriteLine(txt);
                    sw.WriteLine("---------------------------------------");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch { }
        }

        private static void CreateLogFile()
        {
            string logfile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            logfile = System.IO.Path.GetDirectoryName(logfile);
            logfile = System.IO.Path.Combine(logfile, "logfile.log");
            if (File.Exists(logfile) == false)
                System.IO.File.Create(logfile).Close();
            mLogFile = logfile;
        }
    }
}
