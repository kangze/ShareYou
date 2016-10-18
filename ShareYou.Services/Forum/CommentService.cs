using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Services.User;
using ShareYou.Utility;
using ShareYou.Utility.Path;

namespace ShareYou.Services.Forum
{
    public class CommentService : ICommentService
    {

        public IDbSession DbSession { get; set; }

        public void AddComment(string content, int postid, int userid, string username, int parentid)
        {

            if (string.IsNullOrEmpty(content))
                throw new UserException("评论内容为空！");
            ForumComment comment = new ForumComment();
            comment.Content = content;
            comment.PostId = postid;
            comment.UserId = userid;
            comment.ParentId = parentid;
            comment.DelFlag = DelFlag.Normal;
            comment.DateLine = DateTime.Now;

            //好友圈的动态我们手动来添加
            object obj = DbSession.CommentDal.AddComment(comment);
            //如果事务失败,返回的id是什么样的类型呢？todo:测试
            int useroperationid = Convert.ToInt32(obj);
            if (null == obj || useroperationid < -1)
                throw new UserException("添加评论失败");
            //这一段从新考究
            //记录好友操作

            if (Convert.ToInt32(DbSession.FriendDal.GetFriendCount(userid)) > 0)
            {

                //添加好友记录
                DbSession.FriendOperationDal.AddFirendOperation(DbSession.FriendDal.GetFriendsId(userid).ToArray(), userid, useroperationid, DateTime.Now);

            }
        }

        public IList<ViewComment> GetViewCommentByPage(int postid, int pageindex, out int pagecount)
        {
            int pageSize = PageSize.Value;
            List<ViewComment> list = DbSession.CommentDal.GetViewComments(postid, pageindex, pageSize, out pagecount).ToList();
            list.ForEach(x =>
            {
                x.User.Head = ConvertPath.RelativeToAppLocation(x.User.Head);
            });
            return list;
        }
    }
}
