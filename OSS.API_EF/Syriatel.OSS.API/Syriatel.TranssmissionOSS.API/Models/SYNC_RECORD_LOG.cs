//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.TranssmissionOSS.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYNC_RECORD_LOG
    {
        public decimal ID { get; set; }
        public decimal LOG_ID { get; set; }
        public Nullable<decimal> RECORD_ID { get; set; }
        public string COLUMN_NAME { get; set; }
        public string OLD_VALUE { get; set; }
        public string NEW_VALUE { get; set; }
    
        public virtual SYNC_LOG SYNC_LOG { get; set; }
    }
}