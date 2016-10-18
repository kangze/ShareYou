using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.User
{
    public interface IFriendMessageDal
    {
        /// <summary>
        /// 添加用户消息数据
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        int AddUserMessage(UserMessage um);
    }
}
