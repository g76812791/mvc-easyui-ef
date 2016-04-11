using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Comm;
namespace KbaseWeb.Common
{
    public static class AppConfig
    {
        public static string CssPath
        {
            get
            {
                return Config.GetValue<string>("CssPath");
            }
        }
        public static string ScriptPath
        {
            get
            {
                return Config.GetValue<string>("ScriptPath");
            }
        }
        public static string ImagesPath
        {
            get
            {
                return Config.GetValue<string>("ImagesPath");
            }
        }
        public static string ImagesPicPath
        {
            get
            {
                return Config.GetValue<string>("ImagesPicPath");
            }
        }

        public static string EasyUiPath
        {
            get
            {
                return Config.GetValue<string>("EasyUiPath");
            }
        }
        public static string LoginUrl
        {
            get
            {
                return Config.GetValue<string>("LoginUrl");
            }
        }

    }
}