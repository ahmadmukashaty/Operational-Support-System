using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_NE_TYPE")]
    public class DATACOM_NE_Type
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("VENDOR")]
        public string VENDOR { get; set; }

        [Column("CLASS")]
        public string CLASS { get; set; }

        [Column("SERIES")]
        public string SERIES { get; set; }

        [Column("MODEL")]
        public string MODEL { get; set; }

        [Column("TOTAL_CHILD_NUMBER")]
        public Nullable<decimal> TOTAL_CHILD_NUMBER { get; set; }
                
        public virtual ICollection<DATACOM_NE> DATACOM_NE { get; set; }

    }
}