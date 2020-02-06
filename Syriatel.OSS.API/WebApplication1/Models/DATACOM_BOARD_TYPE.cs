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
        public int Id { get; set; }
        [Column("ALIAS_NAME")]
        public string aliasName { get; set; }
        [Column("TYPE")]
        public string Type { get; set; }
        [Column("TOTAL_CHILD_NUMBER")]
        public Nullable<short> totalChildNumber { get; set; }
        public virtual ICollection<DATACOME_BOARD> DATACOM_BOARD { get; set; }
    }
}