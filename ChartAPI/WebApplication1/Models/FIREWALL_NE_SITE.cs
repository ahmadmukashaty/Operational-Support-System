using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_NE_SITE
    {
        public int ID { get; set; }
        public int NE_ID { get; set; }
        public int SITE_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }

        public virtual SITE SITE { get; set; }
    }
}