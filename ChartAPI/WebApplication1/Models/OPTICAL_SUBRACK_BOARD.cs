using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_SUBRACK_BOARD
    {
        public int ID { get; set; }
        public int SUBRACK_ID { get; set; }
        public int BOARD_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }

        public virtual OPTICAL_BOARD OPTICAL_BOARD { get; set; }
        public virtual OPTICAL_SUBRACK OPTICAL_SUBRACK { get; set; }
    }
}