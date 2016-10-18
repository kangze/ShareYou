using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.IServices.User
{
    public interface IUserService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
         UserSimp Login(string account, string password);

        /// <summary>
        /// 获取展示其他用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ViewUserInfo GetViewUserInfo(int userid);

        /// <summary>
        /// 转换为View试图所需要的
        /// </summary>
        /// <param name="usersimp"></param>
        /// <returns></returns>
        ViewUser GetViewUser(UserSimp usersimp);
        /// <summary>
        /// 检验用户名时候存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool AccountExist(string account);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="acount"></param>
        /// <param name="username"></param>
        /// <param name="password1"></param>
        /// <param name="password2"></param>
        void RegsiterUser(string acount, string username,string description, string password1, string password2);

        /// <summary>
        /// 发送一个邮件验证代码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="validatecode"></param>
        void SendValidateEmail(string email, string validatecode);

        /// <summary>
        /// 获取用户是否已经完成邮件的验证
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool IsEmailValidated(int userid);

        /// <summary>
        /// 设置用户的邮件验证码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="code"></param>
        void SetUserEmailValidateCode(int userid, string code);

        /// <summary>
        /// 获取用户的邮件地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string GetUserEmail(int userid);

        /// <summary>
        /// 校验用户邮件验证码
        /// </summary>
        /// <param name="emailcode"></param>
        /// <param name="userid"></param>
        /// <param name="sessionemailcode">用户的session中可能存在</param>
        /// <returns></returns>
        bool CheckEamilValidate(string emailcode, int userid,string sessionemailcode);

        /// <summary>
        /// 获取用户的邮件校验码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string GetEmailValidateCode(int userid);

        /// <summary>
        /// 改变用户邮件验证状态
        /// </summary>
        /// <param name="emailValide"></param>
        void ChangeEmailValidateStatus(int userid,EmailValide emailValide);

 
        







  
    }
}
