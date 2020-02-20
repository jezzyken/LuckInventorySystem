using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuckInventorySystem_v2.controller;
using Label = System.Reflection.Emit.Label;

namespace LuckInventorySystem_v2
{
    public partial class ctrInventory : UserControl
    {
        ItemController _itemController = ItemController.GetInstance;
        StockinController _stockinController = StockinController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        private int _userRowIndex1;
        private ListViewItem selItem1;

        private string item_id;
        private string stocks_item_id;
        private int stockin_id;
        private string item_name;
        private int stocks;
        private int total_stocks = 0;
        private int item_stocks;

        public ctrInventory()
        {
            InitializeComponent();

            _itemController.LsvItem = LsvItem;
            LsvItem.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Stocks", 143, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            _itemController.display();

            _itemController.LsvPurchases = LsvPurchases;
            LsvPurchases.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Stocks", 143, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Item Id", 143, HorizontalAlignment.Left);
            _itemController.displayStocks();

        }

        private void btnStocks_Click(object sender, EventArgs e)
        {
            new frmStocks(item_id, item_name, int.Parse(lblStocks.Text)).ShowDialog();
        }

        private void LsvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItem.Items[_userRowIndex];
            item_id = selItem.SubItems[0].Text;
            item_name = selItem.SubItems[1].Text;
            stocks = int.Parse(selItem.SubItems[3].Text);

            lblStocks.Text = stocks.ToString();

        }

        private void LsvItem_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmUpdateStocks(stockin_id, int.Parse(lblStocksAdded.Text), stocks_item_id).ShowDialog();
        }

        private void LsvPurchases_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex1 = e.ItemIndex;
        }

        private void LsvPurchases_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem1 = LsvPurchases.Items[_userRowIndex1];
            stockin_id = int.Parse(selItem1.SubItems[0].Text);
            item_stocks = int.Parse(selItem1.SubItems[3].Text);
            stocks_item_id = selItem1.SubItems[12].Text;
            lblStocksAdded.Text = item_stocks.ToString();
        }
    }
}
