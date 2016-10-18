using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;

namespace ShareYou.Model.ViewModel
{
    public class ViewPost
    {
        public ViewPost()
        {
            Post=new ForumPost();
            User=new UserSimp();
        }
        public ForumPost Post { get; set; }

        public UserSimp User { get; set; }
    }
}
