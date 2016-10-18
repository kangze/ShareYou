using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;

namespace ShareYou.IServices.User
{
    public interface IFriendRequestService
    {
        /// <summary>
        /// 通过好友申请
        /// </summary>
        /// <param name="friendrequestid"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="gid"></param>
        /// <param name="note"></param>
        void PassFriendRequest(int friendrequestid, int userid, string username, int gid, string note);

        /// <summary>
        /// 否决用户的好友申请
        /// </summary>
        /// <param name="friendid"></param>
        /// <param name="userid"></param>
        void NoPassFriendRequest(int friendrequestid);

        /// <summary>
        /// 发起一次好友请求
        /// </summary>
        void AddFriendRequest(int userid, int friendid, string username, int gid, string note);

    }
}
