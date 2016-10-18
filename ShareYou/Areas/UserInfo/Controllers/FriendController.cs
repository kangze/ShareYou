using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ShareYou.App_Config.Filters;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Utility;

namespace ShareYou.Areas.UserInfo.Controllers
{
    public class FriendController : BaseController
    {
        // GET: UserInfo/Friend
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Dependency]
        public IFriendService FriendService { get; set; }
        [Dependency]
        public IFriendRequestService FriendRequestService { get; set; }
        [Dependency]
        public IFriendMessageService FriendMessageService { get; set; }
        //用户接受好友请求，进行添加好友操作
        [HttpPost]
        [AccountFilter]
        public ActionResult PassRequest()
        {

            int friendrequestid = Convert.ToInt32(Request["frid"]);
            int gid = Convert.ToInt32(Request["gid"]);
            string message = string.Empty;
            bool state = false;
            try
            {
                FriendRequestService.PassFriendRequest(friendrequestid, sessionModel.User.UserId,
                    sessionModel.User.UserName, gid, Request["note"]);
                state = true;
                message = "添加好友成功";
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new { state = state, message = message }));
        }

        //发起一次用户好友请求
        public ActionResult SendFriendRequest()
        {
            bool state = false;
            string message = string.Empty;
            try
            {
                FriendRequestService.AddFriendRequest(sessionModel.User.UserId, Convert.ToInt32(Request["friendid"]),
                    sessionModel.User.UserName, Convert.ToInt32(Request["gid"]), Request["note"]);
            }
            catch (UserException e)
            {
                message = e.Message;
                state = true;
            }
            return Content(JsonString.GetString(new { state = state, message = message }));
        }

        public ActionResult SendFriendMessage()
        {
            bool state = false;
            string message = string.Empty;
            try
            {
                FriendMessageService.SendMessageToUser(Convert.ToInt32(Request["userid"]), sessionModel.User.UserId,
                    Request["content"]);
                state = true;
                message = "发送消息成功";
            }
            catch (UserException e)
            {
                message = e.Message;
            }
            return Content(JsonString.GetString(new { state = state, message = message }));
        }
    }
}