using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.Forum
{
    public class ForumPost
    {
        private int _postid;

        public int PostId
        {
            get { return _postid;}
            set { _postid = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _content;

        public string Content
        {
            get { return _content;}
            set { _content = value; }
        }

        private int _userid;

        public int UserId
        {
            get { return _userid;}
            set { _userid = value; }
        }

        private int _like;

        public int Like
        {
            get { return _like; }
            set { _like = value; }
        }

        private int _unlike;

        public int UnLike
        {
            get { return _unlike; }
            set { _unlike = value; }
        }

        private int boardid;

        public int BoardId
        {
            get { return boardid;}
            set { boardid = value; }
        }

        private int _limitedmemberid;

        public int LimitedMemberId
        {
            get { return _limitedmemberid; }
            set { _limitedmemberid = value; }
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
