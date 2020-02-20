using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.db;
using LuckInventorySystem_v2.model;
using MySql.Data.MySqlClient;

namespace LuckInventorySystem_v2.controller
{
    class SalesController : SalesModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static SalesController _instance;

        public static SalesController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SalesController();
                }
                return _instance;
            }
        }
        public SalesController()
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

            qryStringMember = "Insert into tbl_item_sales (item_id, sold_quantity, discount, date_purchased, tranx_no, user_id) VALUES (@item_id, @sold_quantity, @discount, NOW(), @tranx_no, @user_id)";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@item_id", ItemId.Trim());
                Connect.CmSql.Parameters.AddWithValue("@sold_quantity", SoldQuantity.Trim());
                Connect.CmSql.Parameters.AddWithValue("@discount", Discount);
                Connect.CmSql.Parameters.AddWithValue("@tranx_no", TranxNo.Trim());
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



    }
}
