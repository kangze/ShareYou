using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareYou.Model.CustomeException;
using ShareYou.Model.ViewModel;
using ShareYou.Utility;

namespace ShareYou.App_Config.Filters
{
    public class AccountFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            string sessionid = HttpContext.Current.Request["sessionid"];
            SessionModel sessionModel = null;
            bool state = false;
            try
            {
                sessionModel = CacheResolver.GetCache(sessionid) as SessionModel;
            }
            catch (ArgumentNullException)
            {
                //第一次登陆
                state = true;

            }
            catch (NullReferenceException)
            {
                //缓存数据失效
                state = true;
            }
            if (null == sessionModel.User)
                state = true;
            if(state)
                //跳转登录页面
                filterContext.HttpContext.Response.Redirect("/Home/Index",true);
            BaseController ba=filterContext.Controller as BaseController;
            if(null==ba)
                throw new UserException("使用UserSimp属性必须要求控制器继承自BaseController");
            ba.sessionModel = sessionModel;
            base.OnActionExecuting(filterContext);
        }
    }
}