using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess.User;
using ShareYou.Model.UserInfo;

namespace ShareYou.DBAccess.User
{
    public class FriendDal : IFriendDal
    {
        public object GetFriendCount(int userid)
        {
            string sql = "select count(*) from user_friend where userid=@userid";
            SqlParameter spm = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public IList<int> GetFriendsId(int userid)
        {

            string sql = "select id from user_friend where userid=@userid";
            SqlParameter spm = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            DataSet set = new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.Text, set, spm);
            List<int> list=new List<int>();
            foreach (DataRow dataRow in set.Tables[0].Rows)
            {
                list.Add(Convert.ToInt32(dataRow[0]));
            }
            return list;
        }

        public bool AddFriend(Friend friend, Friend friend2)
        {
            //添加好友
            string sql = "insert into user_friend values(@userid,@friendid,@fusername,@gid,@note,@dateline,@delflag)";


            SqlParameter[] spm1 =
                SqlHelper.GetSqpParameters(
                    new string[] { "@userid", "@friendid", "@fusername", "@gid", "@note", "@dateline", "@delflag" },
                    new object[]
                    {
                        friend.UserId, friend.FriendId, friend.FUserName, friend.Gid, friend.Note, friend.DateLine,
                        friend.DelFlag
                    },
                    new SqlDbType[]
                    {
                        SqlDbType.Int, SqlDbType.Int, SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.NVarChar,
                        SqlDbType.DateTime, SqlDbType.Int
                    });
            SqlParameter[] spm2 =
                SqlHelper.GetSqpParameters(
                    new string[] { "@userid", "@friendid", "@fusername", "@gid", "@note", "@dateline", "@delflag" },
                    new object[]
                    {
                        friend2.UserId, friend2.FriendId, friend2.FUserName, friend2.Gid, friend2.Note, friend2.DateLine,
                        friend2.DelFlag
                    }, new SqlDbType[]
                    {
                        SqlDbType.Int, SqlDbType.Int, SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.NVarChar,
                        SqlDbType.DateTime, SqlDbType.Int
                    });

            return SqlHelper.Sqltransaction(new[] { sql, sql }, new SqlParameter[][] { spm1, spm2 });
        }
    }
}
