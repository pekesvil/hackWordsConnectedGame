using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Combinations
{
    public class ReadFlatFileWords
    {
        private string FILE;
        //private string ID;
        private BlockingCollection<string> csvFile;
        private static BlockingCollection<string> inputLines;

        public void CSV_TO_DB(string PATH_TO_FILE)
        {
            //SaveLogToDBEF(ID, "START");
            csvFile = ReadAllLines(PATH_TO_FILE);
            //var first_line = Regex.Split(csvFile.ElementAtOrDefault(0), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"); /*Omite las comas entre comillas, considerando el resto de comas*/
            FILE = PATH_TO_FILE.Substring(PATH_TO_FILE.LastIndexOf('\\'));

            //TotalRowsInserted = 0;
            //SaveLogToDBEF(ID, "Rows: " + csvFile.Count);
            //string new_WORD = null;

            Logs.Log("Total líneas a insertar: " + csvFile.Count.ToString(), PATH_TO_FILE );

            Parallel.ForEach(csvFile, (rowFile, state) =>
            {
                //new_WORD = CleanWORD(rowFile);//ReadFlatFileRow(rowFile, ID);
                SaveDataToDB_SP(rowFile, FILE);

            });

            Logs.Log("Fin palabras insertadas.", PATH_TO_FILE  );
        }
        public static BlockingCollection<string> ReadAllLines(string PATHTOFILE)
        {
            inputLines = new BlockingCollection<string>();

            try
            {
                Parallel.ForEach(File.ReadLines(PATHTOFILE), (line) =>
                {
                    inputLines.Add(line);
                }
                    );
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return inputLines;
        }
        private string CleanWORD(string _word)
        {

            char[] arr = _word.Where(c => (char.IsLetter(c))).ToArray();

            return new string(arr);

        }

        private void SaveDataToDB_SP(string PALABRA, string _file_name)
        {
            //bool result_insert = false;


            try
            {
                //result_insert = (new Connection("WORD")).ExecuteScalar("dbo.INSERT_WORD_FROM_FILE", new string[] { "@PALABRA", "@_FILENAME_" }, new object[] { PALABRA, _file_name });
                (new Connection("WORD")).ExecuteStoredProcedure("dbo.INSERT_WORD_FROM_FILE", new object[] { PALABRA, _file_name });
            }
            catch (Exception ex)
            {
                Logs.Log(PALABRA, ex);
                //throw ex;
            }

            //return result_insert;
        }
    }

}
