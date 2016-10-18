using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.IDBAccess.Forum
{
    public interface IPostDal
    {
        /// <summary>
        /// 返回的就是一个帖子的编号
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        object AddPost(ForumPost post);


        object GetPostId(int userid, string title);

        /// <summary>
        /// 获取发表的帖子的内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<ForumPost> GetPosts(IList<int> ids);

       

        /// <summary>
        /// 分页获取帖子信息,返回的还有就是总的页数
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="boardid"></param>
        /// <returns></returns>
        IList<ForumPost> GetPosts(int pageindex, int pagesize, int boardid,out int pagecount);

        /// <summary>
        /// 根据id获取帖子信息
        /// </summary>
        /// <param name="postid"></param>
        /// <returns></returns>
        ForumPost GetPost(int postid);

        /// <summary>
        /// 获取试图展示帖子列表所需要的数据
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="boardid"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<ViewPost> GetViewPosts(int pageindex, int pagesize, int boardid, out int pagecount);

        /// <summary>
        /// 获取首页最热的帖子相关的信息
        /// </summary>
        /// <returns></returns>
        IList<ViewPost> GetViewPostsTop();

        IList<ForumPost> GetPostsRandom();

            /// <summary>
        /// 获取个人页面帖子信息展示信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<ViewPostPersonList> GetViewPostPersonList(int userid,int pageindex, int pagesize, out int pagecount);


    }
}
