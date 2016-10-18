using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Model.UserInfo
{
    public class FriendOperation
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

        private int _operationid;

        public int OperationId
        {
            get { return _operationid;}
            set { _operationid = value; }
        }

        private DateTime _dateline;

        public DateTime DateLine
        {
            get { return _dateline;}
            set { _dateline = value; }
        }
    }
}
