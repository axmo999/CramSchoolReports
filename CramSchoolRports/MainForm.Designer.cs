namespace CramSchoolReports
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.Year = new System.Windows.Forms.Label();
            this.labelMonth = new System.Windows.Forms.Label();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.labelOffice = new System.Windows.Forms.Label();
            this.comboBoxOffice = new System.Windows.Forms.ComboBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.comboBoxReport = new System.Windows.Forms.ComboBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(151, 16);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(121, 20);
            this.comboBoxYear.TabIndex = 0;
            this.comboBoxYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxYear_SelectedIndexChanged);
            // 
            // Year
            // 
            this.Year.AutoSize = true;
            this.Year.Location = new System.Drawing.Point(13, 19);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(91, 12);
            this.Year.TabIndex = 1;
            this.Year.Text = "年を選んでください";
            // 
            // labelMonth
            // 
            this.labelMonth.AutoSize = true;
            this.labelMonth.Location = new System.Drawing.Point(13, 50);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(91, 12);
            this.labelMonth.TabIndex = 3;
            this.labelMonth.Text = "月を選んでください";
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(151, 47);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(121, 20);
            this.comboBoxMonth.TabIndex = 2;
            // 
            // labelOffice
            // 
            this.labelOffice.AutoSize = true;
            this.labelOffice.Location = new System.Drawing.Point(13, 82);
            this.labelOffice.Name = "labelOffice";
            this.labelOffice.Size = new System.Drawing.Size(103, 12);
            this.labelOffice.TabIndex = 5;
            this.labelOffice.Text = "教室を選んでください";
            // 
            // comboBoxOffice
            // 
            this.comboBoxOffice.FormattingEnabled = true;
            this.comboBoxOffice.Location = new System.Drawing.Point(151, 79);
            this.comboBoxOffice.Name = "comboBoxOffice";
            this.comboBoxOffice.Size = new System.Drawing.Size(121, 20);
            this.comboBoxOffice.TabIndex = 4;
            // 
            // labelReport
            // 
            this.labelReport.AutoSize = true;
            this.labelReport.Location = new System.Drawing.Point(13, 114);
            this.labelReport.Name = "labelReport";
            this.labelReport.Size = new System.Drawing.Size(116, 12);
            this.labelReport.TabIndex = 7;
            this.labelReport.Text = "レポートを選んでください";
            // 
            // comboBoxReport
            // 
            this.comboBoxReport.FormattingEnabled = true;
            this.comboBoxReport.Location = new System.Drawing.Point(151, 111);
            this.comboBoxReport.Name = "comboBoxReport";
            this.comboBoxReport.Size = new System.Drawing.Size(121, 20);
            this.comboBoxReport.TabIndex = 6;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(88, 155);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(111, 36);
            this.buttonCreate.TabIndex = 8;
            this.buttonCreate.Text = "作成";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 215);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.labelReport);
            this.Controls.Add(this.comboBoxReport);
            this.Controls.Add(this.labelOffice);
            this.Controls.Add(this.comboBoxOffice);
            this.Controls.Add(this.labelMonth);
            this.Controls.Add(this.comboBoxMonth);
            this.Controls.Add(this.Year);
            this.Controls.Add(this.comboBoxYear);
            this.Name = "MainForm";
            this.Text = "生徒管理システムレポート出力";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label Year;
        private System.Windows.Forms.Label labelMonth;
        private System.Windows.Forms.ComboBox comboBoxMonth;
        private System.Windows.Forms.Label labelOffice;
        private System.Windows.Forms.ComboBox comboBoxOffice;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.ComboBox comboBoxReport;
        private System.Windows.Forms.Button buttonCreate;
    }
}

