using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Tree
    {
        public long id { get; set; }
        public string text { get; set; }
        /// <summary>
        /// open / closed，默认 open
        /// </summary>
        public string state { get; set; }
        public bool Checked { get; set; }
        public object attributes { get; set; }
        public List<Tree> children { get; set; }
    }
}

