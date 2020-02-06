using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Syriatel.OSS.API.Models
{
    [Table("RIM_CATEGORY")]

    public partial class RIM_CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_CATEGORY()
        {
            this.RIM_LEVELS = new HashSet<RIM_LEVELS>();
        }
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("NAME")]

        public string Name { get; set; }
        [Column("CAT_ORDER")]

        public Nullable<int> CAT_ORDER { get; set; }
        [Column("MODULE_ID")]

        public Nullable<int> MODULE_ID { get; set; }
        public virtual RIM_MODULES RIM_MODULES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_LEVELS> RIM_LEVELS { get; set; }
    }
}