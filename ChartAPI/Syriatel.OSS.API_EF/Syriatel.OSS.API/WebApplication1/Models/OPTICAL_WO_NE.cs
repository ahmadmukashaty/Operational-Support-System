using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_WO_NE
    {
        public int ID { get; set; }
        public int WO_ID { get; set; }
        public int NE_ID { get; set; }
        public string NE_TYPE { get; set; }

        public virtual OPTICAL_NE OPTICAL_NE { get; set; }
        public virtual OPTICAL_WORK_ORDER OPTICAL_WORK_ORDER { get; set; }
    }
}