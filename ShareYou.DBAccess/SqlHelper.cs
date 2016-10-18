using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.DBAccess
{

    public static class SqlHelper
    {
       
        public static readonly string ConnString = ConfigurationManager.ConnectionStrings["ShareYou"].ConnectionString;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="spm"></param>
        /// <returns>返回受影响的行数,Update Add Delete</returns>
        public static int ExecuteNonquery(string sql, CommandType type, params SqlParameter[] spm)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    command.CommandType = type;
                    if (spm != null)
                    {
                        //开始填充
                        command.Parameters.AddRange(spm);
                    }
                    //开始执行

                    con.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="spm"></param>
        /// <returns>返回的是第一行第一个对象</returns>
        public static object ExecuteScalar(string sql, CommandType type, params SqlParameter[] spm)
        {
            if(string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql","sql语句为空");
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    command.CommandType = type;
                    if (spm != null)
                    {
                        command.Parameters.AddRange(spm);
                    }
                    con.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="spm"></param>
        /// <returns>返回一个SqlDataReader对象，如果关闭这个对象，那么对应的连接对象也会进行关闭</returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] spm)
        {
            SqlConnection con = new SqlConnection(ConnString);
            using (SqlCommand command = new SqlCommand(sql, con))
            {
                command.CommandType = type;
                if (spm != null)
                {
                    command.Parameters.AddRange(spm);
                }
                try
                {
                    con.Open();
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch
                {
                    con.Close();
                    con.Dispose();
                    throw;
                    //清理工作做完了之后，抛出对应的异常
                }
            }

        }

        public static bool Sqltransaction(string[] sql, SqlParameter[][] spm)
        {
            SqlConnection con = new SqlConnection(ConnString);
            con.Open();
            SqlTransaction sqlTransaction = con.BeginTransaction();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.Transaction = sqlTransaction;
            try
            {
                int i = 0;
                foreach (string sqll in sql)
                {
                    command.CommandText = sqll;
                    command.Parameters.Clear();
                    command.Parameters.AddRange(spm[i]);
                    command.ExecuteNonQuery();
                    i++;
                }
                sqlTransaction.Commit();
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                throw;//继续向外抛出这个异常
            }
            finally
            {
                con.Close();
                command.Dispose();
                con.Dispose();
            }
            return true;
        }

        public static void GetDataTable(string sql, CommandType type, DataSet set,params SqlParameter[] spm)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlDataAdapter adapter=new SqlDataAdapter(sql,con);
                adapter.SelectCommand.CommandType = type;
                adapter.SelectCommand.Parameters.AddRange(spm);
                adapter.Fill(set);
            }
        }

        /// <summary>
        /// 构建参数，其中不包括输出参数
        /// </summary>
        /// <param name="param"></param>
        /// <param name="value"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqpParameters(string[] param, object[] value, SqlDbType[] types)
        {
            
            if(param.Length!=value.Length&&param.Length!=types.Length&param.Length<1)
                throw new ArgumentException("参数个数不匹配");
            List<SqlParameter> list=new List<SqlParameter>();
            for (int i = 0; i < param.Length; i++)
            {
                SqlParameter spm=new SqlParameter(param[i],types[i]){Value = value[i]};
                list.Add(spm);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 数据更新回数据库中,但是仅限于一张表
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="action"></param>
        public static void UpdateDataTable(string selectSql, Action<DataTable> action)
        {
            using (SqlConnection con = new SqlConnection(SqlHelper.ConnString))
            {
                using (SqlCommand selectCommand = new SqlCommand("select top 0 userid,friendid,operationid,dateline from user_friendoperation", con))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(selectCommand);
                    sda.Fill(dt);
                    //
                    action(dt);
                    //
                    SqlCommandBuilder db = new SqlCommandBuilder(sda);
                    sda.Update(dt.GetChanges());
                    dt.AcceptChanges();
                }
            }
        }

    }
}
