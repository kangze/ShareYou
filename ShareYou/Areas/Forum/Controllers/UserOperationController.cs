using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareYou.IServices.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Services.Forum;
using ShareYou.Utility;

namespace ShareYou.Areas.Forum.Controllers
{
    public class UserOperationController:Controller
    {
        public IUserOperationService UserOperationService { get; set; }
        public ActionResult GetUserOperationByPage()
        {
            bool state = false;
            int pageindex = Convert.ToInt32(Request["pageindex"]);
            int userid = Convert.ToInt32(Request["userid"]);
            int pagecount;
            IList<UserOperation> list = UserOperationService.GetUserOperationByPage(userid, pageindex, out pagecount);
            ViewUserOperation viewUserOperation = UserOperationService.GetUserOperationContent(list);
            return Content(JsonString.GetString(viewUserOperation));
        }
    }
}