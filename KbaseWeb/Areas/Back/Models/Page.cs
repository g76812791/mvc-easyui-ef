using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbaseWeb.Areas.Back.Models
{
    public class PagerSearch<T>
    {
        /// <summary>
        /// easyui list 大小
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// easyui 第页
        /// </summary>
        public int page { get; set; }

        public T Model { get; set; }
       
    }
}