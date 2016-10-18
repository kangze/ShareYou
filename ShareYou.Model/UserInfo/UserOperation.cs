using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class UserOperation
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

        private string _username;

        public string UserNmae
        {
            get { return _username;}
            set { _username = value; }
        }

        private UserAction _action;

        public UserAction Action
        {
            get { return _action;}
            set { _action = value; }
        }

        private int _eventid;

        public int EventId
        {
            get { return _eventid;}
            set { _eventid = value; }
        }

        private DelFlag _delflag;

        public DelFlag DelFlag
        {
            get { return _delflag;}
            set { _delflag = value; }
        }

        private DateTime _dateline;

        public DateTime DateLine
        {
            get { return _dateline;}
            set { _dateline = value; }
        }

    }
}
