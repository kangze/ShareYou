using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ShareYou.IDBAccess;
using ShareYou.IDBAccess.Forum;
using ShareYou.IDBAccess.User;
using ShareYou.IDBSession;

namespace ShareYou.DBSession
{
    public class DbSession:IDbSession
    {
        

        public IUserDal UserDal { get; set; }

        //todo:依赖注入

        public IPostDal PostDal { get; set; }
        //todo:依赖注入

        public ICommentDal CommentDal { get; set; }

        public ILikePersonDal LikePersonDal { get; set; }

        public IFriendDal FriendDal { get; set; }


        public IFriendOperationDal FriendOperationDal { get; set; }

        public IFriendRequestDal FriendRequestDal { get; set; }


        public IUserOperationDal UserOperationDal { get; set; }

        public IFriendMessageDal FriendMessageDal { get; set; }

        public ILikePostDal LikePostDal { get; set; }
        public IBoardDal BoardDal { get; set; }
    }
}
