using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Practices.Unity;
using ShareYou.App_Config.Filters;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.IState;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.Forum;
using ShareYou.Model.ViewModel;
using ShareYou.Services.User;
using ShareYou.Utility;
using ShareYou.Utility.Session;

namespace ShareYou.Areas.Forum.Controllers
{
    public class PostController:BaseController
    {

        
        public IPostService PostService { get; set; }

        public IUserService UserService { get; set; }

        public ICommentService CommentServcie { get; set; }

        public IUserState UserState { get; set; }
        /// <summary>
        /// 帖子详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //判断当前否非为一个登录状态
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
            int postid = Convert.ToInt32(Request["postid"]);
            int commentPageCount;
            ViewPostIndex postIndex=new ViewPostIndex();
            
            try
            {
                postIndex.post = PostService.GetForumPost(postid);
                postIndex.userinfo = UserService.GetViewUserInfo(postIndex.post.UserId); //这里就获取到了用户的相关的信息
                postIndex.comments = CommentServcie.GetViewCommentByPage(postid, 1, out commentPageCount);
                postIndex.commentPagecount = commentPageCount;

            }
            catch (UserException)
            {
                //发生错误强制跳转到首页
              Response.Redirect("/home/index",true);
            }
            //获取发帖人相关的信息

            return View(postIndex);

        }

        //发表帖子
        [HttpPost]
        [AccountFilter]
        public ActionResult WriterPost()
        {
            bool state = false;
            string message = string.Empty;
            try
            {
                PostService.WriterPost(sessionModel.User.UserId,Convert.ToInt32(Request["boardid"]), sessionModel.User.UserName,
                    Request["title"], Request["content"], Convert.ToInt32(Request["limited"]));
                state = true;
                message = "发帖成功";
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new {state=state,message=message}));
        }

        public ActionResult GetViewPost()
        {

            int pageindex = Convert.ToInt32(Request["pageindex"]);
            int boardid = Convert.ToInt32(Request["boardid"]);
            int pagecount;
            List<ViewPost> ViewPosts = PostService.GetViewPost(pageindex, boardid, out pagecount).ToList();
            return Content(JsonString.GetString(new{PageCount=pagecount,Posts=ViewPosts}));
        }

        public ActionResult GetPersonPosts()
        {
            bool state = true;
            int userid = Convert.ToInt32(Request["userid"]);
            int pageindex = Convert.ToInt32(Request["pageindex"]);
            int pagecount;
            List<ViewPostPersonList> list = null;
            //获取所需要的数据
            try
            {
                list = PostService.GetViewPostPersonLists(userid, pageindex, out pagecount).ToList();
            }
            catch (UserException)
            {
                state = false;
                pagecount = 0;
            }
            return Content(JsonString.GetString(new{state=state,posts=list,pagecount=pagecount}));
        }

    }
}