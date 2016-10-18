using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess.Forum;
using ShareYou.IDBAccess.User;
using ShareYou.Model.CustomeException;
using ShareYou.Model.UserInfo;

namespace ShareYou.DBAccess.Forum
{
    public class FriendOperationDal:IFriendOperationDal
    {
        public void AddFirendOperation(int[] friendids,int userid,int useroperationid,DateTime dateline)
        {
            
            SqlHelper.UpdateDataTable("select top 0 userid,friendid,operationid,dateline from user_friendoperation",
                dt =>
                {
                    foreach (int friendid in friendids)
                    {
                        dt.Rows.Add(userid, friendid, useroperationid, dateline); //更新回数据库
                    }        
                });
        }
        
        
    }
}
