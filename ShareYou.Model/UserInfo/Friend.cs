using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class Friend
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _userid;}
            set { _userid = value; }
        }

        private int _friendid;

        public int FriendId
        {
            get { return _friendid;}
            set { _friendid = value; }
        }

        private string _fusername;

        public string FUserName
        {
            get { return _fusername; }
            set { _fusername = value; }
        }

        private int _gid;

        public int Gid
        {
            get { return _gid;}
            set { _gid = value; }
        }

        private string _note;

        public string Note
        {
            get { return _note;}
            set { _note = value; }
        }

        private DateTime _dateline;

        public DateTime DateLine
        {
            get { return _dateline;}
            set { _dateline = value; }
        }

        private DelFlag _delflag;

        public DelFlag DelFlag
        {
            get { return _delflag; }
            set { _delflag = value; }
        }

    }
}
