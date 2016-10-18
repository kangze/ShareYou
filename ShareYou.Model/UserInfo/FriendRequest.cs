using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class FriendRequest
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _friendid;

        public int FriendId
        {
            get { return _friendid;}
            set { _friendid = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _userid;}
            set { _userid = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username;}
            set { _username = value; }
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

        private UserLooked _looked;

        public UserLooked Looked
        {
            get { return _looked;}
            set { _looked = value; }
        }

        private PrResult _result;

        public PrResult Result
        {
            get { return _result;}
            set { _result = value; }
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
            get { return _delflag;}
            set { _delflag = value; }
        }
    }
}
