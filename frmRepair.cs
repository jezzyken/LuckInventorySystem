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
    public partial class frmRepair : Form
    {
        private int _userRowIndex;
        private ListViewItem selItem;

        RepairController repairController = RepairController.GetInstance;


        public frmRepair()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
          new frmNewRepair("New").ShowDialog();
        }

        private void frmRepair_Load(object sender, EventArgs e)
        {
            repairController.LsvRepair = LsvRepair;
            LsvRepair.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Customer Name", 200, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("contact No", 143, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Item", 300, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Issue", 200, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Status", 143, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Imei", 143, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Downpayment", 143, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Repair Price", 143, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Representative", 143, HorizontalAlignment.Left);
            LsvRepair.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            repairController.display();

            btnRepaired.Enabled = false;
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LsvRepair_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvRepair.Items[_userRowIndex];
            repairController.RepairId = int.Parse(selItem.SubItems[0].Text);
            repairController.CustomerName = selItem.SubItems[1].Text;
            repairController.ContactNo = selItem.SubItems[2].Text;
            repairController.ItemName = selItem.SubItems[3].Text;
            repairController.Issue = selItem.SubItems[4].Text;
            repairController.Status = selItem.SubItems[5].Text;
            repairController.Imei = selItem.SubItems[6].Text;
            repairController.Downpayment = double.Parse(selItem.SubItems[7].Text);
            repairController.RepairPrice = double.Parse(selItem.SubItems[8].Text);
            repairController.StoreRepresentative = selItem.SubItems[9].Text;


            if (repairController.Status == "Pending")
            {
                btnRepaired.Enabled = true;
                btnCancel.Enabled = true;
                btnUpdate.Enabled = true;
            }

            if (repairController.Status == "Repaired")
            {
                btnRepaired.Enabled = false;
                btnCancel.Enabled = true;
                btnUpdate.Enabled = false;
            }
        }

        private void LsvRepair_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (LsvRepair.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item");
            }
            else
            {
                frmNewRepair newRepair = new frmNewRepair("Update");
                newRepair.repair_id = repairController.RepairId;
                newRepair.customer_name = repairController.CustomerName;
                newRepair.contact_no = repairController.ContactNo;
                newRepair.item_name = repairController.ItemName;
                newRepair.issue = repairController.Issue;
                newRepair.imei = repairController.Imei;
                newRepair.downpayment = repairController.Downpayment;
                newRepair.repair_price = repairController.RepairPrice;
                newRepair.store_representative = repairController.StoreRepresentative;
                newRepair.ShowDialog();
            }

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvRepair.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item");
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to update this order as REPAIRED?",
                    "Updating...",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    repairController.RepairId = repairController.RepairId;
                    repairController.Status = "Repaired";
                    repairController.updateStatus();
                    MessageBox.Show("Updated");
                    repairController.display();
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvRepair.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item");
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Are you sure you want to cancel?",
                    "Cancelling...",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    new frmVerification(repairController.RepairId, "Repair").ShowDialog();
                }

            }
        }
    }
}
