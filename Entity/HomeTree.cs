using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public  class HomeTree
    {
        public long menuid { get; set; }
        public string icon { get; set; }
        public string menuname { get; set; }
        public string url { get; set; }
        public List<HomeTree> menus { get; set; }
    }

}
