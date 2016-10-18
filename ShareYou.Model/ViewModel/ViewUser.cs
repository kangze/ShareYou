using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Model.ViewModel
{
    [Serializable]
    public class ViewUser
    {
        private int _userid;

        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _head;

        public string Head
        {
            get { return _head;}
            set { _head = value; }
        }
    }
}
