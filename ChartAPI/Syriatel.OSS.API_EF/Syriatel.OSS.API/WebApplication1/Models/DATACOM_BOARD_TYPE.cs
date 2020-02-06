using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_BOARD_TYPE")]
    public class DATACOM_BOARD_TYPE
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("ALIAS_NAME")]
        public string ALIAS_NAME { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }
        [Column("TOTAL_CHILD_NUMBER")]
        public Nullable<short> TOTAL_CHILD_NUMBER { get; set; }
        public virtual ICollection<DATACOME_BOARD> DATACOM_BOARD { get; set; }
    }
}