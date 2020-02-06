using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class RIM_ATTRIBUTES
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string ATTR_TYPE { get; set; }
        public Nullable<short> IS_MAIN { get; set; }
        public Nullable<int> ATTR_ORDER { get; set; }
        public Nullable<int> NODE_ID { get; set; }

        public virtual RIM_NODE_TABLES RIM_NODE_TABLES { get; set; }
    }
}