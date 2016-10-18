using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ShareYou.IState;
using ShareYou.Model.CustomeException;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Utility;

namespace ShareYou.AspNetSession
{
    public class UserState:IUserState 
    {
        public SessionModel GetCurrentUser(string sessionid)
        {
            if(string.IsNullOrEmpty(sessionid))
                throw new ArgumentNullException("用户SessionId为空",innerException:null);
            SessionModel sessionModel = null;
            try
            {
                sessionModel = CacheResolver.GetCache(sessionid) as SessionModel;
            }
            catch (NullReferenceException)
            {
                throw new UserException("用户信息已经失效，请重新登陆");
            }
            return sessionModel;
        }

        public void SetCurrentUser(string sessionid, SessionModel sessionModel)
        {
            if (string.IsNullOrEmpty(sessionid)||null==sessionModel)
                throw new ArgumentNullException("用户SessionId或者用户对象为空", innerException: null);
            try
            {
                CacheResolver.SetCache(sessionid, sessionModel);
            }
            catch (CacheException)
            {
                //写入用户状态失败了
                throw new UserException("用户信息写入失败");
            }
        }
    }
}
