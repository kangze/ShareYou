using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.Forum
{
    public interface IFriendOperationDal
    {
        /// <summary>
        /// 添加用户好友圈操作记录
        /// </summary>
        /// <param name="friendids"></param>
        /// <param name="userid"></param>
        /// <param name="useroperationid"></param>
        /// <param name="dateline"></param>
        void AddFirendOperation(int[] friendids,int userid,int useroperationid,DateTime dateline);
    }
}
