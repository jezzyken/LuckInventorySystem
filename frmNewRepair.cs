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
    public partial class frmNewRepair : Form
    {

         RepairController repairController = RepairController.GetInstance;

        public int repair_id { get; set; }
        public string customer_name { get; set; }
        public string contact_no { get; set; }
        public string item_name { get; set; }
        public string issue { get; set; }
        public string imei { get; set; }
        public double downpayment { get; set; }
        public double repair_price { get; set; }
        public string store_representative { get; set; }


        private string state;
        public frmNewRepair(string state)
        {
            InitializeComponent();
            this.state = state;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (state == "New")
            {
                repairController.ItemName = txtItem.Text;
                repairController.Imei = txtImei.Text;
                repairController.Issue = txtIssue.Text;
                repairController.CustomerName = txtCustomerName.Text;
                repairController.ContactNo = txtContactNo.Text;
                repairController.Status = "Pending";
                repairController.Downpayment = double.Parse(txtDownpayment.Text);
                repairController.RepairPrice = double.Parse(txtRepairPrice.Text);
                repairController.StoreRepresentative = txtStoreRepresentative.Text;
                repairController.add();
                MessageBox.Show("Repair Added");
                repairController.display();
                this.Close();
            }
            else
            {
                repairController.RepairId = repair_id;
                repairController.ItemName = txtItem.Text;
                repairController.Imei = txtImei.Text;
                repairController.Issue = txtIssue.Text;
                repairController.CustomerName = txtCustomerName.Text;
                repairController.ContactNo = txtContactNo.Text;
                repairController.Status = "Pending";
                repairController.Downpayment = double.Parse(txtDownpayment.Text);
                repairController.RepairPrice = double.Parse(txtRepairPrice.Text);
                repairController.StoreRepresentative = txtStoreRepresentative.Text;
                repairController.update();
                MessageBox.Show("Updated");
                repairController.display();
                this.Close();
            }
 
        }

        private void frmNewRepair_Load(object sender, EventArgs e)
        {
            txtCustomerName.Text = customer_name;
            txtContactNo.Text = contact_no;
            txtItem.Text = item_name;
            txtIssue.Text = issue;
            txtImei.Text = imei;
            txtDownpayment.Text = downpayment.ToString();
            txtRepairPrice.Text = repair_price.ToString();
            txtStoreRepresentative.Text = store_representative;

            MessageBox.Show(state);
        }
    }
}
