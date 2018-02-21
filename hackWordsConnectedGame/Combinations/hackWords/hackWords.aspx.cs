using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;


namespace hackWords
{
    public partial class hackWordsWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearchWords_Click(object sender, EventArgs e)
        {
            btnSearchWords.Enabled = false;
            if (txtGroupsize.Text.Length > 0)
            {
                if (txtLetters.Text.Length > 1)
                    ShowWords(txtLetters.Text, Convert.ToInt32(txtGroupsize.Text));
                
                else
                    //MessageBox.Show("Ingrese al menos 2 caractéres");
                    lblMensaje.Text = "Ingrese al menos 2 caractéres";
            }
            else
                //MessageBox.Show("Ingrese al menos 1 dígito");
                lblMensaje.Text = "Ingrese al menos 1 dígito";
            btnSearchWords.Enabled = true;
        }

        private void ShowWords(string _WORD, int group_length)
        {
            try
            {
                string outMessage = "";

                //List<string>[] result = GenerateWordsAndPermutations(_WORD, group_length);
                DataSet _Result = GenerateWordsAndPermutations(_WORD, group_length);

                lblValidWords.Text = _Result.Tables[0].Rows.Count.ToString();
                lblTotalPermutes.Text = _Result.Tables[1].Rows.Count.ToString();

                gvValidWords.DataSource = _Result.Tables[0];//.Select(X => new { PALABRAS = X });
                gvValidWords.DataBind();
                gvTotalPermutations.DataSource = _Result.Tables[1];//.Select(X => new { PERMUTACIONES = X });
                gvTotalPermutations.DataBind();

                if (outMessage != "0")
                    lblMensaje.Text = outMessage;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
            finally
            {

            }
        }


        private DataSet GenerateWordsAndPermutations(string _WORD, int group_length)//, out DataSet _dsResult)
        {
            CacheManager cm = new CacheManager();
            try
            {
                if (cm[_WORD + "_" + group_length] == null)
                {
                    Combinations.hackWords hw = new Combinations.hackWords(_WORD, group_length);
                    string outMessage;
                    DataSet _dsResult = new DataSet();
                    outMessage = hw.WordList2(out _dsResult);
                    //new CacheManager().Add(_WORD + "_" + group_length + "_ValidWords", _dsResult.Tables[0]);
                    //new CacheManager().Add(_WORD + "_" + group_length + "_Permutations", _dsResult.Tables[1]);
                    new CacheManager().Add(_WORD + "_" + group_length, _dsResult);
                    return _dsResult;
                }

                //DataTable vw = (DataTable)cm[_WORD + "_" + group_length + "_ValidWords"];
                //DataTable p = (DataTable)cm[_WORD + "_" + group_length + "_Permutations"];
                //DataSet dsResult = new DataSet();

                //dsResult.Tables.Add(vw);
                //dsResult.Tables.Add(p);
                return (DataSet)cm[_WORD + "_" + group_length];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        protected void gvTotalPermutations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTotalPermutations.PageIndex = e.NewPageIndex;
            CacheManager cm = new CacheManager();

            //gvTotalPermutations.DataSource = ((List<string>)cm[txtLetters.Text + "_" + txtGroupsize.Text + "_Permutations"]);//Permutations;
            //gvTotalPermutations.DataBind();
            gvTotalPermutations.DataSource = ((DataSet) cm[txtLetters.Text + "_" + txtGroupsize.Text]).Tables[1];
            gvTotalPermutations.DataBind();

        }
    }
}