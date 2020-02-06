using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_LEVEL_NODES")]
    public class RIM_LEVEL_NODES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_NODE_TABLES> RIM_NODE_TABLES { get; set; }
        public virtual RIM_NODE_TABLES RIM_NODE_TABLES1 { get; set; }
    }
}