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
using Timer = System.Threading.Timer;

namespace LuckInventorySystem_v2
{
    public partial class frmMain : Form
    {

        private UserController _userController = UserController.GetInstance;
        public string name { get; set; }
        public string user_level { get; set; }
        public int user_id { get; set; }

        public frmMain()
        {
            InitializeComponent();

            _userController.GetImage(user_id);
            _userController.UserImage = userImage;
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrItems());
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrInventory());
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrInventory());

         

            txtName.Text = name;
            txtUserType.Text = user_level;
            timer1.Start();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrSupplier());
        }

        private void frmUsers_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrUsers());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new ctrReports());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtSystemDate.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            _userController.GetImage(user_id);
            _userController.UserImage = userImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }
    }
}
