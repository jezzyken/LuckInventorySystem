using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2.model
{
    class SupplierModel
    {
        private int supplier_id;
        private string supplier_name;
        private string representative;
        private string contact_no;
        private string email;
        private string address;
        private int isDeleted;
        private ListView lsvSupplier;
        private ListView lsvSupplierArchived;
        private ComboBox cbo_supplier_name;
        private int count;
        private int user_id;


        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }


        public int Count
        {
            get { return count; }
            set { count = value; }
        }


        public ComboBox CboSupplierName
        {
            get { return cbo_supplier_name; }
            set { cbo_supplier_name = value; }
        }


        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string ContactNo
        {
            get { return contact_no; }
            set { contact_no = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string Representative
        {
            get { return representative; }
            set { representative = value; }
        }

        public ListView LsvSupplier
        {
            get { return lsvSupplier; }
            set { lsvSupplier = value; }
        }

        public ListView LsvSupplierArchived
        {
            get { return lsvSupplierArchived; }
            set { lsvSupplierArchived = value; }
        }

        public int SupplierId
        {
            get { return supplier_id; }
            set { supplier_id = value; }
        }

        public string SupplierName
        {
            get { return supplier_name; }
            set { supplier_name = value; }
        }
    }
}
