using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.Forum
{
    public interface ILikePostDal
    {
        /// <summary>
        /// 向数据库添加点赞帖子数据
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        object AddLikePost(LikePost lp);

        /// <summary>
        /// 获取用户喜欢帖子列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<ForumPost> GetLikePosts(IList<int> ids);
        
    }
}
