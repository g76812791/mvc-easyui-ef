//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class user:Pager
    {
        public user()
        {
            
            
            
            CreatTime =DateTime.Now;
        }
        public long Id { get; set; }
        public string LoginName { get; set; }
        public string Pwd { get; set; }
        public Nullable<System.DateTime> CreatTime { get; set; }
    }
}
