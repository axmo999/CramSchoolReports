﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CramSchoolReports
{
    public partial class Reports : Form
    {
        public ReportViewer _rptViewer;
        public int _Year;
        public int _Month;
        public int _OfficeNum;
        public string _OfficeName;
        public string _Reports;

        private Assembly _assembly = Assembly.GetExecutingAssembly();

        private Stream _stream;

        private getData _getData = new getData();

        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            switch (_Reports)
            {
                case "StudentMonthlyAttend":

                    _stream = _assembly.GetManifestResourceStream("CramSchoolReports.Reports.StudentMonthlyAttend.rdlc");

                    this.reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.LoadReportDefinition(_stream);
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dataTable", this._getData.getAttend(_Year, _Month, _OfficeNum)));

                    List<ReportParameter> ListParametersAttend = new List<ReportParameter>();
                    ReportParameter paramYearAttend = new ReportParameter("Year", _Year.ToString());
                    ReportParameter paramMonthAttnd = new ReportParameter("Month", _Month.ToString());
                    ReportParameter paramOfficeAttnd = new ReportParameter("OfficeName", _OfficeName.ToString());
                    ListParametersAttend.Add(paramYearAttend);
                    ListParametersAttend.Add(paramMonthAttnd);
                    ListParametersAttend.Add(paramOfficeAttnd);
                    reportViewer1.LocalReport.SetParameters(ListParametersAttend);

                    reportViewer1.LocalReport.DisplayName = _Year + "年" + _Month + "月分出席一覧表" + "　" + _OfficeName;
                    break;

                case "StudentMonthlyGuid":
                    _stream = _assembly.GetManifestResourceStream("CramSchoolReports.Reports.StudentMonthlyGuid.rdlc");

                    this.reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.LoadReportDefinition(_stream);
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dataTable", this._getData.getGuide(_Year, _Month, _OfficeNum)));

                    List<ReportParameter> ListParametersGuid = new List<ReportParameter>();
                    ReportParameter paramYearGuid = new ReportParameter("Year", _Year.ToString());
                    ReportParameter paramMonthGuid = new ReportParameter("Month", _Month.ToString());
                    ReportParameter paramOfficeGuid = new ReportParameter("OfficeName", _OfficeName.ToString());
                    ListParametersGuid.Add(paramYearGuid);
                    ListParametersGuid.Add(paramMonthGuid);
                    ListParametersGuid.Add(paramOfficeGuid);
                    reportViewer1.LocalReport.SetParameters(ListParametersGuid);

                    reportViewer1.LocalReport.DisplayName = _Year + "年" + _Month + "月分指導一覧表" + "　" + _OfficeName;
                    break;

            } 
            
            
            
            
            
            
            if (reportViewer1.LocalReport.IsReadyForRendering)
            {
                this.reportViewer1.RefreshReport();
            }
        }
    }
}