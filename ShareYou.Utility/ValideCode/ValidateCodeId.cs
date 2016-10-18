using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Utility.Session;

namespace ShareYou.Utility.ValideCode
{
    public class ValidateCodeId
    {
        public static string GetValidateCodeId()
        {
            return "validatecode_" +SessionId.GetSessionId();
        }
    }
}
