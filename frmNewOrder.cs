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
    public partial class frmNewOrder : Form
    {

        OrderController _orderController = OrderController.GetInstance;

        private string state;

        public int order_id { get; set; }
        public string ordered_item { get; set; }
        public string additional_info { get; set; }
        public string customer_name { get; set; }
        public string contact_no { get; set; }
        public double downpayment { get; set; }
        public double order_price { get; set; }
        public string store_representative { get; set; }

        public frmNewOrder(string state)
        {
            InitializeComponent();
            this.state = state;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (state == "New")
            {
                _orderController.OrderItem = txtOrderedItem.Text;
                _orderController.AdditionalInfo = txtAdditionalInfo.Text;
                _orderController.CustomerName = txtCustomerName.Text;
                _orderController.ContactNo = txtContactNo.Text;
                _orderController.Downpayment = double.Parse(txtDownpayment.Text);
                _orderController.OrderPrice = double.Parse(txtOrderPrice.Text);
                _orderController.Status = "Pending";
                _orderController.StoreRepresentative = txtStoreRepresentative.Text;
                _orderController.add();
                _orderController.display();
            }
            else
            {
                _orderController.OrderId = order_id;
                _orderController.OrderItem = txtOrderedItem.Text;
                _orderController.AdditionalInfo = txtAdditionalInfo.Text;
                _orderController.CustomerName = txtCustomerName.Text;
                _orderController.ContactNo = txtContactNo.Text;
                _orderController.Downpayment = double.Parse(txtDownpayment.Text);
                _orderController.OrderPrice = double.Parse(txtOrderPrice.Text);
                _orderController.update();
                _orderController.display();
            }
         

        }

        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            order_id = order_id;
            txtOrderedItem.Text = ordered_item;
            txtAdditionalInfo.Text = additional_info;
            txtCustomerName.Text = customer_name;
            txtContactNo.Text = contact_no;
            txtDownpayment.Text = downpayment.ToString();
            txtOrderPrice.Text = order_price.ToString();
            txtStoreRepresentative.Text = store_representative;


            MessageBox.Show(state);

        }
    }
}
