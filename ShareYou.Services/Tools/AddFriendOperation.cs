using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;

namespace ShareYou.Services.Tools
{
    public static class AddFriendOperation
    {
        public static void Add(IDbSession DbSession,int userid,int useroperationid)
        {
            if (Convert.ToInt32(DbSession.FriendDal.GetFriendCount(userid)) != 0)
            {
                DbSession.FriendOperationDal.AddFirendOperation(DbSession.FriendDal.GetFriendsId(userid).ToArray(), userid, useroperationid, DateTime.Now);
            }
        }
    }
}
