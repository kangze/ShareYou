using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;

namespace ShareYou.Model.ViewModel
{
    public class ViewHomeIndex
    {
        public IList<ForumBoard> Boards { get; set; }

        public IList<ViewPost> Posts { get; set; }

        public IList<ForumPost> PostsRandom { get; set; }
    }
}
