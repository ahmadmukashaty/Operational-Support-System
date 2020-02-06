using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Views
{
    [Table("OPTICAL_NE_SFPS")]
    public class OPTICAL_NE_SFPS
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("NE_ID")]
        public int? NE_ID { get; set; }
        [Column("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }
    }
}