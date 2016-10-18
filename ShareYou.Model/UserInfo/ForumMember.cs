using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Model.UserInfo
{
    public class ForumMember
    {
        private int _memberid;

        public int MemberId
        {
            get { return _memberid;}
            set { _memberid = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title;}
            set { _title = value; }
        }
    }
}
