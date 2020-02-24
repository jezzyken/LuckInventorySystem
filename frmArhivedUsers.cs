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
    public partial class frmArhivedUsers : Form
    {

        UserController _userController = UserController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        public frmArhivedUsers()
        {
            InitializeComponent();

            _userController.LsvUserAchived = LsvUser;
            LsvUser.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvUser.Columns.Add("Name", 200, HorizontalAlignment.Left);
            LsvUser.Columns.Add("Contact No", 200, HorizontalAlignment.Left);
            LsvUser.Columns.Add("Email", 200, HorizontalAlignment.Left);
            LsvUser.Columns.Add("Username", 200, HorizontalAlignment.Left);
            LsvUser.Columns.Add("User Type", 200, HorizontalAlignment.Left);
            _userController.displayArchived();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvUser.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please Select User",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to set as active this user?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _userController.IsDeleted = 1;
                    _userController.UserId = _userController.UserId;
                    _userController.Archive();

                    XtraMessageBox.Show("Succesfully Updated",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _userController.displayArchived();
                    _userController.display();

                }
            }
        }

        private void LsvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvUser.Items[_userRowIndex];
            _userController.UserId = int.Parse(selItem.SubItems[0].Text);
        }

        private void LsvUser_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }
    }
}
