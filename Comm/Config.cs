using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
namespace Comm
{
    /// <summary>
    /// webconfig key获取value
    /// </summary>
    public  static  class Config
    {
        public static T GetValue<T>(string key, bool isDecode = true)
        {
            string text = ConfigurationManager.AppSettings[key];
            System.Type typeFromHandle = typeof(T);
            if (typeFromHandle == typeof(string) && isDecode)
            {
                text = HttpUtility.HtmlDecode(text);
            }
            if (typeFromHandle.IsEnum)
            {
                return (T)((object)System.Enum.Parse(typeFromHandle, text, true));
            }
            return (T)((object)System.Convert.ChangeType(text, typeof(T)));
        }
    }

   
}
