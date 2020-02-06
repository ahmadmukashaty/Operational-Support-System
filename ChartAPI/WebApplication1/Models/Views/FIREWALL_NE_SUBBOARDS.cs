using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Views
{
    [Table("FIREWALL_NE_SUBBOARDS")]
    public class FIREWALL_NE_SUBBOARDS
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("NE_ID")]
        public int? NE_ID { get; set; }
        [Column("SUBSLOT_ID")]
        public int? SUBSLOT_ID { get; set; }
        [Column("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }
    }
}