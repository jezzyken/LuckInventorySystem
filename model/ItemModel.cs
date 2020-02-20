using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2.model
{
    class ItemModel
    {
        private string item_id;
        private string category;
        private string item_name;
        private string model;
        private string brand;
        private double selling_price;
        private double wholesale_price;
        private string description;
        private string item_note;
        private int isDeleted;
        private int stocks;
        private int check_item;
        private ListView lsvItem;
        private ListView lsvPurchases;



        private int supplier_id;

        public string ItemId
        {
            get { return item_id; }
            set { item_id = value; }
        }


        public int CheckItem
        {
            get { return check_item; }
            set { check_item = value; }
        }

        public int Stocks
        {
            get { return stocks; }
            set { stocks = value; }
        }

        public int SupplierId
        {
            get { return supplier_id; }
            set { supplier_id = value; }
        }


        public int IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string ItemName
        {
            get { return item_name; }
            set { item_name = value; }
        }

        public string ItemNote
        {
            get { return item_note; }
            set { item_note = value; }
        }

        public ListView LsvItem
        {
            get { return lsvItem; }
            set { lsvItem = value; }
        }

        public ListView LsvPurchases
        {
            get { return lsvPurchases; }
            set { lsvPurchases = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public double SellingPrice
        {
            get { return selling_price; }
            set { selling_price = value; }
        }

        public double WholesalePrice
        {
            get { return wholesale_price; }
            set { wholesale_price = value; }
        }
    }
}
