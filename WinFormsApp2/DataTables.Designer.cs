using static WinFormsApp2.Form1;
using static WinFormsApp2.Program;
namespace WinFormsApp2
{
    partial class DataTables
    {
        private System.ComponentModel.IContainer components = null;

        
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

        
        private void InitializeComponent(string path)
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Words = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfOcurances = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Words,
            this.NumberOfOcurances});
            this.dataGridView1.Location = new System.Drawing.Point(45, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(571, 301);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Words
            // 
            this.Words.HeaderText = "Word";
            this.Words.Name = "Words";
            // 
            // NumberOfOcurances
            // 
            this.NumberOfOcurances.HeaderText = "Top 20 Number Of Ocurances";
            this.NumberOfOcurances.Name = "NumberOfOcurances";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(613, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 60);
            this.button1.TabIndex = 1;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 524);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DataTables";
            this.Text = "DataTables";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            resultsOfStatistics(path);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Words;
        private DataGridViewTextBoxColumn NumberOfOcurances;
        private void resultsOfStatistics(string path)
        {
            
            var final = fileStatistics(path);
            for(int i = 0; i < 20;i++)
            {
        
                    this.dataGridView1.Rows.Add(final[((final.Length/2)-1) - i, 0], final[((final.Length/2)-1)-i, 1]);
                
            }
            
        }

        private Button button1;
    }
}