using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.DBAccess;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Services.Tools;
using ShareYou.Services.User;

namespace ShareYou.Services.Forum
{
    public class LikePostService : ILikePostService
    {
        public IDbSession DbSession { get; set; }

        //依赖注入

        public void AddLikePost(int postid, int userid, string username)
        {
            if(string.IsNullOrEmpty(username))
                throw new UserException("用户名为空");
            LikePost lp=new LikePost();
            lp.PostId = postid;
            lp.UserLikeId = userid;
            lp.DateLine= DateTime.Now;
            lp.Looked= UserLooked.UnLooked;
            object obj = DbSession.LikePostDal.AddLikePost(lp);
            if(null==obj)
                throw new UserException("添加点赞帖子数据失败");
            int useroperationid = Convert.ToInt32(obj);
            //记录好友操作
            AddFriendOperation.Add(DbSession,userid,useroperationid);
            
        }


    }
}
