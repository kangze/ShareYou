using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;

namespace ShareYou.Services.User
{
    public class FriendService:IFriendService
    {
        public IDbSession DbSession { get; set; }
        public int GetFriendsCount(int userid)
        {
            object obj = DbSession.FriendDal.GetFriendCount(userid);
            if(null==obj)
                throw new UserException("获取用户好友数量失败");
            return Convert.ToInt32(obj);
        }

        public IList<int> GetFriendsId(int userid)
        {
            IList<int> list = DbSession.FriendDal.GetFriendsId(userid);
            if(null==list)
                throw new UserException("用户没有好友");
            return list;
        }

        
    }
}
