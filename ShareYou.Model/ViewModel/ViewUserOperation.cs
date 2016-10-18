using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;

namespace ShareYou.Model.ViewModel
{
    public class ViewUserOperation
    {
        public ViewUserOperation()
        {
            LikePersons=new List<UserSimp>();
            LikePosts=new List<ForumPost>();
            Posts=new List<ForumPost>();
            Comments=new List<ForumComment>();
        }
        public IList<UserSimp> LikePersons { get;set; }
        public IList<ForumPost> LikePosts { get; set; }
        public IList<ForumPost> Posts{get; set; }
        public IList<ForumComment> Comments { get; set; }
    }
}
