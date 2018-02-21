namespace Combinations
{
    partial class hackWordsAdministrator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gvTotalPermutations = new System.Windows.Forms.DataGridView();
            this.gvValidWords = new System.Windows.Forms.DataGridView();
            this.lblTotalPermutes = new System.Windows.Forms.Label();
            this.lblValidWords = new System.Windows.Forms.Label();
            this.txtGrouped = new System.Windows.Forms.TextBox();
            this.txtWords = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTotalPermutations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvValidWords)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(547, 513);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gvTotalPermutations);
            this.tabPage1.Controls.Add(this.gvValidWords);
            this.tabPage1.Controls.Add(this.lblTotalPermutes);
            this.tabPage1.Controls.Add(this.lblValidWords);
            this.tabPage1.Controls.Add(this.txtGrouped);
            this.tabPage1.Controls.Add(this.txtWords);
            this.tabPage1.Controls.Add(this.btnGenerate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(539, 487);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gvTotalPermutations
            // 
            this.gvTotalPermutations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTotalPermutations.Location = new System.Drawing.Point(263, 95);
            this.gvTotalPermutations.Name = "gvTotalPermutations";
            this.gvTotalPermutations.Size = new System.Drawing.Size(240, 345);
            this.gvTotalPermutations.TabIndex = 13;
            // 
            // gvValidWords
            // 
            this.gvValidWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvValidWords.Location = new System.Drawing.Point(8, 95);
            this.gvValidWords.Name = "gvValidWords";
            this.gvValidWords.Size = new System.Drawing.Size(240, 345);
            this.gvValidWords.TabIndex = 12;
            // 
            // lblTotalPermutes
            // 
            this.lblTotalPermutes.AutoSize = true;
            this.lblTotalPermutes.Location = new System.Drawing.Point(260, 62);
            this.lblTotalPermutes.Name = "lblTotalPermutes";
            this.lblTotalPermutes.Size = new System.Drawing.Size(0, 13);
            this.lblTotalPermutes.TabIndex = 11;
            // 
            // lblValidWords
            // 
            this.lblValidWords.AutoSize = true;
            this.lblValidWords.Location = new System.Drawing.Point(8, 62);
            this.lblValidWords.Name = "lblValidWords";
            this.lblValidWords.Size = new System.Drawing.Size(0, 13);
            this.lblValidWords.TabIndex = 10;
            // 
            // txtGrouped
            // 
            this.txtGrouped.Location = new System.Drawing.Point(323, 35);
            this.txtGrouped.Name = "txtGrouped";
            this.txtGrouped.Size = new System.Drawing.Size(38, 20);
            this.txtGrouped.TabIndex = 9;
            // 
            // txtWords
            // 
            this.txtWords.Location = new System.Drawing.Point(8, 35);
            this.txtWords.Name = "txtWords";
            this.txtWords.Size = new System.Drawing.Size(309, 20);
            this.txtWords.TabIndex = 8;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(8, 5);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(539, 487);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // hackWordsAdministrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 600);
            this.Controls.Add(this.tabControl1);
            this.Name = "hackWordsAdministrator";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTotalPermutations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvValidWords)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView gvTotalPermutations;
        private System.Windows.Forms.DataGridView gvValidWords;
        private System.Windows.Forms.Label lblTotalPermutes;
        private System.Windows.Forms.Label lblValidWords;
        private System.Windows.Forms.TextBox txtGrouped;
        private System.Windows.Forms.TextBox txtWords;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

