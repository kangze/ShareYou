using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.UserInfo;

namespace ShareYou.Services.Forum
{
    public class FriendOperationService:IFriendOperationService
    {
        public IDbSession DbSession { get; set; }

        
    }
}
