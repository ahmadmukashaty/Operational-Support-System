using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_FINANCE_NE
    {
        public int ID { get; set; }
        public int FINANCE_TERM_ID { get; set; }
        public int NE_ID { get; set; }
        public string NE_TYPE { get; set; }

        public virtual OPTICAL_NE OPTICAL_NE { get; set; }
        public virtual OPTICAL_FINANCE_TERM OPTICAL_FINANCE_TERM { get; set; }
    }
}