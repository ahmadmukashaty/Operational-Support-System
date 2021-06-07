using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Syriatel.OSS.API.Models
{
    [Table("RIM_CATEGORIES")]

    public partial class RIM_CATEGORIES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_CATEGORIES()
        {
            this.RIM_LEVELS = new HashSet<RIM_LEVELS>();
        }
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("NAME")]

        public string NAME { get; set; }
        [Column("CAT_ORDER")]

        public Nullable<int> CAT_ORDER { get; set; }
        [Column("MODULE_ID")]

        public Nullable<int> MODULE_ID { get; set; }
        public virtual RIM_MODULES RIM_MODULES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_LEVELS> RIM_LEVELS { get; set; }
    }
}