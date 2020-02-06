using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Syriatel.OSS.API.Models.Views
{
    [Table("FIREWALL_NE_PORTS")]
    public class FIREWALL_NE_PORTS
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("NE_ID")]
        public int? NE_ID { get; set; }
        [Column("PORT_NAME")]
        public string PORT_NAME { get; set; }
        [Column("PORT_ID")]
        public int? PORT_ID { get; set; }

    }
}