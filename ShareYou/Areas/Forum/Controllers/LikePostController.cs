using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ShareYou.App_Config.Filters;
using ShareYou.IServices.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Utility;

namespace ShareYou.Areas.Forum.Controllers
{
    public class LikePostController : BaseController
    {
        // GET: Forum/LikePost
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Dependency]
        public ILikePostService LikePostService { get; set; }
        [AccountFilter]
        //[HttpPost]
        public ActionResult AddLikePost()
        {
            bool state = false;
            string message = string.Empty;
            int postid = Convert.ToInt32(Request["postid"]);
            try
            {
                LikePostService.AddLikePost(postid, sessionModel.User.UserId, sessionModel.User.UserName);
                state = true;
                message = "点赞成功";
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new {state = state, message = message}));
        }
    }
}