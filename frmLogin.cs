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
    public partial class frmLogin : Form
    {

        UserController _userController = UserController.GetInstance;

        private int count;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _userController.UserLevel = cboType.Text;
            _userController.Username = txtUsername.Text;
            _userController.Password = txtPassword.Text;

            if (_userController.Login())
            {
                if (cboType.Text == "Admin")
                {
                    
                    frmMain _main = new frmMain();
                    _main.user_id = _userController.UserId;
                    _main.name = _userController.Name;
                    _main.user_level = _userController.UserLevel;
                    _main.Show();
                    this.Hide();

                }
            }

            if (_userController.Login())
            {
                if (cboType.Text == "Cashier")
                {

                    frmPOS _POS = new frmPOS();
                    _POS.name = _userController.Name;
                    _POS.user_id = _userController.UserId;
                    _POS.Show();
                    this.Hide();

                }
            }
            else
            {
                XtraMessageBox.Show("Username or Password is incorrect");
            }
            
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
