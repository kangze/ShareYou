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
using ShareYou.Model.UserInfo;

namespace ShareYou.DBAccess.Forum
{
    public class UserOperationDal:IUserOperationDal
    {
        
        public IList<UserOperation> GetUserOperationsByPage(int userid, int pageindex, int pagesize, out int pagecount)
        {
            string sql = "usp_getuseroperation";
            SqlParameter[] spms =
                SqlHelper.GetSqpParameters(new string[] {"@userid", "@pageindex", "@pagesize", "@pagecount"},
                    new object[] {userid, pageindex, pagesize, DBNull.Value},
                    new SqlDbType[] {SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int});
            spms[3].Direction= ParameterDirection.Output;
            DataSet set=new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.StoredProcedure, set,spms);
            pagecount = Convert.ToInt32(spms[3].Value);
            List<UserOperation> list=new List<UserOperation>();
            foreach (DataRow dataRow in set.Tables[0].Rows)
            {
                UserOperation model=new UserOperation();
                model.Id = Convert.ToInt32(dataRow[0]);
                model.UserId = Convert.ToInt32(dataRow[1]);
                model.UserNmae = dataRow[2].ToString();
                model.Action = (UserAction) Convert.ToInt32(dataRow[3]);
                model.EventId = Convert.ToInt32(dataRow[4]);
                model.DelFlag = (DelFlag) Convert.ToInt32(dataRow[5]);
                model.DateLine = Convert.ToDateTime(dataRow[6]);
                list.Add(model);
            }
            return list;
        }
    }
}
