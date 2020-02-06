using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_NE_BOARD")]
    public class DATACOM_NE_BOARD
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("PARENT_ID")]
        public int parentId { get; set; }

        [Column("CHILD_ID")]
        public int childId { get; set; }

        [Column("CREATE_DATE")]

        public DateTime createDate { get; set; }

        [Column("RETIRE_DATE")]
        public Nullable<System.DateTime> retireDate { get; set; } 

        public virtual DATACOM_NE DATACOM_NE { get; set; }
        public virtual DATACOME_BOARD DATACOM_BOARD { get; set; }
    }
}