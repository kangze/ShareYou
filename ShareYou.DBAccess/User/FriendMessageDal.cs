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
    public class FriendMessageDal:IFriendMessageDal
    {
        public int AddUserMessage(UserMessage um)
        {
            string sql = "insert into user_message values(@content,@userid,@writerid,@looked,@dateline)";
            SqlParameter[] spm =
                SqlHelper.GetSqpParameters(new string[] {"@content", "@userid", "@writerid", "@looked", "@dateline"},
                    new object[] {um.Content, um.UserId, um.WriterId, um.Looked, um.Looked, um.DateLine},
                    new SqlDbType[]
                    {SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.DateTime});
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, spm);
        }
    }
}
