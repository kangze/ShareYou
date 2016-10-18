using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShareYou.Utility.Session
{
    public class SessionId
    {
        public static string GetSessionId()
        {
            string sessionid =HttpContext.Current.Request["sessionid"];
            if (string.IsNullOrEmpty(sessionid))
                //这个绝对获取的到
                sessionid = HttpContext.Current.Response.Cookies.Get("sessionid").Value;
            return sessionid;
        }
    }
}
