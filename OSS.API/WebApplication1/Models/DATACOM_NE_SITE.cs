using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_NE_SITE")]
    public class DATACOM_NE_SITE
    {
         [Column("ID")]
        public int ID { get; set; }
            [Column("NE_ID")]
         public int NE_ID { get; set; }
            [Column("SITE_ID")]
         public int SITE_ID { get; set; }
            [Column("CREATE_DATE")]
         public System.DateTime CREATE_DATE { get; set; }
            [Column("RETIRE_DATE")]
         public Nullable<System.DateTime> RETIRE_DATE { get; set; }

         public virtual DATACOM_NE DATACOM_NE { get; set; }
         public virtual SITE SITE { get; set; }
    }
}