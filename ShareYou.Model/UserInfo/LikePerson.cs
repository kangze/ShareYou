using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class LikePerson
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

        
        private int _userlikeid;
        /// <summary>
        /// 这个属性表示点赞的发起人
        /// </summary>
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
