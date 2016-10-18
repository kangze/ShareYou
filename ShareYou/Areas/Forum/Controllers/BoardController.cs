using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.IState;
using ShareYou.Model.CustomeException;
using ShareYou.Model.ViewModel;
using ShareYou.Utility.Session;

namespace ShareYou.Areas.Forum.Controllers
{
    public class BoardController : BaseController
    {
        public IBoardService BoardService { get; set; }
        public IPostService PostService { get; set; }
        public IUserService UserService { get; set; }
        public IUserState UserState { get; set; }
        public ActionResult Index()
        {
            bool state = false;
            string message = string.Empty;
            int boardid = Convert.ToInt32(Request["boardid"]);
            int pagecount;
            //获取版块的信息
            ViewBoardIndex boardIndex = new ViewBoardIndex();
            //获取版块信息
            boardIndex.Board = BoardService.GetBoard(boardid);


            boardIndex.ViewPosts = PostService.GetViewPost(1, boardid, out pagecount);
            boardIndex.PageCount = pagecount;

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
            return View(boardIndex);
        }
    }
}