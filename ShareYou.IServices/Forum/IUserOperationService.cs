using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.IServices.Forum
{
    public interface IUserOperationService
    {
        

        /// <summary>
        /// 获取用户分页的动态信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        IList<UserOperation> GetUserOperationByPage(int userid, int pageindex, out int pagecount);

        /// <summary>
        /// 获取用户操作的简短表述
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        ViewUserOperation GetUserOperationContent(IList<UserOperation> lists);
    }
}
