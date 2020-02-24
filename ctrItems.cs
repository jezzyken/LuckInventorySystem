using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.controller;

namespace LuckInventorySystem_v2
{
    public partial class ctrItems : UserControl
    {

        ItemController _itemController = ItemController.GetInstance;
        SupplierController _supplierController = SupplierController.GetInstance;

        private int supplier_id;
        private string item_id;

        private int _userRowIndex;
        private ListViewItem selItem;
        private int stocks;

       

        public ctrItems()
        {
            InitializeComponent();
            _supplierController.CboSupplierName = cboSupplier;
            _supplierController.displaySupplier();

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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "Update")
            {
                _itemController.SupplierId = supplier_id;
                _itemController.ItemId = item_id;
                _itemController.ItemName = txtItemName.Text;
                _itemController.Category = CBoCategory.Text;
                _itemController.Brand = txtBrand.Text;
                _itemController.Model = txtModel.Text;
                _itemController.Description = txtDescription.Text;
                _itemController.SellingPrice = double.Parse(txtSellingPrice.Text);
                _itemController.WholesalePrice = double.Parse(txtWholesalePrice.Text);
                _itemController.ItemNote = txtNote.Text;

                _itemController.update();
                MessageBox.Show("Updated");
                _itemController.display();
                clear();


            }
            else
            {
                generateItemId();

                _itemController.SupplierId = supplier_id;
                _itemController.ItemId = item_id;
                _itemController.ItemName = txtItemName.Text;
                _itemController.Category = CBoCategory.Text;
                _itemController.Brand = txtBrand.Text;
                _itemController.Model = txtModel.Text;
                _itemController.Description = txtDescription.Text;
                _itemController.SellingPrice = double.Parse(txtSellingPrice.Text);
                _itemController.WholesalePrice = double.Parse(txtWholesalePrice.Text);
                _itemController.ItemNote = txtNote.Text;


                _itemController.checkItem(txtItemName.Text, supplier_id, CBoCategory.Text, txtBrand.Text, txtModel.Text, double.Parse(txtSellingPrice.Text), double.Parse(txtWholesalePrice.Text));

                if (_itemController.Count >= 1)
                {
                    MessageBox.Show("item Already Exist!");
                }
                else
                {
                    _itemController.add();
                    MessageBox.Show("Added");
                    _itemController.display();
                    clear();
                }
              
            }
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            _supplierController.getSupplierId(cboSupplier.Text);
            supplier_id = _supplierController.SupplierId;
        }

        private void LsvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItem.Items[_userRowIndex];
            item_id = selItem.SubItems[0].Text;
            txtItemName.Text = selItem.SubItems[1].Text;
            cboSupplier.Text = selItem.SubItems[2].Text;
            stocks = int.Parse(selItem.SubItems[3].Text);
            CBoCategory.Text = selItem.SubItems[4].Text;
            txtBrand.Text = selItem.SubItems[5].Text;
            txtModel.Text = selItem.SubItems[6].Text;
            txtSellingPrice.Text = selItem.SubItems[7].Text;
            txtWholesalePrice.Text = selItem.SubItems[8].Text;
            txtDescription.Text = selItem.SubItems[9].Text;
            txtNote.Text = selItem.SubItems[10].Text;
        } 

        private void LsvItem_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;

            _supplierController.getSupplierId(cboSupplier.Text);
            supplier_id = _supplierController.SupplierId;

            if (LsvItem.SelectedItems.Count == 1)
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }

        }

        private string generateItemId()
        {
            string strItem = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strId = "";

            Random rnd = new Random();
            for (int i = 0; i <= 10; i++)
            {
                int iRandom = rnd.Next(0, strItem.Length - 1);
                strId += strItem.Substring(iRandom, 1);
            }

            item_id = "Item" + strId;

            return "TransNo" + strId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txtItemName.Text = "";
            txtBrand.Text = "";
            txtModel.Text = "";
            txtNote.Text = "";
            txtSellingPrice.Text = "";
            txtWholesalePrice.Text = "";
            txtDescription.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvItem.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please Select Item",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to archived this Item?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _itemController.IsDeleted = 0;
                    _itemController.ItemId = item_id;
                    _itemController.archive();

                    XtraMessageBox.Show("Succesfully Updated",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _itemController.display();

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new frmArchivedItems().ShowDialog();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _itemController.search(txtSearch.Text);
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
}
