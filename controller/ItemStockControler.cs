﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.db;
using LuckInventorySystem_v2.model;
using MySql.Data.MySqlClient;

namespace LuckInventorySystem_v2.controller
{
    internal class ItemStockControler : ItemStockModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static ItemStockControler _instance; //---------

        public static ItemStockControler GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemStockControler();
                }
                return _instance;
            }
        }

        public ItemStockControler()
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

            qryStringMember = "Insert into tbl_item_stocks (item_id, remaining_stocks) VALUES (@item_id, @remaining_stocks)";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@item_id", ItemId);
                Connect.CmSql.Parameters.AddWithValue("@remaining_stocks", RemainingStocks);
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

            qryStringMember = "UPDATE tbl_item_stocks set " +
                              "remaining_stocks=@remaining_stocks " + "WHERE item_id=@item_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@remaining_stocks", RemainingStocks);
                Connect.CmSql.Parameters.AddWithValue("@item_id", ItemId);

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


        public void checkItem(string item_id)
        {
            int x = 0;

            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();

            String qryStr = "Select count(*) as Record from tbl_item_stocks where item_id  = '" + item_id + "'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();

            while (Connect.DrSql.Read())
            {
                try
                {
                    CheckItem = Connect.DrSql.GetInt32("Record");

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
