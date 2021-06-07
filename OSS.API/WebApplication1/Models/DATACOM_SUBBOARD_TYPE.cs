using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_SUBBOARD_TYPE")]
    public class DATACOM_SUBBOARD_TYPE
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("ALIAS_NAME")]
        public string ALIAS_NAME { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }
        [Column("PORT_TYPE")]
        public string PORT_TYPE { get; set; }
        [Column("TOTAL_CHILD_NUMBER")]
        public Nullable<decimal> TOTAL_CHILD_NUMBER { get; set; }
        [Column("RESERVED_SUBSLOT_COUNT")]
        public Nullable<decimal> RESERVED_SUBSLOT_COUNT { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_SUBBOARD> DATACOM_SUBBOARD { get; set; }
    }
}