using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Utility
{
    public static class PageSize
    {
        public static readonly int Value;

        static PageSize()
        {
            Value = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }
    }
}
