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
    
    public partial class OPTICAL_SUBRACK_TYPE
    {
        public OPTICAL_SUBRACK_TYPE()
        {
            this.OPTICAL_SUBRACK = new HashSet<OPTICAL_SUBRACK>();
        }
    
        public int ID { get; set; }
        public string TYPE { get; set; }
        public Nullable<int> TOTAL_SLOTS_NUMBER { get; set; }
        public Nullable<int> SLOTS_FOR_SERVICES_BOARDS { get; set; }
    
        public virtual ICollection<OPTICAL_SUBRACK> OPTICAL_SUBRACK { get; set; }
    }
}
