using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Services.User;

namespace ShareYou.Services.Forum
{
    public class LikePersonService : ILikePersonService
    {
        public IDbSession DbSession { get; set; }

        public void AddUserLikePerson(int userid, string username, int userlikedid)
        {
            //构建对象
            if (string.IsNullOrEmpty(username))
                throw new UserException("用户名为空");
            LikePerson lp = new LikePerson();
            lp.UserId = userid;
            lp.UserLikeId = userlikedid;
            lp.DateLine = DateTime.Now;
            lp.Looked = UserLooked.UnLooked;
            object obj = DbSession.LikePersonDal.AddLikePerson(lp);
            //todo:同样的问题
            if (null == obj || Convert.ToInt32(obj) == -1)
                throw new UserException("点赞失败");
            int useroperationid = Convert.ToInt32(obj);

            //好友操作记录
            if (Convert.ToInt32(DbSession.FriendDal.GetFriendCount(userid)) != 0)
            {
                //添加好友记录
                DbSession.FriendOperationDal.AddFirendOperation(DbSession.FriendDal.GetFriendsId(userid).ToArray(), userid, useroperationid, DateTime.Now);
            }
        }
    }
}
