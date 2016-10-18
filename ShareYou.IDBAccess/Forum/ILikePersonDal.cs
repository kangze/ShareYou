using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.Forum
{
    public interface ILikePersonDal
    {
        /// <summary>
        /// 添加点赞用户数据
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        object AddLikePerson(LikePerson lp);

        /// <summary>
        /// 获取被点赞的用户的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<UserSimp> GetLikedPersons(IList<int> ids);
    }
}
