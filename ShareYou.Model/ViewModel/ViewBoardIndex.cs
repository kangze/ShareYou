using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;

namespace ShareYou.Model.ViewModel
{
    public class ViewBoardIndex
    {
        public IList<ViewPost> ViewPosts { get; set; }

        public ForumBoard Board { get; set; }

        public int PageCount { get; set; }
    }
}
