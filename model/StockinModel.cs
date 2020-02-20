using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckInventorySystem_v2.model
{
    class StockinModel
    {
        private int stockin_id;
        private string item_id;
        private int quantity_added;
        private int isDeleted;
        private int totalStocks;

        public int TotalStocks
        {
            get { return totalStocks; }
            set { totalStocks = value; }
        }

        public int IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string ItemId
        {
            get { return item_id; }
            set { item_id = value; }
        }

        public int QuantityAdded
        {
            get { return quantity_added; }
            set { quantity_added = value; }
        }

        public int StockinId
        {
            get { return stockin_id; }
            set { stockin_id = value; }
        }
    }
}
