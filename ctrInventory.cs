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

namespace LuckInventorySystem_v2
{
    public partial class ctrInventory : UserControl
    {
        ItemController _itemController = ItemController.GetInstance;
        StockinController _stockinController = StockinController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        private int item_id;
        private string item_name;
        private int totalStocks;

        public ctrInventory()
        {
            InitializeComponent();

            _itemController.LsvItem = LsvItem;
            LsvItem.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            _itemController.display();
        }

        private void btnStocks_Click(object sender, EventArgs e)
        {
            new frmStocks(item_id, item_name).ShowDialog();
        }

        private void btnUpdateStocks_Click(object sender, EventArgs e)
        {
            new frmUpdateStocks().ShowDialog();
        }

        private void LsvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItem.Items[_userRowIndex];
            item_id = int.Parse(selItem.SubItems[0].Text);
            item_name= selItem.SubItems[1].Text;

            _stockinController.getTotalStocks(item_id);
            _stockinController.TotalStocks = totalStocks;

        }

        private void LsvItem_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }
    }
}
