using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2.model
{
    class RepairModel
    {
        private int repair_id;
        private string customer_name;
        private string contact_no;
        private string item_name;
        private string issue;
        private string imei;
        private double downpayment;
        private double repair_price;
        private string status;
        private string store_representative;
        private ListView lsvRepair;
        private ListView lsvRepairList;
        private ListView lsvCancelledRepair;

        private int user_id;

        private int count;

        public ListView LsvCancelledRepair
        {
            get { return lsvCancelledRepair; }
            set { lsvCancelledRepair = value; }
        }

        public ListView LsvRepairList
        {
            get { return lsvRepairList; }
            set { lsvRepairList = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public string ContactNo
        {
            get { return contact_no; }
            set { contact_no = value; }
        }

        public string CustomerName
        {
            get { return customer_name; }
            set { customer_name = value; }
        }

        public double Downpayment
        {
            get { return downpayment; }
            set { downpayment = value; }
        }

        public string Imei
        {
            get { return imei; }
            set { imei = value; }
        }

        public string Issue
        {
            get { return issue; }
            set { issue = value; }
        }

        public string ItemName
        {
            get { return item_name; }
            set { item_name = value; }
        }

        public ListView LsvRepair
        {
            get { return lsvRepair; }
            set { lsvRepair = value; }
        }

        public int RepairId
        {
            get { return repair_id; }
            set { repair_id = value; }
        }

        public double RepairPrice
        {
            get { return repair_price; }
            set { repair_price = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string StoreRepresentative
        {
            get { return store_representative; }
            set { store_representative = value; }
        }
    }
}
