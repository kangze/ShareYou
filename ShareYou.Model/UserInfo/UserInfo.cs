using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    public class UserInfo
    {
        private int _userid;

        public int UserId
        {
            get { return _userid;}
            set { _userid = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description;}
            set { _description = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email;}
            set { _email = value; }
        }

        private string _emailvalidatecode;

        public string EmailValidateCode
        {
            get { return _emailvalidatecode;}
            set { _emailvalidatecode = value; }
        }

        private EmailValide _isvalide;

        public EmailValide IsValide
        {
            get { return _isvalide;}
            set { _isvalide = value; }
        }

        private string _number;

        public string Number
        {
            get { return _number;}
            set { _number = value; }
        }
                                
        private int _liked;

        public int Liked
        {
            get { return _liked;}
            set { _liked = value; }
        }

        private int _follower;

        public int Follower
        {
            get { return _follower; }
            set { _follower = value; }
        }

        private int _follow;

        public int Follow
        {
            get { return _follow; }
            set { _follow = value; }
        }

        private DelFlag _delflag;

        public DelFlag DelFlag
        {
            get { return _delflag;}
            set { _delflag = value; }
        }
    }
}
