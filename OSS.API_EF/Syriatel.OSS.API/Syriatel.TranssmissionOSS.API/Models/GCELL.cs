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
    
    public partial class GCELL
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> BCC { get; set; }
        public Nullable<int> BCCHNO { get; set; }
        public Nullable<int> CI { get; set; }
        public Nullable<int> LAC { get; set; }
        public Nullable<int> MCC { get; set; }
        public Nullable<int> MNC { get; set; }
        public Nullable<int> NCC { get; set; }
        public string TYPE { get; set; }
        public string DETAILS { get; set; }
    
        public virtual CELL CELL { get; set; }
    }
}
