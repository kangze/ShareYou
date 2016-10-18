using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;

namespace ShareYou.Model.ViewModel
{
    public class ViewPostIndex
    {
        public ViewPostIndex()
        {
            post=new ForumPost();
            userinfo=new ViewUserInfo();
            comments=new List<ViewComment>();
        }
        public int commentPagecount { get; set; }

        public ForumPost post { get; set; }

        public ViewUserInfo userinfo { get; set; }

        public IList<ViewComment> comments { get; set; }
    }
}
