using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.IServices.Forum
{
    public interface ILikePersonService
    {
        /// <summary>
        ///添加用户点赞操作记录，返回记录id
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="userlikedid"></param>
        void AddUserLikePerson(int userid, string username, int userlikedid);
    }
}
