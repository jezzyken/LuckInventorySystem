using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
   
        private void btnItems_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrItems());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrInventory());
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrInventory());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrSupplier());
        }

        private void frmUsers_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrUsers());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrReports());
        }
    }
}
