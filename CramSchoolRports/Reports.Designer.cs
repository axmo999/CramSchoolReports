namespace CramSchoolReports
{
    partial class Reports
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
            this.components = new System.ComponentModel.Container();
            this.dataTable = new CramSchoolReports.Models.dataTables();
            this.dataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTable
            // 
            this.dataTable.DataSetName = "dataTable";
            this.dataTable.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataTableBindingSource
            // 
            this.dataTableBindingSource.DataSource = this.dataTable;
            this.dataTableBindingSource.Position = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(654, 579);
            this.reportViewer1.TabIndex = 0;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 579);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource dataTableBindingSource;
        private Models.dataTables dataTable;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}