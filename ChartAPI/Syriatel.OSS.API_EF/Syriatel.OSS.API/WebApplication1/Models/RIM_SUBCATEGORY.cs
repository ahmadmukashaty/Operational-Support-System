using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_SUBCATEGORY")]

    public class RIM_SUBCATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_SUBCATEGORY()
        {
            this.RIM_SUBCATEGORIES1 = new HashSet<RIM_SUBCATEGORY>();
        }
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("NAME")]

        public string Name { get; set; }
        [Column("SUBCAT_ORDER")]

        public Nullable<int> SUBCAT_ORDER { get; set; }
        [Column("CATEGORY_ID")]

        public Nullable<int> CATEGORY_ID { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_SUBCATEGORY> RIM_SUBCATEGORIES1 { get; set; }
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORIES2 { get; set; }
    }
}