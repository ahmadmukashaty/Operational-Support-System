//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.RadioOSS.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MW_NE_BOARD_OLD
    {
        public int ID { get; set; }
        public int PARENT_ID { get; set; }
        public int CHILD_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual MW_BOARD_OLD MW_BOARD_OLD { get; set; }
        public virtual MW_NE_OLD MW_NE_OLD { get; set; }
    }
}
