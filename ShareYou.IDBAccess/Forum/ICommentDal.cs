using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.IDBAccess.Forum
{
    public interface ICommentDal
    {
        /// <summary>
        /// 添加一评论,然后返回的是操作记录ID
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        object AddComment(ForumComment comment);

        /// <summary>
        /// 获取评论的内容
        /// </summary>
        /// <param name="commentid"></param>
        /// <returns></returns>
        IList<ForumComment> GetComments(IList<int> ids);

        IList<ViewComment> GetViewComments(int postid,int pageindex,int pagesize,out int pagecount);
    }
}
