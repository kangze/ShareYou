using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Model.UserInfo
{
    public class FriendGroup
    {
        private int _gid;

        public int Gid
        {
            get { return _gid;}
            set { _gid = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _gid; }
            set { _gid = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title;}
            set { _title = value; }
        }

        private DateTime _dateline;

        public DateTime DateLine
        {
            get { return _dateline;}
            set { _dateline = value; }
        }


    }
}
