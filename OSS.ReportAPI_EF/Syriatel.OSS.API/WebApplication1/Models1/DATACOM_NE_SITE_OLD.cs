//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.OSS.API.Models1
{
    using System;
    using System.Collections.Generic;
    
    public partial class DATACOM_NE_SITE_OLD
    {
        public int ID { get; set; }
        public int NE_ID { get; set; }
        public int SITE_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual DATACOM_NE_OLD DATACOM_NE_OLD { get; set; }
    }
}
