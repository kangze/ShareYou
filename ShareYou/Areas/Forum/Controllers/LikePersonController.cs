using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.IState;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Utility;

namespace ShareYou.Areas.Forum.Controllers
{
    public class LikePersonController : BaseController
    {
        //// GET: Forum/LikePerson
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Dependency]
        public ILikePersonService LikePersonService { get; set; }

        public ActionResult AddLike()
        {
            //!--设计失误，太多的业务逻辑在里面
            bool state = false;
            string message = string.Empty;
            int likepersonid = Convert.ToInt32(Request["likepersonid"]);
            try
            {
                LikePersonService.AddUserLikePerson(sessionModel.User.UserId,sessionModel.User.UserName,likepersonid);    
                state = true;
                message = "点赞成功";
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new {state=state,message=message}));
        }
    }
}