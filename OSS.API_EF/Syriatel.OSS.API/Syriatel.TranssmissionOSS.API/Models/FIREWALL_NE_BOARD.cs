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
    
    public partial class FIREWALL_NE_BOARD
    {
        public int ID { get; set; }
        public int FIREWALL_NE_ID { get; set; }
        public int FIREWALL_BOARD_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual FIREWALL_BOARD FIREWALL_BOARD { get; set; }
        public virtual FIREWALL_NE FIREWALL_NE { get; set; }
    }
}
