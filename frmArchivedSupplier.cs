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
    public partial class frmArchivedSupplier : Form
    {

        SupplierController _supplierController = SupplierController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        public frmArchivedSupplier()
        {
            InitializeComponent();

            _supplierController.LsvSupplierArchived = LsvSuppliers;
            LsvSuppliers.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Representative", 200, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Contact No", 200, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Email", 200, HorizontalAlignment.Left);
            _supplierController.displayArchived();
        }

        private void LsvSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvSuppliers.Items[_userRowIndex];
            _supplierController.SupplierId = int.Parse(selItem.SubItems[0].Text);
        }

        private void LsvSuppliers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvSuppliers.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please Select Supplier",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to set as active this supplier?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _supplierController.IsDeleted = 1;
                    _supplierController.UserId = _supplierController.SupplierId;
                    _supplierController.archive();

                    XtraMessageBox.Show("Succesfully Updated",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _supplierController.displayArchived();
                    _supplierController.display();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
