using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.Model.ViewModel
{
    public class ViewComment
    {
        public ViewComment()
        {
            User=new UserSimp();
            Comment=new ForumComment();
        }
        public UserSimp User { get; set; }

        public ForumComment Comment { get; set; }
    }
}
