using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Services.Tools;
using ShareYou.Services.User;
using ShareYou.Utility;
using ShareYou.Utility.Path;

namespace ShareYou.Services.Forum
{
    public class PostService:IPostService
    {

        public IDbSession DbSession { get; set; }


        public void WriterPost(int userid, int boardid, string username, string title, string content, int limitedid)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
                throw new UserException("标题或者内容为空");
            //构造兑现
            ForumPost post=new ForumPost();
            post.Title = title;
            post.Content = content;
            post.UserId = userid;
            post.Like = 0;
            post.UnLike = 0;
            post.BoardId = boardid;
            post.LimitedMemberId = limitedid;
            post.DateLine= DateTime.Now;
            post.DelFlag= DelFlag.Normal;
            object obj = DbSession.PostDal.AddPost(post);
            if(obj==null||Convert.ToInt32(obj)==-1)
                throw new UserException("增加用户帖子数据失败");
            //记录发帖操作
            int operationid = Convert.ToInt32(obj);
            //记录好友操作记录
            AddFriendOperation.Add(DbSession,userid,operationid);

        }

        public int GetPostId(int userid, string title)
        {
            if(string.IsNullOrEmpty(title))
                throw new UserException("标题为空");
            object obj = DbSession.PostDal.GetPostId(userid, title);
            if(null==obj)
                throw new UserException("未找到该帖子");
            return Convert.ToInt32(obj);
        }

        public ForumPost GetForumPost(int postid)
        {
            ForumPost post = DbSession.PostDal.GetPost(postid);
            if(null==post)
                throw new UserException("没有该帖子记录");
            return post;
        }

        //这个被废弃了，没有人用它
        public IList<ForumPost> GetPosts(int pageindex,int boardid,out int pagecount)
        {
            int pageSize = PageSize.Value;
            return DbSession.PostDal.GetPosts(pageindex, pageSize, boardid, out pagecount);
        }

        public IList<ViewPost> GetViewPost(int pageindex, int boardid, out int pagecount)
        {
            int pageSize = PageSize.Value;
            List<ViewPost> list = DbSession.PostDal.GetViewPosts(pageindex, pageSize, boardid, out pagecount).ToList();
            list.ForEach((post) =>
            {
                post.User.Head = ConvertPath.RelativeToAppLocation(post.User.Head);
            });
            return list;
        }

        public IList<ViewPost> GetViewPostTop()
        {
            //对图片路径做一个处理
            IList<ViewPost> viewPosts = DbSession.PostDal.GetViewPostsTop();
            viewPosts.ForEach(post => post.User.Head = ConvertPath.RelativeToAppLocation(post.User.Head));
            return viewPosts;
        }
        /// <summary>
        /// 随机推荐的内容
        /// </summary>
        /// <returns></returns>
        public IList<ForumPost> GetViewPostRandom()
        {
            return DbSession.PostDal.GetPostsRandom();
        }

        public IList<ViewPostPersonList> GetViewPostPersonLists(int userid, int pageindex, out int pagecount)
        {
            int pagesize = PageSize.Value;
            IList<ViewPostPersonList> models = DbSession.PostDal.GetViewPostPersonList(userid, pageindex, pagesize, out pagecount);
            return models;
        }
    }
}
