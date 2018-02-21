using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;

namespace Combinations
{
    public class hackWords
    {
        //private Dictionary<string, string> _dTotalPermutations;
        private ConcurrentDictionary<string, string> _cdTotalPermutations = new ConcurrentDictionary<string, string>();
        private int _Group_Size;
        private string _WORD;
        private char[] _Letters;//
        private string _connection_string_name;//<add key="connection_string_name" value="WORD"/>
        private readonly char[] replacement_list = new char[] { '!', '#', '%', '/', ')', '?', '+', '~', '[', '}', '-', '.', ',', '<', '"', '$', '&', '(', '=', '¡', '*', '{', '^', ']', '_', ':', ';', '>' };
        //private int globalCounter = 0;
        private bool enable_log_groups_parallel;
        private int sleep_time_parallel;
        private int number_split_parallel;
        private Hashtable duplicatesHT;
        private List<string> _lValidatedWords;// = new List<string>();
        private List<string> _lPermutations;// = new List<string>();
        //private DataTable dtResultWords = new DataTable();
        private DataSet _ds_RESULT = new DataSet();
        private DataTable _dt_WORDS = new DataTable("WORDS");
        private DataTable _dt_PERMUTATIONS = new DataTable("PERMUTATIONS");


        public hackWords(string word, int group_size)
        {
            _WORD = word;
            _Letters = _WORD.ToCharArray();
            _Group_Size = group_size;

            enable_log_groups_parallel = Convert.ToBoolean(ConfigurationManager.AppSettings["enable_log_groups_parallel"].ToString());
            sleep_time_parallel = Convert.ToInt32(ConfigurationManager.AppSettings["sleep_time_parallel"].ToString());
            number_split_parallel = Convert.ToInt32(ConfigurationManager.AppSettings["number_split_parallel"].ToString());
            _connection_string_name = ConfigurationManager.AppSettings["connection_string_name"].ToString();
			
            _lValidatedWords = new List<string>();
            _lPermutations = new List<string>();
            _ds_RESULT = new DataSet();
            _dt_WORDS = new DataTable("WORDS");
            _dt_PERMUTATIONS = new DataTable("PERMUTATIONS");
            _dt_WORDS.Columns.Add("Words", typeof(string));
            _dt_PERMUTATIONS.Columns.Add("Permutations", typeof(string));
        }

        private string GetPermutationsResult()//int grouped)//, out int pg)//, out DataTable dtResult)
        {
            string result = "0";
            char[] letters_cleaned = _Letters;

            duplicatesHT = new Hashtable();
            try
            {
                if (_Letters.Length != _Letters.Distinct().Count())
                    letters_cleaned = CleanLetters(_Letters);

                IEnumerable<IEnumerable<char>> result_char = Permutation.GetPermutations(letters_cleaned, _Group_Size);

                HashSet<string> hashSetString = new HashSet<string>();
                Logs.Log(_WORD, "Start permutation calc.");

                foreach (var T in result_char)
                {
                    var line = "";
                    foreach (char A in T)
                    {
                        if (duplicatesHT.ContainsKey(A))
                            line += duplicatesHT[A];
                        else
                            line += A;
                    }

                    if (hashSetString.Add(line.ToString()))
                    {    //continue;
                    
                        //cd.AddOrUpdate(1, 1, (key, oldValue) => oldValue + 1);
                        _cdTotalPermutations.AddOrUpdate(line, "FALSE", (key, oldValue) => "FALSE");
                    }
                    
                }

                Logs.Log(_WORD, "End permutation calc.");
                Logs.Log(_WORD, "Total words: " + hashSetString.Count);
                //_cdTotalPermutations = hashSetString.ToDictionary(h => h, h => "FALSE");
            }
            catch (Exception ex)
            {

                Logs.Log(_WORD, ex);
                result = ex.Message;
            }
            return result;
        }

        /*
        private string GetPermutationsResult_CheckInLine()//int grouped)//, out int pg)//, out DataTable dtResult)
        {
            string result = "0";
            char[] letters_cleaned = _Letters;

            duplicatesHT = new Hashtable();
            try
            {
                if (_Letters.Length != _Letters.Distinct().Count())
                    letters_cleaned = CleanLetters(_Letters);

                IEnumerable<IEnumerable<char>> result_char = Permutation.GetPermutations(letters_cleaned, _Group_Size);

                HashSet<string> hashSetString = new HashSet<string>();
                Logs.Log(_WORD, "Start permutation calc.");

                foreach (var T in result_char)
                {
                    var line = "";
                    foreach (char A in T)
                    {
                        if (duplicatesHT.ContainsKey(A))
                            line += duplicatesHT[A];
                        else
                            line += A;
                    }

                    if (!hashSetString.Add(line.ToString())) continue;
                    else
                    {
                        if (WordExist(line.ToString()))
                            //continue;
                            _lValidatedWords.Add(line.ToString());
                        else
                            _lPermutations.Add(line.ToString());
                    }

                }
                Logs.Log(_WORD, "End permutation calc.");
                Logs.Log(_WORD, "Total words: " + hashSetString.Count);
                //_dTotalPermutations = hashSetString.ToDictionary(h => h, h => (string)"FALSE");
            }
            catch (Exception ex)
            {

                Logs.Log(_WORD, ex);
                result = ex.Message;
            }
            return result;
        }
        */

