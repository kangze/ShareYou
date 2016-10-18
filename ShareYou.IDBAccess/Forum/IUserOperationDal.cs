using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;

namespace ShareYou.IDBAccess.Forum
{
    public interface IUserOperationDal
    {


        /// <summary>
        /// 分页获取用户操作记录
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<UserOperation> GetUserOperationsByPage(int userid, int pageindex, int pagesize,out int pagecount);
    }
}
