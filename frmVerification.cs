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
    public partial class frmVerification : Form
    {
        UserController userController = UserController.GetInstance;
        OrderController orderController = OrderController.GetInstance;
        RepairController repairController = RepairController.GetInstance;

        private Boolean state;
        public Boolean State
        {
            get { return state; }
            set { state = value; }
        }


        private int order_id;
        private string status;


        public frmVerification(int order_id, string status)
        {
            InitializeComponent();
            this.order_id = order_id;
            this.status = status;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (status == "Order")
            {
                userController.Username = txtUsername.Text;
                userController.Password = txtPassword.Text;
                if (userController.verify())
                {
                    orderController.OrderId = order_id;
                    orderController.Status = "Cancelled";
                    orderController.UserId = userController.UserId;
                    orderController.cancelOrder();
                    MessageBox.Show("Order Cancelled");
                    orderController.display();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Access Denied");
                }
            }
            else if (status == "Repair")
            {
                userController.Username = txtUsername.Text;
                userController.Password = txtPassword.Text;

                if (userController.verify())
                {
                    repairController.RepairId = repairController.RepairId;
                    repairController.Status = "Cancelled";
                    repairController.UserId = userController.UserId;
                    repairController.cancelRepair();
                    MessageBox.Show("Cancelled");
                    repairController.display();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Access Denied");
                }
            }
            else
            {
                //userController.Username = txtUsername.Text;
                //userController.Password = txtPassword.Text;

                //if (userController.verify())
                //{
                //    repairController.RepairId = repairController.RepairId;
                //    repairController.Status = "Cancelled";
                //    repairController.UserId = userController.UserId;
                //    repairController.cancelRepair();
                //    MessageBox.Show("Cancelled");
                //    repairController.display();

                //   // frmDefectiveItem();
                //    state = true;
                //    this.Close();


                //}
                //else
                //{
                //    MessageBox.Show("Access Denied");
                //}
            }

        }
    }
}
