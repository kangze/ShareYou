using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;

namespace ShareYou.Model.ViewModel
{
    public class ViewPostPersonList
    {
        public ViewPostPersonList()
        {
            Post=new ForumPost();
        }
        public ForumPost Post { get; set; }

        public int CountComment { get; set; }
    }
}
