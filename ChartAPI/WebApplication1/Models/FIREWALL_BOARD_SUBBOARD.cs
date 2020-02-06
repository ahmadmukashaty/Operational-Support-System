using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_BOARD_SUBBOARD
    {
        public int ID { get; set; }
        public int CHILD_ID { get; set; }
        public int PARENT_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }

        public virtual FIREWALL_BOARD FIREWALL_BOARD { get; set; }
        public virtual FIREWALL_SUBBOARD FIREWALL_SUBBOARD { get; set; }
    }
}