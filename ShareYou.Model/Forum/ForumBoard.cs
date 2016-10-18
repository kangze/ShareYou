using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.Forum
{
    public class ForumBoard
    {
        private int _boardid;

        public int BoardId
        {
            get { return _boardid;}
            set { _boardid = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title;}
            set { _title = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description;}
            set { _description = value; }
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
