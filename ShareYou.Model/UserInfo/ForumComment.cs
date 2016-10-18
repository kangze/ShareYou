using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class ForumComment
    {
        private int _commentid;

        public int CommentId
        {
            get { return _commentid;}
            set { _commentid = value; }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private int _postid;

        public int PostId
        {
            get { return _postid;}
            set { _postid = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        private int _parentid;

        public int ParentId
        {
            get { return _parentid;}
            set { _parentid = value; }
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
