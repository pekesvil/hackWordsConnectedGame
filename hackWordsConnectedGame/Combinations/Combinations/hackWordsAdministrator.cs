using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace Combinations
{
    public partial class hackWordsAdministrator : Form
    {
        public string FILE { get; private set; }

        public hackWordsAdministrator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtWords.CharacterCasing = CharacterCasing.Upper;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = false;
            if (txtGrouped.Text.Length > 0)
            {
                if (txtWords.Text.Length > 1)
                {
                    ShowWords(txtWords.Text, Convert.ToInt32(txtGrouped.Text));
                }
                else
                    MessageBox.Show("Ingrese al menos 2 caractéres");
            }
            else
                MessageBox.Show("Ingrese al menos 1 dígito");
            btnGenerate.Enabled = true;
        }

        private void ShowWords(string _WORD, int group_length)
        {
            try
            {

                
                string outMessage = "";


                hackWords hw = new hackWords(_WORD, group_length);

                //Dictionary<string, string> TotalPermutations = new Dictionary<string, string>();
                List<string>[] result = new List<string>[2];
                outMessage = hw.WordsList(out result[0], out result[1]);

                var ValidWords = (from w in result[0] select new { Value = w } ).OrderBy(q => q.Value).ToList();
                var Permutations = (from w in result[1] select new { Value = w }).OrderBy(q => q.Value).ToList();

                lblValidWords.Text = result[0].Count().ToString();//ValidWords.Count.ToString();
                lblTotalPermutes.Text = result[1].Count().ToString();//Permutations.Count.ToString();

                gvValidWords.DataSource = ValidWords;
                gvTotalPermutations.DataSource = Permutations;

                if (outMessage != "0")
                    MessageBox.Show(outMessage);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();

            //FILE = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\")).Replace("\\", "");
            ////ID = FILE + " & " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");
            ////SaveLogToDBEF(ID, "File selected.");
            //(new ReadFlatFileWords()).CSV_TO_DB(openFileDialog1.FileName);
        }


        /*
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
        */
    }
}
