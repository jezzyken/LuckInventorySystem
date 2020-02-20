using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace LuckInventorySystem_v2
{
    public partial class ctrItemReport : UserControl
    {
        ReportDocument cryRpt = new ReportDocument();

        public ctrItemReport()
        {
            InitializeComponent();

            cryRpt.Load("C:\\Users\\YouJezzy\\Documents\\Visual Studio 2015\\Projects\\LuckInventorySystem_v2\\LuckInventorySystem_v2\\rptItems.rpt");

            //cryRpt.Load(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? "rptSalesReport.rpt"));

            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
