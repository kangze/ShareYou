using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ShareYou.Model.Enum;

namespace ShareYou.Model.UserInfo
{
    [Serializable]
    public class UserSimp
    {
        private int _userid;

        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _account;

        public string Account
        {
            get{return _account;}
            set { _account = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password;}
            set { _password = value; }
        }

        private int _memeberid;

        public int MemberId
        {
            get { return _memeberid;}
            set { _memeberid = value; }
        }

        private string _head;

        public string Head
        {
            get { return _head;}
            set { _head = value; }
        }

        private DelFlag _delflag;

        /// <summary>
        /// 0代表正常,1代表删除状态，2代表冻结状态
        /// </summary>
        public DelFlag DelFlag
        {
            get { return _delflag;}
            set { _delflag = value; }
        }

    }
}
