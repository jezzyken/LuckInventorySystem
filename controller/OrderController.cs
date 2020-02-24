using System;
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
    class OrderController : OrderModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static OrderController _instance;

        public static OrderController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderController();
                }
                return _instance;
            }
        }

        public OrderController()
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

            qryStringMember = "Insert into tbl_order (ordered_item, additional_info, customer_name, contact_no, downpayment, order_price, status, store_representative, date_ordered) VALUES (@ordered_item, @additional_info, @customer_name, @contact_no, @downpayment, @order_price, @status, @store_representative, NOW())";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@ordered_item", OrderItem.Trim());
                Connect.CmSql.Parameters.AddWithValue("@additional_info", AdditionalInfo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@customer_name", CustomerName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact_no", ContactNo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@downpayment",Downpayment);
                Connect.CmSql.Parameters.AddWithValue("@order_price", OrderPrice);
                Connect.CmSql.Parameters.AddWithValue("@status", Status);
                Connect.CmSql.Parameters.AddWithValue("@store_representative", StoreRepresentative.Trim());

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

            String qryStr = "Select * from tbl_order where status != 'Cancelled' order by date_ordered DESC";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvOrder.Items.Clear();

            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("order_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("ordered_item").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("additional_info").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("order_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_ordered").Trim());


                    if (x % 2 == 1)
                    {
                        lvi.BackColor = Color.MintCream;
                    }
                    else
                    {
                        lvi.BackColor = Color.White;
                    }

                    x++;
                    LsvOrder.Items.Add(lvi);
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

        public void displayOrder()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            Connect.CnSql.Open();

            String qryStr = "Select * from view_orders";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvOrderList.Items.Clear();

            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("order_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("ordered_item").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("additional_info").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("order_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_ordered").Trim());
                   // lvi.SubItems.Add(Connect.DrSql.GetString("name").Trim());


                    if (x % 2 == 1)
                    {
                        lvi.BackColor = Color.MintCream;
                    }
                    else
                    {
                        lvi.BackColor = Color.White;
                    }

                    x++;
                    LsvOrderList.Items.Add(lvi);
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

            qryStringMember = "UPDATE tbl_order set " +
                              "ordered_item=@ordered_item, " +
                              "additional_info=@additional_info, " +
                              "customer_name=@customer_name, contact_no=@contact_no, " +
                              "downpayment=@downpayment, order_price=@order_price " +
                              "WHERE order_id=@order_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@ordered_item", OrderItem.Trim());
                Connect.CmSql.Parameters.AddWithValue("@additional_info", AdditionalInfo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@customer_name", CustomerName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact_no", ContactNo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@downpayment", Downpayment);
                Connect.CmSql.Parameters.AddWithValue("@order_price", OrderPrice);
                Connect.CmSql.Parameters.AddWithValue("@order_id", OrderId);

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

        public Boolean updateStatus()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_order set " +
                              "status=@status " +
                              "WHERE order_id=@order_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@status", Status.Trim());
                Connect.CmSql.Parameters.AddWithValue("@order_id", OrderId);

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

        public Boolean cancelOrder()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_order set " +
                              "status=@status, " +
                              "user_id=@user_id " +
                              "WHERE order_id=@order_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@status", Status.Trim());
                Connect.CmSql.Parameters.AddWithValue("@user_id", UserId);
                Connect.CmSql.Parameters.AddWithValue("@order_id", OrderId);

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

            String qryStr = "Select count(*) as NoOfItem from tbl_order where status = 'Ordered'";

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

        public void search(string filtered)
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();
            String qryStr = "select * from view_orders where status = '"+ filtered + "'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvOrderList.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("order_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("ordered_item").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("additional_info").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("order_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_ordered").Trim());

                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvOrderList.Items.Add(lvi);
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

        public void searchCancelled()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();
            String qryStr = "select * from view_cancelled_order";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvCancelled.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("order_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("ordered_item").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("additional_info").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("order_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_ordered").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("name").Trim());



                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvCancelled.Items.Add(lvi);
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

        public Boolean removedOrder()
        {
            string qryStringMember;

            qryStringMember = "DELETE from tbl_order WHERE order_id=@order_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@order_id", OrderId);

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
