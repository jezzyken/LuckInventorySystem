using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Diagram.Core.Native.Ribbon;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.db;
using LuckInventorySystem_v2.model;
using MySql.Data.MySqlClient;

namespace LuckInventorySystem_v2.controller
{
    class DefectiveController : DefectiveModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static DefectiveController _instance;

        public static DefectiveController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DefectiveController();
                }
                return _instance;
            }
        }

        public DefectiveController()
        {
            Connect = new dbConnection();
        }

        public MySqlTransaction Trans1
        {
            get { return Trans; }
            set { Trans = value; }
        }

        public Boolean add()
        {
            string qryStringMember;

            qryStringMember = "Insert into tbl_defective_items (item_id, no_of_defective_item, item_issue, user_id, date_added) VALUES (@item_id, @no_of_defective_item, @item_issue, @user_id, NOW())";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@item_id", ItemId.Trim());
                Connect.CmSql.Parameters.AddWithValue("@no_of_defective_item", NoOfDefectiveItem);
                Connect.CmSql.Parameters.AddWithValue("@item_issue", ItemIssue.Trim());
                Connect.CmSql.Parameters.AddWithValue("@user_id", UserId);

                Trans = Connect.CnSql.BeginTransaction();
                Connect.CmSql.Transaction = Trans;
                Connect.CmSql.ExecuteNonQuery();
                Trans.Commit();
                return true;
            }
            catch (MySqlException e)
            {
                XtraMessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Trans.Rollback();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "General Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Trans.Rollback();

            }
            finally
            {
                Connect.CnSql.Close();
                Connect.CnSql.Dispose();
                Connect.CmSql.Dispose();
                Connect.CmSql = null;
                Connect.DrSql = null;
            }
            return false;
        }


        public void countItem()
        {

            int x = 0;

            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();

            String qryStr = "Select count(*) as NoOfItem from tbl_defective_items ";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();

            while (Connect.DrSql.Read())
            {
                try
                {
                    Count = Connect.DrSql.GetInt32("NoOfItem");

                }
                catch (SqlNullValueException e)
                {

                }
            }

            Connect.CnSql.Close();
            Connect.CnSql.Dispose();
            Connect.CmSql.Dispose();
            Connect.CmSql = null;
            Connect.DrSql.Dispose();
            Connect.DrSql = null;
        }

        public void display()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            Connect.CnSql.Open();

            String qryStr = "Select * from view_defective_items";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvDefective.Items.Clear();

            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("defective_item_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("no_of_defective_item").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_issue").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("supplier_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("category").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("brand").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("model").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_added").Trim());


                    if (x % 2 == 1)
                    {
                        lvi.BackColor = Color.MintCream;
                    }
                    else
                    {
                        lvi.BackColor = Color.White;
                    }

                    x++;
                    LsvDefective.Items.Add(lvi);
                }
                catch (SqlNullValueException e)
                {

                }
            }
            Connect.CnSql.Close();
            Connect.CnSql.Dispose();
            Connect.CmSql.Dispose();
            Connect.CmSql = null;
            Connect.DrSql.Dispose();
            Connect.DrSql = null;

        }




    }
}
