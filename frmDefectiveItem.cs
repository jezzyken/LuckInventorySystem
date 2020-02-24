using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.controller;

namespace LuckInventorySystem_v2
{
    public partial class frmDefectiveItem : Form
    {

        private ItemController _itemController = ItemController.GetInstance;
        DefectiveController _defectiveController = DefectiveController.GetInstance;

        private int _userRowIndex;
        private int _userRowIndex1;
        private ListViewItem selItem;
        private ListViewItem selItem1;


        public string sample { get; set; }


        private string supplier;

        public frmDefectiveItem()
        {
            InitializeComponent();

            _itemController.LsvItem = LsvItem;
            LsvItem.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Stocks", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            _itemController.display();


            LsvDefectiveItem.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("No of Defective Item", 200, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("Item Issue", 300, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvDefectiveItem.Columns.Add("Model", 200, HorizontalAlignment.Left);



        }

        private void LsvDefectiveItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LsvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItem.Items[_userRowIndex];
            _itemController.ItemId = selItem.SubItems[0].Text;
            _itemController.ItemName = selItem.SubItems[1].Text;
            supplier = selItem.SubItems[2].Text;
            _itemController.Category = selItem.SubItems[4].Text;
            _itemController.Brand = selItem.SubItems[5].Text;
            _itemController.Model = selItem.SubItems[6].Text;

            //get stocks
            _itemController.Stocks = int.Parse(selItem.SubItems[3].Text);

            lblItemName.Text = _itemController.ItemName;

        }

        private void LsvItem_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem();
            item.Text = _itemController.ItemId;
            item.SubItems.Add(_itemController.ItemName);
            item.SubItems.Add(txtNoOfItem.Text);
            item.SubItems.Add(txtIssue.Text);
            item.SubItems.Add(supplier);
            item.SubItems.Add(_itemController.Category);
            item.SubItems.Add(_itemController.Brand);
            item.SubItems.Add(_itemController.Model);


            if (int.Parse(txtNoOfItem.Text) > _itemController.Stocks)
            {
                MessageBox.Show("Not Enough Stocks");
            }
            else
            {
                if (LsvDefectiveItem.FindItemWithText(_itemController.ItemId) == null)
                {
                    if (int.Parse(txtNoOfItem.Text) == 0)
                    {
                        MessageBox.Show("Please add an Item");
                    }
                    else
                    {
                        if (txtIssue.Text == "")
                        {
                            MessageBox.Show("Please specify the issue of the item");
                        }
                        else
                        {
                            txtIssue.Text = "";
                            LsvDefectiveItem.Items.Add(item);
                        }

                    }
                }
                else
                {
                    foreach (ListViewItem i in LsvDefectiveItem.Items)
                    {

                        if (i.SubItems[0].Text == _itemController.ItemId)
                        {
                            if (int.Parse(txtNoOfItem.Text) == 0)
                            {
                                MessageBox.Show("Please add stock");
                            }
                            else
                            {
                                i.SubItems[2].Text =
                                    Convert.ToString(Convert.ToInt32(txtNoOfItem.Text) +
                                                     Convert.ToInt32(i.SubItems[2].Text));

                                if (int.Parse(i.SubItems[2].Text) > _itemController.Stocks)
                                {
                                    i.SubItems[2].Text = _itemController.Stocks.ToString();

                                    MessageBox.Show("Not Enought Stocks");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult;

            dialogResult =
                XtraMessageBox.Show("Selected Item will recorded as Defective/Damage, Do you want to continue?",
                    "Saving..",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

            if (dialogResult == DialogResult.OK)
            {
                //new frmVerification(1, "Defective").ShowDialog();

                foreach (ListViewItem item in LsvDefectiveItem.Items)
                {
                    _itemController.ItemId = item.Text;
                    _itemController.getAvailableStocks(item.Text);
                    _itemController.Stocks = _itemController.Stocks - int.Parse(item.SubItems[2].Text);
                    _itemController.updateStocks();
                    _itemController.display();

                    _defectiveController.ItemId = item.Text;
                    _defectiveController.NoOfDefectiveItem = int.Parse(item.SubItems[2].Text);
                    _defectiveController.ItemIssue = item.SubItems[3].Text;
                    _defectiveController.UserId = 1;
                    _defectiveController.add();
                }
                MessageBox.Show("Save");
                this.Close();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (LsvDefectiveItem.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select item to be removed");
            }
            else
            {
                LsvDefectiveItem.SelectedItems[0].Remove();
            }
        }
    }
}
