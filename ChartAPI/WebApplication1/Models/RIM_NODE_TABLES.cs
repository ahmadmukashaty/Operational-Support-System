using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    [Table("RIM_NODE_TABLES")]

    public class RIM_NODE_TABLES
    {
  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_NODE_TABLES()
        {
            this.RIM_ATTRIBUTES = new HashSet<RIM_ATTRIBUTES>();
            this.RIM_LEVEL_NODES1 = new HashSet<RIM_LEVEL_NODES>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> LEVEL_NODE_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_ATTRIBUTES> RIM_ATTRIBUTES { get; set; }
        public virtual RIM_LEVEL_NODES RIM_LEVEL_NODES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_LEVEL_NODES> RIM_LEVEL_NODES1 { get; set; }
    }
}