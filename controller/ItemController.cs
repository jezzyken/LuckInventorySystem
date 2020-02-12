﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
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
    class ItemController : ItemModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static ItemController _instance;

        public static ItemController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemController();
                }
                return _instance;
            }
        }

        public ItemController()
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

            qryStringMember = "Insert into tbl_item (item_name, category, model, brand, selling_price, wholesale_price, description, item_note, date_added, supplier_id) VALUES (@item_name, @category, @model, @brand, @selling_price, @wholesale_price, @description, @item_note, NOW(), @supplier_id)";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@item_name", ItemName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@category", Category.Trim());
                Connect.CmSql.Parameters.AddWithValue("@model", Model.Trim());
                Connect.CmSql.Parameters.AddWithValue("@brand", Brand.Trim());
                Connect.CmSql.Parameters.AddWithValue("@selling_price", SellingPrice);
                Connect.CmSql.Parameters.AddWithValue("@wholesale_price", WholesalePrice);
                Connect.CmSql.Parameters.AddWithValue("@description", Description.Trim());
                Connect.CmSql.Parameters.AddWithValue("@item_note", ItemNote.Trim());
                Connect.CmSql.Parameters.AddWithValue("@supplier_id", SupplierId);

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

        public void display()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            Connect.CnSql.Open();

            String qryStr = "Select * from item_view";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql); Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvItem.Items.Clear(); while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("item_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("supplier_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("category").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("brand").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("model").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("selling_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("wholesale_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("description").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_note").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_added").Trim());

                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvItem.Items.Add(lvi);
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

        public Boolean update()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_item set " +
                              "item_name=@item_name, " +
                              "category=@category, " +
                              "brand=@brand, model=@model, " +
                              "selling_price=@selling_price, wholesale_price=@wholesale_price, " +
                              "description=@description, item_note=@item_note, " +
                              "supplier_id=@supplier_id " + "WHERE item_id=@item_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@item_name", ItemName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@category", Category.Trim());
                Connect.CmSql.Parameters.AddWithValue("@brand", Brand.Trim());
                Connect.CmSql.Parameters.AddWithValue("@model", Model.Trim());
                Connect.CmSql.Parameters.AddWithValue("@selling_price", SellingPrice);
                Connect.CmSql.Parameters.AddWithValue("@wholesale_price", WholesalePrice);
                Connect.CmSql.Parameters.AddWithValue("@description", Description.Trim());
                Connect.CmSql.Parameters.AddWithValue("@item_note", ItemNote.Trim());
                Connect.CmSql.Parameters.AddWithValue("@supplier_id", SupplierId);
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

        public void getItemId(string item_name, int supplier_id, string category_id, string brand, string model, double selling_price, double wholesale_price )
        {
            int x = 0;

            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();

            String qryStr = "Select * from tbl_item where item_name = '" + item_name + "' and supplier_id = '" + supplier_id + "' and brand = '" + brand + "' and model = '" + model + "' and selling_price = '" + selling_price + "' and wholesale_price = '" + wholesale_price + "'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();

            while (Connect.DrSql.Read())
            {
                try
                {
                    ItemId = Connect.DrSql.GetInt32("item_id");

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