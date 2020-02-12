using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LuckInventorySystem_v2.db
{
    class dbConnection
    {
        private MySqlDataAdapter _caSql;
        private MySqlCommand _cmSql;
        private MySqlConnection _cnSql;
        private string _connStr;
        private MySqlDataReader _drSql;
        private string _db;
        private string _password;
        private string _server;
        private string _username;

        public dbConnection()
        {
            this.Server = "localhost";
            this.Username = "root";
            this.Password = "@Lucky7!!";
            this.Db = "luckyinventory";
            this.ConnStr = "SERVER=" + this.Server + ";DATABASE=" + this.Db + ";UID=" + this.Username + ";PASSWORD=" + this.Password;
        }


        public MySqlDataAdapter CaSql
        {
            get { return _caSql; }
            set { _caSql = value; }
        }

        public MySqlCommand CmSql
        {
            get { return _cmSql; }
            set { _cmSql = value; }
        }

        public MySqlConnection CnSql
        {
            get { return _cnSql; }
            set { _cnSql = value; }
        }

        public string ConnStr
        {
            get { return _connStr; }
            set { _connStr = value; }
        }

        public string Db
        {
            get { return _db; }
            set { _db = value; }
        }

        public MySqlDataReader DrSql
        {
            get { return _drSql; }
            set { _drSql = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}
