using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareYou.Model.ViewModel;

namespace ShareYou
{
    public class BaseController:Controller
    {
        public SessionModel sessionModel { get; set; }
    }
}