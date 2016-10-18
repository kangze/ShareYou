using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShareYou.App_Config.Unity
{
    public class CustomerControllerActivator:IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            //由依赖解析器来构建请求的Controller对象
           return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}