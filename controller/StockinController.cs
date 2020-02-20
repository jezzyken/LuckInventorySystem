using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    class StockinController : StockinModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static StockinController _instance; //---------
        public static StockinController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockinController();
                }
                return _instance;
            }
        }

        public StockinController()
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

            qryStringMember = "Insert into tbl_stockin (item_id, quantity_added, date_added) VALUES (@item_id, @quantity_added, NOW())";

            try
            {

                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@item_id", ItemId);
                Connect.CmSql.Parameters.AddWithValue("@quantity_added", QuantityAdded);
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


        public Boolean update()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_stockin set " +
                              "quantity_added=@quantity_added " + "WHERE stockin_id=@stockin_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@quantity_added", QuantityAdded);
                Connect.CmSql.Parameters.AddWithValue("@stockin_id", StockinId);

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


        public void getTotalStocks(string item_id)
        {
            int x = 0;

            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();

            String qryStr = "Select sum(quantity_added) as total from tbl_stockin where item_id = '" + item_id + "'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();

            while (Connect.DrSql.Read())
            {
                try
                {
                    TotalStocks = Connect.DrSql.GetInt32("total");
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
