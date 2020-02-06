using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_PORT_SFP
    {
        public int ID { get; set; }
        public int PARENT_ID { get; set; }
        public int CHILD_ID { get; set; }
        public System.DateTime CREATE_DATA { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }

        public virtual OPTICAL_PORT OPTICAL_PORT { get; set; }
        public virtual OPTICAL_SFP OPTICAL_SFP { get; set; }
    }
}