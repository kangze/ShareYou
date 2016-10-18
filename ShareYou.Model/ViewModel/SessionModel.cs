using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.Model.ViewModel
{
    [Serializable]
    public class SessionModel
    {
        private string _emailvalidatecode;

        public string EmailValidateCode
        {
            get { return _emailvalidatecode;}
            set { _emailvalidatecode = value; }
        }

        private UserSimp _user;
        /// <summary>
        /// 当前请求的用户的信息
        /// </summary>
        public UserSimp User
        {
            get { return _user; }
            set { _user = value; }
        }
    }
}
