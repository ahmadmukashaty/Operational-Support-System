using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_BOARD_SUBBOARD")]
    public class DATACOM_BOARD_SUBBOARD
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("CHILD_ID")]
        public int childId { get; set; }
        [Column("PARENT_ID")]
        public int parentId { get; set; }
        [Column("CREATE_DATE")]
        public System.DateTime createDate { get; set; }
        [Column("RETIRE_DATE")]
        public Nullable<System.DateTime> retireDate { get; set; }

        public virtual DATACOME_BOARD DATACOM_BOARD { get; set; }
        public virtual DATACOM_SUBBOARD DATACOM_SUBBOARD { get; set; }
    }
}