using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_CPU
    {
        public int ID { get; set; }
        public int SUBBOARD_ID { get; set; }
        public string ALIAS_NAME { get; set; }
        public Nullable<int> CPU_NUMBER { get; set; }

        public virtual FIREWALL_SUBBOARD FIREWALL_SUBBOARD { get; set; }
    }
}