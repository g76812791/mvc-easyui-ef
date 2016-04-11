using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm
{
    public static  class DateExtens
    {
        public static bool IsEmptyDate(this DateTime dt)
        {
            if (dt.ToString() == "0001/1/1 0:00:00" || string.IsNullOrEmpty(dt.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
