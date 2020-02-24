using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2.model
{
    class SalesModel
    {
        private int item_sales_id;
        private string item_id;
        private string sold_quantity;
        private double discount;
        private string tranx_no;
        private int user_id;

        private ListView lsvSales;

        public ListView LsvSales
        {
            get { return lsvSales; }
            set { lsvSales = value; }
        }

        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public string ItemId
        {
            get { return item_id; }
            set { item_id = value; }
        }

        public int ItemSalesId
        {
            get { return item_sales_id; }
            set { item_sales_id = value; }
        }

        public string SoldQuantity
        {
            get { return sold_quantity; }
            set { sold_quantity = value; }
        }

        public string TranxNo
        {
            get { return tranx_no; }
            set { tranx_no = value; }
        }

        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}
