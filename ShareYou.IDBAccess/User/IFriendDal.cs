using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.User
{
    public interface IFriendDal
    {
        /// <summary>
        /// 获取好友的数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        object GetFriendCount(int userid);

        /// <summary>
        /// 获取用户所有好友的id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        IList<int> GetFriendsId(int userid);

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="friend1"></param>
        /// <param name="friend2"></param>
        /// <returns></returns>
        bool AddFriend(Friend friend1, Friend friend2);

    }
}
