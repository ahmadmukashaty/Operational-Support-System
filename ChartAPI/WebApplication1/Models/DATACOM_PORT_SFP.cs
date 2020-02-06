using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_PORT_SFP")]
    public class DATACOM_PORT_SFP
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("PARENT_ID")]
        public int PARENT_ID { get; set; }
        [Column("CHILD_ID")]
        public int CHILD_ID { get; set; }
        [Column("CREATE_DATE")]
        public System.DateTime CREATE_DATE { get; set; }
        [Column("RETIRE_DATE")]
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }

        public virtual DATACOM_PORT DATACOM_PORT { get; set; }
        public virtual DATACOM_SFP DATACOM_SFP { get; set; }
    }
}