        public string WordsList(out List<string> lValidatedWords, out List<string> lPermutations) //out Dictionary<string, string> TotalPermutations)//, out DataTable dtResult)
        {
            string message = "0";

            lValidatedWords = lPermutations = null;
            

            try
            {
                Logs.Log(_WORD, "Start word search calc.");
                message = GetPermutationsResult();
                Dictionary<int, int> _groupOfSearch = GroupOfSearch(_cdTotalPermutations.Count - 1);//hashSetString.Count);
                

                foreach (var item in _groupOfSearch)
                {
                    Parallel.For(item.Key, item.Value, dPosition =>
                    {
                        CheckWordAndUpdateDictionary(dPosition);
                        System.Threading.Thread.Sleep(sleep_time_parallel);
                    });

                    if (enable_log_groups_parallel && item.Key % number_split_parallel == 0)
                        Logs.Log(_WORD, "End group:  " + item.Value);
                }

                lValidatedWords = _lValidatedWords.OrderBy(x => x).ToList();
                lPermutations = _lPermutations.OrderBy(x => x).ToList();

                Logs.Log(_WORD, "End word search calc.");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                if (ex.InnerException != null)
                    message += " IEx: " + ex.InnerException;
                Logs.Log(_WORD, ex);
            }

            //Logs.Log(_WORD, "Words found: " + dtResultWords.Rows.Count);
            //TotalPermutations = _dTotalPermutations.ToDictionary(;
            //var newDictionary = _dTotalPermutations.ToDictionary(kvp => kvp.Key,
                                                          //kvp => kvp.Value,
                                                          //_dTotalPermutations.);

            // or...
            // substitute your actual key and value types in place of TKey and TValue
            //TotalPermutations = new Dictionary<string, string>(_dTotalPermutations, _dTotalPermutations.Comparer);
            return message;
        }

        public string WordList2(out DataSet _RESULT)
        {
            string message = "0";
            _RESULT = new DataSet();

            try
            {
                message = GetPermutationsResult();

                Logs.Log(_WORD, "Start word search calc.");

                Dictionary<int, int> _groupOfSearch = GroupOfSearch(_cdTotalPermutations.Count );//hashSetString.Count);


                try
                {
                    foreach (var item in _groupOfSearch)
                    {

                        Parallel.For(item.Key, item.Value, dPosition =>
                        {
                            CheckWordAndUpdateDictionary(dPosition);
                            System.Threading.Thread.Sleep(sleep_time_parallel);
                        });

                        if (enable_log_groups_parallel && item.Key % number_split_parallel == 0)
                            Logs.Log(_WORD, "End group:  " + item.Value);
                    }
                    message = "0";
                }
                catch (Exception ex)
                {
                    message = "ERROR";
                    Logs.Log(_WORD, ex);
                }

                Logs.Log(_WORD, "End word search calc.");

                //Hyper.ComponentModel.HyperTypeDescriptionProvider.Add(typeof(MyData));

                DataTable _dtWords = ToDataTable(_lValidatedWords.Select(x=> new { WORDS = x }).ToList());
                DataTable _dtPermutations = ToDataTable(_lPermutations.Select(x => new { PERMUTATIONS = x }).ToList());

                DataView dvW = _dtWords.DefaultView;
                dvW.Sort = "Words asc";
                DataView dvP = _dtPermutations.DefaultView;
                dvP.Sort = "Permutations asc";
                
                _RESULT.Tables.Add(dvW.ToTable("WORDS"));
                _RESULT.Tables.Add(dvP.ToTable("PERMUTATIONS"));
            }
            catch (Exception ex)
            {
                message = ex.Message;
                if (ex.InnerException != null)
                    message += " IEx: " + ex.InnerException;
                Logs.Log(_WORD, ex);
            }
            return message;
        }



