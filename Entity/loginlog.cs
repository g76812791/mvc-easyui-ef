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
    
    public partial class loginlog:Pager
    {
        public loginlog()
        {
            
            
            LogTime =DateTime.Now;
            
        }
        public long Id { get; set; }
        public Nullable<long> Uid { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string Ip { get; set; }
    }
}
