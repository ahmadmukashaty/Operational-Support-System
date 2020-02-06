using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models.Views
{
    [Table("MW_NE_SFP")]
    public class MW_NE_SFP
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
