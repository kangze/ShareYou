using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareYou.Areas.UserInfo
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            var defaults = new { controller = "user", action = "index" };
            context.MapRoute("defaults_user", "userinfo/{controller}/{action}", defaults);

            //再来一个路由策略 //他的博文
            //userinfo/controller/action/posts          
            context.MapRoute("person_route", "userinfo/{controller}/{action}/{username}");
        }

        public override string AreaName
        {
            get { return "UserInfo"; }
        }
    }
}