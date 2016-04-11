using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace KbaseWeb.Models
{
    public class caseview
    {
        public anlitype type { get; set; }
        public List<anlidetails> detalis { get; set; }

    }
}