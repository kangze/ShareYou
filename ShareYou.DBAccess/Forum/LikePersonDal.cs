using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Model.UserInfo;

namespace ShareYou.DBAccess.Forum
{
    public class LikePersonDal:ILikePersonDal
    {
        public object AddLikePerson(LikePerson lp)
        {
            if(null==lp)
                throw new UserException("点赞用户数据为空");
            string sql = "usp_addlikeperson";
            
            SqlParameter[] spm =
                SqlHelper.GetSqpParameters(new string[] { "@userid", "@userlikeid", "@looked", "@dateline", "@useroperationid" },
                    new object[] {lp.UserId, lp.UserLikeId, lp.Looked, lp.DateLine,DBNull.Value},
                    new SqlDbType[] {SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Date,SqlDbType.Int, });
            spm[4].Direction= ParameterDirection.Output;
            SqlHelper.ExecuteNonquery(sql, CommandType.Text, spm);
            return spm[4].Value;
        }

        public IList<UserSimp> GetLikedPersons(IList<int> ids)
        {
            //参数id数量不固定,目前方案只能采用拼接sql的方式来进行
            List<UserSimp> list=new List<UserSimp>();
            if(null==ids||ids.Count==0)
                return list; //如果没有id的话，直接返回一个空的集合
            string sql = ids.Aggregate("select usersimp.userid,usersimp.username from user_usersimp usersimp,user_likeperson likeperson where likeperson.userid=usersimp.userid and likeperson.id in(", (current, id) => current + (id + ","));
            //去掉逗号，加上括号
            sql=sql.Substring(0, sql.Length - 1)+")";
            DataSet dataSet=new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.Text, dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                UserSimp model=new UserSimp();
                model.UserId = Convert.ToInt32(row[0]);
                model.UserName = row[1].ToString();
                list.Add(model);
            }
            return list;
            
        }
    }
}
