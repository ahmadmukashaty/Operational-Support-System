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
    
    public partial class MW_PORT_TYPE
    {
        public MW_PORT_TYPE()
        {
            this.MW_PORT = new HashSet<MW_PORT>();
        }
    
        public int ID { get; set; }
        public string TYPE { get; set; }
    
        public virtual ICollection<MW_PORT> MW_PORT { get; set; }
    }
}
