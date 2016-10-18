using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.DBAccess.DB;
using ShareYou.IDBAccess.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;

namespace ShareYou.DBAccess.User
{
    public class FriendRequestDal:IFriendRequestDal
    {
        public int AddFriendRequest(FriendRequest frquest)
        {
            if(null==frquest)
                throw new UserException("好友请求数据为空");
            string sql =
               "insert into user_friendrequest values(@friendid,@userid,@username,@gid,@note,@looked,@result,@dateline,@delflag)";
            
            SqlParameter[] spms =
                SqlHelper.GetSqpParameters(
                    new string[]
                    {
                        "@friendid", "@userid", "@username", "@gid", "@note", "@looked", "@result", "@dateline", "@delflag"
                    },
                    new object[]
                    {
                        frquest.FriendId, frquest.UserId, frquest.UserName, frquest.Gid, frquest.Note, frquest.Looked,
                        frquest.Result, frquest.DateLine, frquest.DelFlag
                    },
                    new SqlDbType[]
                    {
                        SqlDbType.Int, SqlDbType.Int, SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.NVarChar, SqlDbType.Int,
                        SqlDbType.Int, SqlDbType.DateTime, SqlDbType.Int
                    });
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, spms);
        }


        public int ChangeFriendRequestState(int friendrequestid, UserLooked lookied, PrResult result, DelFlag delFlag)
        {
            string sql = "update user_friendrequest set looked=@looked,result=@result,delflag=@delflag where id=@id";
            SqlParameter spm=new SqlParameter("@id",SqlDbType.Int){Value = friendrequestid};
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, spm);
        }

        public FriendRequest GetFriendRequest(int id)
        {
            FriendRequest fr = null;
            string sql = "select * from from user_friendrequest where id=@id";
            SqlParameter spm = new SqlParameter("@id", SqlDbType.Int) {Value = id};
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, spm))
            {
                if (reader.HasRows)
                {
                    fr=new FriendRequest();
                    fr.Id = id;
                    fr.FriendId = reader.GetInt32(1);
                    fr.UserId = reader.GetInt32(2);
                    fr.UserName = reader.GetString(3);
                    fr.Gid = reader.GetInt32(4);
                    fr.Note = reader.GetString(5);
                    fr.Looked = (UserLooked) reader.GetInt32(6);
                    fr.Result = (PrResult) reader.GetInt32(7);
                    fr.DateLine = reader.GetDateTime(8);
                    fr.DelFlag = (DelFlag) reader.GetInt32(9);
                }
            }
            return fr;
        }
    }
}
