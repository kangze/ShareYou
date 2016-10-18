using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.IState;
using ShareYou.Model.ViewModel;
using ShareYou.Services.User;
using ShareYou.Utility;
using ShareYou.Utility.Session;
using ShareYou.Utility.ValideCode;

namespace ShareYou.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public IUserState UserState { get; set; }
        public IUserService UserService { get; set; }

        public IPostService PostService { get; set; }
        public IBoardService BoardService { get; set; }
        public ActionResult Index()
        {
            //判断当前是否为一个登录的状态
            sessionModel = UserState.GetCurrentUser(SessionId.GetSessionId());
            if (sessionModel != null && sessionModel.User != null)
            {
                ViewBag.User = UserService.GetViewUser(sessionModel.User);
                ViewBag.logined = true;
            }
            else
            {
                ViewBag.logined = false;
            }
            ViewHomeIndex homeIndex=new ViewHomeIndex();
            homeIndex.Boards = BoardService.GetBoards();
            //现在要做的就是获取前5个帖子
            homeIndex.Posts = PostService.GetViewPostTop();
            homeIndex.PostsRandom = PostService.GetViewPostRandom();
            return View(homeIndex);
        }

        public ActionResult GetValidateCode()
        {
            //有可能会用户刷新验证码，所以我们要消除原来的验证码
            CacheResolver.DeleteCache(ValidateCodeId.GetValidateCodeId());
            ValidateCode code=new ValidateCode();
            string codevalue = code.CreateValidateCode(6);//6位长度的值
            //开始存入缓存服务器
            CacheResolver.SetCache(ValidateCodeId.GetValidateCodeId(), codevalue);
            return File(code.CreateValidateGraphic(codevalue), @"image/jpeg");
        }
    }
}