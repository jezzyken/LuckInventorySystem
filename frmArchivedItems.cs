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
    public partial class frmArchivedItems : Form
    {

        ItemController _itemController = ItemController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        public frmArchivedItems()
        {
            InitializeComponent();

            _itemController.LsvAarchivedItems = LsvItems;
            LsvItems.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Stocks", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvItems.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            _itemController.displayArchived();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LsvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItems.Items[_userRowIndex];
            _itemController.ItemId = selItem.SubItems[0].Text;
        }

        private void LsvItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvItems.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please Select Item",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to restore this Item?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _itemController.IsDeleted = 1;
                    _itemController.ItemId = _itemController.ItemId;
                    _itemController.archive();

                    XtraMessageBox.Show("Succesfully Updated",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _itemController.displayArchived();
                    _itemController.display();
                }
            }
        }
    }
}
