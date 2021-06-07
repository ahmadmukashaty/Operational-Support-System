using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("SITE")]
    public class SITE
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("CODE")]
        public string CODE { get; set; }
        [Column("ENGLISH_NAME")]
        public string ENGLISH_NAME { get; set; }
        [Column("REGION")]
        public string REGION { get; set; }
        [Column("AREA")]
        public string AREA { get; set; }
        [Column("ZONE")]
        public string ZONE { get; set; }
        [Column("SUBAREA")]
        public string SUBAREA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
    }
}