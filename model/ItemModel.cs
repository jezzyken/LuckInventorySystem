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
        private int item_id;
        private string category;
        private string item_name;
        private string model;
        private string brand;
        private double selling_price;
        private double wholesale_price;
        private string description;
        private string item_note;
        private int isDeleted;
        private ListView lsvItem;


        private int supplier_id;

        public int ItemId
        {
            get { return item_id; }
            set { item_id = value; }
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
