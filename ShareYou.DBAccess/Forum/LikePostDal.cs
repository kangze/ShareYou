using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;

namespace ShareYou.DBAccess.Forum
{
    public class LikePostDal : ILikePostDal
    {
        public object AddLikePost(LikePost lp)
        {
            if (null == lp)
                throw new UserException("点赞帖子数据为空");
            string sql = "usp_addlikepost";
            SqlParameter[] spm =
                SqlHelper.GetSqpParameters(new string[] { "@postid", "@userlikeid", "@looked", "@dateline", "@useroperationid" },
                    new object[] { lp.PostId, lp.UserLikeId, lp.Looked, lp.DateLine,DBNull.Value },
                    new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.DateTime,SqlDbType.Int,  });
            spm[4].Direction= ParameterDirection.Output;
            SqlHelper.ExecuteNonquery(sql, CommandType.StoredProcedure, spm);
            return spm[4].Value;
        }

        public IList<ForumPost> GetLikePosts(IList<int> ids)
        {
            List<ForumPost> list = new List<ForumPost>();
            if (null == ids || ids.Count == 0)
                return list; //如果没有id的话，直接返回一个空的集合
            string sql = ids.Aggregate("select post.postid,post.title from user_likepost likepost,forum_post post where likepost.id=post.postid and likepost.id in(", (current, id) => current + (id + ","));
            //去掉逗号，加上括号
            sql = sql.Substring(0, sql.Length - 1) + ")";
            DataSet dataSet = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.Text, dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                ForumPost model = new ForumPost();
                model.PostId = Convert.ToInt32(row[0]);
                model.Title = row[1].ToString();
                list.Add(model);
            }
            return list;
        }
    }
}
