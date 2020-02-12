using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuckInventorySystem_v2.controller;

namespace LuckInventorySystem_v2
{
    public partial class frmStocks : Form
    {

        private StockinController _stockinController = StockinController.GetInstance;

        private int item_id;
        private string item_name;

        public frmStocks(int item_id, string item_name)
        {
            InitializeComponent();
            this.item_id = item_id;
            this.item_name = item_name;
            txtItemName.Text = item_name;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddStocks_Click(object sender, EventArgs e)
        {
            _stockinController.ItemId = item_id;
            _stockinController.QuantityAdded = int.Parse(txtQuantity.Text);
            _stockinController.add();
        }
    }
}
