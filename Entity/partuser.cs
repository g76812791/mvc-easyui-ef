using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entity
{
    [MetadataType(typeof(attruser))]
    public partial class user
    {
        
    }

    public class attruser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<System.DateTime> CreatTime { get; set; }
    }

}
