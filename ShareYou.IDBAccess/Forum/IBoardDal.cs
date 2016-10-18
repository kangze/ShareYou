using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;

namespace ShareYou.IDBAccess.Forum
{
    public interface IBoardDal
    {
        /// <summary>
        /// 获取id所对应的版块信息
        /// </summary>
        /// <param name="boardid"></param>
        /// <returns></returns>
        ForumBoard GetBoard(int boardid);

        /// <summary>
        /// 返回所有的版块信息的内容
        /// </summary>
        /// <returns></returns>
        IList<ForumBoard> GetBoards();
    }
}
