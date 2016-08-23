using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CramSchoolReports.Models;

namespace CramSchoolReports
{
    public partial class MainForm : Form
    {
        ///// <summary>生徒マスタ情報コンテキスト</summary>
        //private Models.Students_M.Students_mModel student_m_db = new Models.Students_M.Students_mModel();

        ///// <summary>生徒登録情報コンテキスト</summary>
        //private Models.Students_Data.StudentsModel studentdb = new Models.Students_Data.StudentsModel();

        ///// <summary>設定情報コンテキスト</summary>
        //private Models.Settings_M.MastersModel setdb = new Models.Settings_M.MastersModel();

        private getData _getData = new getData();

        private int _year;
        private int _month;
        private int _officeNum;
        private string _officeName;
        private string _report;

        public MainForm()
        {   
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.comboBoxYear.Items.AddRange(_getData.getYearData());

            this.comboBoxOffice.DataSource = new BindingSource(_getData.getOffice(), null);
            this.comboBoxOffice.DisplayMember = "Value";
            this.comboBoxOffice.ValueMember = "Key";

            this.comboBoxReport.DataSource = new BindingSource(Commons.Utility.reports, null);
            this.comboBoxReport.DisplayMember = "Key";
            this.comboBoxReport.ValueMember = "Value";

        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _year = Convert.ToInt32(this.comboBoxYear.SelectedItem);
            this.comboBoxMonth.Items.AddRange(_getData.getMonthData(_year));
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            _year = Convert.ToInt32(this.comboBoxYear.SelectedItem);
            _month = Convert.ToInt32(this.comboBoxMonth.SelectedItem);
            _officeNum = Convert.ToInt32(this.comboBoxOffice.SelectedValue);
            _officeName = this.comboBoxOffice.Text.ToString();
            _report = this.comboBoxReport.SelectedValue.ToString();

            Reports reports = new Reports();
            reports._Month = _month;
            reports._Year = _year;
            reports._OfficeName = _officeName;
            reports._OfficeNum = _officeNum;
            reports._Reports = _report;
            reports.Show();

        }
    }
}
