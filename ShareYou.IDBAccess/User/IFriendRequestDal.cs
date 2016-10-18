using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.User
{
    public interface IFriendRequestDal
    {
        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <param name="frquest"></param>
        /// <returns></returns>
        int AddFriendRequest(FriendRequest frquest);

        /// <summary>
        /// 改变好友申请的状态
        /// </summary>
        /// <param name="friendrequestid"></param>
        /// <param name="lookied"></param>
        /// <param name="result"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        int ChangeFriendRequestState(int friendrequestid,UserLooked lookied,PrResult result,DelFlag delFlag);

        FriendRequest GetFriendRequest(int id);
    }
}
