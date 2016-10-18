using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareYou.Areas.UserInfo.Controllers;
using ShareYou.DBAccess.User;
using ShareYou.DBSession;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Services.User;

namespace UnitTestShareYou
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //创建环境
            UserService userService=new UserService();
            userService.DbSession=new DbSession();
            userService.DbSession.UserDal=new UserDal();
            userService.RegsiterUser("kangze25@126.com","再来测试一边","King_両月满天","123","123");

        }
    }
}
