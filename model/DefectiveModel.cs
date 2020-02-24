using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckInventorySystem_v2.model
{
    class DefectiveModel
    {
        private int defected_item_id;
        private string item_id;
        private int no_of_defective_item;
        private string item_issue;
        private int user_id;
        private ListView lsvDefective;

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int DefectedItemId
        {
            get { return defected_item_id; }
            set { defected_item_id = value; }
        }

        public string ItemId
        {
            get { return item_id; }
            set { item_id = value; }
        }

        public string ItemIssue
        {
            get { return item_issue; }
            set { item_issue = value; }
        }

        public ListView LsvDefective
        {
            get { return lsvDefective; }
            set { lsvDefective = value; }
        }

        public int NoOfDefectiveItem
        {
            get { return no_of_defective_item; }
            set { no_of_defective_item = value; }
        }

        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}