        private void CheckWordAndUpdateDictionary(int hssPosition)
        {
            string key = "";
            bool validated = false;
            try
            {
                try
                {
                    key = _cdTotalPermutations.ElementAt(hssPosition).Key.ToUpper();
                }
                catch (Exception e1)
                {
                    throw e1;
                }
                try
                {
                    validated = WordExist(key);
                }
                catch (Exception ex3)
                {
                    validated = false;
                    throw ex3;
                }

                try
                {
                    if (validated)
                        //_dTotalPermutations.Remove(key);
                        _lValidatedWords.Add(key);
                        //AddItemToDataTableWords(key);
                    else
                        _lPermutations.Add(key);
                        //AddItemToDataTablePermutation(key);
                }
                catch (Exception ex2)
                {

                    throw ex2;
                }
            }
            catch (Exception e0)
            {

                Exception newE;
                if (e0.InnerException != null)
                    newE = new Exception("word: " + _WORD + " pos: " + hssPosition + " eMessage: " + e0.Message + " eIEx: " + e0.InnerException.Message + " eSource: " + e0.Source);
                else
                    newE = new Exception("word: " + _WORD + " pos: " + hssPosition + " eMessage: " + e0.Message + " eSource: " + e0.Source);
                Logs.Log(_WORD, newE);
            }
        }

        // remove "this" if not on C# 3.0 / .NET 3.5
        public DataTable ToDataTable<T>(IList<T> data)
        {

            DataTable table = new DataTable();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return table;
        }

        private void AddItemToDataTableWords(string _WORDS)
        {
            try
            {
                DataRow _word = _dt_WORDS.NewRow();
                _word[0] = _WORDS;
                _dt_WORDS.Rows.Add(_word);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void AddItemToDataTablePermutation(string _PERMUTATION)
        {
            try
            {
                DataRow _word = _dt_PERMUTATIONS.NewRow();
                _word[0] = _PERMUTATION;
                _dt_PERMUTATIONS.Rows.Add(_word);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private char[] CleanLetters(char[] letters)
        {
            char[] letters_cleaned = letters;
            var duplicates = letters.Select((t, i) => new { Index = i, Text = t })
                                        .GroupBy(g => g.Text)
                                        .Where(g => g.Count() > 1);


            duplicatesHT = new Hashtable(duplicates.Count());

            int replacement_list_position = 0;
            foreach (var x in duplicates.SelectMany(@group => @group))
            {
                duplicatesHT.Add(replacement_list[replacement_list_position], letters[x.Index]);
                letters_cleaned[x.Index] = replacement_list[replacement_list_position];
                replacement_list_position += 1;
            }

            return letters_cleaned;

        }

        


        private  Dictionary<int, int> GroupOfSearch(int words)
        {
            Dictionary<int, int> _groupOfSearch = new Dictionary<int, int>();

            if (words <= number_split_parallel)
                _groupOfSearch.Add(0, words);
            else
            {
                bool end_loop = true;
                int first = 0, last = 0, total = words;

                while (end_loop)
                {
                    if (first + number_split_parallel >= total)
                    {
                        end_loop = false;
                        _groupOfSearch.Add(first, total);
                    }
                    else
                    {
                        last = first + (number_split_parallel - 1);
                        _groupOfSearch.Add(first, last);
                        first += number_split_parallel;
                    }
                }
            }

            return _groupOfSearch;
        }

        public bool WordExist(string word)
        {
            bool validation = false;

            try
            {
                validation = (new Connection(_connection_string_name)).ExecuteScalar_OutputParameter("dbo.SEARCH_WORD", new string[] { "@WORD_TO_SEARCH", "@RESULT" }, new object[] { word, false });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
            return validation;
        }
    }
}


/*
 private void addRowsChecked(string word)
        {
            try
            {
                DataRow dr = dtResultWords.NewRow();
                if (WordExist(word))
                {
                    dr = dtResultWords.NewRow();
                    dr[0] = word;
                    dtResultWords.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                Exception newE;
                if (e.InnerException != null)
                    newE = new Exception("word: " + _WORD + " eMessage: " + e.Message + " eIEx: " + e.InnerException.Message + " eSource: " + e.Source);
                else
                    newE = new Exception("word: " + _WORD + " eMessage: " + e.Message + " eSource: " + e.Source);
                Log(_WORD, e);
                throw newE;
            }

        }
     
        private void addRowsChecked(string word)
        {
            try
            {
                DataRow dr = dtResultWords.NewRow();
                if (WordExist(word))
                {
                    dr = dtResultWords.NewRow();
                    dr[0] = word;
                    dtResultWords.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                Exception newE;
                if (e.InnerException != null)
                    newE = new Exception("word: " + _WORD + " eMessage: " + e.Message + " eIEx: " + e.InnerException.Message + " eSource: " + e.Source);
                else
                    newE = new Exception("word: " + _WORD + " eMessage: " + e.Message + " eSource: " + e.Source);
                Log(_WORD, e);
                throw newE;
            }

        }
        
     */
