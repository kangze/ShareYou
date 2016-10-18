using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.DBAccess.DB
{
    public class DbValue
    {
        public static object CheckGetValue(object obj)
        {
            if (null == obj)
                //抛出异常
                throw new ArgumentNullException("将要保存的数据为空", innerException: null);
            return obj;
        }
    }
}
