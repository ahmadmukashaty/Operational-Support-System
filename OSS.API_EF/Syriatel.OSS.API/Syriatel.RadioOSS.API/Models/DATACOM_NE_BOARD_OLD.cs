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
    
    public partial class DATACOM_NE_BOARD_OLD
    {
        public int ID { get; set; }
        public int PARENT_ID { get; set; }
        public int CHILD_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual DATACOM_BOARD_OLD DATACOM_BOARD_OLD { get; set; }
        public virtual DATACOM_NE_OLD DATACOM_NE_OLD { get; set; }
    }
}
