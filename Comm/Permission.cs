﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm
{
    public static class Permission
    {
        #region btn权限判断

        public static bool isHasQuanXian(string action)
        {
            string UserId = CookieHelp.GetCookieValByKey("UserId");

            Func<List<string>> per = () => new List<string>();
            List<string> btnquanxian = CacheHelper.GetCache(UserId, per);


            if (btnquanxian.Contains(action))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
