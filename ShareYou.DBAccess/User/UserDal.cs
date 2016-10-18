using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.DBAccess.DB;
using ShareYou.IDBAccess;
using ShareYou.IDBAccess.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Enum;
using ShareYou.Model.UserInfo;
using ShareYou.Model.ViewModel;

namespace ShareYou.DBAccess.User
{
    public class UserDal : IUserDal
    {
        public ViewUserInfo GetViewUserInfo(int userid)
        {
            string sql = "usp_viewuserinfo";
            ViewUserInfo model = null;
            SqlParameter spm=new SqlParameter("@userid",SqlDbType.Int){Value = userid};
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.StoredProcedure, spm))
            {
                if (reader.HasRows)
                {
                    model=new ViewUserInfo();
                    reader.Read();
                    model.User.UserId = userid;
                    model.User.UserName = reader.GetString(1);
                    model.User.Head = reader.GetString(2);
                    model.UserInfo.Description = reader.GetString(3);
                    model.UserInfo.Liked = reader.GetInt32(4);
                    model.UserInfo.Follow = reader.GetInt32(5);
                    model.UserInfo.Follower = reader.GetInt32(6);
                }
            }
            return model;
        }

        public object GetPassword(string account)
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account", "用户账户为空");
            string sql = "select [password] from user_usersimp where account=@account";
            SqlParameter spm = new SqlParameter("@account", SqlDbType.VarChar, 32) { Value = account };
            object obj = SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
            return obj;
        }

        public object GetUserStatus(string account)
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account", "用户账户为空");
            string sql = "select delflag from user_usersimp where account=@account";
            SqlParameter spm = new SqlParameter("@account", SqlDbType.VarChar, 32) { Value = account };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public UserSimp GetUserSimp(string account)
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account", "用户账户为空");
            UserSimp model = null;
            string sql = "select * from user_usersimp where account=@account";
            SqlParameter spm = new SqlParameter("@account", SqlDbType.VarChar, 20) { Value = account };
            using (var reader = SqlHelper.ExecuteReader(sql, CommandType.Text, spm))
            {
                if (reader.HasRows)
                {
                    model = new UserSimp();
                    while (reader.Read())
                    {
                        model.UserId = reader.GetInt32(0);
                        model.UserName = reader.GetString(1);
                        model.Account = account;
                        model.Password = reader.GetString(3);
                        model.MemberId = reader.GetInt32(4);
                        model.Head = reader.GetString(5);
                        model.DelFlag = (DelFlag)reader.GetInt32(6);
                    }
                }
      
            }
            return model;
        }

        public object GetUserName(int userid)
        {
            string sql = "select username from user_usersimp where userid=@userid";
            SqlParameter spm = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public bool AccountExist(string account)
        {
            if (string.IsNullOrEmpty(account))
                return false;
            string sql = "select count(*) from user_usersimp where account=@account";
            SqlParameter spm = new SqlParameter("@account", SqlDbType.VarChar, 32) { Value = account };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text, spm)) == 1;
        }

        public void AddUser(UserSimp usersimp, UserInfo userinfo)
        {
            if (null == usersimp || null == userinfo)
                throw new ArgumentNullException("userinfo");
            string sqluser = "insert into user_usersimp values(@username,@account,@password,@memberid,@head,@delflag)";
            string sqluserinfo =
                "insert into user_userinfo values(@desc,@email,@emailvalidatecode,@isvalide,@number,@like,@follower,@follow,@delflag)";
            
            SqlParameter[] spm1 =
                SqlHelper.GetSqpParameters(
                    new string[] {"@username", "@account", "@password", "@memberid", "@head", "delflag"},
                    new object[]
                    {
                        usersimp.UserName, usersimp.Account, usersimp.Password, usersimp.MemberId, usersimp.Head,
                        usersimp.DelFlag
                    },
                    new SqlDbType[]
                    {
                        SqlDbType.NVarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar,
                        SqlDbType.Int
                    });
            SqlParameter[] spm2 =
                SqlHelper.GetSqpParameters(
                    new string[]
                    {
                        "@desc", "@email", "@emailvalidatecode", "@isvalide", "@number", "@like", "@follow", "@follow",
                        "@delflag"
                    },
                    new object[]
                    {
                        userinfo.Description, userinfo.Email, userinfo.EmailValidateCode, userinfo.IsValide,
                        userinfo.Number, userinfo.Liked, userinfo.Follower, userinfo.Follow, userinfo.DelFlag
                    },
                    new SqlDbType[]
                    {
                        SqlDbType.NVarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar,
                        SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int
                    });
            
            try
            {
                SqlHelper.Sqltransaction(new string[] { sqluser, sqluserinfo },
                    new SqlParameter[][] { spm1, spm2 });
            }
            catch (Exception e)
            {
                throw new UserException("注册用户失败,请重新注册");
            }
        }

        public object GetEmailValidate(int userid)
        {
            string sql = "select isvalide from user_userinfo where userid=@userid";
            SqlParameter spm = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public int UpdateUserEmailValidateCode(int userid, string emailvalidatecode)
        {
            if (string.IsNullOrEmpty(emailvalidatecode))
                throw new UserException("用户邮件验证码为空");
            string sql = "update user_userinfo set emailvalidatecode=@emailvalidatecode where userid=@userid";
            
            SqlParameter[] spms = SqlHelper.GetSqpParameters(new string[] {"@emailvalidatecode", "@userid"},
                new object[] {emailvalidatecode, userid}, new SqlDbType[] {SqlDbType.VarChar, SqlDbType.Int});
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, spms);
        }

        public object GetUserEmail(int userid)
        {
            string sql = "select email from user_userinfo where userid=@userid";
            SqlParameter spm = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public object GetEmailValidateCode(int userid)
        {
            string sql = "select emailvalidatecode from user_userinfo where userid=@userid";
            SqlParameter spm = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            return SqlHelper.ExecuteScalar(sql, CommandType.Text, spm);
        }

        public int UpdateEmailValidateStatus(int userid, int status)
        {
            string sql = "update user_userinfo set isvalide=@status where userid=@userid";
            SqlParameter[] spms = SqlHelper.GetSqpParameters(new string[] {"@status", "@userid"},
                new object[] {status, userid}, new SqlDbType[] {SqlDbType.Int, SqlDbType.Int});
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, spms);
        }

    }
}
