using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.IDBAccess.User
{
    public interface IUserDal
    {

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ViewUserInfo GetViewUserInfo(int userid);
        /// <summary>
        /// 获取用户的密码
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        object GetPassword(string account);

        /// <summary>
        /// 获取用户的状态
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        object GetUserStatus(string account);

        /// <summary>
        /// 获取用户的信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        UserSimp GetUserSimp(string account);

        /// <summary>
        /// 获取指定id的用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        object GetUserName(int userid);
        /// <summary>
        /// 判断是否存在对应的账户对象
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool AccountExist(string account);

        /// <summary>
        /// 添加一个用户信息,会导致2张表同时添加
        /// </summary>
        /// <param name="usersimp"></param>
        /// <param name="userinfo"></param>
        void AddUser(UserSimp usersimp,UserInfo userinfo);

        /// <summary>
        /// 获取用户邮件信息的验证状态
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        object GetEmailValidate(int userid);

        
        /// <summary>
        /// 更新用户的邮件验证码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="emailvalidatecode"></param>
        /// <returns></returns>
        int UpdateUserEmailValidateCode(int userid, string emailvalidatecode);
        

        
        /// <summary>
        /// 获取用户的邮件地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        object GetUserEmail(int userid);

        /// <summary>
        /// 获取用户的邮件校验码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        object GetEmailValidateCode(int userid);

        /// <summary>
        /// 更新用户邮件验证状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        int UpdateEmailValidateStatus(int userid,int status);

        
       

       
       

       
       

    }
}
