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
    
    public partial class MW_BOARD_TYPE
    {
        public MW_BOARD_TYPE()
        {
            this.MW_BOARD = new HashSet<MW_BOARD>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
    
        public virtual ICollection<MW_BOARD> MW_BOARD { get; set; }
    }
}
