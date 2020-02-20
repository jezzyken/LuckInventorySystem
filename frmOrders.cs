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
    public partial class frmOrders : Form
    {

        OrderController _orderController = OrderController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        private int order_id;
        private string ordered_item;
        private string additional_info;
        private string customer_name;
        private string contact_no;
        private double downpayment;
        private double order_price;
        private string store_representative;
        private string status;

        public frmOrders()
        {
            InitializeComponent();
            buttonState();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void buttonState()
        {
    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new frmNewOrder("New").ShowDialog();
        }

        private void frmOrders_Load(object sender, EventArgs e)
        {
            _orderController.LsvOrder = LsvOrder;
            LsvOrder.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Order Item", 200, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Additional Info", 300, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Customer Name", 143, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Contact No", 143, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Downpayment", 143, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Order Price", 143, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Status", 200, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Representative", 143, HorizontalAlignment.Left);
            LsvOrder.Columns.Add("Date Ordered", 200, HorizontalAlignment.Left);
            _orderController.display();
        }

        private void LsvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvOrder.Items[_userRowIndex];
            order_id = int.Parse(selItem.SubItems[0].Text);
            ordered_item = selItem.SubItems[1].Text;
            additional_info = selItem.SubItems[2].Text;
            customer_name = selItem.SubItems[3].Text;
            contact_no = selItem.SubItems[4].Text;
            downpayment = double.Parse(selItem.SubItems[5].Text);
            order_price = double.Parse(selItem.SubItems[6].Text);
            status = selItem.SubItems[7].Text;
            store_representative = selItem.SubItems[8].Text;

            lblStatus.Text = status;

            if (lblStatus.Text == "Pending")
            {
                btnUpdate.Enabled = true;
                btnOrder.Enabled = true;
            }

            if(lblStatus.Text == "Ordered")
            {
                btnUpdate.Enabled = false;
                btnOrder.Enabled = false;
            }
        }

        private void LsvOrder_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmNewOrder newOrder = new frmNewOrder("Update");
            newOrder.order_id = order_id;
            newOrder.ordered_item = ordered_item;
            newOrder.additional_info = additional_info;
            newOrder.customer_name = customer_name;
            newOrder.contact_no = contact_no;
            newOrder.downpayment = downpayment;
            newOrder.order_price = order_price;
            newOrder.store_representative = store_representative;
            newOrder.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _orderController.OrderId = order_id;
            _orderController.Status = "Ordered";
            _orderController.updateStatus();
            _orderController.display();
        }
    }
}
