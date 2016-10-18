using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareYou.Utility.Path
{
    public static class ConvertPath
    {
        public static readonly string AppLocation;
        public static readonly string BeforeImagePath;
        /// <summary>
        /// 初始化项目具体的位置
        /// </summary>
        static ConvertPath()
        {
            //抛出异常，log4net自己来记录
            AppLocation = ConfigurationManager.AppSettings["applocation"];
            BeforeImagePath = ConfigurationManager.AppSettings["userimage"];
        }
        public static string RelativePath(string absolutePath, string relativeTo)
        {
            if (string.IsNullOrEmpty(absolutePath) || string.IsNullOrEmpty(relativeTo))
                throw new Exception("路径为空");
            return Relative(absolutePath, relativeTo);
        }

        public static string RelativeToAppLocation(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("路径为空");
            return Relative(AppLocation, path);
        }

        private static string Relative(string absolutePath, string relativeTo)
        {
            Uri absoluteuri = new Uri(absolutePath);
            Uri relativeuri = new Uri(relativeTo);
            Uri relativepath = absoluteuri.MakeRelativeUri(relativeuri);
            string path = Uri.UnescapeDataString(relativepath.ToString());
            return path;
        }
    }
}
