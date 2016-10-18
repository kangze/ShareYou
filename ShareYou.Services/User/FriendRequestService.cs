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
    public class FriendRequestService:IFriendRequestService
    {
        public IDbSession DbSession { get; set; }
        public void PassFriendRequest(int friendrequestid,int userid,string username,int gid,string note)
        {
            //通过好友申请,这个由点迷惑 friendid这个代表着自己
            //if(DbSession.FriendRequestDal.ChangeFriendRequestState(friendid, userid, UserLooked.Looked, PrResult.Pass,
            //    DelFlag.Normal)!=1)

            //    
            //这里就只单纯的通过好友申请

            if (
                DbSession.FriendRequestDal.ChangeFriendRequestState(friendrequestid, UserLooked.Looked, PrResult.Pass,
                    DelFlag.Normal) < 0)
            {
                throw new UserException("变更好友申请状态失败");
            }
            else
            {
                //开始添加好友
                //构造对象
                FriendRequest fr = DbSession.FriendRequestDal.GetFriendRequest(friendrequestid);
                Friend f1=new Friend();
                f1.UserId = fr.UserId;
                f1.FriendId = userid;
                f1.FUserName = username;
                f1.Gid = fr.Gid;
                f1.Note = fr.Note;
                f1.DateLine=DateTime.Now;
                f1.DelFlag= DelFlag.Normal;
                //第二个对象
                Friend f2=new Friend();
                f2.UserId = userid;
                f2.FUserName = fr.UserName;
                f2.Gid = gid;
                f2.Note = note;
                f2.DateLine= DateTime.Now;
                f2.DelFlag=DelFlag.Normal;
                if(!DbSession.FriendDal.AddFriend(f1,f2))
                    throw new UserException("添加好友失败");
            }

        }

        public void NoPassFriendRequest(int friendrequestid)
        {
            //否决用户的好友申请
            DbSession.FriendRequestDal.ChangeFriendRequestState(friendrequestid, UserLooked.Looked, PrResult.NotPass,
                DelFlag.Normal);
        }


        public void AddFriendRequest(int userid, int friendid, string username, int gid, string note)
        {
            //发起一次好友请求,
            if(string.IsNullOrEmpty(username)||string.IsNullOrEmpty(note))
                throw new UserException("用户名或者备注信息为空");
            FriendRequest fr=new FriendRequest();
            fr.UserId = userid;
            fr.UserName = username;
            fr.FriendId = friendid;
            fr.Gid = gid;
            fr.Note = note;
            fr.Looked= UserLooked.UnLooked;
            fr.Result= PrResult.NotPass;
            fr.DelFlag= DelFlag.Normal;
            fr.DateLine= DateTime.Now;
            //开始调用数据层
            if(DbSession.FriendRequestDal.AddFriendRequest(fr)!=1)
                throw new UserException("发送好友申请失败");
        }
    }
}
