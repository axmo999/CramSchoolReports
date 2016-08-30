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
            this.Year = new System.Windows.Forms.Label();
            this.labelMonth = new System.Windows.Forms.Label();
            this.labelOffice = new System.Windows.Forms.Label();
            this.comboBoxOffice = new System.Windows.Forms.ComboBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.comboBoxReport = new System.Windows.Forms.ComboBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.textBoxMonth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Year
            // 
            this.Year.AutoSize = true;
            this.Year.Location = new System.Drawing.Point(13, 19);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(101, 12);
            this.Year.TabIndex = 1;
            this.Year.Text = "年を入力してください";
            // 
            // labelMonth
            // 
            this.labelMonth.AutoSize = true;
            this.labelMonth.Location = new System.Drawing.Point(13, 50);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(101, 12);
            this.labelMonth.TabIndex = 3;
            this.labelMonth.Text = "月を入力してください";
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
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(151, 16);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(121, 19);
            this.textBoxYear.TabIndex = 9;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // textBoxMonth
            // 
            this.textBoxMonth.Location = new System.Drawing.Point(151, 47);
            this.textBoxMonth.Name = "textBoxMonth";
            this.textBoxMonth.Size = new System.Drawing.Size(121, 19);
            this.textBoxMonth.TabIndex = 10;
            this.textBoxMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMonth_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 215);
            this.Controls.Add(this.textBoxMonth);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.labelReport);
            this.Controls.Add(this.comboBoxReport);
            this.Controls.Add(this.labelOffice);
            this.Controls.Add(this.comboBoxOffice);
            this.Controls.Add(this.labelMonth);
            this.Controls.Add(this.Year);
            this.Name = "MainForm";
            this.Text = "生徒管理システムレポート出力";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Year;
        private System.Windows.Forms.Label labelMonth;
        private System.Windows.Forms.Label labelOffice;
        private System.Windows.Forms.ComboBox comboBoxOffice;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.ComboBox comboBoxReport;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.TextBox textBoxMonth;
    }
}

