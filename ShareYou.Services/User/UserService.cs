using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ShareYou.IDBSession;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Utility;
using ShareYou.Utility.Mail;
using ShareYou.Utility.Path;

namespace ShareYou.Services.User
{
    public class UserService : IUserService
    {
        public IDbSession DbSession { get; set; }
        
        //如果需要另外的Service来解决，必须new 对象出来，不然存在一个循环引用的问题,但是还是有好处的,就是避免了创建过多的对象出来

        public UserSimp Login(string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                throw new UserException("密码或者账号为空,请重新输入");
            if(!AccountExist(account))
                throw new UserException("账户不存在,大兄弟!");
            DelFlag status =(DelFlag)Convert.ToInt32(DbSession.UserDal.GetUserStatus(account));//可能存在异常,我们继续往上抛出
            if (status == DelFlag.Deleted) //账户被删除
                throw new UserException("此用户已被删除,请申诉或者重新申请账户");
            if (status == DelFlag.Freeze)
                throw new UserException("当前账户被冻结");
            //可以获取用户信息
            UserSimp model = null;
            if (Md5String.GetMd5String(password) == DbSession.UserDal.GetPassword(account).ToString())
            {
                //表示信息校验成功
                //出现的异常继续往上抛出
                model = DbSession.UserDal.GetUserSimp(account);
            }
            else
            {
                //密码错误,出现异常了
                throw new UserException("用户密码错误");
            }
            return model;
        }

        public ViewUserInfo GetViewUserInfo(int userid)
        {
            ViewUserInfo model = DbSession.UserDal.GetViewUserInfo(userid);
            if(null==model)
                throw new UserException("未找到该用户相关信息");
            //转换路径
            model.User.Head = ConvertPath.RelativeToAppLocation(model.User.Head);
            return model;
        }

        public ViewUser GetViewUser(UserSimp usersimp)
        {
            if (null == usersimp)
                throw new ArgumentNullException("usersimp", "usersimp对象为null");
            ViewUser viewUser = new ViewUser();
            viewUser.UserId = usersimp.UserId;
            viewUser.UserName = usersimp.UserName;
            //这里要进行一次路径的转换
            viewUser.Head = ConvertPath.RelativeToAppLocation(usersimp.Head);
            return viewUser;
        }

        public string GetUserName(int userid)
        {
            object obj = DbSession.UserDal.GetUserName(userid);
            if (null == obj)
                throw new UserException("未有该用户");
            return obj.ToString();
        }
        public bool AccountExist(string account)
        {
            if (string.IsNullOrEmpty(account))
                return false;
            return DbSession.UserDal.AccountExist(account);
        }

        public void RegsiterUser(string account, string username,string description, string password1, string password2)
        {
            if (password1 != password2)
                throw new UserException("2次密码输入不一致");
            if(password1.Length<6)
                throw new UserException("密码最小长度为6位");
            if (AccountExist(account))
                throw new UserException("用户名已经存在");
            UserSimp userSimp = new UserSimp()
            {
                Account = account,
                UserName = username,
                DelFlag = DelFlag.Normal,
                //头像，我们给一个默认值
                Head = @"E:\ShareYou\ShareYou\Content\Image\Head\2c8d94e6-3d6b-4b61-8150-c3d3790b9ab8.png",
                //第一级的会员
                MemberId = 0,
                //进行md5加密操作
                Password = Md5String.GetMd5String(password1),
            };
            UserInfo userinfo = new UserInfo()
            {
                Description = description,
                Email = "",
                EmailValidateCode = "",
                DelFlag = DelFlag.Normal,
                IsValide = EmailValide.NotValide,
                Follower = 0,
                Follow = 0,
                Liked = 0,
                Number = "",
            };

            //异常继续往上抛出
            DbSession.UserDal.AddUser(userSimp, userinfo);

        }

        public void SendValidateEmail(string email, string validatecode)
        {
            SendMail sm = new SendMail();
            sm.Send(email, validatecode);
        }

        public bool IsEmailValidated(int userid)
        {
            object obj = DbSession.UserDal.GetEmailValidate(userid);
            if (null == obj)
                throw new UserException("不存在此用户");
            EmailValide emailValide = (EmailValide)Convert.ToInt32(obj);
            return emailValide == EmailValide.Valide;
        }

        public void SetUserEmailValidateCode(int userid, string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new Exception("用户邮件验证码为空");
            if (DbSession.UserDal.UpdateUserEmailValidateCode(userid, code) != 1)
                throw new UserException("设置用户邮件验证码失败");
        }

        public string GetUserEmail(int userid)
        {
            object obj = DbSession.UserDal.GetUserEmail(userid);
            if (null == obj)
                throw new UserException("未有当前用户");
            return obj.ToString();
        }

        public bool CheckEamilValidate(string emailcode, int userid, string sessionemailcode)
        {
            if (string.IsNullOrEmpty(emailcode))
                throw new EmailValideCodeException("邮件验证码为空,请再次输入或者重新验证");
            if (string.IsNullOrEmpty(sessionemailcode))
                return GetEmailValidateCode(userid) == emailcode;//可能抛出异常，我们继续往上抛出，UI层来处理,
            return emailcode == sessionemailcode;
        }

        public string GetEmailValidateCode(int userid)
        {
            object obj = DbSession.UserDal.GetEmailValidateCode(userid);
            if (null == obj)
                throw new EmailValideCodeException("还未发送邮件验证码,请申请发送");
            return obj.ToString();
        }

        public void ChangeEmailValidateStatus(int userid, EmailValide emailValide)
        {
            //1.更改用户邮件验证状态,
            //2.删除邮件验证码,
            if (DbSession.UserDal.UpdateEmailValidateStatus(userid, (int)emailValide) != 1)
                throw new EmailValideCodeException("变更验证状态失败");
            if (DbSession.UserDal.UpdateUserEmailValidateCode(userid, "") != 1)
                throw new EmailValideCodeException("变更邮件验证码失败");
        }

      
    }
}
