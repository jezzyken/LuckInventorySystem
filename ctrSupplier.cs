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
    public partial class ctrSupplier : UserControl
    {

        SupplierController _supplierController = SupplierController.GetInstance;
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
            _supplierController.SupplierName = txtSupplier.Text;
            _supplierController.ContactNo = txtContactNo.Text;
            _supplierController.Email = txtEmail.Text;
            _supplierController.Address = txtAddress.Text;
            _supplierController.Representative = txtRepresentative.Text;
            _supplierController.add();
            _supplierController.display();
        }

        private void LsvSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
