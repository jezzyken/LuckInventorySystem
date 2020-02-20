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
        ItemController _itemController = ItemController.GetInstance;

        private string item_id;
        private string item_name;
        private int total_remaining_stocks;

        public frmStocks(string item_id, string item_name, int total_stocks)
        {

            InitializeComponent();
            this.item_id = item_id;
            this.item_name = item_name;
            txtItemName.Text = item_name;

            txtTotalStocks.Text = total_stocks.ToString();

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
            total_remaining_stocks = int.Parse(txtQuantity.Text) + int.Parse(txtTotalStocks.Text);

            _itemController.ItemId = item_id;
            _itemController.Stocks = total_remaining_stocks;
            _itemController.updateStocks();
            _itemController.display();

        }
    }
}
