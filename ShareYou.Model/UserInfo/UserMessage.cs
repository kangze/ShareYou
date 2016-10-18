using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class UserMessage
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _userid;}
            set { _userid = value; }
        }

        private int _writerid;

        public int WriterId
        {
            get { return _writerid;}
            set { _writerid = value; }
        }

        private UserLooked _lookded;

        public UserLooked Looked
        {
            get { return _lookded; }
            set { _lookded = value; }
        }

        private DateTime _dateline;

        public DateTime DateLine
        {
            get { return _dateline;}
            set { _dateline = value; }
        }
    }
}
