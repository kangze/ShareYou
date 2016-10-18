using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;

namespace ShareYou.Services.User
{
    public class FriendMessageService:IFriendMessageService
    {
        public IDbSession DbSession { get; set; }
        public void SendMessageToUser(int userid, int writerid, string content)
        {
           
            UserMessage um = new UserMessage();
            um.UserId = userid;
            um.WriterId = writerid;
            um.Content = content;
            um.Looked = UserLooked.UnLooked;
            um.DateLine = DateTime.Now;
            //调用数据层  
            if (DbSession.FriendMessageDal.AddUserMessage(um) != 1)
                throw new UserException("发送消息失败");
        }
    }
}
