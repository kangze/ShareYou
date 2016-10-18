using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ShareYou.App_Config.Filters;
using ShareYou.IServices.User;
using ShareYou.IState;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Utility;
using ShareYou.Utility.Mail;
using ShareYou.Utility.Session;
using ShareYou.Utility.ValideCode;
//b*[^:b#/]+.$ 代码行数
//2016-4-17   3865
//2016-4-17   4134
//2016-4-17   4500
//2016-4-17   4899
//2016-4-18   5116
//2016-4-18   5340
//--------代码重构
//2016-4-21   5180
//2016-4-23   5866
//2016-4-28   6030
namespace ShareYou.Areas.UserInfo.Controllers
{
    public class UserController : BaseController
    {

        
        public IUserService UserService { get; set; }
        
        public IUserState UserState { get; set; }


        [AccountFilter]
        public ActionResult Index()
        {
            ViewBag.logined = true;
            ViewBag.User = UserService.GetViewUser(sessionModel.User);
            return View();
        }
        
        public ActionResult Person()
        {
            //构建登录状态
            if (sessionModel != null && sessionModel.User != null)
            {
                ViewBag.User = UserService.GetViewUser(sessionModel.User);
                ViewBag.logined = true;
            }
            else
            {
                ViewBag.logined = false;
            }
            int userid = Convert.ToInt32(Request["userid"]);
            ViewUserInfo viewUserInfo = null;
            try
            {
                viewUserInfo = UserService.GetViewUserInfo(userid);
            }
            catch (UserException e)
            {
                //没有找到该用户进行跳转
                Response.Redirect("/userinfo/user/index",true);
            }

            return View(viewUserInfo);
        }
        //用户登录
        [HttpPost]
        public ActionResult Login()
        {

            string message = string.Empty;
            bool state = false;
            if (!ValidateUserCode(Request["validatecode"]))
            {
                message = "验证码错误";
                CacheResolver.DeleteCache(ValidateCodeId.GetValidateCodeId());
                return Content(JsonString.GetString(new { state = state, message = message }));
            }
            string account = Request["account"];
            string password = Request["password"];
            SessionModel currentSessionModel = new SessionModel();
            try
            {
                currentSessionModel.User = UserService.Login(account, password);
                state = true;
                message = "登录成功";
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            catch (ArgumentNullException e)
            {
                message = e.Message;
            }//删除验证码
            finally
            {
                CacheResolver.DeleteCache(ValidateCodeId.GetValidateCodeId());
            }
            if (state == false)
                //开始返回
                return Content(JsonString.GetString(new { state = state, message = message }));
            //在请求的开始已经处理了sessionid的写入了
            UserState.SetCurrentUser(SessionId.GetSessionId(), currentSessionModel);
            //不进行捕获异常，由mvc来进行处理
            bool rem = Request["remember"] == "1";
            //其中的密码是md5密码
            RememeberMe(account, currentSessionModel.User.Password, rem);
            //登录成功要做的就是要消除这个验证码
            CacheResolver.DeleteCache(ValidateCodeId.GetValidateCodeId());
            return Content(JsonString.GetString(new { state = state, message = message, user = UserService.GetViewUser(currentSessionModel.User) }));
        }

 
        //校验返回是否存在此账号
        public ActionResult CheckAccount()
        {
            string account = Request["account"];
            bool state = UserService.AccountExist(account);
            return Content(JsonString.GetString((new { state = state })));
        }

        //用户注册
        [HttpPost]
        public ActionResult Register()
        {
            bool state = false;
            string message = string.Empty;
            if (!ValidateUserCode(Request["validatecode"]))
            {
                message = "验证码错误";
                return Content(JsonString.GetString(new {state=state,message=message}));
            }
            string account = Request["account"];
            string username = Request["username"];
            string password1 = Request["password1"];
            string password2 = Request["password2"];
            string description = Request["description"];
            try
            {
                UserService.RegsiterUser(account, username,description, password1, password2);
                //注册成功了，当前session必须记住改用户,最终这个session是保存在memecache中的
                UserState.SetCurrentUser(SessionId.GetSessionId(),new SessionModel(){User = UserService.Login(account,password1)});
                message = "注册成功";
                state = true;
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new { state = state, message = message }));
        }

        
        // 对于用户邮箱的验证
        [AccountFilter]
        public ActionResult ValidateEmail()
        {
            bool state = UserService.IsEmailValidated(sessionModel.User.UserId);
            if(state)
                return Content("邮箱已经被验证了");
            //开始邮箱验证工作，首先产生验证码，然后存入SessionModel中去
            ValidateCode codeobj=new ValidateCode();
            string emailcode = codeobj.CreateValidateCode(6);
            //更改缓存对象,同时我们数据库也要进行一次更改,用户完成了验证之后，记得要进行删除
            sessionModel.EmailValidateCode = emailcode;
            //缓存服务器保存
            CacheResolver.SetCache(SessionId.GetSessionId(),sessionModel);
            UserService.SetUserEmailValidateCode(sessionModel.User.UserId,emailcode);
            //设置完数据库之后，我们就要进行一次邮件的发送了
            UserService.SendValidateEmail(UserService.GetUserEmail(sessionModel.User.UserId),emailcode);
            return Content("验证邮件已经发送,注意接收");
        }

    

      
        
        //用户开始校验自己的邮件验证码
        [AccountFilter]
        public ActionResult SetValidateEmail()
        {
            string emailcode = Request["emailcode"];
            bool state = true;
            string message = string.Empty;
            try
            {
                state = UserService.CheckEamilValidate(emailcode, sessionModel.User.UserId,
                    sessionModel.EmailValidateCode);
                if(state)
                    UserService.ChangeEmailValidateStatus(sessionModel.User.UserId,EmailValide.Valide);
                sessionModel.EmailValidateCode = string.Empty;
                CacheResolver.SetCache(SessionId.GetSessionId(),sessionModel);
                message = state ? "邮箱验证成功" : "邮箱验证失败";
            }
            catch (EmailValideCodeException e)
            {
                state = false;
                message = e.Message;
            }
            return Content(JsonString.GetString(new {state=state,message=message}));
        }

        /*
         * ************控制器私有方法************
         */
        private bool ValidateUserCode(string code)
        {
            //先获取服务器端的code
            if (string.IsNullOrEmpty(code))
                return false;
            string servercode = String.Empty;
            try
            {
                servercode = CacheResolver.GetCache(ValidateCodeId.GetValidateCodeId()) as string;
            }
            catch (NullReferenceException)
            {
                //缓存的数据失效了
                return false;
            }
            if (null == servercode)
                return false;
            if (code != servercode)
                return false;
            return true;
        }

        private void RememeberMe(string account, string md5Password, bool state)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(md5Password))
                throw new ArgumentNullException("用户名或者密码为空", innerException: null);
            HttpCookie cookie1 = new HttpCookie("ck1", account);
            HttpCookie cookie2 = new HttpCookie("ck2", Md5String.GetMd5String(md5Password));
            if (state)
            {
                cookie1.Expires = DateTime.Now.AddDays(7);
                cookie2.Expires = DateTime.Now.AddDays(7);
            }
            else
            {
                cookie1.Expires = DateTime.Now.AddDays(-7);
                cookie2.Expires = DateTime.Now.AddDays(-7);
            }
            Response.Cookies.Add(cookie1);
            Response.Cookies.Add(cookie2);
        }

        
    }
}