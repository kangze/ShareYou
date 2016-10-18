using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Model.CustomeException
{
    public class EmailValideCodeException:Exception
    {
        /// <summary>
        /// 用户邮件验证码异常
        /// </summary>
        /// <param name="message"></param>
        public EmailValideCodeException(string message) : base(message)
        {
            
        }
    }
}
