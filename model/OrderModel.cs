using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2.model
{
    class OrderModel
    {
        private int order_id;
        private string order_item;
        private string additional_info;
        private string customer_name;
        private string contact_no;
        private double downpayment;
        private double order_price;
        private string status;
        private string store_representative;
        private int quantity;
        private ListView lsvOrder;
        private int user_id;

        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public string AdditionalInfo
        {
            get { return additional_info; }
            set { additional_info = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
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

        public ListView LsvOrder
        {
            get { return lsvOrder; }
            set { lsvOrder = value; }
        }

        public int OrderId
        {
            get { return order_id; }
            set { order_id = value; }
        }

        public string OrderItem
        {
            get { return order_item; }
            set { order_item = value; }
        }

        public double OrderPrice
        {
            get { return order_price; }
            set { order_price = value; }
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
