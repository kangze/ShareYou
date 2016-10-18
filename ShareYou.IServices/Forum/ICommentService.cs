using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.ViewModel;

namespace ShareYou.IServices.Forum
{
    public interface ICommentService
    {
        /// <summary>
        /// 添加一条评论
        /// </summary>
        /// <param name="content"></param>
        /// <param name="postid"></param>
        /// <param name="userid"></param>
        /// <param name="parentid"></param>
        void AddComment(string content, int postid, int userid,string username, int parentid);

        IList<ViewComment> GetViewCommentByPage(int postid, int pageindex, out int pagecount);
    }
}
