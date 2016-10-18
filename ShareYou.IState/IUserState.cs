using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.IState
{ 
    public interface IUserState
    {
        SessionModel GetCurrentUser(string sessionid);

        void SetCurrentUser(string sessionid, SessionModel sessionModel);

    }
}
