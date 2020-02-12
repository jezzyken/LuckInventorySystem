using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckInventorySystem_v2.model
{
    class ItemStockModel
    {
        private int item_id;
        private int remaining_stocks;

        public int ItemId
        {
            get { return item_id; }
            set { item_id = value; }
        }

        public int RemainingStocks
        {
            get { return remaining_stocks; }
            set { remaining_stocks = value; }
        }
    }
}
