using System;
using System.IO;
using System.Text;

namespace FreeSrun.Util
{
    public class SimpleLogger
    {
        #region 日志输出
        private TextWriter logger;
        public string LogFile { get; set; }

        public  SimpleLogger()
        {
            LogFile = AppDomain.CurrentDomain.BaseDirectory +  "FreeSrun_Log.txt";
        }

        public SimpleLogger(string logFilename)
        {
            LogFile = logFilename;
        }

        private void PrintTimestamp()
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logger.Write(BoldBraceWraped(time));
        }

        public void AppendLog(string format,params object[] arg)
        {
            if (logger == null)
            {
                logger = new StreamWriter(LogFile,true); 
            }
            PrintTimestamp();
            logger.WriteLine(format, arg);
            logger.Flush();
        }
        public void AppendLog(object o)
        {
            if (logger == null)
            {
                logger = new StreamWriter(LogFile, true);
            }
            PrintTimestamp();
            logger.WriteLine(o);
            logger.Flush();
        }
        public void AppendLog(Exception ex)
        {
            if (logger == null)
            {
                logger = new StreamWriter(LogFile, true);
            }
            PrintTimestamp();
            logger.WriteLine(ex.Message);
            logger.WriteLine(ex.StackTrace);
        }

        public void Close()
        {
            if (logger != null)
            {
                logger.Flush();
                logger.Close();
            }
        }

        private string BoldBraceWraped(object str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[").Append(str).Append("]");
            return sb.ToString();
        }

        #endregion
    }
}
