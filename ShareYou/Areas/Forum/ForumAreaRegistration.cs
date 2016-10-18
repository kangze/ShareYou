using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareYou.Areas.Forum
{
    public class ForumAreaRegistration:AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            var defaults = new {controller = "Post", action = "Index"};
            context.MapRoute("defaults_Forum", "Forum/{controller}/{action}", defaults);
        }

        public override string AreaName
        {
            get { return "Forum"; }
        }
    }
}