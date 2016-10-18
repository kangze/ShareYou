using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.IServices.Forum
{
    public interface ILikePostService
    {
        /// <summary>
        /// 用户点赞帖子
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="userid"></param>
        void AddLikePost(int postid, int userid,string username);


    }
}
