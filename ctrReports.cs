using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2
{
    public partial class ctrReports : UserControl
    {
        public ctrReports()
        {
            InitializeComponent();

            panelReports.Controls.Clear();
            panelReports.Controls.Add(new ctrSales());
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            panelReports.Controls.Clear();
            panelReports.Controls.Add(new ctrSales());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelReports.Controls.Clear();
            panelReports.Controls.Add(new ctrStocks()); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelReports.Controls.Clear();
            panelReports.Controls.Add(new ctrItemReport());
        }
    }
}
