using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace LuckInventorySystem_v2
{
    public partial class frmReceipt : Form
    {
        ReportDocument cryRpt = new ReportDocument();

        private string tranx_no;

        public frmReceipt(string tranx_no)
        {
            InitializeComponent();
            this.tranx_no = tranx_no;
            receipt();
        }

        public void receipt()
        {
            try
            {
                cryRpt.Load("C:\\Users\\YouJezzy\\Documents\\Visual Studio 2015\\Projects\\LuckInventorySystem_v2\\LuckInventorySystem_v2\\rptReciept.rpt");

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                crParameterDiscreteValue.Value = tranx_no;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["tranx_no"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
