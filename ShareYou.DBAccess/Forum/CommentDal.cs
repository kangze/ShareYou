using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.DBAccess.DB;
using ShareYou.IDBAccess.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.DBAccess.Forum
{
    public class CommentDal : ICommentDal
    {
        public object AddComment(ForumComment comment)
        {
            //返回的是用户操作号id
            if (null == comment)
                throw new UserException("评论对象为空");
            string sql = "usp_addcomment";
            SqlParameter[] spm =
                SqlHelper.GetSqpParameters(
                    new string[] { "@content", "postid", "@userid", "@parentid", "@delflag", "@dateline" },
                    new object[]
                    {
                        comment.Content, comment.PostId, comment.UserId, comment.ParentId, comment.DelFlag,
                        comment.DateLine
                    },
                    new SqlDbType[] { SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.DateTime });
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public IList<ForumComment> GetComments(IList<int> ids)
        {
            List<ForumComment> list = new List<ForumComment>();
            if (null == ids || ids.Count == 0)
                return list; //如果没有id的话，直接返回一个空的集合
            string sql = ids.Aggregate("select commentid,postid,content from forum_comment where commentid in(", (current, id) => current + (id + ","));
            //去掉逗号，加上括号
            sql = sql.Substring(0, sql.Length - 1) + ")";
            DataSet dataSet = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.Text, dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
               ForumComment model=new ForumComment();
                model.CommentId = Convert.ToInt32(row[0]);
                model.Content = row[2].ToString();
                model.PostId = Convert.ToInt32(row[1]);
                list.Add(model);
            }
            return list;
        }

        public IList<ViewComment> GetViewComments(int postid, int pageindex, int pagesize, out int pagecount)
        {
            string sql = "usp_getcommentinfo";
            SqlParameter[] spm =
                SqlHelper.GetSqpParameters(new string[] { "@postid", "@pageindex", "@pagesize", "@pagecount" },
                    new object[] { postid, pageindex, pagesize, DBNull.Value },
                    new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, });
            spm[3].Direction= ParameterDirection.Output;
            DataSet set = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.StoredProcedure, set, spm);
            pagecount = Convert.ToInt32(spm[3].Value);
            List<ViewComment> list = new List<ViewComment>();
            foreach (DataRow comment in set.Tables[0].Rows)
            {
                ViewComment viewComment = new ViewComment();
                viewComment.User.UserId = Convert.ToInt32(comment[0]);
                viewComment.User.UserName = comment[1].ToString();
                viewComment.User.Head = comment[2].ToString();
                viewComment.Comment.CommentId = Convert.ToInt32(comment[3]);
                viewComment.Comment.Content = comment[4].ToString();
                viewComment.Comment.ParentId = Convert.ToInt32(comment[5]);
                viewComment.Comment.DateLine = Convert.ToDateTime(comment[6]);
                list.Add(viewComment);
            }
            return list;
        }
    }
}
