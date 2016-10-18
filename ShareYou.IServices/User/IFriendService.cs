using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.IServices.User
{
    public interface IFriendService
    {
        /// <summary>
        /// 获取好友的数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int GetFriendsCount(int userid);
        /// <summary>
        /// 获取所有好友的id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        IList<int> GetFriendsId(int userid);

        

       
    }
}
