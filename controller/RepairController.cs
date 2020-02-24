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
    class RepairController : RepairModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static RepairController _instance;

        public static RepairController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RepairController();
                }
                return _instance;
            }
        }

        public RepairController()
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

            qryStringMember = "Insert into tbl_repair (customer_name, contact_no, item_name, issue, imei, downpayment, repair_price, status, store_representative, date_added) VALUES (@customer_name, @contact_no, @item_name, @issue, @imei, @downpayment, @repair_price, @status, @store_representative, NOW())";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@customer_name", CustomerName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact_no", ContactNo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@item_name", ItemName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@issue", Issue.Trim());
                Connect.CmSql.Parameters.AddWithValue("@imei", Imei.Trim());
                Connect.CmSql.Parameters.AddWithValue("@downpayment", Downpayment);
                Connect.CmSql.Parameters.AddWithValue("@repair_price", RepairPrice);
                Connect.CmSql.Parameters.AddWithValue("@status", Status.Trim());
                Connect.CmSql.Parameters.AddWithValue("@store_representative", StoreRepresentative);


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

            String qryStr = "Select * from tbl_repair where status != 'Cancelled'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvRepair.Items.Clear();

            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("repair_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("issue").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("imei").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("repair_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_added").Trim());

                    Status = Connect.DrSql.GetString("status");

                    if (Status == "Pending")
                    {
                        lvi.BackColor = Color.Red;
                    }

                    if (x % 2 == 1)
                    {
                        lvi.BackColor = Color.MintCream;
                    }
                    else
                    {
                        lvi.BackColor = Color.White;
                    }

                    x++;

                    LsvRepair.Items.Add(lvi);
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

            qryStringMember = "UPDATE tbl_repair set " +
                              "customer_name=@customer_name, " +
                              "contact_no=@contact_no, " +
                              "item_name=@item_name, issue=@issue, " +
                              "imei=@imei, downpayment=@downpayment, " +
                              "repair_price=@repair_price, status=@status, " +
                              "store_representative=@store_representative " + "WHERE repair_id=@repair_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@customer_name", CustomerName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact_no", ContactNo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@item_name", ItemName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@issue", Issue.Trim());
                Connect.CmSql.Parameters.AddWithValue("@imei", Imei);
                Connect.CmSql.Parameters.AddWithValue("@downpayment", Downpayment);
                Connect.CmSql.Parameters.AddWithValue("@repair_price", RepairPrice);
                Connect.CmSql.Parameters.AddWithValue("@status", Status.Trim());
                Connect.CmSql.Parameters.AddWithValue("@store_representative", StoreRepresentative);
                Connect.CmSql.Parameters.AddWithValue("@repair_id", RepairId);

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

            qryStringMember = "UPDATE tbl_repair set " +
                              "status=@status " +
                              "WHERE repair_id=@repair_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@status", Status.Trim());
                Connect.CmSql.Parameters.AddWithValue("@repair_id", RepairId);

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

        public Boolean cancelRepair()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_repair set " +
                              "status=@status, " +
                              "user_id=@user_id " +
                              "WHERE repair_id=@repair_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@status", Status.Trim());
                Connect.CmSql.Parameters.AddWithValue("@user_id", UserId);
                Connect.CmSql.Parameters.AddWithValue("@repair_id", RepairId);

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

            String qryStr = "Select count(*) as NoOfItem from tbl_repair where status = 'Pending' ";

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
            String qryStr = "select * from tbl_repair where status = '" + filtered + "'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvRepairList.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("repair_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("issue").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("imei").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("repair_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_added").Trim());

                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvRepairList.Items.Add(lvi);
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
            String qryStr = "select * from view_cancelled_repair where status = 'Cancelled'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvCancelledRepair.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetString("repair_id"));
                    lvi.SubItems.Add(Connect.DrSql.GetString("customer_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("item_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("issue").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("status").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("imei").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("downpayment").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("repair_price").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("store_representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("date_added").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("name").Trim());


                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvCancelledRepair.Items.Add(lvi);
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

        public Boolean removed()
        {
            string qryStringMember;

            qryStringMember = "DELETE from tbl_repair WHERE repair_id=@repair_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@repair_id", RepairId);

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
