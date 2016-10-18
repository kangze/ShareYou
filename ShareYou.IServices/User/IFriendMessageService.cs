using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.IServices.User
{
    public interface IFriendMessageService
    {
        void SendMessageToUser(int userid, int writerid, string content);
    }
}
