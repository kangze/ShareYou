using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.Model.ViewModel
{
    public class ViewUserInfo
    {
        public ViewUserInfo()
        {
            UserInfo=new UserInfo.UserInfo();
            User=new UserSimp();
        }
        public UserInfo.UserInfo UserInfo { get; set; }

        public UserSimp User { get; set; }
    }
}
