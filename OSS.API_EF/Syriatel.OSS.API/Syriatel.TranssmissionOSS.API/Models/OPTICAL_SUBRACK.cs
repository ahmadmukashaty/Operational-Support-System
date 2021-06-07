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
    
    public partial class OPTICAL_SUBRACK
    {
        public OPTICAL_SUBRACK()
        {
            this.OPTICAL_NE_SUBRACK = new HashSet<OPTICAL_NE_SUBRACK>();
            this.OPTICAL_SUBRACK_BOARD = new HashSet<OPTICAL_SUBRACK_BOARD>();
        }
    
        public int ID { get; set; }
        public int OPTICAL_SUBRACK_TYPE_ID { get; set; }
        public string NAME { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string ALIAS { get; set; }
        public string REMARKS { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public Nullable<int> FREE_BOARD { get; set; }
        public Nullable<int> RACK_ID { get; set; }
    
        public virtual ICollection<OPTICAL_NE_SUBRACK> OPTICAL_NE_SUBRACK { get; set; }
        public virtual OPTICAL_SUBRACK_TYPE OPTICAL_SUBRACK_TYPE { get; set; }
        public virtual ICollection<OPTICAL_SUBRACK_BOARD> OPTICAL_SUBRACK_BOARD { get; set; }
    }
}
