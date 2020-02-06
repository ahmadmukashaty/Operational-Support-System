using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_NE_AREA
    {
        public int ID { get; set; }
        public int NE_ID { get; set; }
        public int AREA_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }

        public virtual OPTICAL_AREA_ID_MAPPING OPTICAL_AREA_ID_MAPPING { get; set; }
        public virtual OPTICAL_NE OPTICAL_NE { get; set; }
    }
}