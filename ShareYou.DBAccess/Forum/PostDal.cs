using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.Forum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.DBAccess.Forum
{
    public class PostDal : IPostDal
    {
        public object AddPost(ForumPost post)
        {
            string sql = "usp_addpost";
            SqlParameter[] spm =
                SqlHelper.GetSqpParameters(
                    new string[] { "@title", "@content", "@userid", "@like", "@unlike", "@boardid", "@limit", "@dateline", "@delflag", "@useroperationid" },
                    new object[]
                    {
                        post.Title, post.Content, post.UserId, post.Like, post.UnLike, post.BoardId,
                        post.LimitedMemberId, post.DateLine, post.DelFlag,DBNull.Value
                    },
                    new SqlDbType[]
                    {
                        SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int,
                        SqlDbType.Int, SqlDbType.DateTime, SqlDbType.Int,SqlDbType.Int, 
                    });
            spm[9].Direction = ParameterDirection.Output;
            return SqlHelper.ExecuteScalar(sql, CommandType.StoredProcedure, spm);
        }

        public object GetPostId(int userid, string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new UserException("帖子标题为空");
            string sql =
                "select top 1 postid from forum_post where userid=@userid and title=@title order by dateline desc ";
            SqlParameter[] spm = new SqlParameter[]
            {
                new SqlParameter("@userid",SqlDbType.Int){Value = userid},
                new SqlParameter("@title",SqlDbType.NVarChar,32){Value = title},  
            };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public IList<ForumPost> GetPosts(IList<int> ids)
        {
            List<ForumPost> list = new List<ForumPost>();
            if (null == ids || ids.Count == 0)
                return list; //如果没有id的话，直接返回一个空的集合
            string sql = ids.Aggregate("select postid,title from forum_post where postid in(", (current, id) => current + (id + ","));
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



        public IList<ForumPost> GetPosts(int pageindex, int pagesize, int boardid, out int pagecount)
        {
            string sql = "usp_getpostbypage";
            SqlParameter[] spm = SqlHelper.GetSqpParameters(new string[] { "@pagesize", "@pageindex", "@pagecount" },
                new object[] { pagesize, pageindex, DBNull.Value },
                new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.Int });
            spm[2].Direction = ParameterDirection.Output;
            DataSet set = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.StoredProcedure, set, spm);
            pagecount = Convert.ToInt32(spm[2].Value);
            List<ForumPost> list=new List<ForumPost>();
            foreach (DataRow dataRow in set.Tables[0].Rows)
            {
                ForumPost post=new ForumPost();
                post.PostId = Convert.ToInt32(dataRow[0]);
                post.Title = dataRow[1].ToString();
                post.Content = dataRow[2].ToString();
                post.UserId = Convert.ToInt32(dataRow[3]);
                post.Like = Convert.ToInt32(dataRow[4]);
                post.UnLike = Convert.ToInt32(dataRow[5]);
                post.BoardId = Convert.ToInt32(dataRow[6]);
                post.LimitedMemberId = Convert.ToInt32(dataRow[7]);
                post.DateLine = Convert.ToDateTime(dataRow[8]);
                post.DelFlag = (DelFlag) Convert.ToInt32(dataRow[9]);
                list.Add(post);
            }
            return list;
        }

        public ForumPost GetPost(int postid)
        {
            string sql = "select * from forum_post where postid=@postid";
            SqlParameter spm = new SqlParameter("@postid", SqlDbType.Int) { Value = postid };
            ForumPost post = null;
            using (var reader = SqlHelper.ExecuteReader(sql, CommandType.Text, spm))
            {
                if (reader.HasRows)
                {
                    post = new ForumPost();
                    reader.Read();
                    post.PostId = postid;
                    post.Title = reader.GetString(1);
                    post.Content = reader.GetString(2);
                    post.UserId = reader.GetInt32(3);
                    post.Like = reader.GetInt32(4);
                    post.UnLike = reader.GetInt32(5);
                    post.BoardId = reader.GetInt32(6);
                    post.LimitedMemberId = reader.GetInt32(7);
                    post.DateLine = reader.GetDateTime(8);
                    post.DelFlag = (DelFlag)reader.GetInt32(9);
                }
            }
            return post;
        }

        public IList<ViewPost> GetViewPosts(int pageindex, int pagesize, int boardid, out int pagecount)
        {
            string sql = "usp_getpostinfobypage";
            SqlParameter[] spms = SqlHelper.GetSqpParameters(new string[] { "@boardid", "@pagesize", "@pageindex", "@pagecount" },
                new object[] { boardid, pagesize, pageindex, DBNull.Value },
                new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int });
            spms[3].Direction = ParameterDirection.Output;
            DataSet dataSet = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.StoredProcedure, dataSet, spms);
            pagecount = Convert.ToInt32(spms[3].Value);
            List<ViewPost> list=new List<ViewPost>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                ViewPost post=new ViewPost();
                post.Post.PostId = Convert.ToInt32(dataRow[0]);
                post.Post.Title = dataRow[1].ToString();
                post.Post.Content = dataRow[2].ToString();
                post.Post.UserId = Convert.ToInt32(dataRow[3]);
                post.Post.Like = Convert.ToInt32(dataRow[4]);
                post.Post.UnLike = Convert.ToInt32(dataRow[5]);
                post.Post.DateLine = Convert.ToDateTime(dataRow[6]);
                post.User.Head = dataRow[7].ToString();
                list.Add(post);
            }
            return list;
        }

        public IList<ViewPost> GetViewPostsTop()
        {
            string sql = "usp_getpoststop";
            DataSet dataSet = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.StoredProcedure, dataSet);
            List<ViewPost> list = new List<ViewPost>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                ViewPost post = new ViewPost();
                post.Post.PostId = Convert.ToInt32(dataRow[0]);
                post.Post.Title = dataRow[1].ToString();
                post.Post.Content = dataRow[2].ToString();
                post.Post.UserId = Convert.ToInt32(dataRow[3]);
                post.Post.Like = Convert.ToInt32(dataRow[4]);
                post.Post.UnLike = Convert.ToInt32(dataRow[5]);
                post.Post.DateLine = Convert.ToDateTime(dataRow[6]);
                post.User.Head = dataRow[7].ToString();
                post.User.UserId = Convert.ToInt32(dataRow[8]);
                post.User.UserName = dataRow[9].ToString();
                list.Add(post);
            }
            return list;
            
        }

        public IList<ForumPost> GetPostsRandom()
        {
            string sql = "select top 10 postid,title from forum_post order by NEWID()";
            DataSet dataSet = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.Text, dataSet);
            List<ForumPost> list = new List<ForumPost>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                ForumPost post = new ForumPost();
                post.PostId = Convert.ToInt32(dataRow[0]);
                post.Title = dataRow[1].ToString();
                list.Add(post);
            }
            return list;
        }

        public IList<ViewPostPersonList> GetViewPostPersonList(int userid, int pageindex, int pagesize, out int pagecount)
        {
            string sql = "usp_getpersonlistpost";
            SqlParameter[] spms =
                SqlHelper.GetSqpParameters(new string[] { "@userid", "@pageindex", "@pagesize", "pagecount" },
                    new object[] { userid, pageindex, pagesize, DBNull.Value }, new SqlDbType[]
                    {
                        SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int,
                    });
            spms[3].Direction= ParameterDirection.Output;
            DataSet set = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.StoredProcedure, set, spms);
            pagecount = Convert.ToInt32(spms[3].Value);
            List<ViewPostPersonList> list=new List<ViewPostPersonList>();
            foreach (DataRow dataRow in set.Tables[0].Rows)
            {
                ViewPostPersonList model=new ViewPostPersonList();
                model.Post.PostId = Convert.ToInt32(dataRow[0]);
                model.Post.Title = dataRow[1].ToString();
                model.Post.DateLine = Convert.ToDateTime(dataRow[2]);
                model.Post.Like = Convert.ToInt32(dataRow[3]);
                model.CountComment = Convert.ToInt32(dataRow[4]);
                list.Add(model);
            }
            return list;
        }
    }
}
