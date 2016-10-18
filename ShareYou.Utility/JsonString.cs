using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShareYou.Utility
{
    public class JsonString
    {
        public static string GetString(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("序列化对象为空", innerException: null);
            return JsonConvert.SerializeObject(obj);
        }
    }
}
