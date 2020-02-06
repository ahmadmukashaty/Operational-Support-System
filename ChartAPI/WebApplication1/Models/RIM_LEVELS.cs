using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_LEVELS")]

    public class RIM_LEVELS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_LEVELS()
        {
            this.RIM_LEVEL_NODES = new HashSet<RIM_LEVEL_NODES>();
            //this.RIM_LEVELS1 = new HashSet<RIM_LEVELS>();
        }
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("NAME")]

        public string NAME { get; set; }
        [Column("LEVEL_ORDER")]

        public Nullable<int> LEVEL_ORDER { get; set; }
        [Column("PARENT_ID")]

        public Nullable<int> PARENT_ID { get; set; }
        [Column("CATEGORY_ID")]

        public Nullable<int> CATEGORY_ID { get; set; }
        public virtual RIM_CATEGORY RIM_CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_LEVEL_NODES> RIM_LEVEL_NODES { get; set; }
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<RIM_LEVELS> RIM_LEVELS1 { get; set; }
        //public virtual RIM_LEVELS RIM_LEVELS2 { get; set; }
    }
}