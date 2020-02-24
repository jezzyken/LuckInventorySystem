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
    public partial class ctrSupplier : UserControl
    {

        SupplierController _supplierController = SupplierController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        public ctrSupplier()
        {
            InitializeComponent();
            _supplierController.LsvSupplier = LsvSuppliers;
            LsvSuppliers.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Representative", 200, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Contact No", 200, HorizontalAlignment.Left);
            LsvSuppliers.Columns.Add("Email", 200, HorizontalAlignment.Left);
            _supplierController.display();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "Update")
            {
                _supplierController.SupplierId = _supplierController.SupplierId;
                _supplierController.SupplierName = txtSupplier.Text;
                _supplierController.ContactNo = txtContactNo.Text;
                _supplierController.Email = txtEmail.Text;
                _supplierController.Address = txtAddress.Text;
                _supplierController.Representative = txtRepresentative.Text;
                _supplierController.update();
                MessageBox.Show("Updated");
                _supplierController.display();
            }
            else
            {
                _supplierController.SupplierName = txtSupplier.Text;
                _supplierController.ContactNo = txtContactNo.Text;
                _supplierController.Email = txtEmail.Text;
                _supplierController.Address = txtAddress.Text;
                _supplierController.Representative = txtRepresentative.Text;
                _supplierController.add();
                MessageBox.Show("Save");
                _supplierController.display();
            }

        }


        private void button3_Click(object sender, EventArgs e)
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
                dialogResult = XtraMessageBox.Show("Do you want to archived this supplier?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _supplierController.IsDeleted = 0;
                    _supplierController.SupplierId = _supplierController.SupplierId;
                    _supplierController.archive();

                    XtraMessageBox.Show("Succesfully Updated",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _supplierController.display();

                }
            }
        }

        private void LsvSuppliers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;

            if (LsvSuppliers.SelectedItems.Count == 1)
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Add";
            }
        }


        private void LsvSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvSuppliers.Items[_userRowIndex];
            _supplierController.SupplierId = int.Parse(selItem.SubItems[0].Text);
            txtSupplier.Text = selItem.SubItems[1].Text;
            txtRepresentative.Text = selItem.SubItems[2].Text;
            txtContactNo.Text = selItem.SubItems[3].Text;
            txtEmail.Text= selItem.SubItems[4].Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new frmArchivedSupplier().ShowDialog();
        }
    }
}
