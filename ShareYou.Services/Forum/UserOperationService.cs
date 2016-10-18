using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.IServices.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;
using ShareYou.Utility;

namespace ShareYou.Services.Forum
{
    public class UserOperationService : IUserOperationService
    {
        public IDbSession DbSession { get; set; }


        public IList<UserOperation> GetUserOperationByPage(int userid, int pageindex, out int pagecount)
        {
            int pageSize = PageSize.Value;
            IList<UserOperation> list = DbSession.UserOperationDal.GetUserOperationsByPage(userid, pageindex, pageSize,
                out pagecount);
            return list;
        }

        public ViewUserOperation GetUserOperationContent(IList<UserOperation> lists)
        {
            if (null == lists)
                throw new UserException("传入参数为空");
            //---声明list 来保存来做的数据库操作,然后能够一次操作完成
            List<int> likePersonIds=new List<int>();
            List<int> likePostIds=new List<int>();
            List<int> postIds=new List<int>();
            List<int> commentIds=new List<int>();
            foreach (UserOperation operation in lists)
            {
                switch (operation.Action)
                {
                    case UserAction.LikePerson:
                        likePersonIds.Add(operation.EventId);
                        break;
                    case UserAction.LikePost:
                        likePostIds.Add(operation.EventId);
                        break;
                    case UserAction.Post:
                        //帖子的标题
                        postIds.Add(operation.EventId);
                        break;
                    case UserAction.Comment:
                        commentIds.Add(operation.EventId);
                        break;
                    default:
                        break;
                }
            }
            ViewUserOperation viewUserOperation=new ViewUserOperation();
            viewUserOperation.LikePersons = DbSession.LikePersonDal.GetLikedPersons(likePersonIds);
            viewUserOperation.LikePosts = DbSession.LikePostDal.GetLikePosts(likePersonIds);
            viewUserOperation.Posts = DbSession.PostDal.GetPosts(postIds);
            viewUserOperation.Comments  = DbSession.CommentDal.GetComments(commentIds);
            return viewUserOperation;
        }
    }
}
