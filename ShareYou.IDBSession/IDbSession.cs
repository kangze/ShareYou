using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess;
using ShareYou.IDBAccess.Forum;
using ShareYou.IDBAccess.User;

namespace ShareYou.IDBSession
{
    public interface IDbSession
    {
        
        IUserDal UserDal { get; set; }

        IPostDal PostDal { get; set; }

        ICommentDal CommentDal { get; set; }

        ILikePersonDal LikePersonDal { get; set; }

        IFriendDal FriendDal { get; set; }

        IFriendOperationDal FriendOperationDal { get; set; }

        IFriendRequestDal FriendRequestDal { get; set; }

        IUserOperationDal UserOperationDal { get; set; }

        IFriendMessageDal FriendMessageDal { get; set; }

        ILikePostDal LikePostDal { get; set; }

        IBoardDal BoardDal { get; set; }

        
    }
}
