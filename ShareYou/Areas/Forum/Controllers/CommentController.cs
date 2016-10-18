using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ShareYou.App_Config.Filters;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.IState;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Services.User;
using ShareYou.Utility;

namespace ShareYou.Areas.Forum.Controllers
{
    public class CommentController : BaseController
    {
        [Dependency]
        public IUserState UserState { get; set; }
        [Dependency]
        public ICommentService CommentService { get; set; }
        [Dependency]
        public IUserOperationService UserOperationService { get; set; }
        [Dependency]
        public IUserService UserService { get; set; }
        // GET: Forum/Comment
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AccountFilter]
        public ActionResult WriterComment()
        {
            
            int postid = Convert.ToInt32(Request["postid"]);
            int parentid = Convert.ToInt32(Request["parentid"]);
            string content = Request["content"];
            bool state = false;
            string message = string.Empty;
            try
            {
                CommentService.AddComment(content, postid, sessionModel.User.UserId, sessionModel.User.UserName,
                    parentid);
                message = "添加评论成功";
                state = true;
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new {state=state,message=message}));
        }
        

    }
}