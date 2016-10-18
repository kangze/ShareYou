using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;
using ShareYou.Model.Forum;
using ShareYou.Model.ViewModel;

namespace ShareYou.IServices.Forum
{
    public interface IPostService
    {

        /// <summary>
        /// 用户发表帖子,返回发表的帖子的编号
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="boardid"></param>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="limitedid"></param>
        /// <param name="postid"></param>
        void WriterPost(int userid, int boardid, string username, string title, string content, int limitedid);

        /// <summary>
        /// 获取标题的编号
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        int GetPostId(int userid, string title);

        /// <summary>
        /// 根据id获取帖子的详细信息
        /// </summary>
        /// <param name="postid"></param>
        /// <returns></returns>
        Model.Forum.ForumPost GetForumPost(int postid);
        
        /// <summary>
        /// 获取分页帖子信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="boardid"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<ForumPost> GetPosts(int pageindex,int boardid,out int pagecount);

        /// <summary>
        /// 获取分页的列表帖子展示的详细信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="boardid"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<ViewPost> GetViewPost(int pageindex, int boardid, out int pagecount);

        /// <summary>
        /// 获取最新的帖子
        /// </summary>
        /// <returns></returns>
        IList<ViewPost> GetViewPostTop();

        IList<ForumPost> GetViewPostRandom();

            /// <summary>
        /// 获取个人博文列表分页的数据
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<ViewPostPersonList> GetViewPostPersonLists(int userid, int pageindex, out int pagecount);


    }
}
