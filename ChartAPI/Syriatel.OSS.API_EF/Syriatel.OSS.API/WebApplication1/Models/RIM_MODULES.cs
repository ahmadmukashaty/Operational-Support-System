using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_MODULES")]

    public class RIM_MODULES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_MODULES()
        {
            this.RIM_CATEGORY = new HashSet<RIM_CATEGORY>();
        }
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("NAME")]

        public string NAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_CATEGORY> RIM_CATEGORY { get; set; }
    }
}