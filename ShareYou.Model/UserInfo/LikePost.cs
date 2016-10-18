using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class LikePost
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _postid;

        public int PostId
        {
            get { return _postid; }
            set { _postid = value; }
        }

        private int _userlikeid;

        public int UserLikeId
        {
            get { return _userlikeid;}
            set { _userlikeid = value; }
        }

        private UserLooked _looked;

        public UserLooked Looked
        {
            get { return _looked;}
            set { _looked = value; }
        }

        private DateTime _dateline;

        public DateTime DateLine
        {
            get { return _dateline;}
            set { _dateline = value; }
        }
    }
}
