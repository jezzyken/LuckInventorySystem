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
    class SupplierController : SupplierModel
    {
        private dbConnection Connect = null;

        private MySqlTransaction Trans = null;

        private static SupplierController _instance; //---------
        public static SupplierController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SupplierController();
                }
                return _instance;
            }
        }

        public SupplierController()
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

            qryStringMember = "Insert into tbl_supplier (supplier_name, representative, contact, email, address, date_added) VALUES (@supplier_name, @representative, @contact, @email, @address, NOW())";

            try
            {

                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open(); Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@supplier_name", SupplierName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@representative", Representative.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact", ContactNo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@email", Email.Trim());
                Connect.CmSql.Parameters.AddWithValue("@address", Address.Trim());
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

            String qryStr = "Select * from tbl_supplier where isDeleted = 1";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql); Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvSupplier.Items.Clear(); while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("supplier_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("supplier_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("email").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("address").Trim());



                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvSupplier.Items.Add(lvi);
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

        public void DisplayArchivedSupplier()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            Connect.CnSql.Open();

            String qryStr = "Select * from tbl_supplier where isDeleted = 0";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql); Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvSupplier.Items.Clear(); while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("supplier_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("company_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("email").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("address").Trim());



                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvSupplier.Items.Add(lvi);
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

            qryStringMember = "UPDATE tbl_supplier set " +
                              "supplier_name=@supplier_name, " +
                              "representative=@representative, " +
                              "contact=@contact, email=@email, " +
                              "address=@address " + "WHERE supplier_id=@supplier_id";
            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@supplier_name", SupplierName.Trim());
                Connect.CmSql.Parameters.AddWithValue("@representative", Representative);
                Connect.CmSql.Parameters.AddWithValue("@contact", ContactNo);
                Connect.CmSql.Parameters.AddWithValue("@email", Email);
                Connect.CmSql.Parameters.AddWithValue("@address", Address);
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

        public Boolean archived()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_supplier set isDeleted=@isDeleted WHERE supplier_id = @supplier_id";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@isDeleted", IsDeleted);
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

  
        public void Search(string supplier_name)
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();
            String qryStr = "Select * from tbl_supplier where supplier_name like '%" + supplier_name + "%' and isDeleted = 1 ";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvSupplier.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("supplier_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("supplier_name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("representative").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("email").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("address").Trim());
                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvSupplier.Items.Add(lvi);
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


        public void getSupplierId(string supplier_name)
        {
            int x = 0;

            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();

            String qryStr = "Select * from tbl_supplier where supplier_name = '" + supplier_name + "'";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();

            while (Connect.DrSql.Read())
            {
                try
                {
                    SupplierId = Connect.DrSql.GetInt32("supplier_id");

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

        public void displaySupplier()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            Connect.CnSql.Open();

            String qryStr = "Select * from tbl_supplier";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();

            while (Connect.DrSql.Read())
            {
                try
                {
                    string supplier_name = Connect.DrSql.GetString("supplier_name");
                    CboSupplierName.Items.Add(supplier_name);

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
