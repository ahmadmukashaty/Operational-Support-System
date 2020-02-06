using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_PORT_TYPE")]
    public class DATACOM_PORT_TYPE
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_PORT> DATACOM_PORT { get; set; }
    }
}