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
    
    public partial class RIM_LEVEL_NODES
    {
        public RIM_LEVEL_NODES()
        {
            this.RIM_NODE_TABLES = new HashSet<RIM_NODE_TABLES>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string NODE_TYPE { get; set; }
        public Nullable<int> NODE_ORDER { get; set; }
        public Nullable<int> LEVEL_ID { get; set; }
        public Nullable<int> NODE_TABLE_ID { get; set; }
    
        public virtual RIM_LEVELS RIM_LEVELS { get; set; }
        public virtual ICollection<RIM_NODE_TABLES> RIM_NODE_TABLES { get; set; }
        public virtual RIM_NODE_TABLES RIM_NODE_TABLES1 { get; set; }
    }
}
