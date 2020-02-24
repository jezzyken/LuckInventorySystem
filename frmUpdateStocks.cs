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
    public partial class frmUpdateStocks : Form
    {


        StockinController _stockinController = StockinController.GetInstance;
        ItemController _itemController = ItemController.GetInstance;

        private int stockin_id;
        private int item_stocks;
        private int total_remaining_stocks;
        private int total_stocks;
        private string stocks_item_id;

        public frmUpdateStocks(int stockin_id, int item_stocks, string stocks_item_id)
        {
            InitializeComponent();

            this.stockin_id = stockin_id;
            this.item_stocks = item_stocks;
            this.stocks_item_id = stocks_item_id;
            lblStockinStocks.Text = item_stocks.ToString();
          
           

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateStocks_Click(object sender, EventArgs e)
        {
            //this line of code update the stockin stocks added
            _stockinController.StockinId = stockin_id;
            _stockinController.QuantityAdded = int.Parse(txtStocks.Text);
            _stockinController.update();
            _itemController.displayStocks();

            //this line of code update stocks quantity in tbl_item
            _itemController.ItemId = stocks_item_id;
            _itemController.getAvailableStocks(stocks_item_id);
            total_stocks = _itemController.Stocks;
            _itemController.Stocks = (total_stocks - int.Parse(lblStockinStocks.Text)) + int.Parse(txtStocks.Text);
            _itemController.updateStocks();
            MessageBox.Show("Stocks Updated");
            _itemController.display();
            this.Close();

        }
    }
}
