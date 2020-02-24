using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
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
    class UserController  : UserModel
    {

        private dbConnection Connect = null;
        private MySqlTransaction Trans = null;

        private FileStream fs;
        private BinaryReader br;

        private int supplier_id;

        private String _imagePath;
        private byte[] _imageData;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public byte[] ImageData
        {
            get { return _imageData; }
            set { _imageData = value; }
        }


        private static UserController _instance; //---------

        public static UserController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserController();
                }
                return _instance;
            }
        }

        public UserController()
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

            if (ImagePath.Equals(""))
            {
                qryStringMember =
                    "Insert into tbl_users (name, contact_no, email, username, password, user_level, date_added) " +
                    " VALUES (@name, @contact_no, @email, @username, @password, @user_level, NOW())";
            }

            else
            {
                qryStringMember =
                     "Insert into tbl_users (name, contact_no, email, username, password, user_level, user_image, date_added) " +
                    " VALUES (@name, @contact_no, @email, @username, @password, @user_level, @user_image, NOW())";
            }

            try
            {

                if (!ImagePath.Equals("") && !ImagePath.Equals("Remove"))
                {
                    fs = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    ImageData = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                }

                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@name", Name.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact_no", ContactNo.Trim());
                Connect.CmSql.Parameters.AddWithValue("@username", Username.Trim());
                Connect.CmSql.Parameters.AddWithValue("@password", Password);
                Connect.CmSql.Parameters.AddWithValue("@email", Email.Trim());
                Connect.CmSql.Parameters.AddWithValue("@user_level", UserLevel);

                if (!ImagePath.Equals("") && !ImagePath.Equals("Remove"))
                {
                    Connect.CmSql.Parameters.AddWithValue("@user_image", ImageData);
                }
                else if (ImagePath.Equals("Remove"))
                {
                    Connect.CmSql.Parameters.AddWithValue("@user_image", null);
                }

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

            String qryStr = "Select * from tbl_users where isDeleted = 1";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvUser.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("user_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("email").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("username").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("password").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("user_level").Trim());

                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvUser.Items.Add(lvi);
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

        public void displayArchived()
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            Connect.CnSql.Open();

            String qryStr = "Select * from tbl_users where isDeleted = 0";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvUserAchived.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("user_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("username").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("email").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("user_level").Trim());

                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvUserAchived.Items.Add(lvi);
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

            if (ImagePath.Equals(""))
            {
                qryStringMember = "UPDATE tbl_users set " +
                                  "name=@name, " +
                                  "email=@email, " +
                                  "contact_no=@contact_no, " +
                                  "username=@username, " +
                                  "password=@password, " +
                                  "user_level=@user_level " +
                                  "WHERE user_id=@user_id";
            }
            else
            {
                qryStringMember = "UPDATE tbl_users set " +
                                  "name=@name, " +
                                  "email=@email, " +
                                  "contact_no=@contact_no, " +
                                  "username=@username, " +
                                  "password=@password, " +
                                  "user_level=@user_level, " +
                                  "user_image=@user_image " +
                                  "WHERE user_id=@user_id";
            }

            try
            {
                if (!ImagePath.Equals("") && !ImagePath.Equals("Remove"))
                {
                    fs = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    ImageData = br.ReadBytes((int)fs.Length);
                    br.Close(); fs.Close();
                }

                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@name", Name.Trim());
                Connect.CmSql.Parameters.AddWithValue("@contact_no", ContactNo);
                Connect.CmSql.Parameters.AddWithValue("@email", Email);
                Connect.CmSql.Parameters.AddWithValue("@username", Username);
                Connect.CmSql.Parameters.AddWithValue("@password", Password);
                Connect.CmSql.Parameters.AddWithValue("@user_level", UserLevel);
                Connect.CmSql.Parameters.AddWithValue("@user_id", UserId);

                if (!ImagePath.Equals("") && !ImagePath.Equals("Remove"))
                {
                    Connect.CmSql.Parameters.AddWithValue("@user_image", ImageData);
                }
                else if (ImagePath.Equals("Remove"))
                {
                    Connect.CmSql.Parameters.AddWithValue("@user_image", null);
                }

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

        public MemoryStream GetImage(Int32 userId)
        {
            MemoryStream ms = null;
            String qryStr = "SELECT user_image FROM tbl_users WHERE user_id = " + userId;

            Connect.CnSql = new MySqlConnection(Connect.ConnStr);

            try
            {
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
                Connect.CaSql = new MySqlDataAdapter(Connect.CmSql);

                DataSet ds = new DataSet();
                Connect.CaSql.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["user_image"]);
                    UserImage.Image = Image.FromStream(ms); ;
                }
            }
            catch (MySqlException e)
            {

                return null;
            }
            catch (Exception ex)
            {

                return null;

            }
            finally
            {
                Connect.CnSql.Close();
                Connect.CnSql.Dispose();
                Connect.CmSql.Dispose();
                Connect.CmSql = null;
                Connect.DrSql = null;

            }
            return ms;
        }

        public Boolean Archive()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_users set isDeleted=@isDeleted WHERE user_id = @user_id";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@isDeleted", IsDeleted);
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

        public void Search(string username, string name)
        {
            int x = 0;
            Connect.CnSql = new MySqlConnection(Connect.ConnStr);
            Connect.CnSql.Open();
            String qryStr = "Select * from tbl_users where name like '%" + name + "%' or username like '%" + username + "%' and isDeleted = 1 ";

            Connect.CmSql = new MySqlCommand(qryStr, Connect.CnSql);
            Connect.DrSql = Connect.CmSql.ExecuteReader();
            LsvUser.Items.Clear();
            while (Connect.DrSql.Read())
            {
                try
                {
                    ListViewItem lvi = new ListViewItem(Connect.DrSql.GetInt64("user_id").ToString());
                    lvi.SubItems.Add(Connect.DrSql.GetString("name").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("contact_no").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("username").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("email").Trim());
                    lvi.SubItems.Add(Connect.DrSql.GetString("user_level").Trim());

                    if (x % 2 == 1) lvi.BackColor = Color.MintCream;
                    else
                    {
                        lvi.BackColor = Color.White;
                    }
                    x++;
                    LsvUser.Items.Add(lvi);
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

        public Boolean Login()
        {
            string queryString;

            queryString = "select * from tbl_users where username=@username and password=@password and user_level=@user_level and isDeleted = 1 ";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(queryString, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@username", Username);
                Connect.CmSql.Parameters.AddWithValue("@password", Password);
                Connect.CmSql.Parameters.AddWithValue("@user_level", UserLevel);

                Connect.DrSql = Connect.CmSql.ExecuteReader();

                if (Connect.DrSql.Read())
                {
                    UserId = Connect.DrSql.GetInt32("user_id");
                    Username = Connect.DrSql.GetString("username");
                    Password = Connect.DrSql.GetString("password");
                    Name = Connect.DrSql.GetString("name");
                    UserLevel = Connect.DrSql.GetString("user_level");
                    return true;
                }
            }
            catch (MySqlException e)
            {

                XtraMessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "General Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        public Boolean verify()
        {
            string queryString;

            queryString = "select * from tbl_users where username=@username and password=@password and isDeleted = 1 ";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(queryString, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@username", Username);
                Connect.CmSql.Parameters.AddWithValue("@password", Password);

                Connect.DrSql = Connect.CmSql.ExecuteReader();

                if (Connect.DrSql.Read())
                {
                    UserId = Connect.DrSql.GetInt32("user_id");
                    return true;
                }
            }
            catch (MySqlException e)
            {

                XtraMessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "General Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public Boolean archive()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_users set isDeleted=@isDeleted WHERE item_id = @item_id";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@isDeleted", IsDeleted);
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

        public Boolean restore()
        {
            string qryStringMember;

            qryStringMember = "UPDATE tbl_users set isDeleted=@isDeleted WHERE item_id = @item_id";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
                Connect.CmSql.Parameters.AddWithValue("@isDeleted", IsDeleted);
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

        public Boolean truncate()
        {
            string qryStringMember;

            qryStringMember = "TRUNCATE TABLE tbl_logs";

            try
            {
                Connect.CnSql = new MySqlConnection(Connect.ConnStr);
                Connect.CnSql.Open();
                Connect.CmSql = new MySqlCommand(qryStringMember, Connect.CnSql);
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
