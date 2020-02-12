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
    public partial class ctrUsers : UserControl
    {

        private UserController _userControl = UserController.GetInstance;
        private UserController _userController = UserController.GetInstance;

        private String imagePath = "";
        private int _userRowIndex;
        private ListViewItem selItem;
        private int userId;

        public ctrUsers()
        {
            InitializeComponent();
            _userControl.LsvUser = LsvUsers;
            LsvUsers.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvUsers.Columns.Add("Name", 200, HorizontalAlignment.Left);
            LsvUsers.Columns.Add("Contact No", 200, HorizontalAlignment.Left);
            LsvUsers.Columns.Add("Email", 200, HorizontalAlignment.Left);
            LsvUsers.Columns.Add("Username", 200, HorizontalAlignment.Left);
            LsvUsers.Columns.Add("User Type", 200, HorizontalAlignment.Left);
            _userControl.display();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (btnSave.Text == "Update")
            {
               
                _userController.UserId = userId;
                _userController.Name = txtName.Text;
                _userController.ContactNo = txtContactNo.Text;
                _userController.Email = txtEmail.Text;
                _userController.Username = txtUsername.Text;
                _userController.Password = txtPassword.Text;
                _userController.UserLevel = cboUserType.Text;
                _userController.ImagePath = imagePath;
                _userController.update();
                _userControl.display();
            }
            else
            {
              
                _userController.Name = txtName.Text;
                _userController.ContactNo = txtContactNo.Text;
                _userController.Email = txtEmail.Text;
                _userController.Username = txtUsername.Text;
                _userController.Password = txtPassword.Text;
                _userController.UserLevel = cboUserType.Text;
                _userController.ImagePath = imagePath;
                _userController.add();
                _userControl.display();
            }

        }

        private void LsvUsers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;

            if (LsvUsers.SelectedItems.Count == 1)
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Add";
            }
        }

        private void LsvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvUsers.Items[_userRowIndex];
            userId = int.Parse(selItem.SubItems[0].Text);
            txtName.Text = selItem.SubItems[1].Text;
            txtContactNo.Text = selItem.SubItems[2].Text;
            txtEmail.Text = selItem.SubItems[3].Text;
            txtUsername.Text = selItem.SubItems[4].Text;
            cboUserType.Text = selItem.SubItems[5].Text;

        }


    }
}
