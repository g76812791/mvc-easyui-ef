using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Entity
{
    public  class Pager
    {
        /// <summary>
        /// easyui list 大小
        /// </summary>
    
        public virtual int rows { get; set; }
        /// <summary>
        /// easyui 第页
        /// </summary>
        public virtual int page { get; set; }

        public virtual DateTime beginDate { get; set; }

        public virtual DateTime endDate { get; set; }
    }
}
