using System;
using System.Configuration;
using System.IO;

namespace Combinations
{
    public static class Logs
    {
        private static readonly string _Conncection_Name = ConfigurationManager.AppSettings["connection_string_name"].ToString();
        private static readonly string _LOG_FILE_PATH = ConfigurationManager.AppSettings["LOG_FILE_PATH"].ToString();
        public static void Log(string word, string MESSAGE)
        {
            try
            {
                //(new Connection(_Conncection_Name)).ExecuteScalar("dbo.INSERT_LOG", new string[] { "@WORD", "@MENSAJE" }, new object[] { word, MESSAGE });
                (new Connection(_Conncection_Name)).ExecuteStoredProcedure("dbo.INSERT_LOG", new object[] { word, MESSAGE });
            }
            catch (Exception ex)
            {
                Log_Text(word, ex);
                //throw ex;
            }
        }

        public static void Log(string word, Exception ex)
        {
            //Log(word, ex.Message);
            string message = ex.Message;
            if (ex.InnerException != null)
                //Log(word, ex.InnerException.Message);
                message += " InnEx: " + ex.InnerException.Message;
            Log(word, message);
        }

        public static void Log_Text(string word, string MESSAGE)
        {
            using (StreamWriter sw = File.AppendText(_LOG_FILE_PATH))
            {
                sw.WriteLine($"{word} <---> {MESSAGE}");
            }
        }

        public static void Log_Text(string word, Exception ex)
        {
            Log_Text(word, ex.Message);
            if (ex.InnerException != null)
                Log_Text(word, ex.InnerException.Message);
            Log_Text(word, ex.Source);
        }


    }
}
