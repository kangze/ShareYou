using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Model.CustomeException
{
    public class CacheException:Exception
    {
        public CacheException(string str) : base(str)
        {
            //缓存异常
        }
    }
}
