using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ShareYou.App_Config.Unity;
using ShareYou.Model.ViewModel;
using ShareYou.Utility;
using ShareYou.Utility.Mail;

namespace ShareYou
{
    public class Global : System.Web.HttpApplication
    {
        
        private IUnityContainer GetUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration("containerOne");
            container.RegisterType<IControllerActivator, CustomerControllerActivator>();
            return container;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //1.区域路由注册
            AreaRegistration.RegisterAllAreas();

            //2.非区域路由注册,例如现在的Home/Index位置
            RouteTable.Routes.MapRoute("defaults", "{controller}/{action}", new {controller = "Home", action = "Index"});

            //3.全局依赖注入配置
            IUnityContainer container = GetUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //4.邮件发送程序开始执行
            SendMail m=new SendMail();
            SendMail.BeignWork();

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            string key = Request["sessionid"];
            SessionModel model = null;
            bool state = false;
            try
            {
                model = CacheResolver.GetCache(key) as SessionModel;
                if (null != model)
                    state = true;
            }
            catch (ArgumentNullException)
            {
                //没有缓存键，第一次登陆这个网站
                key = Guid.NewGuid().ToString("N");
                model=new SessionModel();
            }
            catch (NullReferenceException)
            {
                model=new SessionModel();
            }
            if (!state)
            {
                //开始重新写入
                CacheResolver.SetCache(key,model);
                //开始吧key写入到cookie中去
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("sessionid",key));
            }

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}