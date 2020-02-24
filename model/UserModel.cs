using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LuckInventorySystem_v2.model
{
    class UserModel
    {
        private int user_id;
        private string name;
        private string contact_no;
        private string email;
        private string username;
        private string password;
        private string user_level;
        private ListView lsvUser;
        private ListView lsvUserAchived;
        private PictureEdit user_image;
        private int isDeleted;

        public int IsDeleted
        {   
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string UserLevel
        {
            get { return user_level; }
            set { user_level = value; }
        }

        public PictureEdit UserImage
        {
            get { return user_image; }
            set { user_image = value; }
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

        public ListView LsvUser
        {
            get { return lsvUser; }
            set { lsvUser = value; }
        }

        public ListView LsvUserAchived
        {
            get { return lsvUserAchived; }
            set { lsvUserAchived = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
    }
}
